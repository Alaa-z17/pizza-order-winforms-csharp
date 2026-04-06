using pizza_order_winforms_csharp;
using PizzaOrderSystem.Models;
using PizzaOrderSystem.Services;

namespace PizzaOrderSystem.Forms
{
    public partial class PizzaOrderForm : Form
    {
        private List<BaseItem> cartItems = new List<BaseItem>();
        private decimal currentOrderTotal = 0;
        private Order? currentActiveOrder;
        private int remainingSeconds;

        public PizzaOrderForm()
        {
            InitializeComponent();
            InitializeCustomControls();
            LoadPizzaImage();
        }

        private void InitializeCustomControls()
        {
            cmbCrust.Items.Clear();
            cmbCrust.Items.AddRange(Enum.GetNames(typeof(CrustType)));
            cmbCrust.SelectedIndex = 0;

            flpToppings.Controls.Clear();
            foreach (Topping topping in Enum.GetValues(typeof(Topping)))
            {
                var chk = new CheckBox
                {
                    Text = topping.ToString(),
                    Width = 120,
                    Tag = topping
                };
                flpToppings.Controls.Add(chk);
            }

            rbMedium.Checked = true;
            nudQuantity.Minimum = 1;
            nudQuantity.Maximum = 10;
            nudQuantity.Value = 1;
        }

        private void LoadPizzaImage()
        {
            try
            {
                // Try to load from embedded resources
                picPizza.Image = Resources.pizza;
            }
            catch
            {
                // Fallback: draw a simple placeholder
                Bitmap bmp = new Bitmap(200, 150);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.LightSalmon);
                    g.DrawString("🍕", new Font("Segoe UI", 80, FontStyle.Bold), new SolidBrush(Color.SaddleBrown), 50, 20);
                    g.DrawString("PIZZA", new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.DarkRed), 60, 110);
                }
                picPizza.Image = bmp;
            }
        }

        private void btnAddPizza_Click(object sender, EventArgs e)
        {
            try
            {
                PizzaSize selectedSize = PizzaSize.Medium;
                if (rbSmall.Checked) selectedSize = PizzaSize.Small;
                else if (rbMedium.Checked) selectedSize = PizzaSize.Medium;
                else if (rbLarge.Checked) selectedSize = PizzaSize.Large;
                else if (rbFamily.Checked) selectedSize = PizzaSize.Family;

                if (cmbCrust.SelectedItem == null)
                {
                    MessageBox.Show("Please select a crust type.", "Missing Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string? crustText = cmbCrust.SelectedItem.ToString();
                if (!Enum.TryParse<CrustType>(crustText, out CrustType selectedCrust))
                {
                    MessageBox.Show("Invalid crust type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var selectedToppings = new List<Topping>();
                foreach (CheckBox chk in flpToppings.Controls.OfType<CheckBox>())
                {
                    if (chk.Checked && Enum.TryParse<Topping>(chk.Text, out Topping topping))
                        selectedToppings.Add(topping);
                }

                int quantity = (int)nudQuantity.Value;
                string pizzaName = $"{selectedSize} Pizza";

                var pizza = new Pizza(pizzaName, quantity, selectedSize, selectedCrust, selectedToppings);
                cartItems.Add(pizza);
                RefreshCartDisplay();

                MessageBox.Show($"✓ Added {quantity} x {selectedSize} {selectedCrust} crust pizza\n" +
                              $"Toppings: {(selectedToppings.Count == 0 ? "None" : string.Join(", ", selectedToppings))}\n" +
                              $"Subtotal: {pizza.CalculateTotal():C}",
                              "Pizza Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding pizza: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddDrink_Click(object sender, EventArgs e)
        {
            // Build the dialog
            Form dialog = new Form
            {
                Text = "Add Drink",
                Size = new Size(300, 200),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            ComboBox cmbDrinkType = new ComboBox
            {
                Location = new Point(20, 20),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbDrinkType.Items.AddRange(new[] { "Coca Cola", "Pepsi", "Sprite", "Fanta", "Water", "Orange Juice" });
            cmbDrinkType.SelectedIndex = 0;

            ComboBox cmbDrinkSize = new ComboBox
            {
                Location = new Point(20, 60),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbDrinkSize.Items.AddRange(Enum.GetNames(typeof(DrinkSize)));
            cmbDrinkSize.SelectedIndex = 0;

            NumericUpDown nudDrinkQty = new NumericUpDown
            {
                Location = new Point(20, 100),
                Size = new Size(80, 25),
                Minimum = 1,
                Maximum = 20,
                Value = 1
            };

            Button btnOk = new Button { Text = "Add", Location = new Point(20, 140), Size = new Size(80, 30), DialogResult = DialogResult.OK };
            Button btnCancel = new Button { Text = "Cancel", Location = new Point(110, 140), Size = new Size(80, 30), DialogResult = DialogResult.Cancel };

            dialog.Controls.AddRange(new Control[] { cmbDrinkType, cmbDrinkSize, nudDrinkQty, btnOk, btnCancel });

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string? drinkName = cmbDrinkType.SelectedItem?.ToString();
                if (string.IsNullOrWhiteSpace(drinkName))
                {
                    MessageBox.Show("Invalid drink selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string? sizeText = cmbDrinkSize.SelectedItem?.ToString();
                if (!Enum.TryParse<DrinkSize>(sizeText, out DrinkSize size))
                {
                    MessageBox.Show("Invalid drink size.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int qty = (int)nudDrinkQty.Value;
                var drink = new Drink(drinkName, qty, size);
                cartItems.Add(drink);
                RefreshCartDisplay();

                MessageBox.Show($"✓ Added {qty} x {size} {drinkName}", "Drink Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAddSide_Click(object sender, EventArgs e)
        {
            Form dialog = new Form
            {
                Text = "Add Side",
                Size = new Size(300, 200),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            ComboBox cmbSideType = new ComboBox
            {
                Location = new Point(20, 20),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbSideType.Items.AddRange(Enum.GetNames(typeof(SideType)));
            cmbSideType.SelectedIndex = 0;

            NumericUpDown nudSideQty = new NumericUpDown
            {
                Location = new Point(20, 60),
                Size = new Size(80, 25),
                Minimum = 1,
                Maximum = 20,
                Value = 1
            };

            Button btnOk = new Button { Text = "Add", Location = new Point(20, 100), Size = new Size(80, 30), DialogResult = DialogResult.OK };
            Button btnCancel = new Button { Text = "Cancel", Location = new Point(110, 100), Size = new Size(80, 30), DialogResult = DialogResult.Cancel };

            dialog.Controls.AddRange(new Control[] { cmbSideType, nudSideQty, btnOk, btnCancel });

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string? sideText = cmbSideType.SelectedItem?.ToString();
                if (!Enum.TryParse<SideType>(sideText, out SideType type))
                {
                    MessageBox.Show("Invalid side selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int qty = (int)nudSideQty.Value;
                var side = new Side(type.ToString(), qty, type);
                cartItems.Add(side);
                RefreshCartDisplay();
                MessageBox.Show($"✓ Added {qty} x {type}", "Side Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (lstCart.SelectedIndex >= 0 && lstCart.SelectedIndex < cartItems.Count)
            {
                var removedItem = cartItems[lstCart.SelectedIndex];
                cartItems.RemoveAt(lstCart.SelectedIndex);
                RefreshCartDisplay();
                MessageBox.Show($"Removed {removedItem.Quantity} x {removedItem.Name}", "Item Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select an item to remove.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (cartItems.Count > 0)
            {
                var result = MessageBox.Show("Clear all items from cart?", "Confirm Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    cartItems.Clear();
                    RefreshCartDisplay();
                }
            }
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                errorProvider.SetError(txtCustomerName, "Customer name is required!");
                MessageBox.Show("Please enter customer name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerName.Focus();
                return;
            }
            else
            {
                errorProvider.SetError(txtCustomerName, "");
            }

            if (cartItems.Count == 0)
            {
                MessageBox.Show("Please add at least one item (pizza, drink, or side) to the order.", "Empty Cart", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var customer = new Customer(txtCustomerName.Text.Trim(), txtPhone.Text.Trim(), txtAddress.Text);
            var order = new Order(customer, new List<BaseItem>(cartItems));

            OrderManager.PlaceOrder(order);

            string orderSummary = $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                                 $"          ORDER CONFIRMED\n" +
                                 $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                                 $"Order #: {order.OrderId}\n" +
                                 $"Customer: {order.Customer.Name}\n" +
                                 $"Phone: {order.Customer.Phone}\n" +
                                 $"Time: {order.OrderTime:hh:mm tt}\n" +
                                 $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                                 $"Items: {cartItems.Count}\n" +
                                 $"Total: {order.TotalAmount:C}\n" +
                                 $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                                 $"Status: {order.Status}\n" +
                                 $"Est. Ready: {DateTime.Now.AddSeconds(order.PreparationSeconds):hh:mm tt}\n" +
                                 $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━";

            MessageBox.Show(orderSummary, "Order Placed Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);

            StopCountdown();
            currentActiveOrder = order;
            remainingSeconds = order.PreparationSeconds;
            UpdateCountdownDisplay();
            countdownTimer.Start();

            ResetForm();
            UpdateStatusBar($"Order #{order.OrderId} placed - Total: {order.TotalAmount:C}");
        }

        private void ResetForm()
        {
            cartItems.Clear();
            RefreshCartDisplay();
            txtCustomerName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            rbMedium.Checked = true;
            cmbCrust.SelectedIndex = 0;
            nudQuantity.Value = 1;

            foreach (CheckBox chk in flpToppings.Controls)
            {
                chk.Checked = false;
            }
        }

        private void RefreshCartDisplay()
        {
            lstCart.Items.Clear();
            currentOrderTotal = 0;

            foreach (var item in cartItems)
            {
                string displayText = $"{item.Quantity} x {item.Name} - {item.CalculateTotal():C}";
                if (item is Pizza pizza)
                {
                    displayText += $" [{pizza.Size}, {pizza.Crust} crust]";
                }
                else if (item is Drink drink)
                {
                    displayText += $" [{drink.Size}]";
                }
                else if (item is Side side)
                {
                    displayText += $" [{side.SideType}]";
                }
                lstCart.Items.Add(displayText);
                currentOrderTotal += item.CalculateTotal();
            }

            lblTotal.Text = $"Total: {currentOrderTotal:C}";
            lblItemCount.Text = $"Items: {cartItems.Count}";

            if (currentOrderTotal > 50)
                lblTotal.ForeColor = Color.Red;
            else if (currentOrderTotal > 30)
                lblTotal.ForeColor = Color.Orange;
            else
                lblTotal.ForeColor = Color.Green;
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (remainingSeconds <= 0)
            {
                countdownTimer.Stop();
                lblCountdown.Text = "✅ ORDER READY FOR PICKUP!";
                lblCountdown.ForeColor = Color.Green;

                if (currentActiveOrder != null)
                {
                    OrderManager.UpdateOrderStatus(currentActiveOrder.OrderId, OrderStatus.Ready);
                    MessageBox.Show($"🎉 Order #{currentActiveOrder.OrderId} is ready for pickup!\n\nCustomer: {currentActiveOrder.Customer.Name}\nTotal: {currentActiveOrder.TotalAmount:C}",
                                    "Order Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateStatusBar($"Order #{currentActiveOrder.OrderId} is ready");
                    currentActiveOrder = null;
                }
                return;
            }

            remainingSeconds--;
            UpdateCountdownDisplay();
        }

        private void UpdateCountdownDisplay()
        {
            if (currentActiveOrder != null)
            {
                TimeSpan t = TimeSpan.FromSeconds(remainingSeconds);
                lblCountdown.Text = $"Order #{currentActiveOrder.OrderId} ready in: {t.Minutes:D2}:{t.Seconds:D2}";
                lblCountdown.ForeColor = remainingSeconds < 300 ? Color.OrangeRed : Color.DarkRed;

                int progress = (int)((currentActiveOrder.PreparationSeconds - remainingSeconds) * 100.0 / currentActiveOrder.PreparationSeconds);
                progressBar1.Value = Math.Min(100, Math.Max(0, progress));
            }
        }

        private void StopCountdown()
        {
            countdownTimer.Stop();
            lblCountdown.Text = "No active order";
            lblCountdown.ForeColor = Color.Gray;
            progressBar1.Value = 0;
            currentActiveOrder = null;
        }

        private void UpdateStatusBar(string message)
        {
            // Optional: update parent MDI status strip
            if (Parent is MainMDIForm mainForm)
            {
                // You can expose a method in MainMDIForm to update statusLabel.Text
            }
        }

        private void txtCustomerName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                errorProvider.SetError(txtCustomerName, "Customer name is required");
            }
            else
            {
                errorProvider.SetError(txtCustomerName, "");
            }
        }
    }
}
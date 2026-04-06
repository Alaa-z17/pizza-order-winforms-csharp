using pizza_order_winforms_csharp;
using PizzaOrderSystem.Models;
using PizzaOrderSystem.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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

            txtPhone.Validating += txtPhone_Validating;
        }

        private void LoadPizzaImage()
        {
            try
            {
                picPizza.Image = Resources.pizza;
            }
            catch
            {
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
                // Find matching pizza
                var existing = FindMatchingPizza(pizza);
                if (existing != null)
                {
                    existing.Quantity += pizza.Quantity;
                    MessageBox.Show($"✓ Added {pizza.Quantity} more {pizza.Size} pizza (total: {existing.Quantity})",
                                    "Quantity Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cartItems.Add(pizza);
                    MessageBox.Show($"✓ Added {pizza.Quantity} x {pizza.Size} {pizza.Crust} crust pizza\n" +
                                    $"Toppings: {(selectedToppings.Count == 0 ? "None" : string.Join(", ", selectedToppings))}\n" +
                                    $"Subtotal: {pizza.CalculateTotal():C}",
                                    "Pizza Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                RefreshCartDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding pizza: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddDrink_Click(object sender, EventArgs e)
        {
            Form dialog = new Form
            {
                Text = "Add Drink",
                Size = new Size(300, 250),
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
            cmbDrinkType.SelectedIndex = 4;

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
                var existing = FindMatchingDrink(drink);
                if (existing != null)
                {
                    existing.Quantity += drink.Quantity;
                    MessageBox.Show($"✓ Added {drink.Quantity} more {drink.Size} {drink.Name} (total: {existing.Quantity})",
                                    "Quantity Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cartItems.Add(drink);
                    MessageBox.Show($"✓ Added {drink.Quantity} x {drink.Size} {drink.Name}",
                                    "Drink Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
                var existing = FindMatchingSide(side);
                if (existing != null)
                {
                    existing.Quantity += side.Quantity;
                    MessageBox.Show($"✓ Added {side.Quantity} more {side.SideType} (total: {existing.Quantity})",
                                    "Quantity Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cartItems.Add(side);
                    MessageBox.Show($"✓ Added {side.Quantity} x {side.SideType}",
                                    "Side Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                RefreshCartDisplay();
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (lstCart.SelectedItems.Count > 0)
            {
                int index = lstCart.SelectedIndices[0];
                if (index >= 0 && index < cartItems.Count)
                {
                    var removedItem = cartItems[index];
                    cartItems.RemoveAt(index);
                    RefreshCartDisplay();
                    MessageBox.Show($"Removed {removedItem.Quantity} x {removedItem.Name}", "Item Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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

        private void UpdateStatusBar(string message)
        {
            if (Parent is MainMDIForm mainForm)
            {
                mainForm.UpdateStatus(message);
            }
        }
        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            // Validate name
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

            // Validate phone
            txtPhone_Validating(null, new CancelEventArgs());
            if (!string.IsNullOrEmpty(errorProvider.GetError(txtPhone)))
            {
                MessageBox.Show("Please enter a valid phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            // Validate cart not empty
            if (cartItems.Count == 0)
            {
                MessageBox.Show("Please add at least one item (pizza, drink, or side) to the order.", "Empty Cart", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create and place order
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
                if (item is Pizza pizza) displayText += $" [{pizza.Size}, {pizza.Crust} crust]";
                else if (item is Drink drink) displayText += $" [{drink.Size}]";
                else if (item is Side side) displayText += $" [{side.SideType}]";

                var listItem = new ListViewItem(displayText);
                lstCart.Items.Add(listItem);
                currentOrderTotal += item.CalculateTotal();
            }

            lblTotal.Text = $"Total: {currentOrderTotal:C}";
            lblItemCount.Text = $"Items: {cartItems.Count}";
            lblTotal.ForeColor = currentOrderTotal > 50 ? Color.Red : (currentOrderTotal > 30 ? Color.Orange : Color.FromArgb(220, 80, 20));
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

        private void txtPhone_Validating(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            string phone = txtPhone.Text.Trim();
            if (string.IsNullOrWhiteSpace(phone))
            {
                errorProvider.SetError(txtPhone, "Phone number is required.");
                return;
            }
            // Basic phone validation: at least 10 digits, allow spaces, dashes, parentheses, plus sign
            string digitsOnly = new string(phone.Where(char.IsDigit).ToArray());
            if (digitsOnly.Length < 10 || digitsOnly.Length > 15)
            {
                errorProvider.SetError(txtPhone, "Enter a valid phone number (10-15 digits).");
            }
            else
            {
                errorProvider.SetError(txtPhone, ""); // clear error
            }
        }
        private Pizza? FindMatchingPizza(Pizza newPizza)
        {
            return cartItems.OfType<Pizza>().FirstOrDefault(p =>
                p.Size == newPizza.Size &&
                p.Crust == newPizza.Crust &&
                p.Toppings.SequenceEqual(newPizza.Toppings));
        }

        private Drink? FindMatchingDrink(Drink newDrink)
        {
            return cartItems.OfType<Drink>().FirstOrDefault(d =>
                d.Name == newDrink.Name &&
                d.Size == newDrink.Size);
        }

        private Side? FindMatchingSide(Side newSide)
        {
            return cartItems.OfType<Side>().FirstOrDefault(s =>
                s.SideType == newSide.SideType);
        }
    }
}
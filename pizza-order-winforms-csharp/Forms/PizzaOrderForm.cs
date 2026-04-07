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
        private class CrustComboBoxItem
        {
            public string Text { get; set; } = string.Empty;
            public CrustType Value { get; set; }
        }

        private List<BaseItem> cartItems = new List<BaseItem>();
        private decimal currentOrderTotal = 0;
        private Order? currentActiveOrder;
        private int remainingSeconds;

        public PizzaOrderForm()
        {
            InitializeComponent();
            InitializeCustomControls();
            LoadPizzaImage();
            ApplyResources();
            LanguageManager.LanguageChanged += ApplyResources;
        }

        private void ApplyResources()
        {
            grpCustomer.TitleText = LanguageManager.GetString("CustomerInfo");
            lblName.Text = LanguageManager.GetString("Name");
            lblPhone.Text = LanguageManager.GetString("Phone");
            lblAddress.Text = LanguageManager.GetString("Address");

            grpPizza.TitleText = LanguageManager.GetString("CustomizePizza");
            lblSize.Text = LanguageManager.GetString("Size");
            lblCrust.Text = LanguageManager.GetString("Crust");
            lblToppings.Text = LanguageManager.GetString("Toppings");
            lblQuantity.Text = LanguageManager.GetString("Quantity");
            btnAddPizza.Text = LanguageManager.GetString("AddPizza");
            btnAddDrink.Text = LanguageManager.GetString("AddDrink");
            btnAddSide.Text = LanguageManager.GetString("AddSide");

            grpCart.TitleText = LanguageManager.GetString("CurrentOrder");
            lblItemCount.Text = string.Format(LanguageManager.GetString("ItemCountFormat"), cartItems.Count);
            btnRemoveItem.Text = LanguageManager.GetString("Remove");
            btnClearCart.Text = LanguageManager.GetString("Clear");
            btnPlaceOrder.Text = LanguageManager.GetString("PlaceOrder");

            grpTimer.TitleText = LanguageManager.GetString("OrderStatus");
            if (currentActiveOrder == null)
                lblCountdown.Text = LanguageManager.GetString("NoActiveOrder");
            else
                UpdateCountdownDisplay();

            UpdatePizzaSizeTexts();
            RefreshCartDisplay();
            PopulateCrustComboBox();
            PopulateToppingsCheckBoxes();
        }

        private void UpdatePizzaSizeTexts()
        {
            var settings = PricesSettings.Load();
            rbSmall.Text = $"{LanguageManager.GetString("Small")} ({GetPriceText(PizzaSize.Small, settings.PizzaPriceMultiplier)})";
            rbMedium.Text = $"{LanguageManager.GetString("Medium")} ({GetPriceText(PizzaSize.Medium, settings.PizzaPriceMultiplier)})";
            rbLarge.Text = $"{LanguageManager.GetString("Large")} ({GetPriceText(PizzaSize.Large, settings.PizzaPriceMultiplier)})";
            rbFamily.Text = $"{LanguageManager.GetString("Family")} ({GetPriceText(PizzaSize.Family, settings.PizzaPriceMultiplier)})";
        }

        private string GetPriceText(PizzaSize size, decimal multiplier)
        {
            decimal basePrice = size switch
            {
                PizzaSize.Small => 8.99m,
                PizzaSize.Medium => 10.99m,
                PizzaSize.Large => 12.99m,
                PizzaSize.Family => 15.99m,
                _ => 10.99m
            };
            basePrice *= multiplier;
            return LanguageManager.FormatCurrency(basePrice);
        }

        private void InitializeCustomControls()
        {
            PopulateCrustComboBox();
            PopulateToppingsCheckBoxes();
            rbMedium.Checked = true;
            nudQuantity.Minimum = 1;
            nudQuantity.Maximum = 10;
            nudQuantity.Value = 1;
            txtPhone.Validating += txtPhone_Validating;

            if (lstCart.Columns.Count > 0)
                lstCart.Columns[0].Width = lstCart.Width - 5;
            lstCart.Resize += (s, e) => { if (lstCart.Columns.Count > 0) lstCart.Columns[0].Width = lstCart.Width - 5; };
        }

        private void LoadPizzaImage()
        {
            try { picPizza.Image = ImageResources.pizza; }
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
                    MessageBox.Show(LanguageManager.GetString("SelectCrust"), LanguageManager.GetString("MissingSelection"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedItem = (CrustComboBoxItem)cmbCrust.SelectedItem;
                CrustType selectedCrust = selectedItem.Value;

                var selectedToppings = new List<Topping>();
                foreach (CheckBox chk in flpToppings.Controls.OfType<CheckBox>())
                {
                    if (chk.Checked && chk.Tag is Topping topping)
                        selectedToppings.Add(topping);
                }

                int quantity = (int)nudQuantity.Value;
                string pizzaName = $"{selectedSize} Pizza";
                var pizza = new Pizza(pizzaName, quantity, selectedSize, selectedCrust, selectedToppings);

                var existing = FindMatchingPizza(pizza);
                if (existing != null)
                {
                    existing.Quantity += pizza.Quantity;
                    MessageBox.Show(string.Format(LanguageManager.GetString("AddedMore"), pizza.Quantity, LanguageManager.GetString(selectedSize.ToString()), existing.Quantity),
                                    LanguageManager.GetString("QuantityUpdated"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cartItems.Add(pizza);
                    string toppingsStr = selectedToppings.Count == 0 ? LanguageManager.GetString("None") : string.Join(", ", selectedToppings.Select(t => LanguageManager.GetString(t.ToString())));
                    string msg = string.Format(LanguageManager.GetString("PizzaAddedMsg"),
                        pizza.Quantity,
                        LanguageManager.GetString(selectedSize.ToString()),
                        LanguageManager.GetString(selectedCrust.ToString()),
                        toppingsStr,
                        LanguageManager.FormatCurrency(pizza.CalculateTotal()));
                    msg = msg.Replace("\\n", Environment.NewLine).Replace("\n", Environment.NewLine);
                    MessageBox.Show(msg, LanguageManager.GetString("PizzaAdded"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                RefreshCartDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding pizza: {ex.Message}", LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // FIXED: Safe combo box initialization for drinks
        private void btnAddDrink_Click(object sender, EventArgs e)
        {
            Form dialog = new Form
            {
                Text = LanguageManager.GetString("AddDrink"),
                Size = new Size(300, 250),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var drinkKeys = new[] { "Drink_Sprite", "Drink_Fanta", "Drink_Water", "Drink_OrangeJuice" };
            var originalDrinkNames = new[] { "Sprite", "Fanta", "Water", "Orange Juice" };

            // Get localized names, fallback to English if missing
            var localizedDrinkNames = drinkKeys
                .Select(k => LanguageManager.GetString(k))
                .Select((name, idx) => string.IsNullOrEmpty(name) || name == drinkKeys[idx] ? originalDrinkNames[idx] : name)
                .ToList();

            if (localizedDrinkNames.Count == 0)
                localizedDrinkNames = originalDrinkNames.ToList();

            ComboBox cmbDrinkType = new ComboBox
            {
                Location = new Point(20, 20),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = localizedDrinkNames
            };
            cmbDrinkType.Tag = originalDrinkNames;

            // Safe selected index
            if (cmbDrinkType.Items.Count > 2)
                cmbDrinkType.SelectedIndex = 2; // Water
            else if (cmbDrinkType.Items.Count > 0)
                cmbDrinkType.SelectedIndex = 0;

            var drinkSizes = Enum.GetValues(typeof(DrinkSize)).Cast<DrinkSize>().ToList();
            var localizedSizeNames = drinkSizes
                .Select(s => LanguageManager.GetString(s.ToString() + "Size"))
                .Select((name, idx) => string.IsNullOrEmpty(name) || name == (drinkSizes[idx].ToString() + "Size") ? drinkSizes[idx].ToString() : name)
                .ToList();

            ComboBox cmbDrinkSize = new ComboBox
            {
                Location = new Point(20, 60),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = localizedSizeNames
            };
            cmbDrinkSize.Tag = drinkSizes;
            if (cmbDrinkSize.Items.Count > 0)
                cmbDrinkSize.SelectedIndex = 0;

            NumericUpDown nudDrinkQty = new NumericUpDown
            {
                Location = new Point(20, 100),
                Size = new Size(80, 25),
                Minimum = 1,
                Maximum = 20,
                Value = 1
            };

            Button btnOk = new Button
            {
                Text = LanguageManager.GetString("AddDrink"),
                Location = new Point(20, 140),
                Size = new Size(80, 30),
                DialogResult = DialogResult.OK
            };
            Button btnCancel = new Button
            {
                Text = LanguageManager.GetString("Cancel"),
                Location = new Point(110, 140),
                Size = new Size(80, 30),
                DialogResult = DialogResult.Cancel
            };

            dialog.Controls.AddRange(new Control[] { cmbDrinkType, cmbDrinkSize, nudDrinkQty, btnOk, btnCancel });

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                int idx = cmbDrinkType.SelectedIndex;
                string drinkName = ((string[])cmbDrinkType.Tag)[idx];
                int sizeIdx = cmbDrinkSize.SelectedIndex;
                DrinkSize size = ((List<DrinkSize>)cmbDrinkSize.Tag)[sizeIdx];
                int qty = (int)nudDrinkQty.Value;

                var drink = new Drink(drinkName, qty, size);

                var existing = FindMatchingDrink(drink);
                if (existing != null)
                {
                    existing.Quantity += drink.Quantity;
                    MessageBox.Show(string.Format(LanguageManager.GetString("AddedMore"), drink.Quantity, drinkName, existing.Quantity),
                                    LanguageManager.GetString("QuantityUpdated"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cartItems.Add(drink);
                    string msg = string.Format(LanguageManager.GetString("DrinkAddedMsg"), drink.Quantity,
                                               LanguageManager.GetString(size.ToString() + "Size"), drinkName);
                    msg = msg.Replace("\\n", Environment.NewLine).Replace("\n", Environment.NewLine);
                    MessageBox.Show(msg, LanguageManager.GetString("DrinkAdded"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                RefreshCartDisplay();
            }
        }

        // FIXED: Safe combo box initialization for sides
        private void btnAddSide_Click(object sender, EventArgs e)
        {
            Form dialog = new Form
            {
                Text = LanguageManager.GetString("AddSide"),
                Size = new Size(300, 200),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var sideTypes = Enum.GetValues(typeof(SideType)).Cast<SideType>().ToList();
            var localizedSideNames = sideTypes
                .Select(st => LanguageManager.GetString(st.ToString()))
                .Select((name, idx) => string.IsNullOrEmpty(name) || name == sideTypes[idx].ToString() ? sideTypes[idx].ToString() : name)
                .ToList();

            if (localizedSideNames.Count == 0)
                localizedSideNames = sideTypes.Select(st => st.ToString()).ToList();

            ComboBox cmbSideType = new ComboBox
            {
                Location = new Point(20, 20),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = localizedSideNames
            };
            cmbSideType.Tag = sideTypes;
            if (cmbSideType.Items.Count > 0)
                cmbSideType.SelectedIndex = 0;

            NumericUpDown nudSideQty = new NumericUpDown
            {
                Location = new Point(20, 60),
                Size = new Size(80, 25),
                Minimum = 1,
                Maximum = 20,
                Value = 1
            };

            Button btnOk = new Button
            {
                Text = LanguageManager.GetString("AddSide"),
                Location = new Point(20, 100),
                Size = new Size(80, 30),
                DialogResult = DialogResult.OK
            };
            Button btnCancel = new Button
            {
                Text = LanguageManager.GetString("Cancel"),
                Location = new Point(110, 100),
                Size = new Size(80, 30),
                DialogResult = DialogResult.Cancel
            };

            dialog.Controls.AddRange(new Control[] { cmbSideType, nudSideQty, btnOk, btnCancel });

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                int idx = cmbSideType.SelectedIndex;
                SideType type = sideTypes[idx];
                int qty = (int)nudSideQty.Value;
                var side = new Side(type.ToString(), qty, type);

                var existing = FindMatchingSide(side);
                if (existing != null)
                {
                    existing.Quantity += side.Quantity;
                    MessageBox.Show(string.Format(LanguageManager.GetString("AddedMore"), side.Quantity,
                                   LanguageManager.GetString(type.ToString()), existing.Quantity),
                                    LanguageManager.GetString("QuantityUpdated"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cartItems.Add(side);
                    string msg = string.Format(LanguageManager.GetString("SideAddedMsg"), side.Quantity,
                                               LanguageManager.GetString(type.ToString()));
                    msg = msg.Replace("\\n", Environment.NewLine).Replace("\n", Environment.NewLine);
                    MessageBox.Show(msg, LanguageManager.GetString("SideAdded"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show(string.Format(LanguageManager.GetString("RemovedMsg"), removedItem.Quantity, removedItem.Name),
                                    LanguageManager.GetString("ItemRemoved"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(LanguageManager.GetString("PleaseSelectItem"), LanguageManager.GetString("NoSelection"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (cartItems.Count > 0)
            {
                var result = MessageBox.Show(LanguageManager.GetString("ClearConfirm"), LanguageManager.GetString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    cartItems.Clear();
                    RefreshCartDisplay();
                }
            }
        }

        private void UpdateStatusBar(string message)
        {
            if (Parent is MainMDIForm mainForm) mainForm.UpdateStatus(message);
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                errorProvider.SetError(txtCustomerName, LanguageManager.GetString("PleaseEnterName"));
                MessageBox.Show(LanguageManager.GetString("PleaseEnterName"), LanguageManager.GetString("ValidationError"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerName.Focus();
                return;
            }
            else errorProvider.SetError(txtCustomerName, "");

            txtPhone_Validating(null, new CancelEventArgs());
            if (!string.IsNullOrEmpty(errorProvider.GetError(txtPhone)))
            {
                MessageBox.Show(LanguageManager.GetString("PleaseEnterPhone"), LanguageManager.GetString("ValidationError"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            if (cartItems.Count == 0)
            {
                MessageBox.Show(LanguageManager.GetString("PleaseAddItems"), LanguageManager.GetString("EmptyCart"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var customer = new Customer(txtCustomerName.Text.Trim(), txtPhone.Text.Trim(), txtAddress.Text);
            var order = new Order(customer, DeepCopyCartItems());
            OrderManager.PlaceOrder(order);

            string orderSummary = $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                                  $"          {LanguageManager.GetString("OrderConfirmed")}\n" +
                                  $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                                  $"{LanguageManager.GetString("Order")}: {order.OrderId}\n" +
                                  $"{LanguageManager.GetString("CustomerLabel")}: {order.Customer.Name}\n" +
                                  $"{LanguageManager.GetString("PhoneLabel")}: {order.Customer.Phone}\n" +
                                  $"{LanguageManager.GetString("TimeLabel")}: {order.OrderTime:hh:mm tt}\n" +
                                  $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                                  $"{LanguageManager.GetString("Items")}: {cartItems.Count}\n" +
                                  $"{LanguageManager.GetString("Total")}: {LanguageManager.FormatCurrency(order.TotalAmount)}\n" +
                                  $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                                  $"{LanguageManager.GetString("StatusLabel")}: {LanguageManager.GetString(order.Status.ToString())}\n" +
                                  $"{LanguageManager.GetString("EstReady")}: {DateTime.Now.AddSeconds(order.PreparationSeconds):hh:mm tt}\n" +
                                  $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━";

            MessageBox.Show(orderSummary, LanguageManager.GetString("OrderPlacedSuccess"), MessageBoxButtons.OK, MessageBoxIcon.Information);

            StopCountdown();
            currentActiveOrder = order;
            remainingSeconds = order.PreparationSeconds;
            UpdateCountdownDisplay();
            countdownTimer.Start();

            ResetForm();
            UpdateStatusBar($"{LanguageManager.GetString("OrderPlaced")} {LanguageManager.FormatCurrency(order.TotalAmount)}");
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
            foreach (CheckBox chk in flpToppings.Controls) chk.Checked = false;
        }

        private void RefreshCartDisplay()
        {
            lstCart.Items.Clear();
            currentOrderTotal = 0;
            string xSign = LanguageManager.GetString("XSign");
            string dash = LanguageManager.GetString("Dash");

            foreach (var item in cartItems)
            {
                string displayText = GetLocalizedItemDisplay(item, xSign, dash);
                lstCart.Items.Add(new ListViewItem(displayText));
                currentOrderTotal += item.CalculateTotal();
            }

            lblTotal.Text = string.Format(LanguageManager.GetString("TotalFormat"), LanguageManager.FormatCurrency(currentOrderTotal));
            lblItemCount.Text = string.Format(LanguageManager.GetString("ItemCountFormat"), cartItems.Count);
            lblTotal.ForeColor = currentOrderTotal > 50 ? Color.Red : (currentOrderTotal > 30 ? Color.Orange : Color.FromArgb(220, 80, 20));
        }

        private string GetLocalizedItemDisplay(BaseItem item, string xSign, string dash)
        {
            string baseText = $"{item.Quantity} {xSign} {GetLocalizedName(item)} {dash} {LanguageManager.FormatCurrency(item.CalculateTotal())}";

            if (item is Pizza pizza)
            {
                string toppingsStr = pizza.Toppings.Count == 0 ? LanguageManager.GetString("None") : string.Join(", ", pizza.Toppings.Select(t => LanguageManager.GetString(t.ToString())));
                baseText += $" [{LanguageManager.GetString(pizza.Size.ToString())}, {LanguageManager.GetString("CrustWord")} {LanguageManager.GetString(pizza.Crust.ToString())}, {LanguageManager.GetString("ToppingsWord")}: {toppingsStr}]";
            }
            else if (item is Drink drink)
            {
                baseText += $" [{LanguageManager.GetString(drink.Size.ToString() + "Size")}]";
            }
            else if (item is Side side)
            {
                baseText += $" [{LanguageManager.GetString(side.SideType.ToString())}]";
            }
            return baseText;
        }

        private string GetLocalizedName(BaseItem item)
        {
            if (item is Pizza pizza)
                return $"{LanguageManager.GetString(pizza.Size.ToString())} {LanguageManager.GetString("Pizza")}";
            else if (item is Drink drink)
                return drink.Name;
            else if (item is Side side)
                return LanguageManager.GetString(side.SideType.ToString());
            else
                return item.Name;
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (remainingSeconds <= 0)
            {
                countdownTimer.Stop();
                lblCountdown.Text = LanguageManager.GetString("OrderReady");
                lblCountdown.ForeColor = Color.Green;
                if (currentActiveOrder != null)
                {
                    OrderManager.UpdateOrderStatus(currentActiveOrder.OrderId, OrderStatus.Ready);
                    string msg = string.Format(LanguageManager.GetString("OrderReadyMsg"),
                                               currentActiveOrder.OrderId,
                                               currentActiveOrder.Customer.Name,
                                               LanguageManager.FormatCurrency(currentActiveOrder.TotalAmount));
                    msg = msg.Replace("\\n", Environment.NewLine).Replace("\n", Environment.NewLine);
                    MessageBox.Show(msg, LanguageManager.GetString("OrderReadyTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                lblCountdown.Text = $"{LanguageManager.GetString("Order")}{currentActiveOrder.OrderId} {LanguageManager.GetString("ReadyIn")} {t.Minutes:D2}:{t.Seconds:D2}";
                lblCountdown.ForeColor = remainingSeconds < 300 ? Color.OrangeRed : Color.DarkRed;
                int progress = (int)((currentActiveOrder.PreparationSeconds - remainingSeconds) * 100.0 / currentActiveOrder.PreparationSeconds);
                progressBar1.Value = Math.Min(100, Math.Max(0, progress));
            }
        }

        private void StopCountdown()
        {
            countdownTimer.Stop();
            lblCountdown.Text = LanguageManager.GetString("NoActiveOrder");
            lblCountdown.ForeColor = Color.Gray;
            progressBar1.Value = 0;
            currentActiveOrder = null;
        }

        private void txtCustomerName_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtCustomerName, string.IsNullOrWhiteSpace(txtCustomerName.Text) ? LanguageManager.GetString("PleaseEnterName") : "");
        }

        private void txtPhone_Validating(object? sender, CancelEventArgs e)
        {
            string phone = txtPhone.Text.Trim();
            if (string.IsNullOrWhiteSpace(phone))
                errorProvider.SetError(txtPhone, LanguageManager.GetString("PleaseEnterPhone"));
            else
            {
                string digitsOnly = new string(phone.Where(char.IsDigit).ToArray());
                errorProvider.SetError(txtPhone, (digitsOnly.Length < 10 || digitsOnly.Length > 15) ? LanguageManager.GetString("PleaseEnterPhone") : "");
            }
        }

        private Pizza? FindMatchingPizza(Pizza newPizza) =>
            cartItems.OfType<Pizza>().FirstOrDefault(p => p.Size == newPizza.Size && p.Crust == newPizza.Crust && p.Toppings.SequenceEqual(newPizza.Toppings));

        private Drink? FindMatchingDrink(Drink newDrink) =>
            cartItems.OfType<Drink>().FirstOrDefault(d => d.Name == newDrink.Name && d.Size == newDrink.Size);

        private Side? FindMatchingSide(Side newSide) =>
            cartItems.OfType<Side>().FirstOrDefault(s => s.SideType == newSide.SideType);

        private void PopulateCrustComboBox()
        {
            cmbCrust.Items.Clear();
            foreach (CrustType crust in Enum.GetValues(typeof(CrustType)))
            {
                string localizedName = LanguageManager.GetString(crust.ToString());
                cmbCrust.Items.Add(new CrustComboBoxItem { Text = localizedName, Value = crust });
            }
            cmbCrust.DisplayMember = "Text";
            if (cmbCrust.Items.Count > 0) cmbCrust.SelectedIndex = 0;
        }

        private void PopulateToppingsCheckBoxes()
        {
            flpToppings.Controls.Clear();
            foreach (Topping topping in Enum.GetValues(typeof(Topping)))
            {
                string localizedName = LanguageManager.GetString(topping.ToString());
                var chk = new CheckBox { Text = localizedName, Width = 120, Tag = topping };
                flpToppings.Controls.Add(chk);
            }
        }

        private List<BaseItem> DeepCopyCartItems()
        {
            var copy = new List<BaseItem>();
            foreach (var item in cartItems)
            {
                if (item is Pizza pizza)
                {
                    var newPizza = new Pizza(pizza.Name, pizza.Quantity, pizza.Size, pizza.Crust, new List<Topping>(pizza.Toppings));
                    copy.Add(newPizza);
                }
                else if (item is Drink drink)
                {
                    var newDrink = new Drink(drink.Name, drink.Quantity, drink.Size);
                    copy.Add(newDrink);
                }
                else if (item is Side side)
                {
                    var newSide = new Side(side.Name, side.Quantity, side.SideType);
                    copy.Add(newSide);
                }
            }
            return copy;
        }
    }
}
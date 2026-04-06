namespace PizzaOrderSystem.Forms
{
    partial class PizzaOrderForm
    {
        private System.ComponentModel.IContainer components = null;

        // Controls
        private System.Windows.Forms.GroupBox grpCustomer;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.ErrorProvider errorProvider;

        private System.Windows.Forms.GroupBox grpPizza;
        private System.Windows.Forms.RadioButton rbSmall;
        private System.Windows.Forms.RadioButton rbMedium;
        private System.Windows.Forms.RadioButton rbLarge;
        private System.Windows.Forms.RadioButton rbFamily;
        private System.Windows.Forms.ComboBox cmbCrust;
        private System.Windows.Forms.Label lblCrust;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.FlowLayoutPanel flpToppings;
        private System.Windows.Forms.Label lblToppings;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Button btnAddPizza;

        private System.Windows.Forms.Button btnAddDrink;
        private System.Windows.Forms.Button btnAddSide;

        private System.Windows.Forms.GroupBox grpCart;
        private System.Windows.Forms.ListBox lstCart;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblItemCount;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Button btnClearCart;
        private System.Windows.Forms.Button btnPlaceOrder;

        private System.Windows.Forms.GroupBox grpTimer;
        private System.Windows.Forms.Label lblCountdown;
        private System.Windows.Forms.Timer countdownTimer;
        private System.Windows.Forms.ProgressBar progressBar1;

        private System.Windows.Forms.PictureBox picPizza;

        private System.Windows.Forms.ToolTip toolTip;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // grpCustomer
            this.grpCustomer = new System.Windows.Forms.GroupBox();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);

            // grpPizza
            this.grpPizza = new System.Windows.Forms.GroupBox();
            this.rbSmall = new System.Windows.Forms.RadioButton();
            this.rbMedium = new System.Windows.Forms.RadioButton();
            this.rbLarge = new System.Windows.Forms.RadioButton();
            this.rbFamily = new System.Windows.Forms.RadioButton();
            this.cmbCrust = new System.Windows.Forms.ComboBox();
            this.lblCrust = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.flpToppings = new System.Windows.Forms.FlowLayoutPanel();
            this.lblToppings = new System.Windows.Forms.Label();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.btnAddPizza = new System.Windows.Forms.Button();

            this.btnAddDrink = new System.Windows.Forms.Button();
            this.btnAddSide = new System.Windows.Forms.Button();

            // grpCart
            this.grpCart = new System.Windows.Forms.GroupBox();
            this.lstCart = new System.Windows.Forms.ListBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblItemCount = new System.Windows.Forms.Label();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.btnClearCart = new System.Windows.Forms.Button();
            this.btnPlaceOrder = new System.Windows.Forms.Button();

            // grpTimer
            this.grpTimer = new System.Windows.Forms.GroupBox();
            this.lblCountdown = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.countdownTimer = new System.Windows.Forms.Timer(this.components);

            this.picPizza = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);

            // Suspend layout
            this.grpCustomer.SuspendLayout();
            this.grpPizza.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.grpCart.SuspendLayout();
            this.grpTimer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPizza)).BeginInit();
            this.SuspendLayout();

            // grpCustomer
            this.grpCustomer.Controls.Add(this.txtCustomerName);
            this.grpCustomer.Controls.Add(this.txtPhone);
            this.grpCustomer.Controls.Add(this.txtAddress);
            this.grpCustomer.Controls.Add(this.lblName);
            this.grpCustomer.Controls.Add(this.lblPhone);
            this.grpCustomer.Controls.Add(this.lblAddress);
            this.grpCustomer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpCustomer.Location = new System.Drawing.Point(12, 12);
            this.grpCustomer.Name = "grpCustomer";
            this.grpCustomer.Size = new System.Drawing.Size(450, 130);
            this.grpCustomer.TabIndex = 0;
            this.grpCustomer.TabStop = false;
            this.grpCustomer.Text = "👤 Customer Information";

            // txtCustomerName
            this.txtCustomerName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCustomerName.Location = new System.Drawing.Point(80, 25);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(350, 25);
            this.txtCustomerName.TabIndex = 0;
            this.toolTip.SetToolTip(this.txtCustomerName, "Enter customer full name");
            this.txtCustomerName.Validating += new System.ComponentModel.CancelEventHandler(this.txtCustomerName_Validating);

            // txtPhone
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPhone.Location = new System.Drawing.Point(80, 55);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 25);
            this.txtPhone.TabIndex = 1;
            this.toolTip.SetToolTip(this.txtPhone, "Enter phone number (optional)");

            // txtAddress
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAddress.Location = new System.Drawing.Point(80, 85);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(350, 25);
            this.txtAddress.TabIndex = 2;
            this.toolTip.SetToolTip(this.txtAddress, "Enter delivery address (optional)");

            // Labels
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblName.Location = new System.Drawing.Point(15, 28);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(49, 19);
            this.lblName.Text = "Name:";

            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPhone.Location = new System.Drawing.Point(15, 58);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(51, 19);
            this.lblPhone.Text = "Phone:";

            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAddress.Location = new System.Drawing.Point(15, 88);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(62, 19);
            this.lblAddress.Text = "Address:";

            // grpPizza
            this.grpPizza.Controls.Add(this.rbSmall);
            this.grpPizza.Controls.Add(this.rbMedium);
            this.grpPizza.Controls.Add(this.rbLarge);
            this.grpPizza.Controls.Add(this.rbFamily);
            this.grpPizza.Controls.Add(this.cmbCrust);
            this.grpPizza.Controls.Add(this.lblCrust);
            this.grpPizza.Controls.Add(this.lblSize);
            this.grpPizza.Controls.Add(this.flpToppings);
            this.grpPizza.Controls.Add(this.lblToppings);
            this.grpPizza.Controls.Add(this.nudQuantity);
            this.grpPizza.Controls.Add(this.lblQuantity);
            this.grpPizza.Controls.Add(this.btnAddPizza);
            this.grpPizza.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpPizza.Location = new System.Drawing.Point(12, 150);
            this.grpPizza.Name = "grpPizza";
            this.grpPizza.Size = new System.Drawing.Size(450, 420);
            this.grpPizza.TabIndex = 1;
            this.grpPizza.TabStop = false;
            this.grpPizza.Text = "🍕 Customize Your Pizza";

            // Size RadioButtons
            this.rbSmall.AutoSize = true;
            this.rbSmall.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rbSmall.Location = new System.Drawing.Point(80, 25);
            this.rbSmall.Name = "rbSmall";
            this.rbSmall.Size = new System.Drawing.Size(86, 23);
            this.rbSmall.TabIndex = 0;
            this.rbSmall.Text = "Small ($8.99)";
            this.toolTip.SetToolTip(this.rbSmall, "8-inch pizza - Serves 1");

            this.rbMedium.AutoSize = true;
            this.rbMedium.Checked = true;
            this.rbMedium.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rbMedium.Location = new System.Drawing.Point(180, 25);
            this.rbMedium.Name = "rbMedium";
            this.rbMedium.Size = new System.Drawing.Size(98, 23);
            this.rbMedium.TabIndex = 1;
            this.rbMedium.TabStop = true;
            this.rbMedium.Text = "Medium ($10.99)";
            this.toolTip.SetToolTip(this.rbMedium, "10-inch pizza - Serves 2");

            this.rbLarge.AutoSize = true;
            this.rbLarge.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rbLarge.Location = new System.Drawing.Point(290, 25);
            this.rbLarge.Name = "rbLarge";
            this.rbLarge.Size = new System.Drawing.Size(88, 23);
            this.rbLarge.TabIndex = 2;
            this.rbLarge.Text = "Large ($12.99)";
            this.toolTip.SetToolTip(this.rbLarge, "12-inch pizza - Serves 3");

            this.rbFamily.AutoSize = true;
            this.rbFamily.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rbFamily.Location = new System.Drawing.Point(80, 55);
            this.rbFamily.Name = "rbFamily";
            this.rbFamily.Size = new System.Drawing.Size(99, 23);
            this.rbFamily.TabIndex = 3;
            this.rbFamily.Text = "Family ($15.99)";
            this.toolTip.SetToolTip(this.rbFamily, "14-inch pizza - Serves 4");

            // Crust ComboBox
            this.lblCrust.AutoSize = true;
            this.lblCrust.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCrust.Location = new System.Drawing.Point(180, 58);
            this.lblCrust.Name = "lblCrust";
            this.lblCrust.Size = new System.Drawing.Size(44, 19);
            this.lblCrust.Text = "Crust:";

            this.cmbCrust.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCrust.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCrust.Location = new System.Drawing.Point(230, 55);
            this.cmbCrust.Name = "cmbCrust";
            this.cmbCrust.Size = new System.Drawing.Size(150, 25);
            this.cmbCrust.TabIndex = 4;
            this.toolTip.SetToolTip(this.cmbCrust, "Choose crust type (Stuffed crust +$2)");

            // Size label
            this.lblSize.AutoSize = true;
            this.lblSize.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSize.Location = new System.Drawing.Point(25, 27);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(39, 19);
            this.lblSize.Text = "Size:";

            // Toppings
            this.lblToppings.AutoSize = true;
            this.lblToppings.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblToppings.Location = new System.Drawing.Point(15, 90);
            this.lblToppings.Name = "lblToppings";
            this.lblToppings.Size = new System.Drawing.Size(218, 19);
            this.lblToppings.Text = "Toppings ($1 each after first):";

            this.flpToppings.Location = new System.Drawing.Point(15, 115);
            this.flpToppings.Name = "flpToppings";
            this.flpToppings.Size = new System.Drawing.Size(420, 180);
            this.flpToppings.TabIndex = 5;

            // Quantity
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblQuantity.Location = new System.Drawing.Point(15, 310);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(66, 19);
            this.lblQuantity.Text = "Quantity:";

            this.nudQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudQuantity.Location = new System.Drawing.Point(90, 308);
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(80, 25);
            this.nudQuantity.TabIndex = 6;

            // Add Pizza Button
            this.btnAddPizza.BackColor = System.Drawing.Color.LightGreen;
            this.btnAddPizza.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPizza.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddPizza.Location = new System.Drawing.Point(190, 300);
            this.btnAddPizza.Name = "btnAddPizza";
            this.btnAddPizza.Size = new System.Drawing.Size(150, 40);
            this.btnAddPizza.TabIndex = 7;
            this.btnAddPizza.Text = "➕ Add Pizza to Cart";
            this.toolTip.SetToolTip(this.btnAddPizza, "Add the customized pizza to your order");
            this.btnAddPizza.UseVisualStyleBackColor = false;
            this.btnAddPizza.Click += new System.EventHandler(this.btnAddPizza_Click);

            // Add Drink Button
            this.btnAddDrink.BackColor = System.Drawing.Color.LightBlue;
            this.btnAddDrink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDrink.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddDrink.Location = new System.Drawing.Point(15, 360);
            this.btnAddDrink.Name = "btnAddDrink";
            this.btnAddDrink.Size = new System.Drawing.Size(130, 40);
            this.btnAddDrink.TabIndex = 8;
            this.btnAddDrink.Text = "🥤 Add Drink";
            this.toolTip.SetToolTip(this.btnAddDrink, "Add a drink to your order");
            this.btnAddDrink.UseVisualStyleBackColor = false;
            this.btnAddDrink.Click += new System.EventHandler(this.btnAddDrink_Click);

            // Add Side Button
            this.btnAddSide.BackColor = System.Drawing.Color.LightYellow;
            this.btnAddSide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSide.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddSide.Location = new System.Drawing.Point(160, 360);
            this.btnAddSide.Name = "btnAddSide";
            this.btnAddSide.Size = new System.Drawing.Size(130, 40);
            this.btnAddSide.TabIndex = 9;
            this.btnAddSide.Text = "🍟 Add Side";
            this.toolTip.SetToolTip(this.btnAddSide, "Add a side dish to your order");
            this.btnAddSide.UseVisualStyleBackColor = false;
            this.btnAddSide.Click += new System.EventHandler(this.btnAddSide_Click);

            // grpCart
            this.grpCart.Controls.Add(this.lstCart);
            this.grpCart.Controls.Add(this.lblTotal);
            this.grpCart.Controls.Add(this.lblItemCount);
            this.grpCart.Controls.Add(this.btnRemoveItem);
            this.grpCart.Controls.Add(this.btnClearCart);
            this.grpCart.Controls.Add(this.btnPlaceOrder);
            this.grpCart.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpCart.Location = new System.Drawing.Point(480, 12);
            this.grpCart.Name = "grpCart";
            this.grpCart.Size = new System.Drawing.Size(480, 420);
            this.grpCart.TabIndex = 2;
            this.grpCart.TabStop = false;
            this.grpCart.Text = "🛒 Current Order Cart";

            // lstCart
            this.lstCart.Font = new System.Drawing.Font("Consolas", 9F);
            this.lstCart.FormattingEnabled = true;
            this.lstCart.ItemHeight = 15;
            this.lstCart.Location = new System.Drawing.Point(15, 25);
            this.lstCart.Name = "lstCart";
            this.lstCart.Size = new System.Drawing.Size(450, 244);
            this.lstCart.TabIndex = 0;

            // lblItemCount
            this.lblItemCount.AutoSize = true;
            this.lblItemCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblItemCount.Location = new System.Drawing.Point(15, 280);
            this.lblItemCount.Name = "lblItemCount";
            this.lblItemCount.Size = new System.Drawing.Size(49, 19);
            this.lblItemCount.Text = "Items: 0";

            // lblTotal
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.Green;
            this.lblTotal.Location = new System.Drawing.Point(15, 310);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(105, 25);
            this.lblTotal.Text = "Total: $0.00";

            // btnRemoveItem
            this.btnRemoveItem.BackColor = System.Drawing.Color.LightCoral;
            this.btnRemoveItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveItem.Location = new System.Drawing.Point(250, 275);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(100, 30);
            this.btnRemoveItem.TabIndex = 3;
            this.btnRemoveItem.Text = "❌ Remove";
            this.toolTip.SetToolTip(this.btnRemoveItem, "Remove selected item from cart");
            this.btnRemoveItem.UseVisualStyleBackColor = false;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);

            // btnClearCart
            this.btnClearCart.BackColor = System.Drawing.Color.Orange;
            this.btnClearCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCart.Location = new System.Drawing.Point(365, 275);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new System.Drawing.Size(100, 30);
            this.btnClearCart.TabIndex = 4;
            this.btnClearCart.Text = "🗑 Clear All";
            this.toolTip.SetToolTip(this.btnClearCart, "Clear entire cart");
            this.btnClearCart.UseVisualStyleBackColor = false;
            this.btnClearCart.Click += new System.EventHandler(this.btnClearCart_Click);

            // btnPlaceOrder
            this.btnPlaceOrder.BackColor = System.Drawing.Color.Gold;
            this.btnPlaceOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlaceOrder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnPlaceOrder.Location = new System.Drawing.Point(120, 355);
            this.btnPlaceOrder.Name = "btnPlaceOrder";
            this.btnPlaceOrder.Size = new System.Drawing.Size(250, 50);
            this.btnPlaceOrder.TabIndex = 5;
            this.btnPlaceOrder.Text = "✅ PLACE ORDER";
            this.toolTip.SetToolTip(this.btnPlaceOrder, "Submit your order and start preparation");
            this.btnPlaceOrder.UseVisualStyleBackColor = false;
            this.btnPlaceOrder.Click += new System.EventHandler(this.btnPlaceOrder_Click);

            // grpTimer
            this.grpTimer.Controls.Add(this.lblCountdown);
            this.grpTimer.Controls.Add(this.progressBar1);
            this.grpTimer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpTimer.Location = new System.Drawing.Point(480, 440);
            this.grpTimer.Name = "grpTimer";
            this.grpTimer.Size = new System.Drawing.Size(480, 100);
            this.grpTimer.TabIndex = 3;
            this.grpTimer.TabStop = false;
            this.grpTimer.Text = "⏱ Order Status";

            // lblCountdown
            this.lblCountdown.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold);
            this.lblCountdown.ForeColor = System.Drawing.Color.Gray;
            this.lblCountdown.Location = new System.Drawing.Point(15, 25);
            this.lblCountdown.Name = "lblCountdown";
            this.lblCountdown.Size = new System.Drawing.Size(450, 30);
            this.lblCountdown.TabIndex = 0;
            this.lblCountdown.Text = "No active order";
            this.lblCountdown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // progressBar1
            this.progressBar1.Location = new System.Drawing.Point(15, 65);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(450, 23);
            this.progressBar1.TabIndex = 1;

            // countdownTimer
            this.countdownTimer.Interval = 1000;
            this.countdownTimer.Tick += new System.EventHandler(this.CountdownTimer_Tick);

            // picPizza
            this.picPizza.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPizza.Location = new System.Drawing.Point(12, 580);
            this.picPizza.Name = "picPizza";
            this.picPizza.Size = new System.Drawing.Size(450, 150);
            this.picPizza.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPizza.TabIndex = 4;
            this.picPizza.TabStop = false;

            // PizzaOrderForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 751);
            this.Controls.Add(this.picPizza);
            this.Controls.Add(this.grpTimer);
            this.Controls.Add(this.grpCart);
            this.Controls.Add(this.grpPizza);
            this.Controls.Add(this.grpCustomer);
            this.Name = "PizzaOrderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "🍕 New Pizza Order";
            this.grpCustomer.ResumeLayout(false);
            this.grpCustomer.PerformLayout();
            this.grpPizza.ResumeLayout(false);
            this.grpPizza.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.grpCart.ResumeLayout(false);
            this.grpCart.PerformLayout();
            this.grpTimer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPizza)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
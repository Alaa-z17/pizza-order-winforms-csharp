using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PizzaOrderSystem.Forms
{
    partial class PizzaOrderForm
    {
        private System.ComponentModel.IContainer components = null;

        private RoundedPanel grpCustomer;
        private TextBox txtCustomerName;
        private TextBox txtPhone;
        private TextBox txtAddress;
        private Label lblName;
        private Label lblPhone;
        private Label lblAddress;
        private ErrorProvider errorProvider;

        private RoundedPanel grpPizza;
        private RadioButton rbSmall;
        private RadioButton rbMedium;
        private RadioButton rbLarge;
        private RadioButton rbFamily;
        private ComboBox cmbCrust;
        private Label lblCrust;
        private Label lblSize;
        private FlowLayoutPanel flpToppings;
        private Label lblToppings;
        private NumericUpDown nudQuantity;
        private Label lblQuantity;
        private Button btnAddPizza;
        private Button btnAddDrink;
        private Button btnAddSide;

        private RoundedPanel grpCart;
        private ListView lstCart;
        private Label lblTotal;
        private Label lblItemCount;
        private Button btnRemoveItem;
        private Button btnClearCart;
        private Button btnPlaceOrder;

        private RoundedPanel grpTimer;
        private Label lblCountdown;
        private System.Windows.Forms.Timer countdownTimer;
        private ProgressBar progressBar1;

        private PictureBox picPizza;
        private ToolTip toolTip;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            errorProvider = new ErrorProvider(components);
            toolTip = new ToolTip(components);
            txtCustomerName = new TextBox();
            countdownTimer = new System.Windows.Forms.Timer(components);
            grpCustomer = new RoundedPanel();
            txtPhone = new TextBox();
            txtAddress = new TextBox();
            lblName = new Label();
            lblPhone = new Label();
            lblAddress = new Label();
            grpPizza = new RoundedPanel();
            rbSmall = new RadioButton();
            rbMedium = new RadioButton();
            rbLarge = new RadioButton();
            rbFamily = new RadioButton();
            cmbCrust = new ComboBox();
            lblCrust = new Label();
            lblSize = new Label();
            flpToppings = new FlowLayoutPanel();
            lblToppings = new Label();
            nudQuantity = new NumericUpDown();
            lblQuantity = new Label();
            btnAddPizza = new Button();
            btnAddDrink = new Button();
            btnAddSide = new Button();
            grpCart = new RoundedPanel();
            lstCart = new ListView();
            lblItemCount = new Label();
            lblTotal = new Label();
            btnRemoveItem = new Button();
            btnClearCart = new Button();
            btnPlaceOrder = new Button();
            grpTimer = new RoundedPanel();
            lblCountdown = new Label();
            progressBar1 = new ProgressBar();
            picPizza = new PictureBox();
            ((ISupportInitialize)errorProvider).BeginInit();
            grpCustomer.SuspendLayout();
            grpPizza.SuspendLayout();
            ((ISupportInitialize)nudQuantity).BeginInit();
            grpCart.SuspendLayout();
            grpTimer.SuspendLayout();
            ((ISupportInitialize)picPizza).BeginInit();
            SuspendLayout();
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // txtCustomerName
            // 
            txtCustomerName.Font = new Font("Segoe UI", 11F);
            txtCustomerName.Location = new Point(109, 40);
            txtCustomerName.Margin = new Padding(3, 4, 3, 4);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(388, 32);
            txtCustomerName.TabIndex = 0;
            toolTip.SetToolTip(txtCustomerName, "Enter customer name");
            txtCustomerName.Validating += txtCustomerName_Validating;
            // 
            // countdownTimer
            // 
            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;
            // 
            // grpCustomer
            // 
            grpCustomer.BackColor = Color.White;
            grpCustomer.BorderRadius = 15;
            grpCustomer.BorderWidth = 1;
            grpCustomer.Controls.Add(txtCustomerName);
            grpCustomer.Controls.Add(txtPhone);
            grpCustomer.Controls.Add(txtAddress);
            grpCustomer.Controls.Add(lblName);
            grpCustomer.Controls.Add(lblPhone);
            grpCustomer.Controls.Add(lblAddress);
            grpCustomer.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            grpCustomer.Location = new Point(17, 20);
            grpCustomer.Margin = new Padding(3, 4, 3, 4);
            grpCustomer.Name = "grpCustomer";
            grpCustomer.Size = new Size(559, 185);
            grpCustomer.TabIndex = 0;
            grpCustomer.TitleText = "👤 CUSTOMER INFO";
            // 
            // txtPhone
            // 
            txtPhone.Font = new Font("Segoe UI", 11F);
            txtPhone.Location = new Point(109, 87);
            txtPhone.Margin = new Padding(3, 4, 3, 4);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(228, 32);
            txtPhone.TabIndex = 1;
            txtPhone.Validating += txtPhone_Validating;
            // 
            // txtAddress
            // 
            txtAddress.Font = new Font("Segoe UI", 11F);
            txtAddress.Location = new Point(109, 133);
            txtAddress.Margin = new Padding(3, 4, 3, 4);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(388, 32);
            txtAddress.TabIndex = 2;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 11F);
            lblName.Location = new Point(23, 44);
            lblName.Name = "lblName";
            lblName.Size = new Size(66, 25);
            lblName.TabIndex = 3;
            lblName.Text = "Name:";
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 11F);
            lblPhone.Location = new Point(23, 91);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(70, 25);
            lblPhone.TabIndex = 4;
            lblPhone.Text = "Phone:";
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 11F);
            lblAddress.Location = new Point(23, 137);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(83, 25);
            lblAddress.TabIndex = 5;
            lblAddress.Text = "Address:";
            // 
            // grpPizza
            // 
            grpPizza.BackColor = Color.White;
            grpPizza.BorderRadius = 15;
            grpPizza.BorderWidth = 1;
            grpPizza.Controls.Add(rbSmall);
            grpPizza.Controls.Add(rbMedium);
            grpPizza.Controls.Add(rbLarge);
            grpPizza.Controls.Add(rbFamily);
            grpPizza.Controls.Add(cmbCrust);
            grpPizza.Controls.Add(lblCrust);
            grpPizza.Controls.Add(lblSize);
            grpPizza.Controls.Add(flpToppings);
            grpPizza.Controls.Add(lblToppings);
            grpPizza.Controls.Add(nudQuantity);
            grpPizza.Controls.Add(lblQuantity);
            grpPizza.Controls.Add(btnAddPizza);
            grpPizza.Controls.Add(btnAddDrink);
            grpPizza.Controls.Add(btnAddSide);
            grpPizza.Location = new Point(17, 213);
            grpPizza.Margin = new Padding(3, 4, 3, 4);
            grpPizza.Name = "grpPizza";
            grpPizza.Size = new Size(559, 573);
            grpPizza.TabIndex = 1;
            grpPizza.TitleText = "🍕 CUSTOMIZE YOUR PIZZA";
            // 
            // rbSmall
            // 
            rbSmall.AutoSize = true;
            rbSmall.Font = new Font("Segoe UI", 10F);
            rbSmall.Location = new Point(79, 53);
            rbSmall.Margin = new Padding(3, 4, 3, 4);
            rbSmall.Name = "rbSmall";
            rbSmall.Size = new Size(127, 27);
            rbSmall.TabIndex = 0;
            rbSmall.Text = "Small ($8.99)";
            // 
            // rbMedium
            // 
            rbMedium.AutoSize = true;
            rbMedium.Checked = true;
            rbMedium.Font = new Font("Segoe UI", 10F);
            rbMedium.Location = new Point(212, 53);
            rbMedium.Margin = new Padding(3, 4, 3, 4);
            rbMedium.Name = "rbMedium";
            rbMedium.Size = new Size(158, 27);
            rbMedium.TabIndex = 1;
            rbMedium.TabStop = true;
            rbMedium.Text = "Medium ($10.99)";
            // 
            // rbLarge
            // 
            rbLarge.AutoSize = true;
            rbLarge.Font = new Font("Segoe UI", 10F);
            rbLarge.Location = new Point(376, 52);
            rbLarge.Margin = new Padding(3, 4, 3, 4);
            rbLarge.Name = "rbLarge";
            rbLarge.Size = new Size(137, 27);
            rbLarge.TabIndex = 2;
            rbLarge.Text = "Large ($12.99)";
            // 
            // rbFamily
            // 
            rbFamily.AutoSize = true;
            rbFamily.Font = new Font("Segoe UI", 10F);
            rbFamily.Location = new Point(79, 92);
            rbFamily.Margin = new Padding(3, 4, 3, 4);
            rbFamily.Name = "rbFamily";
            rbFamily.Size = new Size(142, 27);
            rbFamily.TabIndex = 3;
            rbFamily.Text = "Family ($15.99)";
            // 
            // cmbCrust
            // 
            cmbCrust.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCrust.Font = new Font("Segoe UI", 10F);
            cmbCrust.Location = new Point(280, 91);
            cmbCrust.Margin = new Padding(3, 4, 3, 4);
            cmbCrust.Name = "cmbCrust";
            cmbCrust.Size = new Size(159, 31);
            cmbCrust.TabIndex = 4;
            // 
            // lblCrust
            // 
            lblCrust.AutoSize = true;
            lblCrust.Font = new Font("Segoe UI", 10F);
            lblCrust.Location = new Point(223, 96);
            lblCrust.Name = "lblCrust";
            lblCrust.Size = new Size(54, 23);
            lblCrust.TabIndex = 5;
            lblCrust.Text = "Crust:";
            // 
            // lblSize
            // 
            lblSize.AutoSize = true;
            lblSize.Font = new Font("Segoe UI", 10F);
            lblSize.Location = new Point(29, 56);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(44, 23);
            lblSize.TabIndex = 6;
            lblSize.Text = "Size:";
            // 
            // flpToppings
            // 
            flpToppings.Location = new Point(23, 173);
            flpToppings.Margin = new Padding(3, 4, 3, 4);
            flpToppings.Name = "flpToppings";
            flpToppings.Size = new Size(480, 213);
            flpToppings.TabIndex = 5;
            // 
            // lblToppings
            // 
            lblToppings.AutoSize = true;
            lblToppings.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblToppings.Location = new Point(23, 140);
            lblToppings.Name = "lblToppings";
            lblToppings.Size = new Size(248, 23);
            lblToppings.TabIndex = 7;
            lblToppings.Text = "Toppings ($1 each after first):";
            // 
            // nudQuantity
            // 
            nudQuantity.Font = new Font("Segoe UI", 10F);
            nudQuantity.Location = new Point(109, 403);
            nudQuantity.Margin = new Padding(3, 4, 3, 4);
            nudQuantity.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new Size(91, 30);
            nudQuantity.TabIndex = 8;
            nudQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Font = new Font("Segoe UI", 10F);
            lblQuantity.Location = new Point(23, 407);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(80, 23);
            lblQuantity.TabIndex = 9;
            lblQuantity.Text = "Quantity:";
            // 
            // btnAddPizza
            // 
            btnAddPizza.BackColor = Color.FromArgb(220, 80, 20);
            btnAddPizza.FlatAppearance.BorderSize = 0;
            btnAddPizza.FlatStyle = FlatStyle.Flat;
            btnAddPizza.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnAddPizza.ForeColor = Color.White;
            btnAddPizza.Location = new Point(223, 393);
            btnAddPizza.Margin = new Padding(3, 4, 3, 4);
            btnAddPizza.Name = "btnAddPizza";
            btnAddPizza.Size = new Size(206, 53);
            btnAddPizza.TabIndex = 10;
            btnAddPizza.Text = "➕ ADD PIZZA";
            btnAddPizza.UseVisualStyleBackColor = false;
            btnAddPizza.Click += btnAddPizza_Click;
            // 
            // btnAddDrink
            // 
            btnAddDrink.BackColor = Color.FromArgb(70, 130, 200);
            btnAddDrink.FlatStyle = FlatStyle.Flat;
            btnAddDrink.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddDrink.ForeColor = Color.White;
            btnAddDrink.Location = new Point(23, 480);
            btnAddDrink.Margin = new Padding(3, 4, 3, 4);
            btnAddDrink.Name = "btnAddDrink";
            btnAddDrink.Size = new Size(149, 53);
            btnAddDrink.TabIndex = 11;
            btnAddDrink.Text = "\U0001f964 ADD DRINK";
            btnAddDrink.UseVisualStyleBackColor = false;
            btnAddDrink.Click += btnAddDrink_Click;
            // 
            // btnAddSide
            // 
            btnAddSide.BackColor = Color.FromArgb(100, 180, 100);
            btnAddSide.FlatStyle = FlatStyle.Flat;
            btnAddSide.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddSide.ForeColor = Color.White;
            btnAddSide.Location = new Point(194, 480);
            btnAddSide.Margin = new Padding(3, 4, 3, 4);
            btnAddSide.Name = "btnAddSide";
            btnAddSide.Size = new Size(149, 53);
            btnAddSide.TabIndex = 12;
            btnAddSide.Text = "🍟 ADD SIDE";
            btnAddSide.UseVisualStyleBackColor = false;
            btnAddSide.Click += btnAddSide_Click;
            // 
            // grpCart
            // 
            grpCart.BackColor = Color.White;
            grpCart.BorderRadius = 15;
            grpCart.BorderWidth = 1;
            grpCart.Controls.Add(lstCart);
            grpCart.Controls.Add(lblItemCount);
            grpCart.Controls.Add(lblTotal);
            grpCart.Controls.Add(btnRemoveItem);
            grpCart.Controls.Add(btnClearCart);
            grpCart.Controls.Add(btnPlaceOrder);
            grpCart.Location = new Point(614, 20);
            grpCart.Margin = new Padding(3, 4, 3, 4);
            grpCart.Name = "grpCart";
            grpCart.Size = new Size(549, 573);
            grpCart.TabIndex = 2;
            grpCart.TitleText = "\U0001f6d2 CURRENT ORDER";
            // 
            // lstCart
            // 
            lstCart.Font = new Font("Segoe UI", 10F);
            lstCart.FullRowSelect = true;
            lstCart.HeaderStyle = ColumnHeaderStyle.None;
            lstCart.Location = new Point(17, 53);
            lstCart.Margin = new Padding(3, 4, 3, 4);
            lstCart.Name = "lstCart";
            lstCart.Size = new Size(514, 332);
            lstCart.TabIndex = 0;
            lstCart.UseCompatibleStateImageBehavior = false;
            lstCart.View = View.Details;
            this.lstCart.Columns.Add("Item", 400);   
            // 
            // lblItemCount
            // 
            lblItemCount.AutoSize = true;
            lblItemCount.Font = new Font("Segoe UI", 11F);
            lblItemCount.Location = new Point(17, 407);
            lblItemCount.Name = "lblItemCount";
            lblItemCount.Size = new Size(76, 25);
            lblItemCount.TabIndex = 1;
            lblItemCount.Text = "Items: 0";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(220, 80, 20);
            lblTotal.Location = new Point(17, 440);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(180, 41);
            lblTotal.TabIndex = 2;
            lblTotal.Text = "Total: $0.00";
            // 
            // btnRemoveItem
            // 
            btnRemoveItem.BackColor = Color.FromArgb(200, 80, 80);
            btnRemoveItem.FlatStyle = FlatStyle.Flat;
            btnRemoveItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRemoveItem.ForeColor = Color.White;
            btnRemoveItem.Location = new Point(320, 400);
            btnRemoveItem.Margin = new Padding(3, 4, 3, 4);
            btnRemoveItem.Name = "btnRemoveItem";
            btnRemoveItem.Size = new Size(103, 40);
            btnRemoveItem.TabIndex = 3;
            btnRemoveItem.Text = "❌ REMOVE";
            btnRemoveItem.UseVisualStyleBackColor = false;
            btnRemoveItem.Click += btnRemoveItem_Click;
            // 
            // btnClearCart
            // 
            btnClearCart.BackColor = Color.FromArgb(240, 150, 40);
            btnClearCart.FlatStyle = FlatStyle.Flat;
            btnClearCart.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClearCart.ForeColor = Color.White;
            btnClearCart.Location = new Point(434, 400);
            btnClearCart.Margin = new Padding(3, 4, 3, 4);
            btnClearCart.Name = "btnClearCart";
            btnClearCart.Size = new Size(97, 40);
            btnClearCart.TabIndex = 4;
            btnClearCart.Text = "CLEAR";
            btnClearCart.UseVisualStyleBackColor = false;
            btnClearCart.Click += btnClearCart_Click;
            // 
            // btnPlaceOrder
            // 
            btnPlaceOrder.BackColor = Color.FromArgb(50, 180, 70);
            btnPlaceOrder.FlatStyle = FlatStyle.Flat;
            btnPlaceOrder.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnPlaceOrder.ForeColor = Color.White;
            btnPlaceOrder.Location = new Point(137, 493);
            btnPlaceOrder.Margin = new Padding(3, 4, 3, 4);
            btnPlaceOrder.Name = "btnPlaceOrder";
            btnPlaceOrder.Size = new Size(274, 67);
            btnPlaceOrder.TabIndex = 5;
            btnPlaceOrder.Text = "✅ PLACE ORDER";
            btnPlaceOrder.UseVisualStyleBackColor = false;
            btnPlaceOrder.Click += btnPlaceOrder_Click;
            // 
            // grpTimer
            // 
            grpTimer.BackColor = Color.White;
            grpTimer.BorderRadius = 15;
            grpTimer.BorderWidth = 1;
            grpTimer.Controls.Add(lblCountdown);
            grpTimer.Controls.Add(progressBar1);
            grpTimer.Location = new Point(614, 620);
            grpTimer.Margin = new Padding(3, 4, 3, 4);
            grpTimer.Name = "grpTimer";
            grpTimer.Size = new Size(549, 133);
            grpTimer.TabIndex = 3;
            grpTimer.TitleText = "⏱ ORDER STATUS";
            // 
            // lblCountdown
            // 
            lblCountdown.Font = new Font("Consolas", 16F, FontStyle.Bold);
            lblCountdown.ForeColor = Color.DarkRed;
            lblCountdown.Location = new Point(17, 47);
            lblCountdown.Name = "lblCountdown";
            lblCountdown.Size = new Size(514, 40);
            lblCountdown.TabIndex = 0;
            lblCountdown.Text = "No active order";
            lblCountdown.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(17, 93);
            progressBar1.Margin = new Padding(3, 4, 3, 4);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(514, 27);
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 1;
            // 
            // picPizza
            // 
            picPizza.BackColor = Color.FromArgb(245, 245, 245);
            picPizza.Location = new Point(17, 760);
            picPizza.Margin = new Padding(3, 4, 3, 4);
            picPizza.Name = "picPizza";
            picPizza.Size = new Size(526, 173);
            picPizza.SizeMode = PictureBoxSizeMode.StretchImage;
            picPizza.TabIndex = 4;
            picPizza.TabStop = false;
            // 
            // PizzaOrderForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 245, 240);
            ClientSize = new Size(1175, 960);
            Controls.Add(picPizza);
            Controls.Add(grpTimer);
            Controls.Add(grpCart);
            Controls.Add(grpPizza);
            Controls.Add(grpCustomer);
            Margin = new Padding(3, 4, 3, 4);
            Name = "PizzaOrderForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "🍕 New Pizza Order";
            ((ISupportInitialize)errorProvider).EndInit();
            grpCustomer.ResumeLayout(false);
            grpCustomer.PerformLayout();
            grpPizza.ResumeLayout(false);
            grpPizza.PerformLayout();
            ((ISupportInitialize)nudQuantity).EndInit();
            grpCart.ResumeLayout(false);
            grpCart.PerformLayout();
            grpTimer.ResumeLayout(false);
            ((ISupportInitialize)picPizza).EndInit();
            ResumeLayout(false);
        }
    }
}
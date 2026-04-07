using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PizzaOrderSystem.Forms
{
    partial class PizzaOrderForm
    {
        private IContainer components = null;
        private RoundedPanel grpCustomer;
        private TextBox txtCustomerName, txtPhone, txtAddress;
        private Label lblName, lblPhone, lblAddress;
        private ErrorProvider errorProvider;
        private RoundedPanel grpPizza;
        private RadioButton rbSmall, rbMedium, rbLarge, rbFamily;
        private ComboBox cmbCrust;
        private Label lblCrust, lblSize;
        private FlowLayoutPanel flpToppings;
        private Label lblToppings;
        private NumericUpDown nudQuantity;
        private Label lblQuantity;
        private Button btnAddPizza, btnAddDrink, btnAddSide;
        private RoundedPanel grpCart;
        private ListView lstCart;
        private Label lblTotal, lblItemCount;
        private Button btnRemoveItem, btnClearCart, btnPlaceOrder;
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(PizzaOrderForm));
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

            errorProvider.ContainerControl = this;

            txtCustomerName.Font = new Font("Segoe UI", 11F);
            txtCustomerName.Location = new Point(109, 40);
            txtCustomerName.Margin = new Padding(3, 4, 3, 4);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(388, 32);
            txtCustomerName.TabIndex = 0;
            toolTip.SetToolTip(txtCustomerName, "Enter customer name");
            txtCustomerName.Validating += txtCustomerName_Validating;

            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;

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

            txtPhone.Font = new Font("Segoe UI", 11F);
            txtPhone.Location = new Point(109, 87);
            txtPhone.Margin = new Padding(3, 4, 3, 4);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(228, 32);
            txtPhone.TabIndex = 1;
            txtPhone.Validating += txtPhone_Validating;

            txtAddress.Font = new Font("Segoe UI", 11F);
            txtAddress.Location = new Point(109, 133);
            txtAddress.Margin = new Padding(3, 4, 3, 4);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(388, 32);
            txtAddress.TabIndex = 2;

            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 11F);
            lblName.Location = new Point(23, 44);
            lblName.Name = "lblName";
            lblName.Size = new Size(66, 25);
            lblName.Text = "Name:";

            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 11F);
            lblPhone.Location = new Point(23, 91);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(70, 25);
            lblPhone.Text = "Phone:";

            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 11F);
            lblAddress.Location = new Point(23, 137);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(83, 25);
            lblAddress.Text = "Address:";

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

            rbSmall.AutoSize = true;
            rbSmall.Font = new Font("Segoe UI", 10F);
            rbSmall.Location = new Point(79, 53);
            rbSmall.Margin = new Padding(3, 4, 3, 4);
            rbSmall.Name = "rbSmall";
            rbSmall.Size = new Size(127, 27);
            rbSmall.Text = "Small ($8.99)";

            rbMedium.AutoSize = true;
            rbMedium.Checked = true;
            rbMedium.Font = new Font("Segoe UI", 10F);
            rbMedium.Location = new Point(212, 53);
            rbMedium.Margin = new Padding(3, 4, 3, 4);
            rbMedium.Name = "rbMedium";
            rbMedium.Size = new Size(158, 27);
            rbMedium.Text = "Medium ($10.99)";

            rbLarge.AutoSize = true;
            rbLarge.Font = new Font("Segoe UI", 10F);
            rbLarge.Location = new Point(376, 52);
            rbLarge.Margin = new Padding(3, 4, 3, 4);
            rbLarge.Name = "rbLarge";
            rbLarge.Size = new Size(137, 27);
            rbLarge.Text = "Large ($12.99)";

            rbFamily.AutoSize = true;
            rbFamily.Font = new Font("Segoe UI", 10F);
            rbFamily.Location = new Point(79, 92);
            rbFamily.Margin = new Padding(3, 4, 3, 4);
            rbFamily.Name = "rbFamily";
            rbFamily.Size = new Size(142, 27);
            rbFamily.Text = "Family ($15.99)";

            cmbCrust.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCrust.Font = new Font("Segoe UI", 10F);
            cmbCrust.Location = new Point(280, 91);
            cmbCrust.Margin = new Padding(3, 4, 3, 4);
            cmbCrust.Name = "cmbCrust";
            cmbCrust.Size = new Size(159, 31);

            lblCrust.AutoSize = true;
            lblCrust.Font = new Font("Segoe UI", 10F);
            lblCrust.Location = new Point(223, 96);
            lblCrust.Name = "lblCrust";
            lblCrust.Size = new Size(54, 23);
            lblCrust.Text = "Crust:";

            lblSize.AutoSize = true;
            lblSize.Font = new Font("Segoe UI", 10F);
            lblSize.Location = new Point(29, 56);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(44, 23);
            lblSize.Text = "Size:";

            flpToppings.Location = new Point(23, 173);
            flpToppings.Margin = new Padding(3, 4, 3, 4);
            flpToppings.Name = "flpToppings";
            flpToppings.Size = new Size(480, 213);

            lblToppings.AutoSize = true;
            lblToppings.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblToppings.Location = new Point(23, 140);
            lblToppings.Name = "lblToppings";
            lblToppings.Size = new Size(248, 23);
            lblToppings.Text = "Toppings ($1 each after first):";

            nudQuantity.Font = new Font("Segoe UI", 10F);
            nudQuantity.Location = new Point(109, 403);
            nudQuantity.Margin = new Padding(3, 4, 3, 4);
            nudQuantity.Maximum = 10;
            nudQuantity.Minimum = 1;
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new Size(91, 30);
            nudQuantity.Value = 1;

            lblQuantity.AutoSize = true;
            lblQuantity.Font = new Font("Segoe UI", 10F);
            lblQuantity.Location = new Point(23, 407);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(80, 23);
            lblQuantity.Text = "Quantity:";

            btnAddPizza.BackColor = Color.FromArgb(220, 80, 20);
            btnAddPizza.FlatAppearance.BorderSize = 0;
            btnAddPizza.FlatStyle = FlatStyle.Flat;
            btnAddPizza.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnAddPizza.ForeColor = Color.White;
            btnAddPizza.Location = new Point(223, 393);
            btnAddPizza.Margin = new Padding(3, 4, 3, 4);
            btnAddPizza.Name = "btnAddPizza";
            btnAddPizza.Size = new Size(206, 53);
            btnAddPizza.Text = "➕ ADD PIZZA";
            btnAddPizza.UseVisualStyleBackColor = false;
            btnAddPizza.Click += btnAddPizza_Click;

            btnAddDrink.BackColor = Color.FromArgb(70, 130, 200);
            btnAddDrink.FlatStyle = FlatStyle.Flat;
            btnAddDrink.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddDrink.ForeColor = Color.White;
            btnAddDrink.Location = new Point(23, 480);
            btnAddDrink.Margin = new Padding(3, 4, 3, 4);
            btnAddDrink.Name = "btnAddDrink";
            btnAddDrink.Size = new Size(149, 53);
            btnAddDrink.Text = "🥤 ADD DRINK";
            btnAddDrink.Click += btnAddDrink_Click;

            btnAddSide.BackColor = Color.FromArgb(100, 180, 100);
            btnAddSide.FlatStyle = FlatStyle.Flat;
            btnAddSide.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddSide.ForeColor = Color.White;
            btnAddSide.Location = new Point(194, 480);
            btnAddSide.Margin = new Padding(3, 4, 3, 4);
            btnAddSide.Name = "btnAddSide";
            btnAddSide.Size = new Size(149, 53);
            btnAddSide.Text = "🍟 ADD SIDE";
            btnAddSide.Click += btnAddSide_Click;

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
            grpCart.TitleText = "🛒 CURRENT ORDER";

            lstCart.Font = new Font("Segoe UI", 10F);
            lstCart.FullRowSelect = true;
            lstCart.HeaderStyle = ColumnHeaderStyle.None;
            lstCart.Location = new Point(17, 53);
            lstCart.Margin = new Padding(3, 4, 3, 4);
            lstCart.Name = "lstCart";
            lstCart.Size = new Size(514, 332);
            lstCart.View = View.Details;
            lstCart.Columns.Add("Item", 400);
            lstCart.UseCompatibleStateImageBehavior = false;

            lblItemCount.AutoSize = true;
            lblItemCount.Font = new Font("Segoe UI", 11F);
            lblItemCount.Location = new Point(17, 407);
            lblItemCount.Name = "lblItemCount";
            lblItemCount.Size = new Size(76, 25);
            lblItemCount.Text = "Items: 0";

            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(220, 80, 20);
            lblTotal.Location = new Point(17, 440);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(180, 41);
            lblTotal.Text = "Total: $0.00";

            btnRemoveItem.BackColor = Color.FromArgb(200, 80, 80);
            btnRemoveItem.FlatStyle = FlatStyle.Flat;
            btnRemoveItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRemoveItem.ForeColor = Color.White;
            btnRemoveItem.Location = new Point(320, 400);
            btnRemoveItem.Margin = new Padding(3, 4, 3, 4);
            btnRemoveItem.Name = "btnRemoveItem";
            btnRemoveItem.Size = new Size(103, 40);
            btnRemoveItem.Text = "❌ REMOVE";
            btnRemoveItem.Click += btnRemoveItem_Click;

            btnClearCart.BackColor = Color.FromArgb(240, 150, 40);
            btnClearCart.FlatStyle = FlatStyle.Flat;
            btnClearCart.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClearCart.ForeColor = Color.White;
            btnClearCart.Location = new Point(434, 400);
            btnClearCart.Margin = new Padding(3, 4, 3, 4);
            btnClearCart.Name = "btnClearCart";
            btnClearCart.Size = new Size(97, 40);
            btnClearCart.Text = "CLEAR";
            btnClearCart.Click += btnClearCart_Click;

            btnPlaceOrder.BackColor = Color.FromArgb(50, 180, 70);
            btnPlaceOrder.FlatStyle = FlatStyle.Flat;
            btnPlaceOrder.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnPlaceOrder.ForeColor = Color.White;
            btnPlaceOrder.Location = new Point(137, 493);
            btnPlaceOrder.Margin = new Padding(3, 4, 3, 4);
            btnPlaceOrder.Name = "btnPlaceOrder";
            btnPlaceOrder.Size = new Size(274, 67);
            btnPlaceOrder.Text = "✅ PLACE ORDER";
            btnPlaceOrder.Click += btnPlaceOrder_Click;

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

            lblCountdown.Font = new Font("Consolas", 16F, FontStyle.Bold);
            lblCountdown.ForeColor = Color.DarkRed;
            lblCountdown.Location = new Point(17, 47);
            lblCountdown.Name = "lblCountdown";
            lblCountdown.Size = new Size(514, 40);
            lblCountdown.Text = "No active order";
            lblCountdown.TextAlign = ContentAlignment.MiddleCenter;

            progressBar1.Location = new Point(17, 93);
            progressBar1.Margin = new Padding(3, 4, 3, 4);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(514, 27);
            progressBar1.Style = ProgressBarStyle.Continuous;

            picPizza.BackColor = Color.FromArgb(245, 245, 245);
            picPizza.Location = new Point(17, 760);
            picPizza.Margin = new Padding(3, 4, 3, 4);
            picPizza.Name = "picPizza";
            picPizza.Size = new Size(526, 173);
            picPizza.SizeMode = PictureBoxSizeMode.StretchImage;
            picPizza.TabIndex = 4;
            picPizza.TabStop = false;

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 245, 240);
            ClientSize = new Size(1175, 960);
            Controls.Add(picPizza);
            Controls.Add(grpTimer);
            Controls.Add(grpCart);
            Controls.Add(grpPizza);
            Controls.Add(grpCustomer);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "PizzaOrderForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "New Pizza Order";

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
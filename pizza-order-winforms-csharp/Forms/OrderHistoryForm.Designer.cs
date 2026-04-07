namespace PizzaOrderSystem.Forms
{
    partial class OrderHistoryForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.ListBox lstDetails;   // Will be replaced at runtime
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblOrders;
        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrint;   // NEW: print button

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dtpDate = new System.Windows.Forms.DateTimePicker();
            dgvOrders = new System.Windows.Forms.DataGridView();
            lstDetails = new System.Windows.Forms.ListBox();
            lblDate = new System.Windows.Forms.Label();
            lblOrders = new System.Windows.Forms.Label();
            lblDetails = new System.Windows.Forms.Label();
            btnRefresh = new System.Windows.Forms.Button();
            btnClose = new System.Windows.Forms.Button();
            btnPrint = new System.Windows.Forms.Button();  // NEW
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            SuspendLayout();

            // dtpDate
            dtpDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            dtpDate.Location = new System.Drawing.Point(120, 15);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new System.Drawing.Size(200, 28);
            dtpDate.TabIndex = 0;
            dtpDate.ValueChanged += dtpDate_ValueChanged;

            // lblDate
            lblDate.AutoSize = true;
            lblDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblDate.Location = new System.Drawing.Point(12, 20);
            lblDate.Name = "lblDate";
            lblDate.Size = new System.Drawing.Size(94, 23);
            lblDate.Text = "Select Date:";
            lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // btnRefresh
            btnRefresh.BackColor = System.Drawing.Color.FromArgb(70, 130, 200);
            btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnRefresh.ForeColor = System.Drawing.Color.White;
            btnRefresh.Location = new System.Drawing.Point(340, 12);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(80, 32);
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;

            // dgvOrders
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.BackgroundColor = System.Drawing.Color.White;
            dgvOrders.Location = new System.Drawing.Point(12, 55);
            dgvOrders.Name = "dgvOrders";
            dgvOrders.ReadOnly = true;
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.Size = new System.Drawing.Size(580, 220);
            dgvOrders.TabIndex = 2;
            dgvOrders.SelectionChanged += dgvOrders_SelectionChanged;

            // lblOrders
            lblOrders.AutoSize = true;
            lblOrders.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblOrders.Location = new System.Drawing.Point(12, 295);
            lblOrders.Name = "lblOrders";
            lblOrders.Size = new System.Drawing.Size(71, 23);
            lblOrders.Text = "Orders:";

            // lblDetails
            lblDetails.AutoSize = true;
            lblDetails.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblDetails.Location = new System.Drawing.Point(12, 330);
            lblDetails.Name = "lblDetails";
            lblDetails.Size = new System.Drawing.Size(83, 23);
            lblDetails.Text = "Order Details:";

            // lstDetails (will be replaced by txtDetails at runtime)
            lstDetails.Font = new System.Drawing.Font("Consolas", 10F);
            lstDetails.FormattingEnabled = true;
            lstDetails.ItemHeight = 19;
            lstDetails.Location = new System.Drawing.Point(12, 365);
            lstDetails.Name = "lstDetails";
            lstDetails.Size = new System.Drawing.Size(580, 155);
            lstDetails.TabIndex = 3;

            // NEW: btnPrint
            btnPrint.BackColor = System.Drawing.Color.FromArgb(100, 100, 200);
            btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnPrint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnPrint.ForeColor = System.Drawing.Color.White;
            btnPrint.Location = new System.Drawing.Point(12, 530);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new System.Drawing.Size(100, 32);
            btnPrint.Text = "🖨️ Print";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;

            // btnClose
            btnClose.BackColor = System.Drawing.Color.FromArgb(200, 80, 80);
            btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnClose.ForeColor = System.Drawing.Color.White;
            btnClose.Location = new System.Drawing.Point(492, 530);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(100, 32);
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;

            // Form
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(248, 245, 240);
            ClientSize = new System.Drawing.Size(610, 580);
            Controls.Add(btnPrint);    // NEW
            Controls.Add(btnClose);
            Controls.Add(lstDetails);
            Controls.Add(lblDetails);
            Controls.Add(lblOrders);
            Controls.Add(dgvOrders);
            Controls.Add(btnRefresh);
            Controls.Add(dtpDate);
            Controls.Add(lblDate);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "OrderHistoryForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Order History";
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
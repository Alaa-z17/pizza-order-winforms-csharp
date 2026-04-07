using System;
using System.Linq;
using System.Windows.Forms;
using PizzaOrderSystem.Models;
using PizzaOrderSystem.Services;


namespace PizzaOrderSystem.Forms
{
    public partial class OrderHistoryForm : Form
    {
        private TextBox? txtDetails;

        public OrderHistoryForm()
        {
            InitializeComponent();
            ReplaceListBoxWithTextBox();
            ApplyResources();
            LanguageManager.LanguageChanged += ApplyResources;
            LoadOrdersByDate(DateTime.Today);
        }

        private void ReplaceListBoxWithTextBox()
        {
            if (lstDetails == null) return;

            var parent = lstDetails.Parent;
            if (parent == null) return;

            var location = lstDetails.Location;
            var size = lstDetails.Size;
            var tabIndex = lstDetails.TabIndex;

            txtDetails = new TextBox
            {
                Location = location,
                Size = size,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                WordWrap = true,
                BackColor = lstDetails.BackColor,
                Font = lstDetails.Font,
                TabIndex = tabIndex,
                Name = "txtDetails"
            };

            parent.Controls.Remove(lstDetails);
            parent.Controls.Add(txtDetails);
            lstDetails.Dispose();
        }

        private void ApplyResources()
        {
            this.Text = LanguageManager.GetString("OrderHistory");
            lblDate.Text = LanguageManager.GetString("SelectDate");
            btnRefresh.Text = LanguageManager.GetString("Refresh");
            lblOrders.Text = LanguageManager.GetString("Orders");
            lblDetails.Text = LanguageManager.GetString("OrderDetails");
            btnClose.Text = LanguageManager.GetString("Close");
            btnPrint.Text = LanguageManager.GetString("PrintInvoice"); // استخدام مفتاح الطباعة
        }

        private void LoadOrdersByDate(DateTime date)
        {
            var allOrders = OrderManager.AllOrders.ToList();
            var filtered = allOrders.Where(o => o.OrderTime.Date == date.Date).ToList();

            dgvOrders.DataSource = null;
            dgvOrders.DataSource = filtered.Select(o => new
            {
                o.OrderId,
                Customer = o.Customer.Name,
                Total = LanguageManager.FormatCurrency(o.TotalAmount),
                Time = o.OrderTime.ToString("hh:mm tt"),
                Status = LanguageManager.GetString(o.Status.ToString())
            }).ToList();

            if (dgvOrders.Rows.Count > 0) dgvOrders.Rows[0].Selected = true;
        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            var details = txtDetails;
            if (details == null) return;

            if (dgvOrders.SelectedRows.Count == 0) return;

            object? rawValue = dgvOrders.SelectedRows[0].Cells[0].Value;
            if (rawValue == null) return;

            int orderId = Convert.ToInt32(rawValue);
            var order = OrderManager.AllOrders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null) return;

            details.Clear();
            foreach (var item in order.Items)
            {
                string line = $"{item.Quantity} x {GetLocalizedName(item)} - {LanguageManager.FormatCurrency(item.CalculateTotal())}";
                if (item is Pizza p) line += $" [{LanguageManager.GetString(p.Size.ToString())}, {LanguageManager.GetString(p.Crust.ToString())} crust, {LanguageManager.GetString("ToppingsWord")}: {string.Join(", ", p.Toppings.Select(t => LanguageManager.GetString(t.ToString())))}]";
                else if (item is Drink d) line += $" [{LanguageManager.GetString(d.Size.ToString() + "Size")}]";
                else if (item is Side s) line += $" [{LanguageManager.GetString(s.SideType.ToString())}]";
                details.AppendText(line + Environment.NewLine);
            }
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

        // NEW: طباعة الفاتورة للطلب المحدد
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show(LanguageManager.GetString("PleaseSelectOrderToPrint"),
                                LanguageManager.GetString("Warning"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            object? rawValue = dgvOrders.SelectedRows[0].Cells[0].Value;
            if (rawValue == null) return;

            int orderId = Convert.ToInt32(rawValue);
            var order = OrderManager.AllOrders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null) return;

            bool isArabic = (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "ar");
            InvoicePrinter.PrintInvoice(order, isArabic);
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e) => LoadOrdersByDate(dtpDate.Value.Date);
        private void btnRefresh_Click(object sender, EventArgs e) => LoadOrdersByDate(dtpDate.Value.Date);
        private void btnClose_Click(object sender, EventArgs e) => Close();
    }
}
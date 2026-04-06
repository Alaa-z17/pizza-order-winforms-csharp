using System;
using System.Linq;
using System.Windows.Forms;
using PizzaOrderSystem.Models;
using PizzaOrderSystem.Services;

namespace PizzaOrderSystem.Forms
{
    public partial class OrderHistoryForm : Form
    {
        public OrderHistoryForm()
        {
            InitializeComponent();
            LoadOrdersByDate(DateTime.Today);
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
                Total = o.TotalAmount,
                Time = o.OrderTime.ToString("hh:mm tt"),
                Status = o.Status.ToString()
            }).ToList();

            if (dgvOrders.Rows.Count > 0)
                dgvOrders.Rows[0].Selected = true;
        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0) return;
            int orderId = (int)dgvOrders.SelectedRows[0].Cells[0].Value;
            var order = OrderManager.AllOrders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null) return;

            lstDetails.Items.Clear();
            foreach (var item in order.Items)
            {
                string line = $"{item.Quantity} x {item.Name} - {item.CalculateTotal():C}";
                if (item is Pizza p)
                    line += $" [{p.Size}, {p.Crust} crust, toppings: {string.Join(",", p.Toppings)}]";
                else if (item is Drink d)
                    line += $" [{d.Size}]";
                else if (item is Side s)
                    line += $" [{s.SideType}]";
                lstDetails.Items.Add(line);
            }
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            LoadOrdersByDate(dtpDate.Value.Date);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadOrdersByDate(dtpDate.Value.Date);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
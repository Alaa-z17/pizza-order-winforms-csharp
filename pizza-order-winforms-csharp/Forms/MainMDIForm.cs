using System;
using System.Windows.Forms;
using PizzaOrderSystem.Forms;

namespace PizzaOrderSystem
{
    public partial class MainMDIForm : Form
    {
        public MainMDIForm()
        {
            InitializeComponent();
        }

        private void newOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenNewOrderForm();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form child in MdiChildren)
            {
                child.Close();
            }
        }

        private void OpenNewOrderForm()
        {
            var child = new PizzaOrderForm();
            child.MdiParent = this;
            child.Show();
        }
        public void UpdateStatus(string message)
        {
            statusLabel.Text = message;
        }
    }
}
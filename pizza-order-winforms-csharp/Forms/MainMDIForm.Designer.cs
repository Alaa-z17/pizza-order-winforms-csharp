using pizza_order_winforms_csharp;
using System.Drawing;
using System.Windows.Forms;

namespace PizzaOrderSystem
{
    partial class MainMDIForm
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newOrderToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem windowToolStripMenuItem;
        private ToolStripMenuItem cascadeToolStripMenuItem;
        private ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private ToolStripMenuItem tileVerticalToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem closeAllToolStripMenuItem;
        private StatusStrip statusStrip;
        internal ToolStripStatusLabel statusLabel;
        private ToolStrip toolStrip;
        private ToolStripButton newOrderButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip = new MenuStrip();
            this.fileToolStripMenuItem = new ToolStripMenuItem();
            this.newOrderToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.exitToolStripMenuItem = new ToolStripMenuItem();
            this.windowToolStripMenuItem = new ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.closeAllToolStripMenuItem = new ToolStripMenuItem();
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();
            this.toolStrip = new ToolStrip();
            this.newOrderButton = new ToolStripButton();

            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();

            // menuStrip
            this.menuStrip.BackColor = Color.FromArgb(40, 40, 40);
            this.menuStrip.ForeColor = Color.White;
            this.menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, windowToolStripMenuItem });
            this.menuStrip.Location = new Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new Size(984, 28);
            this.menuStrip.TabIndex = 1;

            // fileToolStripMenuItem
            this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newOrderToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";

            // newOrderToolStripMenuItem
             this.newOrderToolStripMenuItem.Image = Resources.pizza; 
            this.newOrderToolStripMenuItem.Name = "newOrderToolStripMenuItem";
            this.newOrderToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            this.newOrderToolStripMenuItem.Size = new Size(180, 26);
            this.newOrderToolStripMenuItem.Text = "&New Order";
            this.newOrderToolStripMenuItem.Click += newOrderToolStripMenuItem_Click;

            // toolStripSeparator1
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(177, 6);

            // exitToolStripMenuItem
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
            this.exitToolStripMenuItem.Size = new Size(180, 26);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;

            // windowToolStripMenuItem
            this.windowToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cascadeToolStripMenuItem, tileHorizontalToolStripMenuItem, tileVerticalToolStripMenuItem, toolStripSeparator2, closeAllToolStripMenuItem });
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new Size(70, 24);
            this.windowToolStripMenuItem.Text = "&Window";

            // cascadeToolStripMenuItem
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new Size(160, 26);
            this.cascadeToolStripMenuItem.Text = "&Cascade";
            this.cascadeToolStripMenuItem.Click += cascadeToolStripMenuItem_Click;

            // tileHorizontalToolStripMenuItem
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Size = new Size(160, 26);
            this.tileHorizontalToolStripMenuItem.Text = "Tile &Horizontal";
            this.tileHorizontalToolStripMenuItem.Click += tileHorizontalToolStripMenuItem_Click;

            // tileVerticalToolStripMenuItem
            this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
            this.tileVerticalToolStripMenuItem.Size = new Size(160, 26);
            this.tileVerticalToolStripMenuItem.Text = "Tile &Vertical";
            this.tileVerticalToolStripMenuItem.Click += tileVerticalToolStripMenuItem_Click;

            // toolStripSeparator2
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(157, 6);

            // closeAllToolStripMenuItem
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new Size(160, 26);
            this.closeAllToolStripMenuItem.Text = "&Close All";
            this.closeAllToolStripMenuItem.Click += closeAllToolStripMenuItem_Click;

            // statusStrip
            this.statusStrip.BackColor = Color.FromArgb(50, 50, 50);
            this.statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
            this.statusStrip.Location = new Point(0, 642);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new Size(984, 28);
            this.statusStrip.TabIndex = 2;

            // statusLabel
            this.statusLabel.ForeColor = Color.White;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new Size(220, 23);
            this.statusLabel.Text = "🍕 Ready - Create a new pizza order";

            // toolStrip
            this.toolStrip.BackColor = Color.FromArgb(220, 80, 20);
            this.toolStrip.ForeColor = Color.White;
            this.toolStrip.Items.AddRange(new ToolStripItem[] { newOrderButton });
            this.toolStrip.Location = new Point(0, 28);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new Size(984, 32);
            this.toolStrip.TabIndex = 3;

            // newOrderButton
            this.newOrderButton.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            this.newOrderButton.Image = Resources.pizza; 
            this.newOrderButton.ImageTransparentColor = Color.Magenta;
            this.newOrderButton.Name = "newOrderButton";
            this.newOrderButton.Size = new Size(95, 29);
            this.newOrderButton.Text = "New Order";
            this.newOrderButton.Click += newOrderToolStripMenuItem_Click;

            // MainMDIForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.ClientSize = new Size(984, 670);
            this.Controls.Add(toolStrip);
            this.Controls.Add(statusStrip);
            this.Controls.Add(menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = menuStrip;
            this.Name = "MainMDIForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "🍕 Pizza Order System - Restaurant Management";
            this.WindowState = FormWindowState.Maximized;

            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
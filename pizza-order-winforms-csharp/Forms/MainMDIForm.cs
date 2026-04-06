using PizzaOrderSystem.Forms;
using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PizzaOrderSystem
{
    public partial class MainMDIForm : Form
    {
        public MainMDIForm()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.Opaque, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            InitializeComponent();
        }

        //override from the base class
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            foreach (Control ctl in Controls)
            {
                if (ctl is MdiClient client)
                {
                    client.Paint += (s, pe) =>
                    {
                        var g = pe.Graphics;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                        // ----- 1. Gradient background (warm restaurant feel) -----
                        using (var bgBrush = new LinearGradientBrush(
                            client.ClientRectangle,
                            Color.FromArgb(255, 250, 245, 235), // light dough color
                            Color.FromArgb(255, 210, 80, 30),   // pizza sauce red-orange
                            LinearGradientMode.ForwardDiagonal))
                        {
                            g.FillRectangle(bgBrush, client.ClientRectangle);
                        }

                        // ----- 2. Draw a large, angled pizza slice at bottom‑right -----
                        int sliceWidth = 500;
                        int sliceHeight = 500;
                        int cornerX = client.Width - sliceWidth - 30;  // 30px from right edge
                        int cornerY = client.Height - sliceHeight - 30; // 30px from bottom

                        // The slice is a triangle (isosceles) representing a pizza slice
                        Point tip = new Point(cornerX + sliceWidth / 2, cornerY + sliceHeight - 20); // tip at bottom center
                        Point leftCorner = new Point(cornerX + 30, cornerY + 60);
                        Point rightCorner = new Point(cornerX + sliceWidth - 30, cornerY + 60);

                        // Create a graphics path for the slice
                        using (var slicePath = new System.Drawing.Drawing2D.GraphicsPath())
                        {
                            slicePath.AddPolygon(new Point[] { tip, leftCorner, rightCorner });

                            // ----- 2a. Crust (thick curved part at the top) -----
                            Point crustStart = new Point(leftCorner.X - 10, leftCorner.Y - 15);
                            Point crustEnd = new Point(rightCorner.X + 10, rightCorner.Y - 15);
                            Point crustControl = new Point(cornerX + sliceWidth / 2, leftCorner.Y - 40);
                            using (var crustPath = new System.Drawing.Drawing2D.GraphicsPath())
                            {
                                crustPath.AddBezier(crustStart, crustControl, crustControl, crustEnd);
                                crustPath.AddLine(crustEnd, rightCorner);
                                crustPath.AddLine(rightCorner, leftCorner);
                                crustPath.AddLine(leftCorner, crustStart);
                                // Fill crust
                                using (var crustBrush = new LinearGradientBrush(
                                    new Rectangle(leftCorner.X, leftCorner.Y - 30, sliceWidth, 50),
                                    Color.FromArgb(200, 210, 150, 70),
                                    Color.FromArgb(200, 160, 90, 30),
                                    LinearGradientMode.Vertical))
                                {
                                    g.FillPath(crustBrush, crustPath);
                                }
                                // Outline crust for definition
                                using (var crustPen = new Pen(Color.FromArgb(180, 120, 60, 20), 2))
                                    g.DrawPath(crustPen, crustPath);
                            }

                            // ----- 2b. Cheese layer (with gradient and melt drips) -----
                            using (var cheeseBrush = new LinearGradientBrush(
                                new Rectangle(leftCorner.X, leftCorner.Y, sliceWidth, sliceHeight),
                                Color.FromArgb(220, 255, 220, 120), // golden cheese
                                Color.FromArgb(220, 240, 180, 60),
                                LinearGradientMode.ForwardDiagonal))
                            {
                                g.FillPolygon(cheeseBrush, new Point[] { tip, leftCorner, rightCorner });
                            }

                            // ----- 2c. Sauce peeking through (random splotches) -----
                            Random rand = new Random();
                            using (var sauceBrush = new SolidBrush(Color.FromArgb(80, 200, 50, 30)))
                            {
                                for (int i = 0; i < 25; i++)
                                {
                                    int x = rand.Next(leftCorner.X, rightCorner.X);
                                    int y = rand.Next(leftCorner.Y, tip.Y);
                                    // Only draw if point is inside the triangle (rough check)
                                    if (IsPointInTriangle(x, y, tip, leftCorner, rightCorner))
                                        g.FillEllipse(sauceBrush, x, y, 8, 8);
                                }
                            }

                            // ----- 2d. Pepperoni circles (3–4 big ones) -----
                            using (var pepperoniBrush = new SolidBrush(Color.FromArgb(220, 160, 40, 30)))
                            using (var pepperoniHighlight = new SolidBrush(Color.FromArgb(100, 255, 150, 100)))
                            {
                                Point[] pepperoniPositions = {
                            new Point(leftCorner.X + 80, leftCorner.Y + 80),
                            new Point(rightCorner.X - 80, leftCorner.Y + 70),
                            new Point(tip.X - 50, tip.Y - 100),
                            new Point(leftCorner.X + 150, leftCorner.Y + 150)
                        };
                                foreach (var pos in pepperoniPositions)
                                {
                                    g.FillEllipse(pepperoniBrush, pos.X - 20, pos.Y - 20, 40, 40);
                                    // highlight
                                    g.FillEllipse(pepperoniHighlight, pos.X - 8, pos.Y - 12, 16, 16);
                                    // small dark spots
                                    using (var dark = new SolidBrush(Color.FromArgb(180, 80, 20, 10)))
                                        g.FillEllipse(dark, pos.X - 4, pos.Y - 6, 8, 8);
                                }
                            }

                            // ----- 2e. Melted cheese drips along the bottom edge -----
                            using (var dripBrush = new SolidBrush(Color.FromArgb(200, 255, 200, 80)))
                            {
                                for (int i = 0; i < 8; i++)
                                {
                                    int dripX = tip.X - 40 + i * 10;
                                    int dripY = tip.Y - 5;
                                    g.FillEllipse(dripBrush, dripX, dripY, 12, 20);
                                }
                            }

                            // ----- 2f. Final slice outline (subtle) -----
                            using (var outlinePen = new Pen(Color.FromArgb(80, 80, 40, 10), 1.5f))
                                g.DrawPolygon(outlinePen, new Point[] { tip, leftCorner, rightCorner });
                        }
                    };
                    client.BackColor = Color.FromArgb(250, 245, 235);
                    break;
                }
            }
        }

        // Helper method to check if a point is inside a triangle
        private bool IsPointInTriangle(int px, int py, Point p1, Point p2, Point p3)
        {
            var area = 0.5f * (-p2.Y * p3.X + p1.Y * (-p2.X + p3.X) + p1.X * (p2.Y - p3.Y) + p2.X * p3.Y);
            var s = 1f / (2 * area) * (p1.Y * p3.X - p1.X * p3.Y + (p3.Y - p1.Y) * px + (p1.X - p3.X) * py);
            var t = 1f / (2 * area) * (p1.X * p2.Y - p1.Y * p2.X + (p1.Y - p2.Y) * px + (p2.X - p1.X) * py);
            return s > 0 && t > 0 && (1 - s - t) > 0;
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
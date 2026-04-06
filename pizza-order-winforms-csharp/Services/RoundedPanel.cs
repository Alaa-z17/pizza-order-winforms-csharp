using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PizzaOrderSystem
{
    public class RoundedPanel : Panel
    {
        private int _borderRadius = 20;
        private Color _borderColor = Color.FromArgb(220, 80, 20);
        private int _borderWidth = 2;
        private string _titleText = "";
        private Font _titleFont = new Font("Segoe UI", 12F, FontStyle.Bold);
        private Color _titleColor = Color.FromArgb(220, 80, 20);

        [Category("Appearance")]
        [DefaultValue(20)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int BorderRadius
        {
            get => _borderRadius;
            set { _borderRadius = value; Invalidate(); }
        }

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "220,80,20")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        [DefaultValue(2)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int BorderWidth
        {
            get => _borderWidth;
            set { _borderWidth = value; Invalidate(); }
        }

        [Category("Appearance")]
        [DefaultValue("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string TitleText
        {
            get => _titleText;
            set { _titleText = value; Invalidate(); }
        }

        [Category("Appearance")]
        // Prevent designer from trying to serialize this complex property
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Font TitleFont
        {
            get => _titleFont;
            set { _titleFont = value; Invalidate(); }
        }

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "220,80,20")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color TitleColor
        {
            get => _titleColor;
            set { _titleColor = value; Invalidate(); }
        }

        // This method tells the designer whether to serialize the TitleFont property
        private bool ShouldSerializeTitleFont()
        {
            // Return false to prevent serialization – use default font
            return false;
        }

        // Reset method for designer
        private void ResetTitleFont()
        {
            TitleFont = new Font("Segoe UI", 12F, FontStyle.Bold);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            var path = GetRoundedRectangle(rect, BorderRadius);
            this.Region = new Region(path);
            using (Pen pen = new Pen(BorderColor, BorderWidth))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(pen, path);
            }

            if (!string.IsNullOrEmpty(TitleText))
            {
                using (SolidBrush brush = new SolidBrush(TitleColor))
                {
                    e.Graphics.DrawString(TitleText, TitleFont, brush, 15, 8);
                }
            }
        }

        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
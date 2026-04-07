using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using PizzaOrderSystem.Models;
using PizzaOrderSystem.Services;

namespace PizzaOrderSystem.Services
{
    public static class InvoicePrinter
    {
        private static Order? _order;
        private static bool _isArabic;
        private static Graphics? _g;
        private static int _y;
        private static int _leftMargin;
        private static int _rightMargin;
        private static int _pageWidth;
        private static Font _titleFont, _headingFont, _regularFont, _smallFont;
        private static int _rowHeight = 22;
        private static int _detailIndent = 15;

        public static void PrintInvoice(Order order, bool isArabic)
        {
            if (order == null) return;
            _order = order;
            _isArabic = isArabic;

            _titleFont = new Font("Segoe UI", 16, FontStyle.Bold);
            _headingFont = new Font("Segoe UI", 11, FontStyle.Bold);
            _regularFont = new Font("Segoe UI", 10);
            _smallFont = new Font("Segoe UI", 8, FontStyle.Italic);

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += PrintPage;
            PrintDialog dialog = new PrintDialog { Document = pd };
            if (dialog.ShowDialog() == DialogResult.OK)
                pd.Print();
        }

        private static void PrintPage(object sender, PrintPageEventArgs e)
        {
            if (e.Graphics == null || _order == null) return;
            _g = e.Graphics;
            _leftMargin = e.MarginBounds.Left;
            _rightMargin = e.MarginBounds.Right;
            _pageWidth = _rightMargin - _leftMargin;
            _y = _leftMargin + 10;

            // --- Restaurant header ---
            DrawCentered(GetText("RestaurantName", "🍕 Pizza Order System"), _titleFont, Brushes.DarkRed);
            _y += 5;
            DrawCentered(GetText("InvoiceTitle", _isArabic ? "فاتورة الطلب" : "Order Invoice"), _headingFont, Brushes.Black);
            _y += 15;
            DrawLine();

            // --- Customer & order info (two columns) ---
            int col1 = _leftMargin;
            int col2 = _leftMargin + (_pageWidth / 2) + 20;

            // Fixed: Use single resource key "OrderNumber" to avoid double "#"
            DrawKeyValue(GetText("OrderNumber", _isArabic ? "رقم الطلب" : "Order #") + ":", _order.OrderId.ToString(), col1);
            // Fixed: Use separate keys for date and time to avoid HTML entity issues
            string dateTimeStr = $"{GetText("DateLabel", "Date")}: {_order.OrderTime:yyyy-MM-dd}   {GetText("TimeLabel", "Time")}: {_order.OrderTime:hh:mm tt}";
            DrawText(dateTimeStr, col2, _regularFont, StringAlignment.Near);
            _y += _rowHeight;

            DrawKeyValue(GetText("CustomerLabel", "Customer") + ":", _order.Customer.Name, col1);
            if (!string.IsNullOrEmpty(_order.Customer.Phone))
                DrawKeyValue(GetText("PhoneLabel", "Phone") + ":", _order.Customer.Phone, col2);
            _y += _rowHeight;
            if (!string.IsNullOrEmpty(_order.Customer.Address))
                DrawKeyValue(GetText("Address", "Address") + ":", _order.Customer.Address, col1);
            _y += 10;
            DrawLine();

            // --- Table headers ---
            int[] cols = CalculateColumns();
            DrawTableHeader(cols);
            _y += _rowHeight + 5;
            DrawLine();

            // --- Items ---
            foreach (var item in _order.Items)
            {
                DrawItemRow(item, cols);
                _y += _rowHeight;

                // Extra details (crust, toppings, size)
                string detail = GetItemDetail(item);
                if (!string.IsNullOrEmpty(detail))
                {
                    using (var detailBrush = new SolidBrush(Color.DimGray))
                    {
                        Rectangle detailRect = new Rectangle(
                            _leftMargin + _detailIndent,
                            _y,
                            _pageWidth - _detailIndent,
                            _rowHeight - 4);
                        _g.DrawString(detail, _smallFont, detailBrush, detailRect);
                    }
                    _y += _rowHeight - 4;
                }
            }

            _y += 10;
            DrawLine();

            // --- Totals ---
            decimal subtotal = _order.Items.Sum(i => i.CalculateTotal());
            DrawKeyValue(GetText("Subtotal", "Subtotal") + ":", FormatCurrency(subtotal), _leftMargin, true);
            _y += _rowHeight;
            DrawKeyValue(GetText("Total", "Total") + ":", FormatCurrency(_order.TotalAmount), _leftMargin, true, true);
            _y += 20;

            // --- Thank you message ---
            DrawCentered(GetText("ThankYouMessage", _isArabic ? "شكراً لطلبك!" : "Thank you for your order!"),
                         _regularFont, Brushes.DarkGreen);

            e.HasMorePages = false;
        }

        private static int[] CalculateColumns()
        {
            // For LTR: Item, Qty, Price, Total
            // For RTL: Total, Price, Qty, Item (mirrored)
            int itemWidth = (int)(_pageWidth * 0.5);
            int qtyWidth = (int)(_pageWidth * 0.12);
            int priceWidth = (int)(_pageWidth * 0.18);
            int totalWidth = (int)(_pageWidth * 0.2);

            if (!_isArabic)
                return new[] { _leftMargin, _leftMargin + itemWidth, _leftMargin + itemWidth + qtyWidth, _leftMargin + itemWidth + qtyWidth + priceWidth };
            else
                return new[] { _rightMargin - totalWidth, _rightMargin - totalWidth - priceWidth, _rightMargin - totalWidth - priceWidth - qtyWidth, _rightMargin - totalWidth - priceWidth - qtyWidth - itemWidth };
        }

        private static void DrawTableHeader(int[] cols)
        {
            string[] headers = _isArabic
                ? new[] { GetText("TotalHeader", "الإجمالي"), GetText("PriceHeader", "السعر"), GetText("QtyHeader", "العدد"), GetText("ItemHeader", "الصنف") }
                : new[] { GetText("ItemHeader", "Item"), GetText("QtyHeader", "Qty"), GetText("PriceHeader", "Price"), GetText("TotalHeader", "Total") };

            for (int i = 0; i < headers.Length; i++)
            {
                StringAlignment align = StringAlignment.Near;
                if (i == 1 || i == 2 || i == 3) align = StringAlignment.Far; // numbers right-aligned
                if (_isArabic && i == 0) align = StringAlignment.Far;
                DrawText(headers[i], cols[i], _headingFont, align);
            }
        }

        private static void DrawItemRow(BaseItem item, int[] cols)
        {
            string name = GetItemDisplayName(item);
            string qty = item.Quantity.ToString();
            string price = FormatCurrency(item.UnitPrice);
            string total = FormatCurrency(item.CalculateTotal());

            // Add a space before price/total for better readability
            if (!_isArabic)
            {
                DrawText(name, cols[0], _regularFont, StringAlignment.Near);
                DrawText(qty, cols[1], _regularFont, StringAlignment.Center);
                DrawText(price, cols[2], _regularFont, StringAlignment.Far);
                DrawText(total, cols[3], _regularFont, StringAlignment.Far);
            }
            else
            {
                DrawText(total, cols[0], _regularFont, StringAlignment.Far);
                DrawText(price, cols[1], _regularFont, StringAlignment.Far);
                DrawText(qty, cols[2], _regularFont, StringAlignment.Center);
                DrawText(name, cols[3], _regularFont, StringAlignment.Near);
            }
        }

        private static string GetItemDetail(BaseItem item)
        {
            if (item is Pizza p)
            {
                string crust = GetText(p.Crust.ToString());
                string toppings = p.Toppings.Any()
                    ? string.Join(", ", p.Toppings.Select(t => GetText(t.ToString())))
                    : GetText("None", "None");
                return $"{GetText("CrustWord", "Crust")}: {crust} | {GetText("ToppingsWord", "Toppings")}: {toppings}";
            }
            if (item is Drink d)
                return $"{GetText("Size", "Size")}: {GetText(d.Size.ToString() + "Size", d.Size.ToString())}";
            return "";
        }

        private static void DrawKeyValue(string key, string value, int x, bool indent = false, bool bold = false)
        {
            if (_g == null) return;
            Font font = bold ? _headingFont : _regularFont;
            int startX = indent ? x + 20 : x;
            SizeF keySize = _g.MeasureString(key + " ", font);
            _g.DrawString(key + " ", font, Brushes.Black, startX, _y);
            _g.DrawString(value, font, Brushes.Black, startX + keySize.Width, _y);
        }

        private static void DrawText(string text, int x, Font font, StringAlignment alignment)
        {
            if (_g == null) return;
            StringFormat sf = new StringFormat { Alignment = alignment, LineAlignment = StringAlignment.Center };
            Rectangle rect = new Rectangle(x, _y, 200, _rowHeight);
            _g.DrawString(text, font, Brushes.Black, rect, sf);
        }

        private static void DrawCentered(string text, Font font, Brush brush)
        {
            if (_g == null) return;
            SizeF size = _g.MeasureString(text, font);
            float x = (_leftMargin + _rightMargin) / 2 - size.Width / 2;
            _g.DrawString(text, font, brush, x, _y);
            _y += (int)size.Height;
        }

        private static void DrawLine()
        {
            if (_g == null) return;
            _g.DrawLine(Pens.Gray, _leftMargin, _y, _rightMargin, _y);
            _y += 8;
        }

        private static string GetText(string key, string fallback = "")
        {
            string val = LanguageManager.GetString(key);
            return string.IsNullOrEmpty(val) || val == key ? (string.IsNullOrEmpty(fallback) ? key : fallback) : val;
        }

        private static string FormatCurrency(decimal amount)
        {
            return amount.ToString("C2", System.Globalization.CultureInfo.CurrentCulture);
        }

        private static string GetItemDisplayName(BaseItem item)
        {
            if (item is Pizza p)
                return $"{GetText(p.Size.ToString())} {GetText("Pizza", "Pizza")}";
            if (item is Drink d)
                return d.Name;
            if (item is Side s)
                return GetText(s.SideType.ToString());
            return item.Name;
        }
    }
}
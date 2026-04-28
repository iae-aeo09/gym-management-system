using System.Drawing;
using System.Windows.Forms;

namespace GymManagementSystem
{
    internal static class ViltrumTheme
    {
        internal static readonly Color AppBackground = Color.FromArgb(16, 20, 27);
        internal static readonly Color Surface = Color.FromArgb(27, 34, 45);
        internal static readonly Color SurfaceAlt = Color.FromArgb(35, 43, 56);
        internal static readonly Color Input = Color.FromArgb(22, 28, 38);
        internal static readonly Color TextPrimary = Color.FromArgb(236, 240, 246);
        internal static readonly Color TextMuted = Color.FromArgb(167, 177, 193);
        internal static readonly Color Accent = Color.FromArgb(56, 116, 203);
        internal static readonly Color AccentHover = Color.FromArgb(73, 136, 226);
        internal static readonly Color Danger = Color.FromArgb(178, 63, 73);
        internal static readonly Color DangerHover = Color.FromArgb(199, 80, 90);
        internal static readonly Color Border = Color.FromArgb(82, 95, 114);
        internal static readonly Color Shadow = Color.FromArgb(12, 0, 0, 0);
        internal static readonly Color Highlight = Color.FromArgb(22, 255, 255, 255);

        internal static void Apply(Form form)
        {
            form.BackColor = AppBackground;
            form.ForeColor = TextPrimary;
            StyleControlTree(form.Controls);
        }

        internal static void WireCard(Panel panel, bool accentTop = true)
        {
            if (panel == null) return;
            panel.Padding = panel.Padding == Padding.Empty ? new Padding(18) : panel.Padding;
            panel.Paint -= PanelCardPaint;
            panel.Paint += PanelCardPaint;
            panel.Tag = accentTop ? "accent" : string.Empty;
        }

        private static void PanelCardPaint(object sender, PaintEventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null) return;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
            using (var shadowPen = new Pen(Shadow, 1))
            using (var borderPen = new Pen(Border, 1))
            using (var highlightPen = new Pen(Highlight, 1))
            {
                // soft inner highlight + border
                e.Graphics.DrawRectangle(highlightPen, new Rectangle(1, 1, rect.Width - 2, rect.Height - 2));
                e.Graphics.DrawRectangle(borderPen, rect);
                e.Graphics.DrawRectangle(shadowPen, new Rectangle(2, 2, rect.Width - 4, rect.Height - 4));
            }

            if ((panel.Tag as string) == "accent")
            {
                using (var accentBrush = new SolidBrush(Color.FromArgb(90, Accent)))
                {
                    e.Graphics.FillRectangle(accentBrush, new Rectangle(0, 0, panel.Width, 3));
                }
            }
        }

        private static void StyleControlTree(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is Panel panel)
                {
                    string panelName = panel.Name?.ToLowerInvariant() ?? string.Empty;
                    if (panelName.Contains("top") || panelName.Contains("header"))
                        panel.BackColor = SurfaceAlt;
                    else if (panelName.Contains("nav"))
                        panel.BackColor = Input;
                    else
                        panel.BackColor = Surface;
                }
                else if (control is Label label)
                {
                    label.ForeColor = label.ForeColor == Color.Red ? Color.FromArgb(248, 116, 124) : TextPrimary;
                }
                else if (control is Button button)
                {
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 1;
                    button.FlatAppearance.BorderColor = Border;
                    button.FlatAppearance.MouseOverBackColor = Accent;
                    button.FlatAppearance.MouseDownBackColor = AccentHover;
                    button.BackColor = SurfaceAlt;
                    button.ForeColor = TextPrimary;
                    button.Cursor = Cursors.Hand;
                }
                else if (control is CheckBox checkBox)
                {
                    checkBox.ForeColor = TextMuted;
                }
                else if (control is TextBox textBox)
                {
                    textBox.BackColor = Input;
                    textBox.ForeColor = TextPrimary;
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (control is DateTimePicker dateTime)
                {
                    dateTime.CalendarForeColor = TextPrimary;
                    dateTime.CalendarMonthBackground = Input;
                    dateTime.CalendarTitleBackColor = SurfaceAlt;
                    dateTime.CalendarTitleForeColor = TextPrimary;
                }
                else if (control is ComboBox combo)
                {
                    combo.BackColor = Input;
                    combo.ForeColor = TextPrimary;
                    combo.FlatStyle = FlatStyle.Flat;
                }
                else if (control is DataGridView grid)
                {
                    grid.BackgroundColor = Surface;
                    grid.BorderStyle = BorderStyle.None;
                    grid.EnableHeadersVisualStyles = false;
                    grid.ColumnHeadersDefaultCellStyle.BackColor = SurfaceAlt;
                    grid.ColumnHeadersDefaultCellStyle.ForeColor = TextPrimary;
                    grid.DefaultCellStyle.BackColor = Surface;
                    grid.DefaultCellStyle.ForeColor = TextPrimary;
                    grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(48, 84, 132);
                    grid.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                    grid.GridColor = Border;
                    grid.RowHeadersVisible = false;
                    grid.RowTemplate.Height = 30;
                    grid.ColumnHeadersHeight = 34;
                }

                if (control.HasChildren)
                {
                    StyleControlTree(control.Controls);
                }
            }
        }
    }
}

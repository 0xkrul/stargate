namespace appliPandora.Classes
{
    internal static class UiTheme
    {
        public static readonly Color Background = Color.FromArgb(11, 18, 32);
        public static readonly Color Surface = Color.FromArgb(20, 31, 50);
        public static readonly Color SurfaceAlt = Color.FromArgb(28, 42, 65);
        public static readonly Color Border = Color.FromArgb(59, 78, 107);
        public static readonly Color Text = Color.FromArgb(232, 238, 247);
        public static readonly Color MutedText = Color.FromArgb(165, 178, 198);
        public static readonly Color Accent = Color.FromArgb(82, 183, 255);
        public static readonly Color AccentStrong = Color.FromArgb(39, 129, 214);
        public static readonly Color Success = Color.FromArgb(94, 234, 154);
        public static readonly Color Danger = Color.FromArgb(248, 113, 113);

        public static void Apply(Form form)
        {
            form.BackColor = Background;
            form.ForeColor = Text;
            form.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            if (form.FormBorderStyle != FormBorderStyle.FixedDialog)
                form.MinimumSize = new Size(Math.Max(form.MinimumSize.Width, 760), Math.Max(form.MinimumSize.Height, 500));

            ApplyToChildren(form.Controls);
        }

        public static void ApplyToChildren(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                Apply(control);
                if (control.HasChildren)
                    ApplyToChildren(control.Controls);
            }
        }

        private static void Apply(Control control)
        {
            switch (control)
            {
                case Button button:
                    StyleButton(button);
                    break;
                case DataGridView grid:
                    StyleGrid(grid);
                    break;
                case GroupBox groupBox:
                    groupBox.ForeColor = Text;
                    groupBox.BackColor = Background;
                    groupBox.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Regular);
                    break;
                case TabControl tabControl:
                    tabControl.BackColor = Background;
                    tabControl.ForeColor = Text;
                    break;
                case TabPage tabPage:
                    tabPage.BackColor = Background;
                    tabPage.ForeColor = Text;
                    break;
                case Label label:
                    label.ForeColor = Text;
                    label.BackColor = Color.Transparent;
                    break;
                case TextBox textBox:
                    textBox.BackColor = SurfaceAlt;
                    textBox.ForeColor = Text;
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    break;
                case ComboBox comboBox:
                    comboBox.BackColor = SurfaceAlt;
                    comboBox.ForeColor = Text;
                    comboBox.FlatStyle = FlatStyle.Flat;
                    break;
                case ListBox listBox:
                    listBox.BackColor = SurfaceAlt;
                    listBox.ForeColor = Text;
                    listBox.BorderStyle = BorderStyle.FixedSingle;
                    break;
                case NumericUpDown numeric:
                    numeric.BackColor = SurfaceAlt;
                    numeric.ForeColor = Text;
                    break;
                case DateTimePicker picker:
                    picker.CalendarMonthBackground = SurfaceAlt;
                    picker.CalendarForeColor = Text;
                    picker.CalendarTitleBackColor = Surface;
                    picker.CalendarTitleForeColor = Text;
                    break;
                default:
                    control.BackColor = control is Form ? Background : control.BackColor;
                    control.ForeColor = Text;
                    break;
            }
        }

        private static void StyleButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = AccentStrong;
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Regular);
            button.Cursor = Cursors.Hand;
            button.Padding = new Padding(8, 0, 8, 0);
        }

        private static void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = Surface;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = Border;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.ColumnHeadersDefaultCellStyle.BackColor = SurfaceAlt;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Text;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular);
            grid.DefaultCellStyle.BackColor = Surface;
            grid.DefaultCellStyle.ForeColor = Text;
            grid.DefaultCellStyle.SelectionBackColor = AccentStrong;
            grid.DefaultCellStyle.SelectionForeColor = Color.White;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(16, 26, 43);
            grid.RowHeadersVisible = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.RowTemplate.Height = 30;
        }
    }
}

using System;
using System.ComponentModel; // Required for designer attributes
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ATMS
{
    public partial class BaseDialogForm : Form
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        // Fix CS8618: Satisfy the nullable reference strictness
        private Panel _pnlTitleBar = null!;
        private Label _lblTitle = null!;
        private Button _btnClose = null!;

        // Fix WFO1000: Hide property from the WinForms visual serializer
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DialogTitle
        {
            get => _lblTitle.Text;
            set => _lblTitle.Text = value;
        }

        public BaseDialogForm()
        {
            InitializeComponent();
            SetupCustomWindow();
        }

        // ... (Keep the rest of your existing SetupCustomWindow 
        // and Native Window methods exactly the same) ...

        private void SetupCustomWindow()
        {
            // Remove standard Windows borders
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // Draw a crisp 1px border around the entire form
            this.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                    ColorTranslator.FromHtml("#1E2A38"), ButtonBorderStyle.Solid);
            };

            // 1. Build Custom Title Bar
            _pnlTitleBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = ColorTranslator.FromHtml("#1E2A38")
            };
            _pnlTitleBar.MouseDown += TitleBar_MouseDown; // Enable dragging

            // 2. Build Title Label
            _lblTitle = new Label
            {
                Text = "ATMS Dialog",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(15, 10),
                AutoSize = true
            };
            _lblTitle.MouseDown += TitleBar_MouseDown;

            // 3. Build Custom Close Button
            _btnClose = new Button
            {
                Text = "✕",
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(40, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Cursor = Cursors.Hand
            };
            // Position it perfectly in the top right corner
            _btnClose.Location = new Point(this.Width - _btnClose.Width, 0);
            _btnClose.FlatAppearance.BorderSize = 0;
            _btnClose.FlatAppearance.MouseOverBackColor = Color.Crimson; // Red hover effect

            // Default close behavior (inherited forms can override if needed)
            _btnClose.Click += (s, e) => this.Close();

            // Assemble the header
            _pnlTitleBar.Controls.Add(_lblTitle);
            _pnlTitleBar.Controls.Add(_btnClose);
            this.Controls.Add(_pnlTitleBar);
        }

        // Trigger native window drag
        private void TitleBar_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0); // WM_NCLBUTTONDOWN, HT_CAPTION
            }
        }

        // Hook into OS rendering to add the native Windows drop-shadow
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= 0x00020000; // CS_DROPSHADOW
                return cp;
            }
        }

        // Ensure the close button stays anchored when the form resizes
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_btnClose != null)
            {
                _btnClose.Location = new Point(this.Width - _btnClose.Width, 0);
            }
        }
    }
}
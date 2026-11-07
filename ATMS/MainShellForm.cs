using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ATMS
{
    public class MainShellForm : Form
    {
        private readonly string _dbConnection;
        private Panel _pnlSidebar;
        private Panel _pnlHeader;
        private Panel _pnlContent;
        private Label _lblHeaderTitle;
        private Button _currentActiveButton;

        public MainShellForm()
        {
            InitializeUI();
            btnDashboard_Click(null, EventArgs.Empty);
        }

        private void InitializeUI()
        {
            this.Text = "ATMS - Annai Theresa Matriculation School";
            this.Size = new Size(1200, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1024, 600);

            // 1. Setup Sidebar Panel
            _pnlSidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 220,
                BackColor = ColorTranslator.FromHtml("#1E2A38")
            };

            // Brand / Logo Area in Sidebar
            var pnlBrand = new Panel { Dock = DockStyle.Top, Height = 70 };
            var lblBrand = new Label
            {
                Text = "ATMS Admin",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlBrand.Controls.Add(lblBrand);

            // Create Navigation Buttons
            // Adding with BringToFront() ensures they stack top-to-bottom in order
            _pnlSidebar.Controls.Add(pnlBrand);
            pnlBrand.BringToFront();

            AddNavButton("Dashboard", btnDashboard_Click);
            AddNavButton("Students", btnStudents_Click);
            AddNavButton("Fee Collection", btnFees_Click);
            AddNavButton("Reports", null); // Placeholder for future
            AddNavButton("Settings", null); // Placeholder for future

            // 2. Setup Header Panel
            _pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White
            };

            _lblHeaderTitle = new Label
            {
                Text = "Dashboard",
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = ColorTranslator.FromHtml("#333333"),
                AutoSize = true,
                Location = new Point(20, 15)
            };
            _pnlHeader.Controls.Add(_lblHeaderTitle);

            // 3. Setup Content Panel (View Swapper)
            _pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ColorTranslator.FromHtml("#F3F4F7"),
                Padding = new Padding(20)
            };

            // Add panels to form (order matters for Docking: Content last)
            this.Controls.Add(_pnlContent);
            this.Controls.Add(_pnlHeader);
            this.Controls.Add(_pnlSidebar);
        }

        private void AddNavButton(string text, EventHandler onClick)
        {
            var btn = new Button
            {
                Text = "   " + text, // Padding for visual indent
                Dock = DockStyle.Top,
                Height = 50,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = ColorTranslator.FromHtml("#1E2A38"),
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#2C3E50");
            btn.FlatAppearance.MouseDownBackColor = ColorTranslator.FromHtml("#151D27");

            if (onClick != null) btn.Click += onClick;

            // Highlight logic
            btn.Click += (s, e) =>
            {
                if (_currentActiveButton != null)
                {
                    _currentActiveButton.BackColor = ColorTranslator.FromHtml("#1E2A38");
                    _currentActiveButton.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                }

                _currentActiveButton = btn;
                _currentActiveButton.BackColor = ColorTranslator.FromHtml("#2C3E50");
                _currentActiveButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                _lblHeaderTitle.Text = text;
            };

            _pnlSidebar.Controls.Add(btn);
            btn.BringToFront();
        }

        // --- View Swapping Engine ---
        private void LoadView(UserControl control)
        {
            _pnlContent.Controls.Clear();
            control.Dock = DockStyle.Fill;
            _pnlContent.Controls.Add(control);
        }

        // --- Navigation Events ---
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            // Injecting a simple temporary placeholder for the dashboard
            var dashboard = new UserControl { BackColor = Color.Transparent };
            dashboard.Controls.Add(new Label
            {
                Text = "Welcome to the ATMS System",
                Font = new Font("Segoe UI", 16),
                AutoSize = true
            });
            LoadView(dashboard);
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            // Ask the DI container for a fresh instance with all its dependencies injected
            var studentControl = Program.ServiceProvider
                .GetRequiredService<StudentManagementControl>();
            LoadView(studentControl);
        }

        private void btnFees_Click(object sender, EventArgs e)
        {
            var feeControl = Program.ServiceProvider
                .GetRequiredService<FeeCollectionControl>();
            LoadView(feeControl);
        }
    }
}
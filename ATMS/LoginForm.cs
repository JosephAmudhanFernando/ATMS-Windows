using ATMS.Data.Repositories;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMS
{
    public partial class LoginForm : BaseDialogForm
    {
        private readonly AuthService _authService;
        public bool IsAuthenticated { get; private set; } = false;

        // Accept AuthService via Constructor Injection
        public LoginForm(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
            this.DialogTitle = "ATMS - Secure Login";
            this.ActiveControl = txtUsername;
            this.AcceptButton = btnLogin;
            btnLogin.Click += async (s, e) => await LoginAsync();
        }

        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter both username and password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "Authenticating...";

            try
            {
                // Execute real BCrypt database validation
                bool success = await _authService.ValidateUserAsync(txtUsername.Text.Trim(), txtPassword.Text);

                if (success)
                {
                    IsAuthenticated = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid credentials.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                }
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Login";
            }
        }
    }
}
using ATMS.Core;
using Dapper;
using Npgsql;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;

namespace ATMS
{
    public partial class UserManagementControl : UserControl
    {
        private readonly string _connectionString;

        public UserManagementControl(IConfiguration config)
        {
            InitializeComponent();
            _connectionString = config.GetConnectionString("AtmsDb")!;

            this.Load += async (s, e) => await LoadDataAsync();
            btnAddUser.Click += async (s, e) => await CreateUserAsync();
        }

        private async Task LoadDataAsync()
        {
            using var conn = new NpgsqlConnection(_connectionString);

            // Load Roles into the Dropdown
            var roles = await conn.QueryAsync("SELECT id, role_name FROM roles WHERE role_name != 'ATMS';");
            cboRoles.DataSource = roles;
            cboRoles.DisplayMember = "role_name";
            cboRoles.ValueMember = "id";

            // Select the columns to perfectly match the C# property names
            const string userSql = @"
                SELECT u.id AS ID, 
                    u.username AS Username, 
                    r.role_name AS Role, 
                    u.is_active AS Active 
                FROM users u 
                JOIN roles r ON u.role_id = r.id 
                ORDER BY u.id;";

            // Ask Dapper for the strongly-typed class instead of 'dynamic'
            var users = await conn.QueryAsync<UserDisplayModel>(userSql);

            // .ToList() ensures WinForms binds it cleanly
            gridUsers.DataSource = users.ToList();
        }

        private async Task CreateUserAsync()
        {
            if (string.IsNullOrWhiteSpace(txtNewUsername.Text) || string.IsNullOrWhiteSpace(txtNewPassword.Text) || cboRoles.SelectedValue == null)
            {
                MessageBox.Show("Please fill all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnAddUser.Enabled = false;
                string hash = BCrypt.Net.BCrypt.HashPassword(txtNewPassword.Text);
                int roleId = Convert.ToInt32(cboRoles.SelectedValue);

                using var conn = new NpgsqlConnection(_connectionString);
                const string sql = "INSERT INTO users (username, password_hash, role_id) VALUES (@User, @Hash, @Role)";

                await conn.ExecuteAsync(sql, new { User = txtNewUsername.Text.Trim(), Hash = hash, Role = roleId });

                MessageBox.Show("User created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNewUsername.Clear(); txtNewPassword.Clear();
                await LoadDataAsync(); // Refresh Grid
            }
            catch (PostgresException ex) when (ex.SqlState == "23505") // Unique constraint violation
            {
                MessageBox.Show("Username already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnAddUser.Enabled = true;
            }
        }
    }

    public class UserDisplayModel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }
    }
}
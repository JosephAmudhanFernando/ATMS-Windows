namespace ATMS
{
    partial class UserManagementControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            label2 = new Label();
            txtNewUsername = new TextBox();
            label1 = new Label();
            txtNewPassword = new TextBox();
            cboRoles = new ComboBox();
            label3 = new Label();
            btnAddUser = new Button();
            gridUsers = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridUsers).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnAddUser);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(cboRoles);
            panel1.Controls.Add(txtNewPassword);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtNewUsername);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1000, 80);
            panel1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(278, 41);
            label2.Name = "label2";
            label2.Size = new Size(67, 17);
            label2.TabIndex = 2;
            label2.Text = "Password:";
            // 
            // txtNewUsername
            // 
            txtNewUsername.Location = new Point(120, 33);
            txtNewUsername.Name = "txtNewUsername";
            txtNewUsername.Size = new Size(150, 25);
            txtNewUsername.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 41);
            label1.Name = "label1";
            label1.Size = new Size(70, 17);
            label1.TabIndex = 0;
            label1.Text = "Username:";
            // 
            // txtNewPassword
            // 
            txtNewPassword.Location = new Point(351, 33);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.PasswordChar = '*';
            txtNewPassword.Size = new Size(150, 25);
            txtNewPassword.TabIndex = 3;
            // 
            // cboRoles
            // 
            cboRoles.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRoles.FormattingEnabled = true;
            cboRoles.Location = new Point(550, 33);
            cboRoles.Name = "cboRoles";
            cboRoles.Size = new Size(150, 25);
            cboRoles.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(507, 36);
            label3.Name = "label3";
            label3.Size = new Size(37, 17);
            label3.TabIndex = 1;
            label3.Text = "Role:";
            // 
            // btnAddUser
            // 
            btnAddUser.BackColor = Color.LightGreen;
            btnAddUser.Location = new Point(706, 33);
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Size = new Size(75, 23);
            btnAddUser.TabIndex = 5;
            btnAddUser.Text = "Add User";
            btnAddUser.UseVisualStyleBackColor = false;
            // 
            // gridUsers
            // 
            gridUsers.AllowUserToAddRows = false;
            gridUsers.AllowUserToDeleteRows = false;
            gridUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridUsers.Dock = DockStyle.Fill;
            gridUsers.Location = new Point(0, 80);
            gridUsers.Name = "gridUsers";
            gridUsers.Size = new Size(1000, 570);
            gridUsers.TabIndex = 1;
            // 
            // UserManagementControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(gridUsers);
            Controls.Add(panel1);
            Name = "UserManagementControl";
            Size = new Size(1000, 650);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridUsers).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label3;
        private ComboBox cboRoles;
        private TextBox txtNewPassword;
        private Label label2;
        private TextBox txtNewUsername;
        private Label label1;
        private Button btnAddUser;
        private DataGridView gridUsers;
    }
}

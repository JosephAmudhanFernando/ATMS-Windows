namespace ATMS
{
    partial class MainShellForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlSidebar = new Panel();
            btnDashboard = new Button();
            btnFees = new Button();
            btnStudents = new Button();
            pnlHeader = new Panel();
            pnlContent = new Panel();
            pnlSidebar.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(30, 42, 56);
            pnlSidebar.Controls.Add(btnDashboard);
            pnlSidebar.Controls.Add(btnFees);
            pnlSidebar.Controls.Add(btnStudents);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(220, 450);
            pnlSidebar.TabIndex = 0;
            // 
            // btnDashboard
            // 
            btnDashboard.Dock = DockStyle.Top;
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.ForeColor = Color.White;
            btnDashboard.Location = new Point(0, 100);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(220, 50);
            btnDashboard.TabIndex = 2;
            btnDashboard.Text = "Dashboard";
            btnDashboard.UseVisualStyleBackColor = true;
            // 
            // btnFees
            // 
            btnFees.Dock = DockStyle.Top;
            btnFees.FlatAppearance.BorderSize = 0;
            btnFees.FlatStyle = FlatStyle.Flat;
            btnFees.ForeColor = Color.White;
            btnFees.Location = new Point(0, 50);
            btnFees.Name = "btnFees";
            btnFees.Size = new Size(220, 50);
            btnFees.TabIndex = 1;
            btnFees.Text = "Fees";
            btnFees.UseVisualStyleBackColor = true;
            btnFees.Click += btnFees_Click;
            // 
            // btnStudents
            // 
            btnStudents.Dock = DockStyle.Top;
            btnStudents.FlatAppearance.BorderSize = 0;
            btnStudents.FlatStyle = FlatStyle.Flat;
            btnStudents.ForeColor = Color.White;
            btnStudents.Location = new Point(0, 0);
            btnStudents.Name = "btnStudents";
            btnStudents.Size = new Size(220, 50);
            btnStudents.TabIndex = 0;
            btnStudents.Text = "Students";
            btnStudents.UseVisualStyleBackColor = true;
            btnStudents.Click += btnStudents_Click;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(220, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(580, 60);
            pnlHeader.TabIndex = 0;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.WhiteSmoke;
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(220, 60);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(580, 390);
            pnlContent.TabIndex = 0;
            // 
            // MainShellForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlContent);
            Controls.Add(pnlHeader);
            Controls.Add(pnlSidebar);
            Name = "MainShellForm";
            Text = "MainShellForm";
            pnlSidebar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSidebar;
        private Button btnStudents;
        private Panel pnlHeader;
        private Panel pnlContent;
        private Button btnDashboard;
        private Button btnFees;
    }
}
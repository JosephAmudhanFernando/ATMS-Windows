using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace ATMS
{
    public partial class MainShellForm : Form
    {
        public MainShellForm()
        {
            InitializeComponent(); // Visual Studio handles the UI here
        }

        // The View-Swapping Engine
        private void LoadView(UserControl control)
        {
            pnlContent.Controls.Clear();
            control.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(control);
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            // Ask DI container for the control
            var studentControl = Program.ServiceProvider.GetRequiredService<StudentManagementControl>();
            LoadView(studentControl);
        }

        private void btnFees_Click(object sender, EventArgs e)
        {
            var feeControl = Program.ServiceProvider.GetRequiredService<FeeCollectionControl>();
            LoadView(feeControl);
        }
    }
}
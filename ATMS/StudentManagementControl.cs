using ATMS.Core.Models;
using ATMS.Data.Repositories;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMS
{
    public partial class StudentManagementControl : UserControl
    {
        private readonly StudentRepository _repository;

        // The DI Container injects the repository here automatically
        public StudentManagementControl(StudentRepository repository)
        {
            InitializeComponent();
            _repository = repository;
            this.Load += async (s, e) => await LoadStudentsAsync();

            // Wire up the button you dragged onto the designer
            btnSave.Click += async (s, e) => await SaveStudentAsync();
        }

        private async Task LoadStudentsAsync()
        {
            var students = await _repository.GetAllActiveAsync();
            gridStudents.DataSource = students;
        }

        private async Task SaveStudentAsync()
        {
            if (string.IsNullOrWhiteSpace(txtAdmNo.Text) || string.IsNullOrWhiteSpace(txtFirst.Text))
            {
                MessageBox.Show("Admission Number and First Name are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal.TryParse(txtFatherIncome.Text, out decimal fIncome);
            decimal.TryParse(txtMotherIncome.Text, out decimal mIncome);

            var student = new Student
            {
                AdmissionNo = txtAdmNo.Text.Trim(),
                FirstName = txtFirst.Text.Trim(),
                LastName = txtLast.Text.Trim(),
                EmisNo = txtEmis.Text.Trim(),
                Standard = cboStd.SelectedItem?.ToString() ?? "",
                Section = txtSec.Text.Trim().ToUpper(),
                Gender = cboGender.SelectedItem?.ToString(),
                Dob = dtpDob.Value,
                AadhaarNo = txtAadhaar.Text.Trim(),
                Religion = txtReligion.Text.Trim(),
                Community = txtCommunity.Text.Trim(),
                MotherTongue = txtMotherTongue.Text.Trim(),
                FatherName = txtFatherName.Text.Trim(),
                FatherContactNo = txtFatherPhone.Text.Trim(),
                FatherIncome = fIncome,
                MotherName = txtMotherName.Text.Trim(),
                MotherContactNo = txtMotherPhone.Text.Trim(),
                MotherIncome = mIncome,
                Address = txtAddress.Text.Trim(),
                Certificates = txtCertificates.Text.Trim(),
                IsActive = true
            };

            try
            {
                btnSave.Enabled = false;
                await _repository.InsertAsync(student);
                await LoadStudentsAsync();
                txtAdmNo.Clear(); txtFirst.Clear(); txtLast.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }
    }
}
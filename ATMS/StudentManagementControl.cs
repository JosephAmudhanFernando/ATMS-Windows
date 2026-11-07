using ATMS.Core.Models;
using ATMS.Data.Repositories;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMS
{
    public class StudentManagementControl : UserControl
    {
        private readonly StudentRepository _repository;
        private DataGridView _gridStudents;
        private Button _btnSave;

        // Input Controls
        private TextBox _txtAdmNo, _txtFirst, _txtLast, _txtSec, _txtEmis, _txtAadhaar;
        private TextBox _txtReligion, _txtCommunity, _txtMotherTongue;
        private TextBox _txtFatherName, _txtFatherPhone, _txtFatherIncome;
        private TextBox _txtMotherName, _txtMotherPhone, _txtMotherIncome;
        private TextBox _txtAddress, _txtCertificates;
        private ComboBox _cboStd, _cboGender;
        private DateTimePicker _dtpDob;

        public StudentManagementControl(StudentRepository studentRepo)
        {
            _repository = studentRepo;
            InitializeUI();
            this.Load += async (s, e) => await LoadStudentsAsync();
        }

        private void InitializeUI()
        {
            this.BackColor = Color.Transparent;

            // Expanded Input Panel
            var pnlInput = new Panel { Dock = DockStyle.Top, Height = 280, Padding = new Padding(10) };

            // Helper for clean layout code
            void AddField(string label, Control c, int x, int y, int w)
            {
                pnlInput.Controls.Add(new Label { Text = label, Location = new Point(x, y + 2), AutoSize = true });
                c.Location = new Point(x + 90, y);
                c.Width = w;
                pnlInput.Controls.Add(c);
            }

            // Row 1: Core Details (Y=15)
            _txtAdmNo = new TextBox(); AddField("Adm No:", _txtAdmNo, 15, 15, 100);
            _txtFirst = new TextBox(); AddField("First Name:", _txtFirst, 230, 15, 130);
            _txtLast = new TextBox(); AddField("Last Name:", _txtLast, 470, 15, 130);
            _txtEmis = new TextBox(); AddField("EMIS No:", _txtEmis, 710, 15, 120);

            // Row 2: Demographics (Y=55)
            _cboStd = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            _cboStd.Items.AddRange(new[] { "LKG", "UKG", "1st", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th", "10th" });
            AddField("Standard:", _cboStd, 15, 55, 60);

            _txtSec = new TextBox(); AddField("Section:", _txtSec, 230, 55, 50);

            _cboGender = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            _cboGender.Items.AddRange(new[] { "Male", "Female" });
            AddField("Gender:", _cboGender, 470, 55, 90);

            _dtpDob = new DateTimePicker { Format = DateTimePickerFormat.Short };
            AddField("DOB:", _dtpDob, 710, 55, 120);

            // Row 3: Identity & Background (Y=95)
            _txtAadhaar = new TextBox(); AddField("Aadhaar No:", _txtAadhaar, 15, 95, 120);
            _txtReligion = new TextBox(); AddField("Religion:", _txtReligion, 230, 95, 130);
            _txtCommunity = new TextBox(); AddField("Community:", _txtCommunity, 470, 95, 130);
            _txtMotherTongue = new TextBox(); AddField("Tongue:", _txtMotherTongue, 710, 95, 120);

            // Row 4: Father Details (Y=135)
            _txtFatherName = new TextBox(); AddField("Father:", _txtFatherName, 15, 135, 140);
            _txtFatherPhone = new TextBox(); AddField("F. Phone:", _txtFatherPhone, 270, 135, 120);
            _txtFatherIncome = new TextBox(); AddField("F. Income:", _txtFatherIncome, 500, 135, 100);

            // Row 5: Mother Details (Y=175)
            _txtMotherName = new TextBox(); AddField("Mother:", _txtMotherName, 15, 175, 140);
            _txtMotherPhone = new TextBox(); AddField("M. Phone:", _txtMotherPhone, 270, 175, 120);
            _txtMotherIncome = new TextBox(); AddField("M. Income:", _txtMotherIncome, 500, 175, 100);

            // Row 6: Address & Save (Y=215)
            _txtAddress = new TextBox(); AddField("Address:", _txtAddress, 15, 215, 250);
            _txtCertificates = new TextBox(); AddField("Certificates:", _txtCertificates, 380, 215, 220);

            _btnSave = new Button
            {
                Text = "Save Student",
                Location = new Point(710, 212),
                Width = 120,
                Height = 30,
                BackColor = ColorTranslator.FromHtml("#4CAF50"),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            _btnSave.FlatAppearance.BorderSize = 0;
            _btnSave.Click += async (s, e) => await SaveStudentAsync();
            pnlInput.Controls.Add(_btnSave);

            // Data Grid Configuration
            _gridStudents = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                RowHeadersVisible = false,
                AllowUserToResizeRows = false,
                EnableHeadersVisualStyles = false
            };

            _gridStudents.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#E0E4E8");
            _gridStudents.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            _gridStudents.ColumnHeadersHeight = 40;
            _gridStudents.RowTemplate.Height = 35;
            _gridStudents.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            _gridStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "AdmissionNo", HeaderText = "Adm No", Width = 90 });
            _gridStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FullName", HeaderText = "Student Name", Width = 220 });
            _gridStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Standard", HeaderText = "Std", Width = 60 });
            _gridStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Section", HeaderText = "Sec", Width = 60 });
            _gridStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FatherName", HeaderText = "Father Name", Width = 150 });
            _gridStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FatherContactNo", HeaderText = "Contact", Width = 120 });

            this.Controls.Add(_gridStudents);
            this.Controls.Add(pnlInput);
        }

        private async Task LoadStudentsAsync()
        {
            var students = await _repository.GetAllActiveAsync();
            _gridStudents.DataSource = students;
        }

        private async Task SaveStudentAsync()
        {
            if (string.IsNullOrWhiteSpace(_txtAdmNo.Text) || string.IsNullOrWhiteSpace(_txtFirst.Text) || _cboStd.SelectedItem == null)
            {
                MessageBox.Show("Admission Number, First Name, and Standard are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Optional parsing logic for numerics
            decimal.TryParse(_txtFatherIncome.Text, out decimal fIncome);
            decimal.TryParse(_txtMotherIncome.Text, out decimal mIncome);

            var student = new Student
            {
                AdmissionNo = _txtAdmNo.Text.Trim(),
                FirstName = _txtFirst.Text.Trim(),
                LastName = _txtLast.Text.Trim(),
                EmisNo = _txtEmis.Text.Trim(),
                Standard = _cboStd.SelectedItem.ToString(),
                Section = _txtSec.Text.Trim().ToUpper(),
                Gender = _cboGender.SelectedItem?.ToString(),
                Dob = _dtpDob.Value,
                AadhaarNo = _txtAadhaar.Text.Trim(),
                Religion = _txtReligion.Text.Trim(),
                Community = _txtCommunity.Text.Trim(),
                MotherTongue = _txtMotherTongue.Text.Trim(),
                FatherName = _txtFatherName.Text.Trim(),
                FatherContactNo = _txtFatherPhone.Text.Trim(),
                FatherIncome = fIncome,
                MotherName = _txtMotherName.Text.Trim(),
                MotherContactNo = _txtMotherPhone.Text.Trim(),
                MotherIncome = mIncome,
                Address = _txtAddress.Text.Trim(),
                Certificates = _txtCertificates.Text.Trim(),
                IsActive = true
            };

            try
            {
                _btnSave.Enabled = false;
                await _repository.InsertAsync(student);
                await LoadStudentsAsync();

                // Keep cursor ready for next entry
                _txtAdmNo.Clear(); _txtFirst.Clear(); _txtLast.Clear();
                _txtAdmNo.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving student: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _btnSave.Enabled = true;
            }
        }
    }
}
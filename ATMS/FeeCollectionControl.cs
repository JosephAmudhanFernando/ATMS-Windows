using ATMS.Core.Models;
using ATMS.Data.Repositories;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMS
{
    public class FeeCollectionControl : UserControl
    {
        private readonly FeeRepository _feeRepo;
        private readonly StudentRepository _studentRepo;

        private TextBox _txtSearchAdmNo, _txtPaymentAmount;
        private Label _lblStudentInfo, _lblSelectedFee;
        private DataGridView _gridFees;
        private Button _btnSearch, _btnPay;

        private Student? _currentStudent;
        private AcademicYear? _activeYear;
        private FeeLedger? _selectedLedger;

        public FeeCollectionControl(FeeRepository feeRepo, StudentRepository studentRepo)
        {
            _feeRepo = feeRepo;
            _studentRepo = studentRepo;
            InitializeUI();
            this.Load += async (s, e) => await InitializeContextAsync();
        }

        private void InitializeUI()
        {
            this.BackColor = Color.Transparent;

            // --- Top Panel: Search & Info ---
            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 80, Padding = new Padding(10) };

            pnlTop.Controls.Add(new Label
            {
                Text = "Search Adm No:",
                Location = new Point(15, 25),
                AutoSize = true
            });

            _txtSearchAdmNo = new TextBox { Location = new Point(120, 23), Width = 150 };
            pnlTop.Controls.Add(_txtSearchAdmNo);

            _btnSearch = new Button
            {
                Text = "Search",
                Location = new Point(280, 21),
                Width = 80,
                BackColor = ColorTranslator.FromHtml("#1E2A38"),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            _btnSearch.FlatAppearance.BorderSize = 0;
            _btnSearch.Click += async (s, e) => await SearchStudentAsync();
            pnlTop.Controls.Add(_btnSearch);

            _lblStudentInfo = new Label
            {
                Text = "No student selected.",
                Location = new Point(390, 25),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#333333")
            };
            pnlTop.Controls.Add(_lblStudentInfo);

            // --- Bottom Panel: Payment Processing ---
            var pnlBottom = new Panel { Dock = DockStyle.Bottom, Height = 80, Padding = new Padding(10) };

            _lblSelectedFee = new Label
            {
                Text = "Select a fee from the grid to process payment.",
                Location = new Point(15, 25),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Italic)
            };
            pnlBottom.Controls.Add(_lblSelectedFee);

            pnlBottom.Controls.Add(new Label
            {
                Text = "Amount (₹):",
                Location = new Point(450, 25),
                AutoSize = true
            });

            _txtPaymentAmount = new TextBox { Location = new Point(530, 23), Width = 100 };
            pnlBottom.Controls.Add(_txtPaymentAmount);

            _btnPay = new Button
            {
                Text = "Process Payment",
                Location = new Point(650, 21),
                Width = 140,
                BackColor = ColorTranslator.FromHtml("#4CAF50"),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            _btnPay.FlatAppearance.BorderSize = 0;
            _btnPay.Click += async (s, e) => await ProcessPaymentAsync();
            pnlBottom.Controls.Add(_btnPay);

            // --- Center Panel: Data Grid ---
            _gridFees = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                EnableHeadersVisualStyles = false,
                RowHeadersVisible = false
            };

            _gridFees.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#E0E4E8");
            _gridFees.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            _gridFees.ColumnHeadersHeight = 40;
            _gridFees.RowTemplate.Height = 35;
            _gridFees.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            _gridFees.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FeeHeadName",
                HeaderText = "Fee Category",
                Width = 250
            });
            _gridFees.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "AllocatedAmount",
                HeaderText = "Total (₹)",
                Width = 120
            });
            _gridFees.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaidAmount",
                HeaderText = "Paid (₹)",
                Width = 120
            });
            _gridFees.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PendingAmount",
                HeaderText = "Pending (₹)",
                Width = 120
            });
            _gridFees.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ReceiptNo",
                HeaderText = "Receipt No",
                Width = 150
            });

            _gridFees.SelectionChanged += GridFees_SelectionChanged;

            this.Controls.Add(_gridFees);
            this.Controls.Add(pnlTop);
            this.Controls.Add(pnlBottom);
        }

        private async Task InitializeContextAsync()
        {
            _activeYear = await _feeRepo.GetActiveAcademicYearAsync();
            if (_activeYear == null)
            {
                MessageBox.Show("No active academic year found. Contact Admin.",
                    "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _btnSearch.Enabled = false;
            }
        }

        private async Task SearchStudentAsync()
        {
            if (string.IsNullOrWhiteSpace(_txtSearchAdmNo.Text)) return;

            _currentStudent = await _studentRepo.GetByAdmissionNoAsync(
                _txtSearchAdmNo.Text.Trim());

            if (_currentStudent == null)
            {
                MessageBox.Show("Student not found.", "Search Result",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _lblStudentInfo.Text = "No student selected.";
                _gridFees.DataSource = null;
                return;
            }

            _lblStudentInfo.Text =
                $"{_currentStudent.FullName} | Std: {_currentStudent.Standard} " +
                $"| Sec: {_currentStudent.Section}";

            await LoadFeeLedgerAsync();
        }

        private async Task LoadFeeLedgerAsync()
        {
            if (_currentStudent == null || _activeYear == null) return;

            var ledger = await _feeRepo.GetStudentLedgerAsync(
                _currentStudent.Id, _activeYear.Id);
            _gridFees.DataSource = ledger.ToList();

            // Clear payment selection
            _gridFees.ClearSelection();
            _selectedLedger = null;
            UpdatePaymentUI();
        }

        private void GridFees_SelectionChanged(object? sender, EventArgs e)
        {
            if (_gridFees.SelectedRows.Count > 0)
            {
                _selectedLedger = _gridFees.SelectedRows[0].DataBoundItem as FeeLedger;
                UpdatePaymentUI();
            }
        }

        private void UpdatePaymentUI()
        {
            if (_selectedLedger != null && _selectedLedger.PendingAmount > 0)
            {
                _lblSelectedFee.Text =
                    $"Paying: {_selectedLedger.FeeHeadName} " +
                    $"(Pending: ₹{_selectedLedger.PendingAmount})";
                _txtPaymentAmount.Text = _selectedLedger.PendingAmount.ToString("0.00");
                _btnPay.Enabled = true;
            }
            else
            {
                _lblSelectedFee.Text = "Select a pending fee from the grid.";
                _txtPaymentAmount.Clear();
                _btnPay.Enabled = false;
            }
        }

        private async Task ProcessPaymentAsync()
        {
            if (_selectedLedger == null || !decimal.TryParse(
                _txtPaymentAmount.Text, out decimal amountToPay)) return;

            if (amountToPay <= 0 || amountToPay > _selectedLedger.PendingAmount)
            {
                MessageBox.Show("Invalid amount entered.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Generate a simple receipt number for RAD prototype
            string receiptNo = $"RCPT-{DateTime.Now:yyyyMMddHHmmss}";

            try
            {
                _btnPay.Enabled = false;
                await _feeRepo.ProcessPaymentAsync(
                    _selectedLedger.Id, amountToPay, receiptNo);

                MessageBox.Show($"Payment Successful!\nReceipt: {receiptNo}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadFeeLedgerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Payment failed: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _btnPay.Enabled = _selectedLedger?.PendingAmount > 0;
            }
        }
    }
}
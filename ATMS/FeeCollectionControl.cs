using ATMS.Core.Models;
using ATMS.Data.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMS
{
    public partial class FeeCollectionControl : UserControl
    {
        private readonly FeeRepository _feeRepo;
        private readonly StudentRepository _studentRepo;

        private Student? _currentStudent;
        private AcademicYear? _activeYear;
        private FeeLedger? _selectedLedger;

        public FeeCollectionControl(FeeRepository feeRepo, StudentRepository studentRepo)
        {
            InitializeComponent();
            _feeRepo = feeRepo;
            _studentRepo = studentRepo;

            this.Load += async (s, e) => await InitializeContextAsync();
            btnSearch.Click += async (s, e) => await SearchStudentAsync();
            btnPay.Click += async (s, e) => await ProcessPaymentAsync();
            gridFees.SelectionChanged += GridFees_SelectionChanged;
        }

        private async Task InitializeContextAsync()
        {
            _activeYear = await _feeRepo.GetActiveAcademicYearAsync();
            if (_activeYear == null) btnSearch.Enabled = false;
        }

        private async Task SearchStudentAsync()
        {
            if (string.IsNullOrWhiteSpace(txtSearchAdmNo.Text)) return;
            _currentStudent = await _studentRepo.GetByAdmissionNoAsync(txtSearchAdmNo.Text.Trim());

            if (_currentStudent == null)
            {
                lblStudentInfo.Text = "Not found.";
                gridFees.DataSource = null;
                return;
            }

            lblStudentInfo.Text = $"{_currentStudent.FullName} | Std: {_currentStudent.Standard}";
            var ledger = await _feeRepo.GetStudentLedgerAsync(_currentStudent.Id, _activeYear!.Id);
            gridFees.DataSource = ledger.ToList();
            UpdatePaymentUI();
        }

        private void GridFees_SelectionChanged(object? sender, EventArgs e)
        {
            if (gridFees.SelectedRows.Count > 0)
            {
                _selectedLedger = gridFees.SelectedRows[0].DataBoundItem as FeeLedger;
                UpdatePaymentUI();
            }
        }

        private void UpdatePaymentUI()
        {
            if (_selectedLedger != null && _selectedLedger.PendingAmount > 0)
            {
                lblSelectedFee.Text = $"Paying: {_selectedLedger.FeeHeadName} (Pending: ₹{_selectedLedger.PendingAmount})";
                txtPaymentAmount.Text = _selectedLedger.PendingAmount.ToString("0.00");
                btnPay.Enabled = true;
            }
            else
            {
                lblSelectedFee.Text = "Select a pending fee.";
                txtPaymentAmount.Clear();
                btnPay.Enabled = false;
            }
        }

        private async Task ProcessPaymentAsync()
        {
            if (_selectedLedger == null || !decimal.TryParse(txtPaymentAmount.Text, out decimal amountToPay)) return;

            string receiptNo = $"RCPT-{DateTime.Now:yyyyMMddHHmmss}";
            btnPay.Enabled = false;
            await _feeRepo.ProcessPaymentAsync(_selectedLedger.Id, amountToPay, receiptNo);

            var ledger = await _feeRepo.GetStudentLedgerAsync(_currentStudent!.Id, _activeYear!.Id);
            gridFees.DataSource = ledger.ToList();
            UpdatePaymentUI();
        }
    }
}
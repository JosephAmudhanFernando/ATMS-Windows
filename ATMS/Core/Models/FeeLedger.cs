using System;

namespace ATMS.Core.Models
{
    public class FeeLedger
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int AcademicYearId { get; set; }
        public int FeeHeadId { get; set; }
        public decimal AllocatedAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string? ReceiptNo { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime CreatedAt { get; set; }

        // Helper properties for UI (populated via SQL JOIN)
        public string FeeHeadName { get; set; } = string.Empty;
        public decimal PendingAmount => AllocatedAmount - PaidAmount;
    }
}
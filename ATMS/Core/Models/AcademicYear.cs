using System;

namespace ATMS.Core.Models
{
    public class AcademicYear
    {
        public int Id { get; set; }
        public string YearName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
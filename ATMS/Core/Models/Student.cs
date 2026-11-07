using System;

namespace ATMS.Core.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string AdmissionNo { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Standard { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }

        // Helper property for UI grid binding
        public string FullName => $"{FirstName} {LastName}".Trim();
        public string? EmisNo { get; set; }
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        public string? AadhaarNo { get; set; }
        public string? Religion { get; set; }
        public string? Community { get; set; }
        public string? MotherTongue { get; set; }
        public string? FatherName { get; set; }
        public string? FatherContactNo { get; set; }
        public decimal? FatherIncome { get; set; }
        public string? MotherName { get; set; }
        public string? MotherContactNo { get; set; }
        public decimal? MotherIncome { get; set; }
        public string? Address { get; set; }
        public string? Certificates { get; set; }
    }
}
namespace ATMS.Core.Models
{
    public class FeeHead
    {
        public int Id { get; set; }
        public string HeadName { get; set; } = string.Empty;
        public decimal DefaultAmount { get; set; }
        public bool IsActive { get; set; }
    }
}
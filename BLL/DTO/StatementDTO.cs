using System;

namespace BLL.DTO
{
    public class StatementDTO
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int ClientId { get; set; }
        public int Sum { get; set; }
        public DateTime DateofIssue { get; set; }
        public DateTime MaturityDate { get; set; }
        public int EmployeeId { get; set; }
        public decimal FinalAmount { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

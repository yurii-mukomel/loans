using System;
using System.Collections.Generic;

#nullable disable

namespace DAL
{
    public partial class Statement
    {
        public Statement()
        {
            Fines = new HashSet<Fine>();
            Transactions = new HashSet<Transaction>();
        }

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

        public virtual Person Client { get; set; }
        public virtual Employee CreatedByNavigation { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual StatementType TypeNavigation { get; set; }
        public virtual Employee UpdatedByNavigation { get; set; }
        public virtual ICollection<Fine> Fines { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}

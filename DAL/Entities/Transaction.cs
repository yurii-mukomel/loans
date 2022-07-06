using System;
using System.Collections.Generic;

#nullable disable

namespace DAL
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int StatementId { get; set; }
        public DateTime DateOfPayment { get; set; }
        public decimal PaidAmount { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int Title { get; set; }
        public decimal RemainingAmount { get; set; }
        public string Description { get; set; }

        public virtual Statement Statement { get; set; }
    }
}

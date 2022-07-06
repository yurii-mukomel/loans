using System;
using System.Collections.Generic;

#nullable disable

namespace DAL
{
    public partial class Fine
    {
        public int Id { get; set; }
        public int StatementId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Amount { get; set; }

        public virtual Statement Statement { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace DAL
{
    public partial class StatementType
    {
        public StatementType()
        {
            Statements = new HashSet<Statement>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public short Percentage { get; set; }
        public int MinAmount { get; set; }
        public int MaxAmount { get; set; }
        public int MinTerm { get; set; }
        public int? MaxTerm { get; set; }

        public virtual ICollection<Statement> Statements { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace DAL
{
    public partial class Employee
    {
        public Employee()
        {
            StatementCreatedByNavigations = new HashSet<Statement>();
            StatementEmployees = new HashSet<Statement>();
            StatementUpdatedByNavigations = new HashSet<Statement>();
        }

        public int Id { get; set; }
        public int PeopleId { get; set; }

        public virtual Person People { get; set; }
        public virtual ICollection<Statement> StatementCreatedByNavigations { get; set; }
        public virtual ICollection<Statement> StatementEmployees { get; set; }
        public virtual ICollection<Statement> StatementUpdatedByNavigations { get; set; }
    }
}

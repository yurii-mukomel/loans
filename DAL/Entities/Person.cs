using System.Collections.Generic;

#nullable disable

namespace DAL
{
    public partial class Person
    {
        public Person()
        {
            Statements = new HashSet<Statement>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? Age { get; set; }
        public string IdNumber { get; set; }
        public string Password { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ICollection<Statement> Statements { get; set; }
    }
}

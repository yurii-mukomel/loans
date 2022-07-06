using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DAL
{
    public partial class quick_lendingContext : DbContext
    {
        public quick_lendingContext()
        {
        }

        public quick_lendingContext(DbContextOptions<quick_lendingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Fine> Fines { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Statement> Statements { get; set; }
        public virtual DbSet<StatementType> StatementTypes { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=quick_lending;Username=postgres;Password=AaAmukomel2511+");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Ukrainian_Ukraine.1251");

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.HasIndex(e => e.PeopleId, "employee_peopleid_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('employee_id_seq'::regclass)");

                entity.HasOne(d => d.People)
                    .WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employee_peopleid_fkey");
            });

            modelBuilder.Entity<Fine>(entity =>
            {
                entity.ToTable("Fine");

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('fine_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Statement)
                    .WithMany(p => p.Fines)
                    .HasForeignKey(d => d.StatementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fine_statementid_fkey");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasIndex(e => e.Email, "people_email_key")
                    .IsUnique();

                entity.HasIndex(e => e.IdNumber, "people_passportnmber_key")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "people_phone_key")
                    .IsUnique();

                entity.HasIndex(e => e.Password, "people_tin_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('people_id_seq'::regclass)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.IdNumber)
                    .IsRequired()
                    .HasMaxLength(9);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Password).IsRequired();
            });

            modelBuilder.Entity<Statement>(entity =>
            {
                entity.ToTable("Statement");

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('statement_id_seq'::regclass)");

                entity.Property(e => e.Status).HasMaxLength(10);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Statements)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("statement_clientid_fkey");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.StatementCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("statement_fk");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.StatementEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("statement_employeeid_fkey");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Statements)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("statement_statementtype_fkey");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.StatementUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("statement_fk_1");
            });

            modelBuilder.Entity<StatementType>(entity =>
            {
                entity.ToTable("StatementType");

                entity.HasIndex(e => e.Name, "type_of_loan_nametype_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('type_of_loan_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('transaction_history_id_seq'::regclass)");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Statement)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.StatementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("transaction_history_statementid_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

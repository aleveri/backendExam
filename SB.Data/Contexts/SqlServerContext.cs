using Audit.Core;
using Audit.EntityFramework;
using Microsoft.EntityFrameworkCore;
using SB.Entities;
using SB.Interfaces;
using SB.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace SB.Data
{
    public sealed class SqlServerContext : AuditDbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options, ITenantProvider tenantProvider) : base(options)
        {
            DbInitializer.Initialize(this);
            AuditEventType = "{database}_{context}";
            Mode = AuditOptionMode.OptOut;
            IncludeEntityObjects = false;
            Audit.Core.Configuration.DataProvider = new AuditProvider();
        }

        public DbSet<User> User { get; set; }
        public DbSet<Catalog> Catalog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
                               .UseSqlServer(string.Format(Parameters.BaseConn, Singleton.Instance.DbName));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Catalog

            modelBuilder.Entity<Catalog>().ToTable("Catalog")
                .HasKey(x => x.Id);

            modelBuilder.Entity<Catalog>(x =>
            {
                x.Property(y => y.Name).IsRequired().HasMaxLength(50);
                x.Property(y => y.Code).IsRequired().HasMaxLength(10);
            });

            modelBuilder.Entity<Catalog>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Catalog>()
                .HasIndex(x => x.Code)
                .IsUnique();

            modelBuilder.Entity<Catalog>()
               .HasOne(x => x.Parent)
               .WithMany(x => x.Childs)
               .OnDelete(DeleteBehavior.Restrict)
               .HasForeignKey(x => x.ParentId);

            #endregion

            #region User
            modelBuilder.Entity<User>().ToTable("User")
               .HasKey(x => x.Id);

            modelBuilder.Entity<User>(x =>
            {
                x.Property(y => y.Name).IsRequired().HasMaxLength(60);
                x.Property(y => y.Address).IsRequired().HasMaxLength(80);
                x.Property(y => y.DocumentNumber).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(x => x.DocumentNumber)
                .IsUnique();

            modelBuilder.Entity<User>()
             .HasOne(x => x.Country)
             .WithMany()
             .OnDelete(DeleteBehavior.Restrict)
             .HasForeignKey(x => x.CountryId);

            modelBuilder.Entity<User>()
             .HasOne(x => x.State)
             .WithMany()
             .OnDelete(DeleteBehavior.Restrict)
             .HasForeignKey(x => x.StateId);

            modelBuilder.Entity<User>()
             .HasOne(x => x.City)
             .WithMany()
             .OnDelete(DeleteBehavior.Restrict)
             .HasForeignKey(x => x.CityId);
            #endregion
        }

        public override void OnScopeSaving(AuditScope auditScope)
        {
            if (!Singleton.Instance.Audit) auditScope.Discard();
            auditScope.SetCustomField("Ip", Singleton.Instance.Ip);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()) => base.SaveChangesAsync(cancellationToken);
    }
}

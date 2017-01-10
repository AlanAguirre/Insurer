using Insurer.Data.Configuration;
using Insurer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurer.Data
{
    public class InsurerEntities : ApplicationDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<InsuranceType> InsuranceTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyLog> CompanyLogs { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new CompanyConfiguration());
            modelBuilder.Configurations.Add(new CompanyLogConfiguration());
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new InsuranceTypeConfiguration());
            modelBuilder.Entity<Customer>()
                .HasMany(i => i.InsuranceTypes)
                .WithMany(c => c.Customers)
                .Map(ic =>
                {
                    ic.MapLeftKey("CustomerId");
                    ic.MapRightKey("InsuranceTypeId");
                    ic.ToTable("CustomerInsuranceType");
                });
        }
    }
}

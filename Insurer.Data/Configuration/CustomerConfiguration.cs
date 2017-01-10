using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Insurer.Model;

namespace Insurer.Data.Configuration
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Customers");
            Property(c => c.Name).IsRequired().HasMaxLength(100);
            Property(c => c.Email).IsRequired().HasMaxLength(200);
            Property(c => c.PhoneNumber).IsRequired().HasMaxLength(50);

        }
    }
}

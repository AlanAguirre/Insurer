using Insurer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurer.Data.Configuration
{
    public class CompanyConfiguration : EntityTypeConfiguration<Company>
    {
        public CompanyConfiguration()
        {
            ToTable("Companies");
            Property(c => c.Name).IsRequired().HasMaxLength(200);
            Property(c => c.Email).IsRequired().HasMaxLength(200);
            Property(c => c.PhoneNumber).IsRequired().HasMaxLength(50);
            Property(c => c.Token).IsRequired().HasMaxLength(150);
        }
    }
}

using Insurer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurer.Data.Configuration
{
    public class CompanyLogConfiguration : EntityTypeConfiguration<CompanyLog>
    {
        public CompanyLogConfiguration()
        {
            ToTable("CompanyLogs");
            Property(c => c.Company).IsRequired().HasMaxLength(200);
            Property(c => c.InsureType).IsRequired().HasMaxLength(100);            
        }
    }
}

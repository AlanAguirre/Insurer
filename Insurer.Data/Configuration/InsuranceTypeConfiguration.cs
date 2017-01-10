using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Insurer.Model;

namespace Insurer.Data.Configuration
{
    public class InsuranceTypeConfiguration : EntityTypeConfiguration<InsuranceType>
    {
        public InsuranceTypeConfiguration()
        {
            ToTable("InsuranceTypes");
            Property(i => i.Name).IsRequired().HasMaxLength(100);
        }
    }
}

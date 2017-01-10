using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurer.Model
{
    public class InsuranceType
    {
        public int InsuranceTypeId { get; set; }
        public string Name { get; set; }

        public virtual List<Customer> Customers { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurer.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Address { get; set; }

        public virtual List<InsuranceType> InsuranceTypes { get; set; }
        
    }
}

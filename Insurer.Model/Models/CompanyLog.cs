using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurer.Model
{
    public class CompanyLog
    {
        public int CompanyLogId { get; set; }
        public string Company { get; set; }
        public string InsureType { get; set; }
        public DateTime? RequestDate { get; set; }
    }
}

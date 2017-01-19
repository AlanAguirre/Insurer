using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Insurer.Web.Service.ViewModels
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Address { get; set; }

        public List<InsuranceTypeViewModel> InsuranceTypes { get; set; }
    }
}
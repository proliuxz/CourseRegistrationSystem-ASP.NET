using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseRegistrationSystem.Models
{
    public class BillingViewModel
    {
        [Required]
        [Display(Name = "Address")]
        public String BillingAddress { get; set; }
        [Required]
        [Display(Name = "PersonName")]
        public String BillingPersonName { get; set; }
        [Required]
        [Display(Name = "Country")]
        public String BillingAddressCountry { get; set; }
        [Required]
        [Display(Name = "PostalCode")]
        public String BillingAddressPostalCode { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CourseRegistration.Models
{
    public class CompanyHR : BaseModel
    {
        [Key]
        [Required]
        public String Email{get;set;}
        public virtual Company Company { get; set; }
        [Required]
        public String Name{get;set;}
        public String ContactNumber{get;set;}
        public String JobTitle{get;set;}
        public String FaxNumber{get;set;}
        public virtual ApplicationUser AppUser { get; set; }
    }
}

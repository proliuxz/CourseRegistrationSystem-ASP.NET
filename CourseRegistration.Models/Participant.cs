using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CourseRegistration.Models
{
    public class Participant : BaseModel
    {
        [Key]
        [Required]
        public int ParticipantId { get; set; }
        [Required]
        [Display(Name = "NRIC/PASSPORT")]
        public String IdNumber{ get;set; }
        public virtual Company Company { get; set; }
        public String Salutation{ get;set; }
        [Display(Name = "Status")]
        public String EmploymentStatus{ get;set; }
        public String CompanyName{ get;set; }
        public String JobTitle{ get;set; }
        public String Department{ get;set; }
        [Required]
        [Display(Name = "Name")]
        public String FullName{ get;set; }
        [Required]
        public Gender Gender { get; set; }
        public String Nationality{ get;set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth{ get;set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public String Email{ get;set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "PhoneNo.")]
        public String ContactNumber{ get;set; }
        public String DietaryRequirement{ get;set; }
        public OrganizationSize OrganizationSize { get; set; }
        [Display(Name = "Annual Salary")]
        public SalaryRange SalaryRange { get; set; }

        private List<Registration> registrations;
        public virtual List<Registration> Registrations
        {
            get { 
                return registrations; 
            }
            set
            {
                registrations = value;
            }
        }

        public ApplicationUser AppUser { get; set; }

    }
}

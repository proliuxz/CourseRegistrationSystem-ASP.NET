using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CourseRegistration.Models
{
    public enum RegistrationStatus { Pending = 0, Successful = 1, Cancel = 2 }

    public enum ClassStatus { Pending = 0, Confirmed = 1, Cancel = 2 }

    public enum OrganizationSize 
    { 
        [Display(Name = @"< 20")]
        Less_Than_20 = 0,
        [Display(Name = @"20 ~ 200")]
        From_20_To_200 = 1,
        [Display(Name = @"200 ~ 500")]
        From_200_To_500 = 2,
        [Display(Name = @"> 500")]
        More_Than_500 = 3 
    }

    public enum SalaryRange 
    { 
        [Display(Name = @"< 60,000")]
        Less_Than_60k = 0,
        [Display(Name = @"60,000 ~ 90,000")]
        From_60_To_90k = 1,
        [Display(Name = @"90,000 ~ 120,000")]
        From_90k_To_120k = 2,
        [Display(Name = @"> 120,000")]
        More_Than_120k = 3 
    }

    public enum Sponsorship { Self = 0, Company = 1 }

    public enum Gender { Female = 0, Male = 1 }


}

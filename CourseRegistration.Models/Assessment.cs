using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseRegistration.Models
{
    public class Assessment : BaseModel
    {
        [Key]
        [Required]
        public int AssessmentId { get; set; }

        [Required]
        public virtual CourseClass CourseClass { get; set; }

        [Required]
        public virtual Participant Participant { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CourseRegistration.Models
{
    public class Instructor : BaseModel
    {
        [Key][Required]
        public int InstructorId { get; set; }

        [Required]
        public String InstructorName { get; set; }

        private List<Course> courses;
        public virtual List<Course> Courses
        {
            get
            {
                return courses;
            }
            set
            {
                courses = value;
            }
        }

        public virtual ApplicationUser AppUser { get; set; }

    }
}

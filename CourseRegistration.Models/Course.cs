using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CourseRegistration.Models
{
    public class Course : BaseModel
    {
        [Key][Required]
        public String CourseCode { get; set; }

        [Required]
        public String CourseTitle { get; set; }

        public String CourseDescription { get; set; }

        [Required]
        public double Fee { get; set; }

        [Required]
        public int NumberOfDays { get; set; }

        public bool enabled { get; set; }

        public virtual Category Category { get; set; }

        private List<Instructor> instructors = new List<Instructor>();
        public virtual List<Instructor> Instructors
        {
            get
            {
                return instructors;
            }
            set
            {
                instructors = value;
            }
        }

        private List<CourseClass> courseClass = new List<CourseClass>();
        public virtual List<CourseClass> CourseClasses
        {
            get
            {
                return courseClass;
            }
            set
            {
                courseClass = value;
            }
        }
    }
}

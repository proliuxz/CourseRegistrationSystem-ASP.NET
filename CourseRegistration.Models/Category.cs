using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CourseRegistration.Models
{
    public class Category : BaseModel
    {
        [Key][Required]
        public int CategoryId { get; set; }

        [Required]
        public String CategoryName { get; set; }

        public String CategoryDescription { get; set; }

        private List<Course> courses = new List<Course>();
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
    }
}

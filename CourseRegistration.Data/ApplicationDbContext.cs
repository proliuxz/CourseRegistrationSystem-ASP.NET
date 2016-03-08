using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CourseRegistration.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace CourseRegistration.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseClass> CourseClasses { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyHR> CompanyHRs { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Assessment> Assessments { get; set; }

    }
}

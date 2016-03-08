using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseRegistration.BLL;
using CourseRegistration.Models;

namespace CourseRegistrationSystem.Controllers
{
    public class InstructorController : Controller
    {
        // GET: Instructor
        public ActionResult Index()
        {
            IEnumerable<Instructor> instructors = InstructorBLL.Instance.GetAllInstructors();
            return View(instructors);
        }

        public ActionResult Details(int id)
        {
            Instructor instructor = InstructorBLL.Instance.GetInstructorById(id);
            return View(instructor);
        }
    }
}
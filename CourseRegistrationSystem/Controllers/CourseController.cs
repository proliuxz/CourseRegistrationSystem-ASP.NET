using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseRegistration.Models;
using CourseRegistration.BLL;

namespace CourseRegistrationSystem.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            List<Category> categories = CategoryBLL.Instance.GetAllCategories();
            //ViewBag.cList = CourseClassBLL.Instance.GetUpcomingClass();
            return View(categories);
        }
        public ActionResult Search(String content)
        {
            List<Course> courses = new List<Course>(); ;
            if (!String.IsNullOrEmpty(content))
            {
                courses = CourseBLL.Instance.SearchCourse(content);
            }
            return View(courses);
        }
        [HttpPost]
        public ActionResult SearchResult()
        {
            ViewBag.Search = Request.Form["Search"];
            List<Course> courses;
            if (ViewBag.Search!=null && ViewBag.Search!=""){
                courses = CourseBLL.Instance.SearchCourse(ViewBag.Search);
            }
            else
            {
                courses = new List<Course>();
            }
            return PartialView("SearchResult",courses);
        }

        // GET: Course/Details/5
        public ActionResult Details(string code)
        {
            Course course = CourseBLL.Instance.GetCourseByCode(code);
            ViewBag.Classes = new List<CourseClass>();
            foreach (var item in course.CourseClasses)
            {
                if (item.isOpenForRegister && item.StartDate > DateTime.Now)
                {
                    ViewBag.Classes.Add(item);
                }
            }
            return View(course);
        }

        public ActionResult CourseList(int categoryId)
        {
            Category category = CategoryBLL.Instance.GetCategoryById(categoryId);
            ViewBag.Courses = new List<Course>();
            foreach (var item in category.Courses)
            {
                if (item.enabled)
                {
                    ViewBag.Courses.Add(item);
                }
            }
            return PartialView(category);
        }
    }
}

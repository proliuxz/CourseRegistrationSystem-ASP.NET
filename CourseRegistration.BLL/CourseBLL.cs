using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Models;
using CourseRegistration.Data;

namespace CourseRegistration.BLL
{
    public class CourseBLL
    {

        private static readonly Lazy<CourseBLL> lazy =
            new Lazy<CourseBLL>(() => new CourseBLL());
      
        public static CourseBLL Instance { get { return lazy.Value; } }

        private CourseBLL()
        {
        }

        public void CreateCourse(Course c)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            uow.CourseRepository.Insert(c);
            uow.Save();
        }

        public List<Course> GetAllCourses()
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.CourseRepository.GetAll().ToList();
        }

        public Course GetCourseByCode(String courseCode)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.CourseRepository.GetAll().Where<Course>(x => x.CourseCode == courseCode).SingleOrDefault();
        }

        public void EditCourse(Course c)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            uow.CourseRepository.Edit(c);
            uow.Save();
        }

        public void DeleteCourse(Course c)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            uow.CourseRepository.Delete(c);
            uow.Save();
            
        }

        public List<Course> SearchCourse(String key)
        {
            IQueryable<Course> result;
            String keyWord = key.ToUpper();
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            
            //title
            IQueryable<Course> titleQuery = uow.CourseRepository.GetAll().Where<Course>(x => x.CourseTitle.ToUpper().Contains(keyWord));
            //category
            IQueryable<Course> categoryQuery = uow.CourseRepository.GetAll().Where<Course>(x => x.Category.CategoryName.ToUpper().Contains(keyWord));
            //instructor
            IQueryable<Course> instructorQuery = from course in uow.CourseRepository.GetAll()
                                                where
                                                 course.Instructors.Any(y => y.InstructorName.ToUpper().Contains(keyWord))
                                                select course;

            //description
            IQueryable<Course> descriptionQuery = uow.CourseRepository.GetAll().Where<Course>(x => x.CourseDescription.ToUpper().Contains(keyWord));

            result = titleQuery.Union(categoryQuery).Union(instructorQuery).Union(descriptionQuery);

            //only enabled courses are showed
            result = result.Where(x => x.enabled == true);
            return result.ToList();
        }

        public List<Course> getCoursesByConds(String categoryID)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            IQueryable<Course> query =
                from course in uow.CourseRepository.GetAll()
                select course;

            if (categoryID.ToString() != Util.C_String_All_Select)
            {
                int tmpInt = int.Parse(categoryID);
                query = query.Where(x => x.Category.CategoryId == tmpInt);
            }

            return query.ToList();
        }

    }
}


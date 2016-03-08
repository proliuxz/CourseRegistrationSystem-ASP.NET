using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Models;
using CourseRegistration.Data;


namespace CourseRegistration.BLL
{
    public class InstructorBLL
    {

        private static readonly Lazy<InstructorBLL> lazy =
            new Lazy<InstructorBLL>(() => new InstructorBLL());

        public static InstructorBLL Instance { get { return lazy.Value; } }

        private InstructorBLL()
        {

        }

        public List<Instructor> GetAllInstructors()
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();

            return uow.InstructorRepository.GetAll().ToList();
        }

        public Instructor GetInstructorById(int instructorId)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.InstructorRepository.GetById(instructorId);
        }

        public Instructor GetInstructorByUserId(String userId)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.InstructorRepository.GetAll().Where(x => x.AppUser.Id == userId).SingleOrDefault();
        }

        public Instructor GetInstructorByName(String name)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.InstructorRepository.GetAll().Where(x=>x.InstructorName==name).SingleOrDefault();
        }

        public bool CheckInstructorClass(String userName, String classId)
        { 
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            IQueryable<Instructor> query = 
                from instructor in uow.InstructorRepository.GetAll()
                where instructor.AppUser.UserName == userName &&
                     (
                        (from course in instructor.Courses
                        where course.CourseClasses.Any(x=>x.ClassId==classId)
                        select 1).Count() > 0)
                select instructor;
            
            return query.Count() > 0;
        }

    }
}

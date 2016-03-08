using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using CourseRegistration.Models;
using CourseRegistration.Data;

namespace CourseRegistration.BLL
{
    public class CourseClassBLL
    {
       
        private static readonly Lazy<CourseClassBLL> lazy =
            new Lazy<CourseClassBLL>(() => new CourseClassBLL());

        private CourseClassBLL() { }

        public static CourseClassBLL Instance { get { return lazy.Value; } }


        public void CreateCourseClass(CourseClass cc)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            uow.CourseClassRepository.Insert(cc);
            uow.Save();
        
        }

        public CourseClass GetCourseClassById(String courseClassId)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.CourseClassRepository.GetById(courseClassId);
        }

        public int getRegNum(String courseClassId)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            int result =
                (from reg in uow.RegistrationRepository.GetAll()
                where reg.Status != RegistrationStatus.Cancel &&
                reg.CourseClass.ClassId == courseClassId
                select 1).Count();

            return result;

        }

        public List<CourseClass> GetAllCourseClass()
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.CourseClassRepository.GetAll().ToList();
            
        }

        public List<CourseClass> GetAvailableClass()
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();

            IQueryable<CourseClass> query =
                from courseClass in uow.CourseClassRepository.GetAll()
                where DateTime.Now < courseClass.StartDate && 
                    courseClass.Status != ClassStatus.Cancel
                select courseClass;

            return query.ToList();
        }

        public List<CourseClass> GetUpcomingClass()
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();

            DateTime d = DateTime.Now.AddMonths(3);

            IQueryable<CourseClass> query =
                from courseClass in uow.CourseClassRepository.GetAll()
                where courseClass.isOpenForRegister &&
                    DateTime.Now < courseClass.StartDate &&
                    d > courseClass.StartDate &&
                    courseClass.Status != ClassStatus.Cancel
                select courseClass;

            return query.ToList();
        }

        public void UpdateCourseClass(CourseClass cc)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            uow.CourseClassRepository.Edit(cc);
            uow.Save();
        }

        public List<CourseClass> GetClassesByConds(String categoryID, String courseCode)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            IQueryable<CourseClass> query =
                from courseClass in uow.CourseClassRepository.GetAll()
                select courseClass;

            if (categoryID.ToString() != Util.C_String_All_Select)
            {
                int tmpInt = int.Parse(categoryID);
                query = query.Where(x => x.Course.Category.CategoryId == tmpInt);
            }

            if (courseCode != Util.C_String_All_Select)
            {
                query = query.Where(x => x.Course.CourseCode == courseCode);
            }
            return query.ToList(); 
        }

        public void DeleteCourseClass(CourseClass cc)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            uow.CourseClassRepository.Delete(cc);
            uow.Save();
        }

        public List<Participant> GetStudentsByClassId(String classId)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            
            List<Registration> regList =
                uow.RegistrationRepository.GetAll()
                    .Where(x => x.CourseClass.ClassId == classId &&
                        x.Status == RegistrationStatus.Successful)
                .Include(x => x.Participant).ToList();

            List<Participant> studentList = new List<Participant>();
            foreach(Registration r in regList){
                studentList.Add(r.Participant);
            }

            return studentList;
        }

        public Boolean VerifyClass(String id)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            IQueryable<CourseClass> query =
                from courseClass in uow.CourseClassRepository.GetAll()
                where courseClass.isOpenForRegister &&
                    DateTime.Now < courseClass.StartDate &&
                    courseClass.Status != ClassStatus.Cancel &&
                    courseClass.ClassId == id
                select courseClass;
            if (query.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public Boolean CancelClass(string classID)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            CourseClass courseClass = GetCourseClassById(classID);
            courseClass.Status = ClassStatus.Cancel;
            courseClass.isOpenForRegister = false;
            List<CourseClass> courseClassList = GetClassesByConds(Util.C_String_All_Select, courseClass.Course.CourseCode);
            CourseClass nextClass = null;

            foreach (CourseClass x in courseClassList)
            {
                if (x.StartDate > DateTime.Today && x.StartDate > courseClass.StartDate && x.Status != ClassStatus.Cancel)
                {
                    if (nextClass == null)
                        nextClass = x;
                    else if (x.StartDate < nextClass.StartDate)
                        nextClass = x;
                }
            }

            List<Registration> Reg = courseClass.Registrations;
            if (nextClass != null)
            {
                foreach (Registration r in Reg)
                {
                    Util.SendEmail(r.Participant.Email.ToString(), "Very Sorry about you changed class!", EmailForTransfer(r,nextClass));
                    r.CourseClass = nextClass;
                }
            }
            else 
            { 
                foreach (Registration r in Reg)
                {
                    Util.SendEmail(r.Participant.Email.ToString(), "Very Sorry about you canceled class!", EmailForCancel(r));
                }
            }

            uow.CourseClassRepository.Edit(courseClass);
            uow.Save();
            return true;
        }
        public Boolean ConfirmClass(String classID)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            CourseClass courseClass = GetCourseClassById(classID);
            courseClass.Status = ClassStatus.Confirmed;
            List<Registration> Reg = courseClass.Registrations;
            foreach (Registration r in Reg)
            {
                Util.SendEmail(r.Participant.Email.ToString(), "Your class has been confirmed!", EmailForConfirm(r));
            }
            courseClass.isOpenForRegister = false;
            uow.CourseClassRepository.Edit(courseClass);
            uow.Save();
            return true;
        }
        public String EmailForCancel(Registration r)
        {
            String context = "";
            context += Environment.NewLine;
            context += "Dear " + r.Participant.FullName + Environment.NewLine;
            context += "I'm very sorry to tell you that the class you choosed has been canceled." + Environment.NewLine;
            context += "Here is the imformation about it:"+Environment.NewLine;
            context += "Course Title:"+ r.CourseClass.Course.CourseTitle+Environment.NewLine;
            context += "Class ID:" +r.CourseClass.ClassId+ Environment.NewLine;
            context += Environment.NewLine;
            context += "Thanks for you support!";
            return context;
        }

        public String EmailForConfirm(Registration r)
        {
            String context = "";
            context += Environment.NewLine;
            context += "Dear " + r.Participant.FullName + Environment.NewLine;
            context += "Congratulations! The class you choosed has been confirmed" + Environment.NewLine;
            context += "Here is the imformation about it:" + Environment.NewLine;
            context += "Course Title:" + r.CourseClass.Course.CourseTitle + Environment.NewLine;
            context += "Class ID:" + r.CourseClass.ClassId + Environment.NewLine;
            context += "The class will start on " + r.CourseClass.StartDate.ToShortDateString() + " Don't be late!" + Environment.NewLine;
            context += Environment.NewLine;
            context += "Thanks for you support!";
            return context;
        }
        public String EmailForTransfer(Registration r,CourseClass newClass)
        {
            String context = "";
            context += Environment.NewLine;
            context += "Dear " + r.Participant.FullName + Environment.NewLine;
            context += "I'm very sorry to tell you that the class you choosed has been canceled. And we have arranged the latest same class for you!" + Environment.NewLine;
            context += "Here is the imformation about your old class:" + Environment.NewLine;
            context += "Course Title:" + r.CourseClass.Course.CourseTitle + Environment.NewLine;
            context += "Class ID:" + r.CourseClass.ClassId + Environment.NewLine;
            context += Environment.NewLine;
            context += "Here is the imformation about your new class:" + Environment.NewLine;
            context += "Course Title:" + newClass.Course.CourseTitle + Environment.NewLine;
            context += "Class ID:" + newClass.ClassId + Environment.NewLine;
            context += "The class will start on " + newClass.StartDate.ToShortDateString() + " Don't be late!" + Environment.NewLine;
            context += Environment.NewLine;
            context += "Thanks for you support!";
            return context;
        }
    }
}

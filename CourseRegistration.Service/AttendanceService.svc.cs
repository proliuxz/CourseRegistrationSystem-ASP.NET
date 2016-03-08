using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CourseRegistration.Models;
using CourseRegistration.BLL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseRegistration.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AttendanceService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AttendanceService.svc or AttendanceService.svc.cs at the Solution Explorer and start debugging.
    public class AttendanceService : IAttendanceService
    {
        //[System.Security.Permissions.PrincipalPermission(System.Security.Permissions.SecurityAction.Demand, Role = Util.C_Role_IndividualUser)]
        public List<SvcStudent> GetStudentList(DateTime date, String classId)
        {

            if (CourseClassBLL.Instance.GetCourseClassById(classId)==null)
            {
                throw new FaultException("invalid classId");
            }

            String userName = OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name;
            if (InstructorBLL.Instance.CheckInstructorClass(userName,classId) == false)
            {
                throw new FaultException("no right to access this info of this class");
            }

            List<SvcStudent> studentList = new List<SvcStudent>();

            List<Participant> participantList = CourseClassBLL.Instance.GetStudentsByClassId(classId);
            foreach (Participant p in participantList)
            {
                SvcStudent newStud = new SvcStudent(
                    p.ParticipantId,
                    p.IdNumber,
                    p.FullName,
                    (Service.Gender)p.Gender,
                    p.Nationality,
                    p.Email,
                    p.ContactNumber
                );
                studentList.Add(newStud);
            }
            //int i=studentList.Count();
            if (studentList.Count() == 0)
            {
                throw new FaultException("No student registered for this class");
            }
            return studentList;
        }


        public Result SubmitAttendance(int participantId, String classId)
        {
            try
            {
                Attendance att = new Attendance();
                att.Participant = ParticipantBLL.Instance.GetParticipantById(participantId);
                att.CourseClass = CourseClassBLL.Instance.GetCourseClassById(classId);
                att.ClassDate = DateTime.Today;
                AttendanceBLL.Instance.CreateAttendance(att);
            }
            catch (BusinessException e)
            {
                return new Result(false, e.ToString());
            }
            return new Result(true, "Attendance is successfull.It is updated in database");
        }
    }
}

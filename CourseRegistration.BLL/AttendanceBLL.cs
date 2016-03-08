using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Models;
using CourseRegistration.Data;

namespace CourseRegistration.BLL
{
    public class AttendanceBLL
    {

        private static readonly Lazy<AttendanceBLL> lazy =
            new Lazy<AttendanceBLL>(() => new AttendanceBLL());

        public static AttendanceBLL Instance { get { return lazy.Value; } }

        private AttendanceBLL()
        {

        }

        public void CreateAttendance(Attendance attendance)
        {
            ValidateAttendance(attendance);

            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            uow.AttendanceRepository.Insert(attendance);
            uow.Save();
        }

        public List<Attendance> GetAttendanceByClassAndParticipant(String classId, int participantId)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            IQueryable<Attendance> query =
                from attend in uow.AttendanceRepository.GetAll()
                where attend.CourseClass.ClassId == classId &&
                    attend.Participant.ParticipantId == participantId
                select attend;

            return query.ToList();
        }

        private void ValidateAttendance(Attendance attendance)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();

            // participant exists
            if (attendance.Participant == null || 
                uow.ParticipantRepository.GetById(attendance.Participant.ParticipantId) == null)
            {
                throw new BusinessException("invalid Participant");
            }

            // class exists
            if (attendance.CourseClass != null) 
            {
                CourseClass cl = uow.CourseClassRepository.GetById(attendance.CourseClass.ClassId);
                if (cl == null)
                {
                    throw new BusinessException("invalid Class");
                }

                // date check
                if (attendance.ClassDate.CompareTo(cl.StartDate) < 0 ||
                    attendance.ClassDate.CompareTo(cl.EndDate) > 0)
                {
                    throw new BusinessException("invalid Date");
                }
            }
            else
            {
                throw new BusinessException("invalid Class");
            }

            // student  enrolled class
            if (uow.RegistrationRepository.GetAll()
                .Where(x=>
                    x.Participant.ParticipantId == attendance.Participant.ParticipantId &&
                    x.CourseClass.ClassId == attendance.CourseClass.ClassId &&
                    x.Status == RegistrationStatus.Successful
                 ).Count() == 0 )
            {
                throw new BusinessException("student not in this class");
            }
            
            // previous record exists
            if (GetCountByClassAndParticipantAndDate(
                attendance.CourseClass.ClassId,
                attendance.Participant.ParticipantId,
                attendance.ClassDate) != 0)
            {
                throw new BusinessException("Attendance already exist");
            }
        }

        public int GetCountByClassAndParticipantAndDate(String classId, int participantId, DateTime date)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            IQueryable<Attendance> query =
                from attend in uow.AttendanceRepository.GetAll()
                where attend.CourseClass.ClassId == classId &&
                    attend.Participant.ParticipantId == participantId &&
                    attend.ClassDate == date
                select attend;

            return query.Count();
        }

    }
}

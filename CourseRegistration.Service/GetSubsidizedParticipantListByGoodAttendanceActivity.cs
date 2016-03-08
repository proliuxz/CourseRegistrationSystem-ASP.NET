using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;


using CourseRegistration.Models;
using CourseRegistration.BLL;

namespace CourseRegistration.Service
{

    public sealed class GetSubsidizedParticipantListByGoodAttendanceActivity : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> classId { get; set; }
        public OutArgument<List<SvcStudent>> obligationFulfillList { get; set; }
        public OutArgument<List<SvcStudent>> obligationViolateList { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            string classId = context.GetValue(this.classId);
            List<SvcStudent> obligeFulfillList = context.GetValue(this.obligationFulfillList);
            List<SvcStudent> obligeViolateList = context.GetValue(this.obligationViolateList);
            CourseClass courseClass = CourseClassBLL.Instance.GetCourseClassById(classId);
            List<Participant> participantList = CourseClassBLL.Instance.GetStudentsByClassId(classId);
            obligeFulfillList = new List<SvcStudent>();
            obligeViolateList = new List<SvcStudent>();
            foreach(Participant p in participantList)
            {
                double numClassesAttended = AttendanceBLL.Instance.GetAttendanceByClassAndParticipant(classId, p.ParticipantId).Count;
                double numClassesHeld = courseClass.Course.NumberOfDays;
                double attendancePercent = (numClassesAttended/numClassesHeld)*100.0;
                SvcStudent stud = new SvcStudent(p.ParticipantId, p.IdNumber, p.FullName, (Gender)p.Gender, p.Nationality, p.Email, p.ContactNumber);
                if (attendancePercent >= 80 && p.Nationality.ToString().ToUpper() == "SINGAPORE")
                    obligeFulfillList.Add(stud);
                else
                    obligeViolateList.Add(stud);
            }
            obligationFulfillList.Set(context, obligeFulfillList);
            obligationViolateList.Set(context, obligeViolateList);
        }
    }
}

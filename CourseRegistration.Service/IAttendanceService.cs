using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CourseRegistration.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAttendanceService" in both code and config file together.
    [ServiceContract]
    public interface IAttendanceService
    {
        [OperationContract]
        List<SvcStudent> GetStudentList(DateTime date, String classId);

        [OperationContract]
        Result SubmitAttendance(int participantId, String classId);
    }
}

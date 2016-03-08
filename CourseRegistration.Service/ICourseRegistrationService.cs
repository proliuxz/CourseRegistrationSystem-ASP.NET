using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CourseRegistration.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICourseRegistrationService" in both code and config file together.
    [ServiceContract]
    public interface ICourseRegistrationService
    {
        [OperationContract]
        Result RegisterCourseForEmployee(SvcParticipant svcParticipant, SvcCourseClass svcCourseClass);
    }
}

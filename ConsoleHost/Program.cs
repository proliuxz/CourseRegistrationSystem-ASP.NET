using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            
                using (System.ServiceModel.ServiceHost host_Attendance_Service = new System.ServiceModel.ServiceHost(typeof(CourseRegistration.Service.AttendanceService)))
                using (System.ServiceModel.ServiceHost host_Course_Registration_Service = new System.ServiceModel.ServiceHost(typeof(CourseRegistration.Service.CourseRegistrationService)))
                {
                    try
                   {
                    host_Attendance_Service.Open();
                    host_Course_Registration_Service.Open();
                    Console.WriteLine("Host started @ " + DateTime.Now.ToString());
                    Console.ReadLine();
                   }
                    catch(Exception exception)
                     {
                        Console.WriteLine(" Exception Occured :"+ exception.Message.ToString());
                     }
                   finally
                   {
                     if(host_Attendance_Service!=null && host_Attendance_Service.State==CommunicationState.Opened)
                     {
                         host_Attendance_Service.Close();
                     }
                     if (host_Course_Registration_Service != null && host_Course_Registration_Service.State == CommunicationState.Opened)
                     {
                         host_Course_Registration_Service.Close();
                     }
                   }
                }
           
            
        }
    }
}

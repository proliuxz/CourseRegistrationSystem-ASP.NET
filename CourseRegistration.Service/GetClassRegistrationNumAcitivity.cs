using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using CourseRegistration.Models;
using CourseRegistration.BLL;


namespace CourseRegistration.Service
{

    public sealed class GetClassRegistrationNumActivity : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> classId { get; set; }

        public OutArgument<int> registrationNum { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            string classId = context.GetValue(this.classId);

            CourseClass cl = CourseClassBLL.Instance.GetCourseClassById(classId);
            if (cl == null)
            {

            }
            else
            {
                registrationNum.Set(context, cl.Registrations.Count());
            }

            
        }
    }
}

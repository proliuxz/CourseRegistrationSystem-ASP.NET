using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using CourseRegistration.Models;
using CourseRegistration.BLL;

namespace CourseRegistration.Service
{

    public sealed class ConfirmationActivity : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> ClassId { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            string classID = context.GetValue(this.ClassId);
            CourseClassBLL.Instance.ConfirmClass(classID);
            Console.WriteLine("confirm");
            System.Diagnostics.Debug.WriteLine("confirm");
        }
    }
}

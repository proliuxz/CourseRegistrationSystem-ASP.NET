using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.BLL
{
    public class BusinessException : Exception
    {
        public String ErrorMessage { set; get; }

        public BusinessException(String errMsg)
        {
             this.ErrorMessage = errMsg;
        }

        public override string ToString()
        {
            return this.ErrorMessage;
        }

    }
}

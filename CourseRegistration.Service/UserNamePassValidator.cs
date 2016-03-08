using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using CourseRegistration.Models;
using CourseRegistration.BLL;


namespace CourseRegistration.Service
{
    public class UserNamePassValidator : System.IdentityModel.Selectors.UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (userName == null || password == null)
            {
                throw new ArgumentNullException();
            }

            if (UserBLL.Instance.ValidateUser(userName, password) == true)
            {
                throw new FaultException("Incorrect Username or Password");
            }
        }

    }
}
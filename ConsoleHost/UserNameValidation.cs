using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel;

namespace ConsoleHost
{
    class UserNameValidation : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {

            if (userName == null || password == null)
            {
                throw new ArgumentNullException();
            }

            if (CourseRegistration.BLL.UserBLL.Instance.ValidateUser(userName, password) == false)
            {
                throw new FaultException("Incorrect Username or Password");
            }
        }

    }

    //public class RoleAuthorizationManager : ServiceAuthorizationManager
    //{
    //    protected override bool CheckAccessCore(OperationContext operationContext)
    //    {
    //         Find out the roleNames from the user database, for example, var roleNames = userManager.GetRoles(user.Id).ToArray();

    //        var roleNames = new string[] { "CourseAdmin", "Individual" };

    //        operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] =
    //            new System.Security.Principal.GenericPrincipal(operationContext.ServiceSecurityContext.PrimaryIdentity, roleNames);
    //        return true;
    //    }

    //}
}

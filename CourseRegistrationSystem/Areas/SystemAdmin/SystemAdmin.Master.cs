using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CourseRegistrationSystem.Areas.SystemAdmin
{
    public partial class SystemAdmin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String username = HttpContext.Current.User.Identity.Name; 

            if (DateTime.Now.Hour < 12)
            {
                SayHello.Text = "Good Morning" + username;
            }
            else if (DateTime.Now.Hour < 17)
            {
                SayHello.Text = "Good Afternoon" + username;
            }
            else
            {
                SayHello.Text = "Good Afternoon" + username;
            }
        }
        protected void LogOff_Click(object sender, EventArgs e)
        {
            HttpContext.Current.GetOwinContext().Authentication.SignOut();
            Response.Redirect(FormsAuthentication.LoginUrl);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("/Home/Index");
        }
    }
}
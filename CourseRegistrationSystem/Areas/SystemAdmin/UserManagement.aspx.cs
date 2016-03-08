using CourseRegistration.BLL;
using CourseRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CourseRegistrationSystem.Areas.SystemAdmin
{
    public partial class UserManagement : System.Web.UI.Page
    {
        String selUserId;
        bool selAccountStatus;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public List<String> getAllRoles()
        {
            return CourseRegistration.BLL.RoleBLL.Instance.GetAllRoles().FindAll(role => role != Util.C_Role_CompanyHR && role != Util.C_Role_IndividualUser);
        }


        public List<ApplicationUser> getAllUsers()
        {
            return CourseRegistration.BLL.UserBLL.Instance.GetAllUsers();
        }

        public void CreateUser(String Id, String UserName, String PasswordHash, String Email, String PhoneNumber)
        {
            if (Session["SysAdmin_UserMgmt_Cre_User_SelRole"] != null)
                CourseRegistration.BLL.UserBLL.Instance.CreateAppUser(Email, Email, PhoneNumber, Session["SysAdmin_UserMgmt_Cre_User_SelRole"].ToString());
        }
        public void UpdateUser(String Id, String UserName, String PasswordHash, String Email, String PhoneNumber)
        {
            if (Session["SysAdmin_UserMgmt_Upd_User_isDisabled"] == null)
                Session["SysAdmin_UserMgmt_Upd_User_isDisabled"] = false;
            CourseRegistration.BLL.UserBLL.Instance.UpdateAppUser(Id, UserName, Email, PhoneNumber, bool.Parse(Session["SysAdmin_UserMgmt_Upd_User_isDisabled"].ToString()));
        }

        public void DeleteUser(String Id)
        {
            var user = CourseRegistration.BLL.UserBLL.Instance.GetAllUsers().FirstOrDefault(u => u.Id == Id);
            CourseRegistration.BLL.UserBLL.Instance.DeleteUser(user);
        }

        protected void RolesRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList rolelist = (RadioButtonList)sender;
            foreach (ListItem role in rolelist.Items)
            {
                if (role.Selected == true)
                {
                    Session["SysAdmin_UserMgmt_Cre_User_SelRole"] = role.Text;
                    break;
                }

            }
        }

        protected void UserGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("ResetPasswordCmd"))
            {
                String userId = e.CommandArgument.ToString();
                CourseRegistration.BLL.UserBLL.Instance.ResetUserPassword(userId);

            }
        }

        protected void DisableAccount_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox isDisabled = (CheckBox)sender;
            Session["SysAdmin_UserMgmt_Upd_User_isDisabled"] = isDisabled.Checked;
        }

        public bool determineIsDisableCheckState(object val)
        {
            if (val == null)
                return true;
            else
                return false;
        }

    }
}
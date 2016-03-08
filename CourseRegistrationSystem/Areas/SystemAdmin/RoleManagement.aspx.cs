using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CourseRegistrationSystem.Areas.SystemAdmin
{
    public partial class RoleManagement : System.Web.UI.Page
    {
        List<String> allRoleList = new List<String>();
        List<String> roleList = new List<String>();
        List<String> userList = new List<String>();
        List<String> userRoleList = new List<String>();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblStatusMsg.Text = "";
            allRoleList = CourseRegistration.BLL.RoleBLL.Instance.GetAllRoles();
            if (!Page.IsPostBack)
            {
                txtRoleName.Text = "";
                roleList = CourseRegistration.BLL.RoleBLL.Instance.GetAllRoles();
                userList = CourseRegistration.BLL.RoleBLL.Instance.GetAllUsers();
                lstBoxUser.DataSource = userList;
                lstBoxRoles.DataSource = roleList;
                lstBoxUserRoles.DataSource = userRoleList;
                drpdwnlstRoles.DataSource = allRoleList;
                lstBoxUser.DataBind();
                lstBoxRoles.DataBind();
                lstBoxUserRoles.DataBind();
                drpdwnlstRoles.DataBind();
                
            }
        }

        protected void lstBoxUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            String userName = lstBoxUser.SelectedValue;
            if (userName != null)
                updateData(userName);
        }

        protected void btnRemoveUserRole_Click(object sender, EventArgs e)
        {
            String userName = lstBoxUser.SelectedValue;
            String roleName = lstBoxUserRoles.SelectedValue; 
            if (userName != "" && roleName != "")
            {
                CourseRegistration.BLL.RoleBLL.Instance.UnAssignRoleToUser(userName, roleName);
                updateData(userName);
            }
        }

        private void updateData(String userName)
        {
            userRoleList = CourseRegistration.BLL.RoleBLL.Instance.GetUserRoles(userName);
            roleList = (allRoleList.Except(userRoleList)).ToList<String>();
            lstBoxRoles.DataSource = roleList;
            lstBoxUserRoles.DataSource = userRoleList;
            drpdwnlstRoles.DataSource = allRoleList;
            lstBoxRoles.DataBind();
            lstBoxUserRoles.DataBind();
            drpdwnlstRoles.DataBind();
        }

        protected void btnAddUserRole_Click(object sender, EventArgs e)
        {
            String userName = lstBoxUser.SelectedValue;
            String roleName = lstBoxRoles.SelectedValue;
            if (userName != "" && roleName != "")
            {
                CourseRegistration.BLL.RoleBLL.Instance.AssignRoleToUser(userName, roleName);
                updateData(userName);
            }
        }

        protected void btnCreateRole_Click(object sender, EventArgs e)
        {
            if(txtRoleName.Text.Trim() == "")                
            {
                lblStatusMsg.ForeColor = (Color)new ColorConverter().ConvertFromString("#FF0000");
                lblStatusMsg.Text = "Role Name Cannot be Empty";
            }                
            else
            {
                int status = CourseRegistration.BLL.RoleBLL.Instance.CreateRole(txtRoleName.Text);
                if(status == 0)
                {
                    lblStatusMsg.ForeColor = (Color)new ColorConverter().ConvertFromString("#FFA500");
                    lblStatusMsg.Text = "Role Name Already Exists!!!";
                }
                    
                else if(status == 1)
                {
                    String userName = lstBoxUser.SelectedValue;
                    if (userName != "")
                    {
                        allRoleList = CourseRegistration.BLL.RoleBLL.Instance.GetAllRoles();
                        updateData(userName);
                    }
                        
                    lblStatusMsg.ForeColor = (Color)new ColorConverter().ConvertFromString("#008000");
                    lblStatusMsg.Text = "Role Created Successfully!!!";
                    txtRoleName.Text = "";
                }
            }
        }

        protected void btnDeleteRole_Click(object sender, EventArgs e)
        {
            String selRole = drpdwnlstRoles.SelectedValue;
            if(selRole == "")                
            {
                lblStatusMsg.ForeColor = (Color)new ColorConverter().ConvertFromString("#FF0000");
                lblStatusMsg.Text = "Role Name Cannot be Empty!!! Select Role To be deleted!!!";
            }                
            else
            {
                CourseRegistration.BLL.RoleBLL.Instance.DeleteRole(selRole);
                String userName = lstBoxUser.SelectedValue;
                if (userName != "")
                {
                    allRoleList = CourseRegistration.BLL.RoleBLL.Instance.GetAllRoles();
                    updateData(userName);
                }
                        
                lblStatusMsg.ForeColor = (Color)new ColorConverter().ConvertFromString("#008000");
                lblStatusMsg.Text = "Role '" + selRole + "' Deleted Successfully!!!";
            }
        }
    }
}
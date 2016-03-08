using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CourseRegistration.BLL;
using CourseRegistration.Models;
using CourseRegistrationSystem.Areas.CourseAdmin.Shared;

namespace CourseRegistrationSystem.Areas.CourseAdmin.RegistrationManagement
{
    public partial class RegistrationList : System.Web.UI.Page
    {
        String classID;
        protected void Page_Load(object sender, EventArgs e)
        {
            classID = WebFormHelper.GetSessionFieldAndRemove(Session, WebFormHelper.C_PrimaryKey, "");
            if (!Page.IsPostBack)
            {
                Bind_CategoryList();
                Bind_CourseList();
                Bind_ClassList();
                DropDownClass.SelectedValue = classID;
                Bind_RegistrationList();
            }
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            Bind_RegistrationList();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Bind_RegistrationList();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow){
                Object obj = DataBinder.Eval(e.Row.DataItem, "CourseClass.StartDate");
                if (obj != null)
                {
                    String classStartDate = obj.ToString();

                    if (DateTime.Compare(DateTime.Parse(classStartDate), DateTime.Now) < 0)
                    {
                        ((Button)e.Row.Cells[0].FindControl("BtnConfirm")).Visible = false;
                        ((Button)e.Row.Cells[0].FindControl("BtnCancel")).Visible = false;
                    }
                    else
                    {
                        ((Button)e.Row.Cells[0].FindControl("BtnConfirm")).Visible = true;
                        ((Button)e.Row.Cells[0].FindControl("BtnCancel")).Visible = true;
                    }
                }
            }
            
        }

        protected void DropDownCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_CourseList();
            Bind_ClassList();
            Bind_RegistrationList();
        }

        protected void DropDownCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_ClassList();
            Bind_RegistrationList();
        }

        protected void DropDownClass_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            Bind_RegistrationList();
        }

        protected void BTNCONFIRM_Click(object sender, EventArgs e)
        {
            int RegistrationId = Int32.Parse(((Button)sender).CommandArgument.ToString());
            Registration r = RegistrationBLL.Instance.getRegistrationById(RegistrationId);
            r.Status = RegistrationStatus.Successful;
            RegistrationBLL.Instance.UpdateRegistration(r);

            Bind_RegistrationList();
        }

        protected void BTNCANCEL_Click(object sender, EventArgs e)
        {
            int RegistrationId = Int32.Parse(((Button)sender).CommandArgument.ToString());
            Registration r = RegistrationBLL.Instance.getRegistrationById(RegistrationId);
            r.Status = RegistrationStatus.Cancel;
            RegistrationBLL.Instance.UpdateRegistration(r);

            Bind_RegistrationList();
        }

        protected void BTNDETAIL_Click(object sender, EventArgs e)
        {
            String RegistrationId = ((Button)sender).CommandArgument.ToString();
            Session.Add(WebFormHelper.C_PrimaryKey, RegistrationId);
            Server.Transfer("RegistrationDetail.aspx");
        }
        protected void BTNVIEW_Click(object sender, EventArgs e)
        {
            String courseCode = ((LinkButton)sender).CommandArgument.ToString();
            Session.Add(WebFormHelper.C_PrimaryKey, courseCode);
            Session.Add(WebFormHelper.C_PageMode, WebFormHelper.C_View_Mode);
            Server.Transfer("../CourseManagement/CourseDetail.aspx");
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            this.DropDownCategory.SelectedValue = Util.C_String_All_Select;
            this.DropDownCourse.SelectedValue = Util.C_String_All_Select;
            this.DropDownClass.SelectedValue = Util.C_String_All_Select;
            
            this.TxtParticipantName.Text = "";
            this.TxtParticipantIdNumber.Text= "";
            this.TxtParticipantCompanyName.Text = "";

            Bind_RegistrationList();
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Bind_RegistrationList();
        }

        private void Bind_RegistrationList()
        {
            List<Registration> list;
            String categoryID = this.DropDownCategory.SelectedValue;
            String courseCode = this.DropDownCourse.SelectedValue;
            String classID = this.DropDownClass.SelectedValue;
            String partName = this.TxtParticipantName.Text;
            String partIdNo = this.TxtParticipantIdNumber.Text;
            String partCompanyName = this.TxtParticipantCompanyName.Text;
            
            list = RegistrationBLL.Instance.getRegistrationByConds(
                categoryID, courseCode, classID,
                partName, partIdNo, partCompanyName);
            
            if (list.Count() == 0)
            {
                list.Add(new Registration());

                this.GridView1.DataSource = list;
                this.GridView1.DataBind();

                int columnCount = this.GridView1.Columns.Count;

                this.GridView1.Rows[0].Cells.Clear();
                this.GridView1.Rows[0].Cells.Add(new TableCell());
                this.GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
                this.GridView1.Rows[0].Cells[0].Text = "No Record";
                this.GridView1.Rows[0].Cells[0].Style.Add("text-align", "center");
            }
            else
            {

                this.GridView1.DataSource = list;
                this.GridView1.DataBind();

                int pNo = this.GridView1.PageIndex;
                int pSize = this.GridView1.PageSize;
                String RecNo = (pNo * pSize + 1).ToString();
                RecNo += "~";
                if ((pNo + 1) * pSize < list.Count())
                {
                    RecNo += ((pNo + 1) * pSize).ToString();
                }
                else
                {
                    RecNo += list.Count().ToString();
                }
                RecNo += " / ";
                RecNo += list.Count().ToString();
                this.LblRecNo.InnerText = RecNo;
            }
        }

        private void Bind_CategoryList()
        {
            List<Category> list;
            list = CategoryBLL.Instance.GetAllCategories();

            this.DropDownCategory.DataSource = list;
            this.DropDownCategory.DataValueField = "CategoryId";
            this.DropDownCategory.DataTextField = "CategoryName";
            this.DropDownCategory.DataBind();

            ListItem AllItem = new ListItem("Select All", Util.C_String_All_Select);
            this.DropDownCategory.Items.Insert(0, AllItem);

        }

        private void Bind_CourseList()
        {
            String categoryID = this.DropDownCategory.SelectedValue;

            List<Course> list;
            list = CourseBLL.Instance.getCoursesByConds(categoryID);

            this.DropDownCourse.DataSource = list;
            this.DropDownCourse.DataValueField = "CourseCode";
            this.DropDownCourse.DataTextField = "CourseTitle";
            this.DropDownCourse.DataBind();

            ListItem AllItem = new ListItem("Select All", Util.C_String_All_Select);
            this.DropDownCourse.Items.Insert(0, AllItem);

        }

        private void Bind_ClassList()
        {
            String categoryID = this.DropDownCategory.SelectedValue;
            String courseCode = this.DropDownCourse.SelectedValue;

            List<CourseClass> list;
            list = CourseClassBLL.Instance.GetClassesByConds(categoryID, courseCode);

            this.DropDownClass.DataSource = list;
            this.DropDownClass.DataValueField = "ClassId";
            this.DropDownClass.DataTextField = "ClassId";
            this.DropDownClass.DataBind();

            ListItem AllItem = new ListItem("Select All", Util.C_String_All_Select);
            this.DropDownClass.Items.Insert(0, AllItem);

        }

        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CourseRegistration.BLL;
using CourseRegistration.Models;
using CourseRegistrationSystem.Areas.CourseAdmin.Shared;


namespace CourseRegistrationSystem.Areas.CourseAdmin.ClassManagement
{
    public partial class CourseList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bind_CategoryList();
                Bind_CoursesList();
            }
            
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            Bind_CoursesList();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Bind_CoursesList();
        }

        protected void DropDownCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_CoursesList();
        }

        protected void BTNCREATE_Click(object sender, EventArgs e)
        {
            Session.Add(WebFormHelper.C_PageMode, WebFormHelper.C_New_Mode);
            Server.Transfer("CourseDetail.aspx");
        }
        protected void BTNVIEW_Click(object sender, EventArgs e)
        {
            String courseCode = ((LinkButton)sender).CommandArgument.ToString();
            Session.Add(WebFormHelper.C_PrimaryKey, courseCode);
            Session.Add(WebFormHelper.C_PageMode, WebFormHelper.C_View_Mode);
            Server.Transfer("CourseDetail.aspx");
        }

        protected void BTNEDIT_Click(object sender, EventArgs e)
        {
            String courseCode = ((Button)sender).CommandArgument.ToString();
            Session.Add(WebFormHelper.C_PrimaryKey, courseCode);
            Session.Add(WebFormHelper.C_PageMode, WebFormHelper.C_Edit_Mode);
            Server.Transfer("CourseDetail.aspx");
        }
        protected void BTNDELETE_Click(object sender, EventArgs e)
        {

            String courseCode = ((Button)sender).CommandArgument.ToString();
            Course course = CourseBLL.Instance.GetCourseByCode(courseCode);
            CourseBLL.Instance.DeleteCourse(course);
            Bind_CoursesList();
        }
        protected void BTNCLASS_Click(object sender, EventArgs e)
        {
            String courseCode = ((Button)sender).CommandArgument.ToString();
            Session.Add(WebFormHelper.C_PrimaryKey, courseCode);
            Server.Transfer("../ClassManagement/ClassList.aspx");
        }
       
        private void Bind_CoursesList()
        {
            List<Course> list;
            String categoryID = this.DropDownCategory.SelectedValue;
            if (categoryID == Util.C_String_All_Select)
            {
                // Select ALL
                list = CourseBLL.Instance.GetAllCourses();
            }
            else
            {
                list = CourseBLL.Instance.getCoursesByConds(categoryID);
            }

            if (list.Count() == 0)
            {
                list.Add(new Course());

                this.GridView1.DataSource = list;
                this.GridView1.DataBind();
                
                int columnCount = this.GridView1.Columns.Count;

                this.GridView1.Rows[0].Cells.Clear();
                this.GridView1.Rows[0].Cells.Add(new TableCell());
                this.GridView1.Rows[0].Cells[0].ColumnSpan = columnCount;
                this.GridView1.Rows[0].Cells[0].Text = "No Record";
                this.GridView1.Rows[0].Cells[0].Style.Add("text-align", "center");
            }else{
                
                this.GridView1.DataSource = list;
                this.GridView1.DataBind();

                int pNo = this.GridView1.PageIndex;
                int pSize = this.GridView1.PageSize;
                String RecNo = (pNo * pSize + 1).ToString();
                RecNo += "~";
                if ((pNo + 1) * pSize < list.Count())
                {
                    RecNo += ((pNo + 1) * pSize).ToString();
                }else{
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

       
        
    }
}
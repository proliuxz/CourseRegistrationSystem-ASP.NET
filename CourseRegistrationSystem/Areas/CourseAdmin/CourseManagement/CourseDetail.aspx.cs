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
    public partial class CourseDetail : System.Web.UI.Page
    {
        
        String PageMode;
        protected void Page_Load(object sender, EventArgs e)
        {

            PageMode = WebFormHelper.GetSessionFieldAndRemove(Session ,WebFormHelper.C_PageMode, WebFormHelper.C_New_Mode);
            if (!Page.IsPostBack)
            {
                Bind_Categories();
                Bind_Instructors();

                String courseCode = WebFormHelper.GetSessionFieldAndRemove(Session, WebFormHelper.C_PrimaryKey, "");
                if (PageMode == WebFormHelper.C_New_Mode)
                {
                }
                else if (PageMode == WebFormHelper.C_Edit_Mode || PageMode == WebFormHelper.C_View_Mode)
                {
                    LoadDetailData(courseCode);
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            ControlDisplayMode();
        }

        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            String courseCode = this.TxtCourseCode.Text;
            try
            {
                SaveCourse(true);
                LoadDetailData(courseCode);
                PageMode = WebFormHelper.C_View_Mode;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                //Violation of PRIMARY KEY constraint
                if (ex.HResult == -2146233087)
                {
                    this.LblMessage.Text = string.Format(WebFormHelper.Err_Update_PK_Violation, "Course", courseCode);
                }
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            String courseCode = this.TxtCourseCode.Text;
            try
            {
                SaveCourse(false);
                LoadDetailData(courseCode);
                PageMode = WebFormHelper.C_View_Mode;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ex)
            {
                this.LblMessage.Text = string.Format(WebFormHelper.Err_Update_Concurrency, "Course", courseCode);
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Server.Transfer("CourseList.aspx");
        }



        private void Bind_Categories()
        {
            List<Category> categoryList = CategoryBLL.Instance.GetAllCategories();
            this.DropDownCategory.DataSource = categoryList;
            this.DropDownCategory.DataValueField = "CategoryId";
            this.DropDownCategory.DataTextField = "CategoryName";
            this.DropDownCategory.DataBind();
        }
        private void Bind_Instructors()
        {
            List<Instructor> instructorList = InstructorBLL.Instance.GetAllInstructors();
            this.ChkBoxListInstructors.DataSource = instructorList;
            this.ChkBoxListInstructors.DataValueField = "InstructorId";
            this.ChkBoxListInstructors.DataTextField = "InstructorName";
            this.ChkBoxListInstructors.DataBind();
        }

        private void LoadDetailData(String courseCode)
        {
            Course course = CourseBLL.Instance.GetCourseByCode(courseCode);
            if (course != null)
            {

                this.TxtCourseCode.Text = course.CourseCode;
                this.HidTimestamp.Value = System.Convert.ToBase64String(course.Timestamp);
                this.TxtCourseTitle.Text = course.CourseTitle;
                this.DropDownCategory.SelectedValue = course.Category.CategoryId.ToString();
                this.TxtDescription.Text = course.CourseDescription;
                this.TxtFee.Text = course.Fee.ToString();
                this.TxtNumberOfDays.Text = course.NumberOfDays.ToString();

                foreach (Instructor i in course.Instructors)
                {
                    this.ChkBoxListInstructors.Items.FindByValue(i.InstructorId.ToString()).Selected = true;
                }

                this.ChkBoxEnabled.Checked = course.enabled;
                this.TxtCreateDate.Text = course.CreateDate.ToString("dd-MMM-yyyy");
            }
        }

        private void SaveCourse(bool isNew)
        {
            Course course;
            if (isNew)
            {
                course = new Course();
                course.CourseCode = this.TxtCourseCode.Text;
            }
            else
            {
                course = CourseBLL.Instance.GetCourseByCode(this.TxtCourseCode.Text);
                if (!WebFormHelper.CheckConcurrency(course, this.HidTimestamp.Value))
                {
                    throw new System.Data.Entity.Infrastructure.DbUpdateConcurrencyException();
                }
            }

            course.CourseTitle = this.TxtCourseTitle.Text;

            Category category = CategoryBLL.Instance.GetCategoryById(int.Parse(this.DropDownCategory.SelectedValue));
            course.Category = category;

            course.CourseDescription = this.TxtDescription.Text;
            course.Fee = double.Parse(this.TxtFee.Text);
            course.NumberOfDays = int.Parse(this.TxtNumberOfDays.Text);

            course.Instructors.Clear();
            foreach (ListItem i in this.ChkBoxListInstructors.Items)
            {
                if (i.Selected)
                {
                    Instructor instructor = InstructorBLL.Instance.GetInstructorById(int.Parse(i.Value));
                    course.Instructors.Add(instructor);
                }
            }
            
            course.enabled = this.ChkBoxEnabled.Checked;

            if (isNew)
            {
                CourseBLL.Instance.CreateCourse(course);
            }
            else
            {
                CourseBLL.Instance.EditCourse(course);
            }
            
        }

        private void ControlDisplayMode()
        {
            if (PageMode == WebFormHelper.C_New_Mode)
            {
                this.EditPanel.Visible = false;
                this.ViewPanel.Visible = false;
            }
            else if (PageMode == WebFormHelper.C_Edit_Mode)
            {
                this.TxtCourseCode.Enabled = false;

                this.NewPanel.Visible = false;
                this.ViewPanel.Visible = false;
            }
            else if (PageMode == WebFormHelper.C_View_Mode)
            {
                this.TxtCourseCode.Enabled = false;
                this.TxtCourseTitle.Enabled = false;
                this.DropDownCategory.Enabled = false;
                this.TxtDescription.Enabled = false;
                this.TxtFee.Enabled = false;
                this.TxtNumberOfDays.Enabled = false;
                this.ChkBoxListInstructors.Enabled = false;
                this.ChkBoxEnabled.Enabled = false;
                this.TxtCreateDate.Enabled = false;

                this.NewPanel.Visible = false;
                this.EditPanel.Visible = false;
                this.ViewPanel.Visible = true;
            }
        }

        
    }

    
}
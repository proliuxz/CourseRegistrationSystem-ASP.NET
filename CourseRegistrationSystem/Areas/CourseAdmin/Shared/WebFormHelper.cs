using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using CourseRegistration.Models;

namespace CourseRegistrationSystem.Areas.CourseAdmin.Shared
{
    public static class WebFormHelper
    {

        public const String C_PageMode = "PageMode";
        public const String C_PrimaryKey = "PrimaryKey";

        public const String C_New_Mode = "NEW";
        public const String C_Edit_Mode = "EDIT";
        public const String C_View_Mode = "VIEW";

        public const String Err_Update_PK_Violation = "{0} primary key {1} already exists.";
        public const String Err_Update_Concurrency = "{0} {1} was updated by someone else.";


        public static String GetRequestField(System.Web.HttpRequest Request, String fieldId, String defaultVal)
        {
            String result = Request.QueryString[fieldId];

            result = Request.Form[fieldId];

            if (String.IsNullOrEmpty(result)) result = defaultVal;
            return result;
        }

        public static String GetSessionFieldAndRemove(System.Web.SessionState.HttpSessionState Session, String fieldId, String defaultVal)
        {
            Object obj = Session[fieldId];
            String result = "";
            if (obj == null)
            { }
            else
            {
                result = obj.ToString();
                Session.Remove(fieldId);
            }

            if (String.IsNullOrEmpty(result)) result = defaultVal;
            return result;
        }

        public static bool CheckConcurrency(BaseModel obj, String timestamp)
        {
            if (obj == null || !System.Convert.ToBase64String(obj.Timestamp).Equals(timestamp))
            {
                return false;
            }
            return true;
        }

        
    }
}
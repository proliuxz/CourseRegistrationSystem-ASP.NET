using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace CourseRegistration.BLL
{
    public class Util
    {
        public const string C_Role_SystemAdmin = "SystemAdmin";
        public const string C_Role_CourseAdmin = "CourseAdmin";
        public const string C_Role_Staff = "Staff";
        public const string C_Role_CompanyHR = "CompanyHR";
        public const string C_Role_IndividualUser = "IndividualUser";

        public const string C_String_All_Select = "";


        public static void SendEmail(String Address, String title, String message)
        {
            OutputToLocalDrive("D:\\MailTest\\" + Address+ "\\",
                title + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".eml", 
                Address,
                title,
                message);
        }

        private static void OutputToLocalDrive(String path,
                    String filename,
                    String Address,
                    String title, 
                    String message)
        {
            try
            {
                string strFolder = path;
                if (!System.IO.Directory.Exists(strFolder))
                {
                    System.IO.Directory.CreateDirectory(strFolder);
                }
                string strFileName = strFolder + filename;
                FileStream fs = new System.IO.FileStream(strFileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
                StreamWriter sw = new System.IO.StreamWriter(fs);
                sw.WriteLine(Address);
                sw.WriteLine(title);
                sw.WriteLine(message);
                sw.Flush();
                sw.Close();
                fs.Close();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static String GeneratePassword()
        {
            String pwd = "";
            String alphaStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            String numStr = "1234567890";
            char[] alphaNumList = (alphaStr.ToUpper() + alphaStr.ToLower() + numStr).ToCharArray();
            Random rnd = new Random();
            for (int i = 0; i < 9; i++)
            {
                int indexVal = rnd.Next(0, alphaNumList.Length);
                pwd = pwd + alphaNumList[indexVal];
            }
            return pwd;
        }

        

    }
}

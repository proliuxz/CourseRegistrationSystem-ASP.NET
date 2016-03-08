using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.IO;

using CourseRegistration.Models;
using CourseRegistration.BLL;

namespace CourseRegistration.Service
{

    public sealed class SendObligationReportToCourseAdminActivity : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<List<SvcStudent>> filteredList { get; set; }
        public InArgument<List<SvcStudent>> obligationFulfillList { get; set; }
        public InArgument<List<SvcStudent>> obligationViolateList { get; set; }
        public OutArgument<bool> status { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            List<SvcStudent> filteredList = context.GetValue(this.filteredList);
            List<SvcStudent> obligationFulfillList = context.GetValue(this.obligationFulfillList);
            List<SvcStudent> obligationViolateList = context.GetValue(this.obligationViolateList);

            //List<SvcStudent> matchList = new List<SvcStudent>();
            //matchList = (from studA in filteredList
            //            join studB in obligationFulfillList on studA.ParticipantId equals studB.ParticipantId
            //            where studA.ParticipantId == studB.ParticipantId
            //            select studA).ToList();
            //List<SvcStudent> diffList = new List<SvcStudent>();

            //diffList = (from studA in filteredList
            //            join studB in obligationFulfillList on studA.ParticipantId equals studB.ParticipantId
            //            where studA.ParticipantId != studB.ParticipantId
            //            select studA).ToList();
            try
            {
                List<SvcStudent> etxraViolateStud = new List<SvcStudent>();
                etxraViolateStud = obligationFulfillList.Except(filteredList).ToList();
                foreach (SvcStudent stud in etxraViolateStud)
                {
                    obligationViolateList.Add(stud);
                }
                string header = "TO: ";
                string subject = "Subject: Service Obligation Report";
                string body = "";
                string path = "D:\\ObligationReportMail.txt";
                List<ApplicationUser> userList = UserBLL.Instance.GetUsersByRole(Util.C_Role_CourseAdmin);
                foreach (ApplicationUser user in userList)
                    header += user.Email + ",";
                header=header.Substring(0, header.Length - 1);
                body += "Message:" + Environment.NewLine;
                body += Environment.NewLine + "List of students fulfilling the service obligation" + Environment.NewLine + Environment.NewLine;
                body += "Id" + "," + "NRIC/Passport" + "," + "ParticipantFullName" + "," + "Gender" + "," + "Email" + "," + "Nationality" + "," + "ContactNumber" + Environment.NewLine;
                foreach (SvcStudent stud in filteredList)
                {
                    body += stud.ParticipantId + ",";
                    body += stud.IdNumber + ",";
                    body += stud.FullName + ",";
                    body += stud.Gender + ",";
                    body += stud.Email + ",";
                    body += stud.Nationality + ",";
                    body += stud.ContactNumber + Environment.NewLine;
                }
                body += Environment.NewLine + "List of students NOT fulfilling the service obligation" + Environment.NewLine + Environment.NewLine;
                body += "Id" + "," + "NRIC/Passport" + "," + "ParticipantFullName" + "," + "Gender" + "," + "Email" + "," + "Nationality" + "," + "ContactNumber" + Environment.NewLine;
                foreach (SvcStudent stud in obligationViolateList)
                {
                    body += stud.ParticipantId + ",";
                    body += stud.IdNumber + ",";
                    body += stud.FullName + ",";
                    body += stud.Gender + ",";
                    body += stud.Email + ",";
                    body += stud.Nationality + ",";
                    body += stud.ContactNumber + Environment.NewLine;
                }
                File.WriteAllText(path, header + Environment.NewLine + subject + Environment.NewLine + body + Environment.NewLine);
                status.Set(context, true);
            }
            catch
            {
                status.Set(context, false);
            }
        }
    }
}

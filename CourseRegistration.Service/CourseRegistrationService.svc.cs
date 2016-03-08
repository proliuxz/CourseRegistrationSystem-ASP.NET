using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CourseRegistration.Models;
using CourseRegistration.BLL;
using Microsoft.AspNet.Identity;


namespace CourseRegistration.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CourseRegistrationService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CourseRegistrationService.svc or CourseRegistrationService.svc.cs at the Solution Explorer and start debugging.
    public class CourseRegistrationService : ICourseRegistrationService
    {
        public Result RegisterCourseForEmployee(SvcParticipant svcParticipant, SvcCourseClass svcClass)
        {

            try
            {
                String userName = OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name;
                ApplicationUser user = UserBLL.Instance.GetUserByName(userName);

                CompanyHR loginHR = CompanyHRBLL.Instance.GetCompanyHRByUserId(user.Id);
                Company company = loginHR.Company;

                // setting value
                Registration newReg = new Registration();
                Participant regParticipant = new Participant();
                
                regParticipant.IdNumber = svcParticipant.IdNumber;
                regParticipant.Salutation = svcParticipant.Salutation;
                regParticipant.EmploymentStatus = svcParticipant.EmploymentStatus;
                regParticipant.JobTitle = svcParticipant.JobTitle;
                regParticipant.Department = svcParticipant.Department;
                regParticipant.FullName = svcParticipant.FullName;
                regParticipant.Gender = (CourseRegistration.Models.Gender)svcParticipant.Gender;
                regParticipant.Nationality = svcParticipant.Nationality;
                regParticipant.DateOfBirth = svcParticipant.DateOfBirth;
                regParticipant.Email = svcParticipant.Email;
                regParticipant.ContactNumber = svcParticipant.ContactNumber;
                regParticipant.DietaryRequirement = svcParticipant.DietaryRequirement;
                regParticipant.OrganizationSize = (CourseRegistration.Models.OrganizationSize)svcParticipant.OrganizationSize;
                regParticipant.SalaryRange = (CourseRegistration.Models.SalaryRange)svcParticipant.SalaryRange;
        
                // company
                regParticipant.Company = company;
                regParticipant.CompanyName = company.CompanyName;
                
                newReg.Participant = regParticipant;

                // class
                newReg.CourseClass = CourseClassBLL.Instance.GetCourseClassById(svcClass.ClassId);

                // registration
                newReg.Sponsorship = Sponsorship.Company;
                newReg.DietaryRequirement = svcParticipant.DietaryRequirement;
                newReg.OrganizationSize = (CourseRegistration.Models.OrganizationSize)svcParticipant.OrganizationSize;

                newReg.BillingAddress = company.BillingAddress;
                newReg.BillingPersonName = company.BillingPersonName;
                newReg.BillingAddressCountry = company.BillingAddressCountry;
                newReg.BillingAddressPostalCode = company.BillingAddressPostalCode;
                newReg.Status = Models.RegistrationStatus.Pending;


                List<Registration> regList = new List<Registration>();
                regList.Add(newReg);
                
                
                // RegistrationBLL.Instance.CreateForCompanyEmployee(regList);
                //List<Registration> failedList1 = RegistrationBLL.Instance.CreateForCompanyEmployee(regList);
                int size =RegistrationBLL.Instance.CreateForCompanyEmployee(regList).Count();
                if(size!=0)
                {
                    return new Result(false,"The candidate is already registered.");
                }
            }
            catch (Exception e)
            {
                return new Result(false, e.ToString());
               
            }
            return new Result(true,"We have successfully registered the candidate");
        }
    }
}

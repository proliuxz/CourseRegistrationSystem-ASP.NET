using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseRegistration.Models;
using CourseRegistration.BLL;
using Microsoft.AspNet.Identity;

namespace CourseRegistrationSystem.Controllers
{
    [Authorize(Roles = Util.C_Role_CompanyHR)]
    public class CompanyHRController : Controller
    {

        #region Company Functions

        // GET: CompanyHR/CompanyHRdetails
        public ActionResult CompanyHRdetails()
        {
            return PartialView();
        }
        // GET: CompanyHR/CompanyDetails
        public ActionResult CompanyDetails()
        {
            return PartialView();
        }

        // GET: CompanyHR/CompanyInfo
        public ActionResult CompanyInfo()
        {
            String loginUserId = User.Identity.GetUserId();
            CompanyHR loginHR = CompanyHRBLL.Instance.GetCompanyHRByUserId(loginUserId);
            return View(loginHR);
        }

        // POST: CompanyHR/CompanyInfo/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyInfo(CompanyHR companyHr,Company company)
        {
            String loginUserId = User.Identity.GetUserId();
            CompanyHR loginHR = CompanyHRBLL.Instance.GetCompanyHRByUserId(loginUserId);
            //loginHR = companyHr;
            loginHR.Name = companyHr.Name;
            loginHR.ContactNumber = companyHr.ContactNumber;
            loginHR.FaxNumber = companyHr.FaxNumber;
            loginHR.JobTitle = companyHr.JobTitle;
            loginHR.Company.BillingAddress = company.BillingAddress;
            loginHR.Company.BillingAddressCountry = company.BillingAddressCountry;
            loginHR.Company.BillingAddressPostalCode = company.BillingAddressPostalCode;
            loginHR.Company.BillingPersonName = company.BillingPersonName;
            loginHR.Company.CompanyAddress = company.CompanyAddress;
            loginHR.Company.CompanyName = company.CompanyName;
            loginHR.Company.CompanyUEN = company.CompanyUEN;
            loginHR.Company.Country = company.Country;
            loginHR.Company.OrganizationSize = company.OrganizationSize;
            loginHR.Company.PostalCode = company.PostalCode;

            try
            {
                // TODO: Add update logic here
                //loginHR.Company = company;
                CourseRegistration.BLL.CompanyHRBLL.Instance.EditCompanyHR(loginHR);

                return RedirectToAction("CompanyInfo");
            }
            catch
            {
                return View(loginHR);
            }
        }

        #endregion

        #region Employee Functions

        // GET: CompanyHR/EmployeeList
        public ActionResult EmployeeList()
        {
            String loginUserId = User.Identity.GetUserId();
            CompanyHR loginHR = CompanyHRBLL.Instance.GetCompanyHRByUserId(loginUserId);
            List<Participant> participantList = loginHR.Company.Employees;
            return View(participantList);
        }

        // GET: CompanyHR/EmployeeDetails/5
        public ActionResult EmployeeDetails(int id)
        {
            Participant participant = ParticipantBLL.Instance.GetParticipantById(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }

        // GET: CompanyHR/EmployeeCreate
        [Authorize(Roles = "CompanyHR")]
        public ActionResult EmployeeCreate()
        {
            return View();
        }

        // POST: CompanyHR/EmployeeCreate
        [HttpPost]
        public ActionResult EmployeeCreate(Participant participant)
        {
            try
            {
                String loginUserId = User.Identity.GetUserId();
                CompanyHR loginHR = CompanyHRBLL.Instance.GetCompanyHRByUserId(loginUserId);
                ParticipantBLL.Instance.CreateForCompanyEmployee(loginHR.Company, participant);

                return RedirectToAction("EmployeeList");
            }
            catch
            {
                return View();
            }
        }

        // GET: CompanyHR/EmployeeEdit/5
        public ActionResult EmployeeEdit(int id)
        {
            Participant participant = ParticipantBLL.Instance.GetParticipantById(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }

        // POST: CompanyHR/EmployeeEdit/5
        [HttpPost]
        public ActionResult EmployeeEdit(int id, FormCollection collection)
        {
            try
            {
                Participant participant = ParticipantBLL.Instance.GetParticipantById(id);
                UpdateModel(participant);
                ParticipantBLL.Instance.EditParticipant(participant);

                return RedirectToAction("EmployeeList");
            }
            catch
            {
                return View();
            }
        }

        // GET: CompanyHR/EmployeeDelete/5
        public ActionResult EmployeeDelete(int id)
        {
            Participant participant = ParticipantBLL.Instance.GetParticipantById(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }

        // POST: CompanyHR/EmployeeDelete/5
        [HttpPost]
        public ActionResult EmployeeDelete(int id, FormCollection collection)
        {
            try
            {
                Participant participant = ParticipantBLL.Instance.GetParticipantById(id);
                UpdateModel(participant);
                ParticipantBLL.Instance.DeleteParticipant(participant);

                return RedirectToAction("EmployeeList");
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region Registration Functions

        public ActionResult RegistrationList()
        {
            String loginUserId = User.Identity.GetUserId();
            CompanyHR loginHR = CompanyHRBLL.Instance.GetCompanyHRByUserId(loginUserId);
            List<Registration> RegList = RegistrationBLL.Instance.getRegistrationByConds(
                Util.C_String_All_Select, Util.C_String_All_Select, Util.C_String_All_Select,
                Util.C_String_All_Select, Util.C_String_All_Select, loginHR.Company.CompanyName);
            return View(RegList);
        }

        public ActionResult RegistrationDetails(int ID)
        {
            Registration Reg = RegistrationBLL.Instance.getRegistrationById(ID);
            if (Reg == null)
            {
                return HttpNotFound();
            }
            return View(Reg);
        }

        public ActionResult RegistrationDelete(int ID)
        {
            Registration Reg = RegistrationBLL.Instance.getRegistrationById(ID);
            if (Reg == null)
            {
                return HttpNotFound();
            }
            return View(Reg);
        }

        [HttpPost]
        public ActionResult RegistrationDelete(int id, FormCollection collection)
        {
            try
            {
                Registration Reg = RegistrationBLL.Instance.getRegistrationById(id);
                UpdateModel(Reg);
                RegistrationBLL.Instance.DeleteRegistration(Reg);
                return RedirectToAction("RegistrationList");
            }
            catch
            {
                return View();
            }
        }
        #endregion
    }
}

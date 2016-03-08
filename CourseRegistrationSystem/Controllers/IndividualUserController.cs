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
    [Authorize(Roles = Util.C_Role_IndividualUser)]
    public class IndividualUserController : Controller
    {

        #region PersonalInfo Functions

        
        public ActionResult PersonalInfo()
        {
            String loginUserId = User.Identity.GetUserId();
            Participant individualUser = ParticipantBLL.Instance.GetParticipantByUserId(loginUserId);
            return View(individualUser);
        }


        public ActionResult PersonalInfoEdit()
        {
            String loginUserId = User.Identity.GetUserId();
            Participant individualUser = ParticipantBLL.Instance.GetParticipantByUserId(loginUserId);
            return View(individualUser);
        }

        [HttpPost]
        public ActionResult PersonalInfoEdit(int id, FormCollection collection)
        {
            try
            {
                String loginUserId = User.Identity.GetUserId();
                Participant individualUser = ParticipantBLL.Instance.GetParticipantByUserId(loginUserId);
                UpdateModel(individualUser);
                ParticipantBLL.Instance.EditParticipant(individualUser);
                return RedirectToAction("PersonalInfo");
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
            Participant participant = ParticipantBLL.Instance.GetParticipantByUserId(loginUserId);
            List<Registration> RegList = participant.Registrations;
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

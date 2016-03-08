using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using CourseRegistration.Models;
using CourseRegistration.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseRegistration.BLL
{
    public class RegistrationBLL
    {
        private static readonly Lazy<RegistrationBLL> lazy =
        new Lazy<RegistrationBLL>(() => new RegistrationBLL());

        private RegistrationBLL() { }

        public static RegistrationBLL Instance { get { return lazy.Value; } }

        public Boolean CreateForIndividualUser(Registration reg)
        {
            //validate
            if (ValidateRegistration(reg))
            {
                IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
                // Participant exists or not
                IQueryable<Participant> query =
                    from participant in uow.ParticipantRepository.GetAll()
                    where
                        (participant.IdNumber == reg.Participant.IdNumber && participant.Company == null)
                    select participant;


                if (query.Count() == 0)
                {
                    // Participant does not exist
                    // Create New User
                    var userRole = uow.AppRoleManager.FindByName("IndividualUser");
                    ApplicationUser user = new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = reg.Participant.IdNumber,
                        Email = reg.Participant.Email,
                        PhoneNumber = reg.Participant.ContactNumber,
                        isSysGenPassword = true
                    };
                    user.Roles.Add(new IdentityUserRole { RoleId = userRole.Id, UserId = user.Id });

                    String pwd = Util.GeneratePassword();

                    uow.AppUserManager.Create(user, pwd);

                    // Create New Participant
                    reg.Participant.AppUser = user;
                    uow.ParticipantRepository.Insert(reg.Participant);

                }
                else
                {
                    // already exist
                    // get participant record with same IdNo and Company == null
                    Participant p = query.Single();
                    // update existing one
                    UpdateRegParticipant(p, reg.Participant);
                    uow.ParticipantRepository.Edit(p);

                    reg.Participant = p;
                }

                // Create Registration
                uow.RegistrationRepository.Insert(reg);

                uow.Save();
                return true;
            }
            return false;
        }

        public List<Registration> CreateForCompanyEmployee(List<Registration> registrationList)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            List<Registration> failedList = new List<Registration>();

            foreach (Registration reg in registrationList)
            {
                if (ValidateRegistration(reg))
                {
                    // Participant exists or not
                    IQueryable<Participant> query =
                        from participant in uow.ParticipantRepository.GetAll()
                        where
                            (participant.IdNumber == reg.Participant.IdNumber &&
                            participant.Company.CompanyId == reg.Participant.Company.CompanyId)
                        select participant;

                    // Participant does not exist
                    if (query.Count() == 0)
                    {
                        // EF automatically Create Participant
                    }
                    else
                    {
                        // get participant record with same IdNo and Company == passed
                        Participant p = query.Single();
                        // update existing one
                        UpdateRegParticipant(p, reg.Participant);
                        uow.ParticipantRepository.Edit(p);

                        reg.Participant = p;

                    }

                    // insert registration
                    uow.RegistrationRepository.Insert(reg);
                }
                else
                {
                    failedList.Add(reg);
                } 
            }
            uow.Save();
            return failedList;    
        }

        public Boolean validateIfRegistered(string classId, int pId)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            Registration r = uow.RegistrationRepository.GetAll().Where(x => x.CourseClass.ClassId == classId && x.Participant.ParticipantId == pId).FirstOrDefault();
            if (r != null)
            {
                return true;
            }
            return false;
        }

        public Registration getRegistrationById(int id)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.RegistrationRepository.GetAll().Where(x=>x.RegistrationId==id)
                .Include(x=>x.Participant)
                .Include(x=> x.CourseClass)
                .Single();
        }

        public List<Registration> getAllRegistration()
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.RegistrationRepository.GetAll().ToList();
        }

        public List<Registration> getRegistrationByConds(
            String categoryID, String courseCode, String classID,
            String participantName, String participantIdNumber, String participantCompanyName)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            IQueryable<Registration> query = 
                from reg in uow.RegistrationRepository.GetAll()
                select reg;

            if (categoryID.ToString() != Util.C_String_All_Select)
            {
                int tmpInt = int.Parse(categoryID);
                query = query.Where(x => x.CourseClass.Course.Category.CategoryId == tmpInt);
            }

            if (courseCode != Util.C_String_All_Select)
            {
                query = query.Where(x => x.CourseClass.Course.CourseCode == courseCode);
            }

            if (classID.ToString() != Util.C_String_All_Select)
            {
                query = query.Where(x => x.CourseClass.ClassId == classID);
            }

            if (participantName != Util.C_String_All_Select)
            {
                query = query.Where(x => x.Participant.FullName.ToUpper().Contains(participantName));
            }

            if (participantIdNumber != Util.C_String_All_Select)
            {
                query = query.Where(x => x.Participant.IdNumber.ToUpper().Contains(participantIdNumber));
            }

            if (participantCompanyName != Util.C_String_All_Select)
            {
                query = query.Where(x =>
                    x.Participant.Company.CompanyName.ToUpper().Contains(participantCompanyName)
                    || x.Participant.CompanyName.ToUpper().Contains(participantCompanyName));
            }

            return query.ToList();

        }

        public void UpdateRegistration(Registration r)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            uow.RegistrationRepository.Edit(r);
            uow.Save();
             
        }

        public void DeleteRegistration(Registration r)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            uow.RegistrationRepository.Delete(r);
            uow.Save();
             
        }



        private Boolean ValidateRegistration(Registration r)
        {
            // class open for reg
            if (r.CourseClass.Status == ClassStatus.Cancel && DateTime.Now >= r.CourseClass.StartDate) {
                return false;
            }
            // this participant doesn't register this class
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            IQueryable<Registration> rList = from registration in uow.RegistrationRepository.GetAll()
                                             where (registration.Participant.IdNumber == r.Participant.IdNumber && registration.CourseClass.ClassId == r.CourseClass.ClassId)
                                             select registration;
            if (rList.Count() != 0)
            {
                return false;
            }
            return true;
        }


        private void UpdateRegParticipant(Participant oldP, Participant newP)
        {
            //
            oldP.Salutation = newP.Salutation;
            oldP.EmploymentStatus = newP.EmploymentStatus;
            oldP.CompanyName = newP.CompanyName;
            oldP.JobTitle = newP.JobTitle;
            oldP.Department = newP.Department;
            oldP.FullName = newP.FullName;
            oldP.Gender = newP.Gender;
            oldP.Nationality = newP.Nationality;
            oldP.DateOfBirth = newP.DateOfBirth;
            oldP.Email = newP.Email;
            oldP.ContactNumber = newP.ContactNumber;
            oldP.DietaryRequirement = newP.DietaryRequirement;
            oldP.OrganizationSize = newP.OrganizationSize;
            oldP.SalaryRange = newP.SalaryRange;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Models;
using CourseRegistration.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseRegistration.BLL
{
    public class UserBLL
    {
        private static readonly Lazy<UserBLL> lazy =
            new Lazy<UserBLL>(() => new UserBLL());
      
        public static UserBLL Instance { get { return lazy.Value; } }

        private UserBLL()
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            uow.AppUserManager.EmailService = new EmailService();
            uow.Save();
        }

        public async Task<bool> ResetUserPassword(String userId)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            var user = uow.AppUserManager.FindById(userId);
            String pwd = Util.GeneratePassword();
            user.isSysGenPassword = true;
            var result = await setPasswordAsync(user.Id, pwd,true);
            if (uow.AppUserManager.EmailService == null) 
                uow.AppUserManager.EmailService = new EmailService();
            uow.AppUserManager.SendEmail(userId, "Account Credentials", "Your Login Credentials are <br/> UserName:" + user.UserName + "<br/> Password:" + pwd);
            uow.Save();
            return result;
        }

        public ApplicationUser CreateAppUser(String userName,String email,String contactNumber, String roleName)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            // Create New User
            var userRole = uow.AppRoleManager.FindByName(roleName);
            ApplicationUser user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userName,
                Email = email,
                EmailConfirmed = true,
                PhoneNumber = contactNumber,
                isSysGenPassword = true
            };
            if (userRole == null)
            {
                CourseRegistration.BLL.RoleBLL.Instance.CreateRole(roleName);
                userRole = uow.AppRoleManager.FindByName(roleName);
            }
                
            user.Roles.Add(new IdentityUserRole { RoleId = userRole.Id, UserId = user.Id });
            String pwd = Util.GeneratePassword();

            uow.AppUserManager.Create(user, pwd);

            uow.Save();
            MailUserCredentials(user.Id, user.UserName, pwd);
            return user;

        }

        public ApplicationUser UpdateAppUser(String userId,String userName, String email, String contactNumber,bool DisableAccount)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            // Update User Details
            var user = uow.AppUserManager.FindById(userId);
            user.UserName = userName;
            user.Email = email;
            user.PhoneNumber = contactNumber;
            if(DisableAccount)
            {
                uow.AppUserManager.RemovePassword(userId);
            }
            else if(user.PasswordHash == null)
            {
                ResetUserPassword(userId);
            }
            uow.Save();
            return user;

        }

        public void MailUserCredentials(String userId,String userName,String password)
        {
            //IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            //if (uow.AppUserManager.EmailService == null) 
            //    uow.AppUserManager.EmailService = new EmailService();
            //uow.AppUserManager.SendEmail(userId, "Account Credentials", "Your Login Credentials are <br/> UserName:" + userName + "<br/> Password:" + password);
            //uow.Save();
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            var user = uow.AppUserManager.FindById(userId);
            Util.SendEmail(user.Email, "Account Credentials", "Your Login Credentials are <br/> UserName:" + userName + "<br/> Password:" + password);

        }
        public void MailUser(String userId, String subject,String msg)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            if (uow.AppUserManager.EmailService == null)
                uow.AppUserManager.EmailService = new EmailService();
            uow.AppUserManager.SendEmail(userId,subject,msg);
            uow.Save();
        }
        public ApplicationUser CreateIndividualUser(Participant p)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();

            // Create New User
            var userRole = uow.AppRoleManager.FindByName(Util.C_Role_IndividualUser);
            ApplicationUser user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = p.IdNumber,
                Email = p.Email,
                EmailConfirmed = true,
                PhoneNumber = p.ContactNumber,
                isSysGenPassword = true
            };
            user.Roles.Add(new IdentityUserRole { RoleId = userRole.Id, UserId = user.Id });

            String pwd = Util.GeneratePassword();

            uow.AppUserManager.Create(user, pwd);

            // Create New Participant
            p.AppUser = user;
            uow.ParticipantRepository.Insert(p);
            uow.Save();
            MailUserCredentials(user.Id, user.UserName, pwd);
            return user;

        }

        public ApplicationUser CreateCompanyHR(Company company, CompanyHR HR)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();

            // Create New User
            var userRole = uow.AppRoleManager.FindByName(Util.C_Role_CompanyHR);
            ApplicationUser user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = HR.Email,
                Email = HR.Email,
                EmailConfirmed = true,
                PhoneNumber = HR.ContactNumber,
                isSysGenPassword = true
            };
            user.Roles.Add(new IdentityUserRole { RoleId = userRole.Id, UserId = user.Id });

            String pwd = Util.GeneratePassword();

            uow.AppUserManager.Create(user, pwd);

            // Create Comapany
            uow.CompanyRepository.Insert(company);

             // Create CompanyHR
            HR.Company = company;
            HR.AppUser = user;
            uow.CompanyHRRepository.Insert(HR);

            uow.Save();
            MailUserCredentials(user.Id, user.UserName, pwd);
            return user;

        }

        public void CreateCourseAdmin()
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            ApplicationUser user = new ApplicationUser();
            user.EmailConfirmed = true;
            // Create New User
            var userRole = uow.AppRoleManager.FindByName(Util.C_Role_CourseAdmin);
            user.Roles.Add(new IdentityUserRole { RoleId = userRole.Id, UserId = user.Id });
            String pwd = Util.GeneratePassword();

            uow.AppUserManager.Create(user, pwd);

            uow.Save();

        }

        public List<ApplicationUser> GetAllUsers()
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.AppUserManager.Users.ToList();
        }

        public List<ApplicationUser> GetUsersByRole(String roleName)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            var requestRole = uow.AppRoleManager.FindByName(roleName);
            IQueryable<ApplicationUser> query = 
                from user in uow.AppUserManager.Users
                where
                    (from role in user.Roles where role.RoleId == requestRole.Id select true).FirstOrDefault() != null
                select user;

            return query.ToList();
        }

        public ApplicationUser GetUserById(String id)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.AppUserManager.FindById(id);
        }

        public ApplicationUser GetUserByName(String userName)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.AppUserManager.FindByName(userName);
        }

        public async Task<bool> ConfirmEmail(string id,bool status)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            ApplicationUser user = GetUserById(id);
            if (user == null || status == false)
                return false;
            else
            {
                await uow.AppUserStore.SetEmailConfirmedAsync(user, status);
                uow.Save();
                return true;
            }
        }

        public async Task<bool> setPasswordAsync(string id,string password , bool status)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            ApplicationUser user = GetUserById(id);
            if (user == null || status == false)
                return false;
            else
            {
                await uow.AppUserStore.SetPasswordHashAsync(user, uow.AppUserManager.PasswordHasher.HashPassword(password));
                if (user.isSysGenPassword)
                    user.isSysGenPassword = false;
                uow.Save();
                return true;
            }
        }

        public void DeleteUser(ApplicationUser user)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            uow.AppUserManager.Delete(user);
            uow.Save();
            
        }

        public bool ValidateUser(String userName, String password)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return (uow.AppUserManager.Find(userName, password) != null);
        }

    }
}

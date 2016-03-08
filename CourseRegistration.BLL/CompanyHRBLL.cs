using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Models;
using CourseRegistration.Data;

namespace CourseRegistration.BLL
{
    public class CompanyHRBLL
    {
        private static readonly Lazy<CompanyHRBLL> lazy =
            new Lazy<CompanyHRBLL>(() => new CompanyHRBLL());
      
        public static CompanyHRBLL Instance { get { return lazy.Value; } }

        private CompanyHRBLL()
        {
        }

        public void CreateCompanyHR(CompanyHR hr)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            uow.CompanyHRRepository.Insert(hr);
            uow.Save();
        }

        public List<CompanyHR> GetAllCompanyHRs()
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.CompanyHRRepository.GetAll().ToList();
        }

        public CompanyHR GetCompanyHRByUserId(String userId)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.CompanyHRRepository.GetAll().Where(x => x.AppUser.Id == userId).SingleOrDefault();
        }

        public void EditCompanyHR(CompanyHR hr)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            uow.CompanyHRRepository.Edit(hr);
            uow.Save();
            
        }

        public void DeleteCompanyHR(CompanyHR hr)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            uow.CompanyHRRepository.Delete(hr);
            uow.Save();
        }

    }
}

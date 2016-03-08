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
    public class ParticipantBLL
    {
    private static readonly Lazy<ParticipantBLL> lazy =
            new Lazy<ParticipantBLL>(() => new ParticipantBLL());
      
        public static ParticipantBLL Instance { get { return lazy.Value; } }

        private ParticipantBLL()
        {
        }
        public Participant GetParticipantByUserId(String userId)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            IQueryable<Participant> pList = from participant in uow.ParticipantRepository.GetAll()
                            where (participant.AppUser.Id == userId)
                            select participant;
            if (pList.Count() == 0)
            {
                return new Participant();
            }
            else
            {
                return pList.First();
            }
        }
        public void CreateForCompanyEmployee(Company c,Participant p)
        {
            // set value for employee
            p.Company = c;
            p.CompanyName = c.CompanyName;
            p.EmploymentStatus = "Employed";
            p.OrganizationSize = c.OrganizationSize;

            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            uow.ParticipantRepository.Insert(p);
            uow.Save();
        }



        public List<Participant> GetAllParticipants()
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.ParticipantRepository.GetAll().ToList();
            
        }

        public List<Participant> GetAllParticipantsByCompanyId(int compId)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.ParticipantRepository.GetAll().Where(x => x.Company.CompanyId == compId).ToList();

        }

        public Participant GetParticipantById(int id)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.ParticipantRepository.GetById(id);
        }

        public Participant GetParticipantByIdNumber(String id)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.ParticipantRepository.GetAll().Where(x => x.IdNumber == id).ToList().First();
        }

        public Participant GetIUParticipantByIdNumber(String id)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.ParticipantRepository.GetAll().Where(x => x.IdNumber == id && x.Company == null).ToList().FirstOrDefault();
        }

        public List<Participant> GetCmpParticipantByIdNumber(int cmpId, String idNumber)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            return uow.ParticipantRepository.GetAll().Where(x => x.IdNumber == idNumber && x.Company.CompanyId == cmpId).ToList();
        }
        public void EditParticipant(Participant p)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            uow.ParticipantRepository.Edit(p);
            uow.Save();
            
        }

        public void DeleteParticipant(Participant p)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();

            //validate 
            Participant participant = GetParticipantById(p.ParticipantId);
            if (participant.Registrations.Count > 0)
            {
                throw new Exception("Not allow deletion");
            }

            uow.ParticipantRepository.Delete(p);
            uow.Save();
            
        }
        
    }
}

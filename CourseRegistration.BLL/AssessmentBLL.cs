using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Models;
using CourseRegistration.Data;

namespace CourseRegistration.BLL
{
    public class AssessmentBLL
    {

        private static readonly Lazy<AssessmentBLL> lazy =
            new Lazy<AssessmentBLL>(() => new AssessmentBLL());

        public static AssessmentBLL Instance { get { return lazy.Value; } }

        private AssessmentBLL()
        {

        }

        public void CreateAssessment(Assessment assessment)
        {
            ValidateAssessment(assessment);
            
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            uow.AssessmentRepository.Insert(assessment);
            uow.Save();
        }

        public Assessment GetAssessmentByClassAndParticipant(String classId, int participantId)
        {
            return GetAssessmentByClassPart(classId, participantId);

        }

        private Assessment GetAssessmentByClassPart(String classId, int participantId)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();
            IQueryable<Assessment> query =
                from assess in uow.AssessmentRepository.GetAll()
                where assess.CourseClass.ClassId == classId &&
                    assess.Participant.ParticipantId == participantId
                select assess;

            return query.SingleOrDefault();
        }

        private void ValidateAssessment(Assessment assessment)
        {
            IUnitOfWork uow = UnitOfWorkHelper.GetUnitOfWork();

            // student exists
            if (assessment.Participant == null ||
                uow.ParticipantRepository.GetById(assessment.Participant.ParticipantId) == null)
            {
                throw new BusinessException("invalid Participant");
            }

            // class exists
            if (assessment.CourseClass == null ||
                uow.CourseClassRepository.GetById(assessment.CourseClass.ClassId) == null)
            {
                throw new BusinessException("invalid Class");
            }

            // previous record exists
            if (GetAssessmentByClassPart(
                assessment.CourseClass.ClassId, 
                assessment.Participant.ParticipantId) != null)
            {
                throw new BusinessException("Assessment already exist");
            }
        }


    }
}

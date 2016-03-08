using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.Service
{

    [DataContract]
    public class SvcParticipant
    {
        [DataMember]
        public String IdNumber{ get;set; }
        [DataMember]
        public String Salutation { get; set; }
        [DataMember]
        public String EmploymentStatus { get; set; }
        [DataMember]
        public String JobTitle { get; set; }
        [DataMember]
        public String Department { get; set; }
        [DataMember]
        public String FullName { get; set; }
        [DataMember]
        public Gender Gender { get; set; }
        [DataMember]
        public String Nationality { get; set; }
        [DataMember]
        public DateTime DateOfBirth { get; set; }
        [DataMember]
        public String Email { get; set; }
        [DataMember]
        public String ContactNumber { get; set; }
        [DataMember]
        public String DietaryRequirement { get; set; }
        [DataMember]
        public OrganizationSize OrganizationSize { get; set; }
        [DataMember]
        public SalaryRange SalaryRange { get; set; }

    }

    [DataContract]
    public class SvcCourseClass
    {
        [DataMember]
        public String ClassId { get; set; }
    }
}
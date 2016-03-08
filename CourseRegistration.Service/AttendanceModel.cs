using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.Service
{

    [DataContract]
    public class SvcStudent
    {
        [DataMember]
        public int ParticipantId { get; set; }
        [DataMember]
        public String IdNumber { get; set; }
        [DataMember]
        public String FullName { get; set; }
        [DataMember]
        public Gender Gender { get; set; }
        [DataMember]
        public String Nationality { get; set; }
        [DataMember]
        public String Email { get; set; }
        [DataMember]
        public String ContactNumber { get; set; }

        public SvcStudent() { }

        public SvcStudent(
            int ParticipantId,
            String IdNumber,
            String FullName,
            Gender Gender,
            String Nationality,
            String Email,
            String ContactNumber)
        {
            this.ParticipantId = ParticipantId;
            this.IdNumber = IdNumber;
            this.FullName = FullName;
            this.Gender = Gender;
            this.Nationality = Nationality;
            this.Email = Email;
            this.ContactNumber = ContactNumber;
        }

        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var args = obj as SvcStudent;

            // TODO: write your implementation of Equals() here

            return this.ParticipantId == args.ParticipantId;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return ParticipantId.GetHashCode();
        }
    
    }

}
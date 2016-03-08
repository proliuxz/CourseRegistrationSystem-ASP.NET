using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.Service
{
    [DataContract]
    public class Result
    {
        [DataMember]
        public Boolean bResult { set; get; }
        
        [DataMember]
        public String Message { set; get; }

        public Result() { }

        public Result(Boolean result, string message)
        {
            bResult = result;
            Message = message;
        }
    }

    [DataContract]
    public enum OrganizationSize
    {
        [EnumMember]
        Less_Than_20 = 0,
        [EnumMember]
        From_20_To_200 = 1,
        [EnumMember]
        From_200_To_500 = 2,
        [EnumMember]
        More_Than_500 = 3
    }

    [DataContract]
    public enum SalaryRange
    {
        [EnumMember]
        Less_Than_60k = 0,
        [EnumMember]
        From_60_To_90k = 1,
        [EnumMember]
        From_90k_To_120k = 2,
        [EnumMember]
        More_Than_120k = 3
    }

    [DataContract]
    public enum Gender
    {
        [EnumMember]
        Female = 0,
        [EnumMember]
        Male = 1
    }
}
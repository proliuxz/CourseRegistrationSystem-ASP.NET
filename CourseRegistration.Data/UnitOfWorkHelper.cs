using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;

namespace CourseRegistration.Data
{
    public class UnitOfWorkHelper
    {
        public static IUnitOfWork GetUnitOfWork()
        {
            IUnitOfWork uow = CallContext.GetData(typeof(IUnitOfWork).Name) as IUnitOfWork;
            if (uow == null)
            {
                uow = new UnitOfWork();
                CallContext.SetData(typeof(IUnitOfWork).Name, uow);
            }
            return uow;
        }
    }
}

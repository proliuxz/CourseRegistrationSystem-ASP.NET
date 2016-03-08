using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Models;


namespace CourseRegistration.Data
{
    public interface IRepository<TEntity> where TEntity : BaseModel
    {
        #region Public Methods

        TEntity GetById(int id);
        TEntity GetById(String id);
        IQueryable<TEntity> GetAll();
        void Edit(TEntity entity);
        void Insert(TEntity entity);
        void Delete(TEntity entity);

        #endregion
    }
}

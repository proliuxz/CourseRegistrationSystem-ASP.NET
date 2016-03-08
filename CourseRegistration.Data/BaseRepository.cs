using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Models;
using System.Data.Entity;

namespace CourseRegistration.Data
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseModel
    {
        #region Attribute

        public DbContext context;
        public DbSet<TEntity> dbset;
        
        #endregion

        #region Public Methods
        public BaseRepository(DbContext context)
        {
            this.context = context;
            dbset = context.Set<TEntity>();
        }
        public BaseRepository()
        {

        }

        public TEntity GetById(int id)
        {
            return dbset.Find(id);
        }

        public TEntity GetById(String id)
        {
            return dbset.Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbset;
        }
        public void Insert(TEntity entity)
        {
            dbset.Add(entity);
        }

        public void Edit(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }
        #endregion
    }
}

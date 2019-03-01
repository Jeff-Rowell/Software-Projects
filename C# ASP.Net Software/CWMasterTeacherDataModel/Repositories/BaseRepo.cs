using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public abstract class BaseRepo<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly MasterTeacherContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepo(MasterTeacherContext context)
        {
            this._context = context;
            this._dbSet = _context.Set<TEntity>();
        }

        // Made virtual to mock using Moq.
        public virtual TEntity GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entityToDelete = _dbSet.Find(id);
            _dbSet.Remove(entityToDelete);
            _context.SaveChanges();
        }

        public void DeleteMultiple(List<TEntity> entityCollection)
        {
            List<Guid> idList = new List<Guid>();
            foreach(var xEntity in entityCollection)
            {
                idList.Add(xEntity.Id);
            }
            foreach(Guid xId in idList)
            {
                Delete(xId);
            }
        }

    }
}

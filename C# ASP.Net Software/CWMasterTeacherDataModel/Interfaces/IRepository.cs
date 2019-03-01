using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        TEntity GetById(Guid id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(Guid id);
        void DeleteMultiple(List<TEntity> entityCollection);
    }
}

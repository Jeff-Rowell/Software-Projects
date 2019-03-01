using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public interface IRepoListWithOneId <TEntity>
    {
        IEnumerable<TEntity> List(int id);
    }

    public interface IRepoListWithTwoIds<TEntity>
    {
        IEnumerable<TEntity> List(int idOne, int idTwo);
    }
}

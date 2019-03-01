using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDataModel;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface IDomainObjBuilder<FullObj, BasicObj, DBobj> where DBobj : IEntity
    {
        FullObj BuildFromId(Guid id);
        BasicObj BuildBasicFromId(Guid id);
    }
}

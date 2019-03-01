using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface IListable
    {
        Guid Id { get; }
        string Name { get; }

    }
}

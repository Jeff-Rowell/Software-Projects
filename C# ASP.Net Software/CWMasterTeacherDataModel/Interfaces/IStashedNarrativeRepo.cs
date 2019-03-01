using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface IStashedNarrativeRepo: IRepository<StashedNarrative>
    {
        List<StashedNarrative> StashedNarrativesForLesson(Guid lessonId);
        StashedNarrative StashedNarrativeForLessonAndNarrative(Guid lessonId, Guid narrativeId);
    }
}

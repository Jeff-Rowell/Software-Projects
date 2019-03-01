using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDomain;

namespace CWMasterTeacherDataModel
{
    public class StashedNarrativeRepo : BaseRepo <StashedNarrative>, IStashedNarrativeRepo
    {
        public StashedNarrativeRepo(MasterTeacherContext context)
            : base(context)
        {

        }

        public StashedNarrative CreateStashedNarrative (Guid narrativeId, Guid lessonId)
        {
            StashedNarrative stashedNarrative = new StashedNarrative();
            stashedNarrative.NarrativeId = narrativeId;
            stashedNarrative.LessonId = lessonId;
            stashedNarrative.CreationDate = DateTime.Now;

            Insert(stashedNarrative);

            return stashedNarrative;

        }

        public List<StashedNarrative> StashedNarrativesForLesson (Guid lessonId)
        {
            var stashQuery = from x in _context.StashedNarratives
                             where x.LessonId == lessonId
                             select x;
            return stashQuery.ToList();
        }

        public StashedNarrative StashedNarrativeForLessonAndNarrative(Guid lessonId, Guid narrativeId)
        {
            var stashQuery = from x in _context.StashedNarratives
                             where x.LessonId == lessonId && x.NarrativeId == narrativeId
                             select x;
            return stashQuery.FirstOrDefault();
        }



    }
}

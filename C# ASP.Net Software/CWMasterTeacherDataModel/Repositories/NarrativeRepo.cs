using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class NarrativeRepo: BaseRepo<Narrative>, INarrativeRepo
    {

        public NarrativeRepo(MasterTeacherContext context)
            : base(context)
        {
        }


        public void CleanUpOrphanNarratives()
        {
            var orphanQuery = from x in _context.Narratives
                              where x.Lessons.Count() == 0 && x.StashedNarratives.Count() == 0
                              select x;
            if(orphanQuery.Count() > 0)
            {
                List<Narrative> deleteList = orphanQuery.ToList();
                foreach(var xNarrative in deleteList)
                {
                    DeleteComments(xNarrative);
                }

                List<Guid> idList = deleteList.Select(x => x.Id).ToList();
                foreach(var xId in idList)
                {
                    Delete(xId);
                }
            }
        }

        public void DeleteComments(Narrative narrative)
        {
            List<Guid> idList = narrative.CommentNarratives.Select(x => x.Id).ToList();
            foreach(var xId in idList)
            {
                Delete(xId);
            }
        }





    }//end class
}

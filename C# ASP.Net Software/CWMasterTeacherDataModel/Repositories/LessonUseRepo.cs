using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class LessonUseRepo: BaseRepo<LessonUse>
    {
        
        private ClassMeetingRepo _classMeetingRepo;

        public LessonUseRepo() : base(new MasterTeacherContext())
        {
            _classMeetingRepo = null;
        }

        public LessonUseRepo(MasterTeacherContext context, ClassMeetingRepo classMeetingRepo)
            : base(context)
        {
            _classMeetingRepo = classMeetingRepo;  
        }

        public IEnumerable<LessonUse> LessonUsesForClassMeeting(Guid classMeetingId)
        {
            var listQuery = from x in _context.LessonUses
                                             where x.ClassMeetingId == classMeetingId
                                             orderby x.SequenceNumber
                                             select x;
            return listQuery.ToList();                                      
        }

        public LessonUse LessonUseAboveGivenSequenceNumber(ClassMeeting classMeeting, int sequenceNumber)
        {
            var useQuery = from x in _context.LessonUses
                           where x.ClassMeetingId == classMeeting.Id && x.SequenceNumber > sequenceNumber
                           orderby x.SequenceNumber 
                           select x;
            return useQuery.FirstOrDefault();
        }

        public LessonUse LessonUseBelowGivenSequenceNumber(ClassMeeting classMeeting, int sequenceNumber)
        {
            var useQuery = from x in _context.LessonUses
                           where x.ClassMeetingId == classMeeting.Id && x.SequenceNumber < sequenceNumber
                           orderby x.SequenceNumber descending
                           select x;
            return useQuery.FirstOrDefault();
        }






        

        
    }//End Class
}

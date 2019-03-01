using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class MessageRepo: BaseRepo<Message>
    {
        private LessonRepo _lessonRepo;
        private MessageUseRepo _messageUseRepo;
        private CourseRepo _courseRepo;

        //TODO: Revove reference to CUDServices
        public MessageRepo(MasterTeacherContext context, LessonRepo lessonRepo, MessageUseRepo messageUseRepo, CourseRepo courseRepo)
            : base(context)
        {
            _lessonRepo = lessonRepo;
            _messageUseRepo = messageUseRepo;
            _courseRepo = courseRepo;
        }

        public IEnumerable<Message> MessagesForThreadParent(Guid threadParentId)
        {
            var listQuery = from x in _context.Messages
                                             where x.ThreadParentId == threadParentId
                                             orderby x.TimeStamp 
                                             select x;
            return listQuery.ToList();                                      
        }



     


    }//End class
}

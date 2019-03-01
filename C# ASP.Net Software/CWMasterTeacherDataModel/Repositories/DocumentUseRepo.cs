using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class DocumentUseRepo : BaseRepo<DocumentUse>, IDocumentUseRepo
    {

        public DocumentUseRepo(MasterTeacherContext context)
            : base(context)
        {
            
        }

        /// <summary>
        /// Returns all Active DocumentUses not marked as IsReference or IsInstructorOnly
        /// Ordered by Name
        /// </summary>
        public IEnumerable<DocumentUse> TeachingDocumentUsesForLesson(Guid lessonId)
        {
            List<DocumentUse> docUseList = new List<DocumentUse>();
            if (lessonId != Guid.Empty)
            {
                var listQuery = from x in _context.DocumentUses
                                where x.LessonId == lessonId && !x.IsReference && !x.IsInstructorOnly && x.IsActive 
                                orderby x.Document.Name
                                select x;
                docUseList = listQuery.ToList();
            }

            return docUseList;                          
        }

        /// <summary>
        /// Returns all active DocumentUses marked as IsInstructorOnly. Ordered by Name
        /// </summary>
        public IEnumerable<DocumentUse> InstructorOnlyDocumentUsesForLesson(Guid lessonId)
        {
            List<DocumentUse> docUseList = new List<DocumentUse>();
            if (lessonId != Guid.Empty)
            {
                var listQuery = from x in _context.DocumentUses
                                where x.LessonId == lessonId && !x.IsReference && x.IsInstructorOnly  && x.IsActive
                                orderby x.Document.Name
                                select x;
                docUseList = listQuery.ToList();
            }

            return docUseList;
        }

        /// <summary>
        /// Returns all active DocumentUses marked as IsInstructorOnly.  Ordered by Name.
        /// </summary>
        public IEnumerable<DocumentUse> ReferenceDocumentUsesForLesson(Guid lessonId)
        {
            var listQuery = from x in _context.DocumentUses
                            where x.LessonId == lessonId && x.IsReference && x.IsActive
                            orderby x.Document.Name
                            select x;
            return listQuery.ToList();
        }



        /// <summary>
        /// Returns all DocumentUses for Lesson.  Sorted by Name
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public IEnumerable<DocumentUse> AllDocumentUsesForLesson(Guid lessonId)
        {
            IEnumerable<DocumentUse> docUses;

            docUses = from x in _context.DocumentUses
                        where x.LessonId == lessonId
                        orderby x.Document.Name
                        select x;
            return docUses;
        }

        /// <summary>
        /// Returns active DocumentUses for Lesson.  Sorted by Name
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public IEnumerable<DocumentUse> ActiveDocumentUsesForLesson(Guid lessonId)
        {
            IEnumerable<DocumentUse> docUses;
           
            docUses = from x in _context.DocumentUses
                        where x.LessonId == lessonId && x.IsActive
                        orderby x.Document.Name
                        select x;
            return docUses;
        }

        public IEnumerable<DocumentUse> AllDocumentUsesThatShareMasterLesson(Guid lessonId)
        {
            IEnumerable<DocumentUse> docUses;
            Guid? masterLessonId = _context.Lessons
                                          .Where(theLesson => theLesson.Id == lessonId)
                                          .Single().MasterLessonId;
            masterLessonId = masterLessonId == null ? lessonId : masterLessonId;
            docUses = from x in _context.DocumentUses
                     where x.Lesson.MasterLessonId == masterLessonId
                     orderby x.Document.Name
                     select x;
            return docUses;
        }

        public IEnumerable<DocumentUse> ActiveDocumentUsesThatShareMasterLesson(Guid lessonId)
        {
            IEnumerable<DocumentUse> docUses;
            Guid? masterLessonId = _context.Lessons
                                          .Where(theLesson => theLesson.Id == lessonId)
                                          .Single().MasterLessonId;
            masterLessonId = masterLessonId == null ? lessonId : masterLessonId;
            docUses = from x in _context.DocumentUses
                      where x.IsActive && (x.Lesson.MasterLessonId == masterLessonId ||x.Lesson.Id == masterLessonId)
                      orderby x.Document.Name
                      select x;
            return docUses;
        }

        /// <summary>
        /// Returns all DocumentUses, active and inactive, for given document and lesson.  Used for finding inactive documents so the inactive part is important.
        /// </summary>
        /// <param name="lessonId"></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public DocumentUse DocumentUseForDocumentAndLesson(Guid lessonId, Guid documentId)
        {
            var listQuery = from x in _context.DocumentUses
                            where x.DocumentId == documentId && x.LessonId == lessonId
                            orderby x.Lesson.Name
                            select x;
            if (listQuery.Count() > 0)
            {
                return listQuery.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }


       

    }//End Class
}

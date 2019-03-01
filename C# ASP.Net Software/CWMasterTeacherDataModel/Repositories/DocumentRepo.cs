using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class DocumentRepo : BaseRepo<Document>
    {
        private LessonRepo _lessonRepo;
        private DocumentUseRepo _documentUseRepo;
        private CourseRepo _courseRepo;

        //TODO: Remove reference to CUDService
        public DocumentRepo(MasterTeacherContext context, LessonRepo lessonRepo, DocumentUseRepo documentUseRepo, CourseRepo courseRepo)
            : base(context)
        {
            _lessonRepo = lessonRepo;
            _documentUseRepo = documentUseRepo;
            _courseRepo = courseRepo;
        }

        ///// <summary>
        ///// Ordered by DateModified descending
        ///// </summary>
        //public IEnumerable<Document> DocumentsForUser(int userId)
        //{
        //    var listQuery = from x in _context.Documents
        //                                     where x.ModifiedByUserId  == userId
        //                                     orderby x.DateModified descending
        //                                     select x;
        //    return listQuery.ToList();                                      
        //}

        ///// <summary>
        ///// All Documents that share OriginalDocumentId.  Ordered by DateModified descending
        ///// </summary>
        //public IEnumerable<Document> DocumentsForOriginal(int originalDocumentId)
        //{
        //    var listQuery = from x in _context.Documents
        //                    where x.OriginalDocumentId == originalDocumentId
        //                    orderby x.DateModified descending
        //                    select x;
        //    return listQuery.ToList();
        //}


        public Document DocumentForDocumentUse(Guid documentUseId)
        {
            DocumentUse docUse = _documentUseRepo.GetById(documentUseId);
            if (docUse != null)
            {
                return docUse.Document;
            }
            else
            {
                return null;
            }
        }




       

    }//End Class
}

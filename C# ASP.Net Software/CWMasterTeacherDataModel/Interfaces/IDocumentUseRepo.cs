using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface IDocumentUseRepo: IRepository<DocumentUse>
    {
        IEnumerable<DocumentUse> TeachingDocumentUsesForLesson(Guid lessonId);
        IEnumerable<DocumentUse> InstructorOnlyDocumentUsesForLesson(Guid lessonId);
        IEnumerable<DocumentUse> ReferenceDocumentUsesForLesson(Guid lessonId);
        IEnumerable<DocumentUse> AllDocumentUsesForLesson(Guid lessonId);
        IEnumerable<DocumentUse> ActiveDocumentUsesForLesson(Guid lessonId);
        IEnumerable<DocumentUse> AllDocumentUsesThatShareMasterLesson(Guid lessonId);
        IEnumerable<DocumentUse> ActiveDocumentUsesThatShareMasterLesson(Guid lessonId);
        DocumentUse DocumentUseForDocumentAndLesson(Guid lessonId, Guid documentId);
    }
}

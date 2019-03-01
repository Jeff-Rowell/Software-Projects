using System;
using System.Collections.Generic;
using CWMasterTeacherDomain.DomainObjects;

namespace CWMasterTeacherDataModel.Interfaces
{
    /// <summary>
    /// Interface to be implemented by classes wishing to provide services that build
    /// and return DocumentUseDomainObjs.
    /// </summary>
    public interface IDocumentUseDomainObjBuilder : Interfaces.IDomainObjBuilder<DocumentUseDomainObj, 
                                                                                 DocumentUseDomainObjBasic, 
                                                                                 DocumentUse>
    {
        List<DocumentUseDomainObj> AllDocumentUsesForLesson(Guid lessonId);
        List<DocumentUseDomainObj> ActiveDocumentUsesForLesson(Guid lessonId);
        List<DocumentUseDomainObj> AllDocumentUsesThatShareMasterLesson(Guid lessonId);
        List<DocumentUseDomainObj> ActiveDocumentUsesThatShareMasterLesson(Guid lessonId);
        List<DocumentUseDomainObj> TeachingDocumentUsesForLessonUse(Guid lessonUseId);
        List<DocumentUseDomainObj> InstructorOnlyDocumentUsesForLesson(Guid lessonUseId);
        List<DocumentUseDomainObj> ReferenceDocumentUsesForLesson(Guid lessonUseId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDataModel.Interfaces;

namespace CWMasterTeacherDataModel.ObjectBuilders
{
    /// <summary>
    /// Provides services that build and return DocumentUseDomainObjs.
    /// </summary>
    public class DocumentUseDomainObjBuilder : IDocumentUseDomainObjBuilder
    {
        private DocumentUseRepo _documentUseRepo;
        private DocumentRepo _documentRepo;
        private LessonUseRepo _lessonUseRepo;

        /// <summary>
        /// Provides services that build and return DocumentUseDomainObjs using the provided repositories.
        /// </summary>
        /// <param name="documentUseRepo">A document use repository that is used to when retrieving the 
        /// data used to build a DocumentUseDomainObj</param>
        /// <param name="documentRepo">A document repository that is used to when retrieving the 
        /// data used to build a DocumentUseDomainObj</param>
        public DocumentUseDomainObjBuilder(DocumentUseRepo documentUseRepo, DocumentRepo documentRepo,
                                            LessonUseRepo lessonUseRepo)
        {
            Debug.Assert(documentUseRepo != null);
            Debug.Assert(documentRepo != null);

            _documentUseRepo = documentUseRepo;
            _documentRepo = documentRepo;
            _lessonUseRepo = lessonUseRepo;

        }


        /// <summary>
        /// Builds a DocumentUseDomainObj from the provided DocumentUse.
        /// </summary>
        /// <param name="dbObject">A DocumentUse object containing the data used to build the DocumentUseDomainObj</param>
        /// <returns>A new DocumentUseDomainObj initialized with the data from the argument.</returns>
        public static DocumentUseDomainObj Build(DocumentUse dbObject)
        {
            return new DocumentUseDomainObj
            (
                BuildBasic(dbObject),
                DocumentDomainObjBuilder.Build(dbObject.Document),
                dbObject.LessonId,
                dbObject.IsActive,
                dbObject.IsReference,
                dbObject.IsInstructorOnly,
                dbObject.IsVisibleToStudents,
                dbObject.IsOutForEdit,
                dbObject.Document.Name,
                dbObject.Lesson.DateTimeDocumentsChoiceConfirmed.GetValueOrDefault()
            ); 
            
        }

        /// <summary>
        /// Builds a DocumentUseDomainObj from the DocumentUse retrieved using the id parameter.
        /// </summary>
        /// <param name="id">The id of the DocumentUse to be used to build the DocumentUseDomainObj</param>
        /// <returns>A new DocumentUseDomainObj initialized with the data retrieved using the argument.</returns>
        public DocumentUseDomainObj BuildFromId(Guid id)
        {
            Debug.Assert(id != Guid.Empty);

            return Build(_documentUseRepo.GetById(id));
        }

        /// <summary>
        /// Builds a DocumentUseDomainObjBasic from the provided DocumentUse.
        /// </summary>
        /// <param name="dbObject">A DocumentUse object containing the data used to build the DocumentUseDomainObjBasic</param>
        /// <returns>A new DocumentUseDomainObjBasic initialized with the data from the argument.</returns>
        public static DocumentUseDomainObjBasic BuildBasic(DocumentUse dbObject)
        {
            Debug.Assert(dbObject != null);

            return new DocumentUseDomainObjBasic(dbObject.Id);    
        }

        /// <summary>
        /// Builds a DocumentUseDomainObjBasic from the DocumentUse retrieved using the id parameter.
        /// </summary>
        /// <param name="id">The id of the DocumentUse to be used to build the DocumentUseDomainObjBasic</param>
        /// <returns>A new DocumentUseDomainObjBasic initialized with the data retrieved using the argument.</returns>
        public DocumentUseDomainObjBasic BuildBasicFromId(Guid id)
        {
            Debug.Assert(id != Guid.Empty);

            return BuildBasic(_documentUseRepo.GetById(id));
        }

        /// <summary>
        /// Builds all document uses associated with a lesson.
        /// </summary>
        /// <param name="lessonId">The id of the lesson for which all document uses will be built.</param>
        /// <returns>A list of all DocumentUseDomainObjs that are associated with the lesson.</returns>
        public List<DocumentUseDomainObj> AllDocumentUsesForLesson(Guid lessonId)
        {
            Debug.Assert(lessonId != Guid.Empty );

            List<DocumentUse> docUseList = _documentUseRepo.AllDocumentUsesForLesson(lessonId).ToList();
            return docUseList.Select(x => Build(x)).ToList();  
        }

        /// <summary>
        /// Builds only the active document uses associated with a lesson.
        /// </summary>
        /// <param name="lessonId">The id of the lesson for which the active document uses will be built.</param>
        /// <returns>A list of active DocumentUseDomainObjs that are associated with the lesson.</returns>
        public List<DocumentUseDomainObj> ActiveDocumentUsesForLesson(Guid lessonId)
        {
            Debug.Assert(lessonId != Guid.Empty);

            return _documentUseRepo.ActiveDocumentUsesForLesson(lessonId)
                                   .ToList()
                                   .Select(x => Build(x))
                                   .ToList();
        }

        /// <summary>
        /// Builds all document uses that share the same master lesson as the argument.
        /// </summary>
        /// <param name="lessonId">The id of the lesson for which all document uses that share the same master lesson will be built.</param>
        /// <returns>A list of all DocumentUseDomainObjs that share the same master lesson as the argument.</returns>
        public List<DocumentUseDomainObj> AllDocumentUsesThatShareMasterLesson(Guid lessonId)
        {
            Debug.Assert(lessonId != Guid.Empty);

            return _documentUseRepo.AllDocumentUsesThatShareMasterLesson(lessonId)
                                .ToList()
                                .Select(x => Build(x))
                                .ToList();
        }

        /// <summary>
        /// Builds active document uses that share the same master lesson as the argument.
        /// </summary>
        /// <param name="lessonId">The id of the lesson for which active document uses that share the same master lesson will be built.</param>
        /// <returns>A list of active DocumentUseDomainObjs that share the same master lesson as the argument.</returns>
        public List<DocumentUseDomainObj> ActiveDocumentUsesThatShareMasterLesson(Guid lessonId)
        {
            Debug.Assert(lessonId != Guid.Empty);

            return _documentUseRepo.ActiveDocumentUsesThatShareMasterLesson(lessonId)
                                .ToList()
                                .Select(x => Build(x))
                                .ToList();
        }

        /// <summary>
        /// Builds teaching document uses for a lesson.  Called from Classroom view
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public List<DocumentUseDomainObj> TeachingDocumentUsesForLessonUse(Guid lessonUseId)
        {
            Debug.Assert(lessonUseId != Guid.Empty);
            LessonUse lessonUse = _lessonUseRepo.GetById(lessonUseId);
            return (lessonUse == null && lessonUse.LessonId.HasValue)? new List <DocumentUseDomainObj>() :
                                _documentUseRepo.TeachingDocumentUsesForLesson(lessonUse.LessonId.Value)
                                        .ToList()
                                        .Select(x => Build(x))
                                        .ToList();
        }

        /// <summary>
        /// Builds teaching document uses for a lesson.  Called from Classroom view
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public List<DocumentUseDomainObj> InstructorOnlyDocumentUsesForLesson(Guid lessonId)
        {
            Debug.Assert(lessonId != Guid.Empty);

            return _documentUseRepo.InstructorOnlyDocumentUsesForLesson(lessonId)
                                .ToList()
                                .Select(x => Build(x))
                                .ToList();
        }

        /// <summary>
        /// Builds teaching document uses for a lesson.  Called from Classroom view
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public List<DocumentUseDomainObj> ReferenceDocumentUsesForLesson(Guid lessonId)
        {
            Debug.Assert(lessonId!= Guid.Empty);

            return _documentUseRepo.ReferenceDocumentUsesForLesson(lessonId)
                                .ToList()
                                .Select(x => Build(x))
                                .ToList();
        }

    }
}

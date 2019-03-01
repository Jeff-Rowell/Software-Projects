using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class DocumentCUDService
    {
        private LessonRepo _lessonRepo;
        private DocumentUseRepo _documentUseRepo;
        private CourseRepo _courseRepo;
        private CourseCUDService _courseCUDService;
        private DocumentRepo _documentRepo;
        private DocumentUseCUDService _documentUseCUDService;
        private LessonCUDService _lessonCUDService;

        //TODO: Remove reference to CUDService
        public DocumentCUDService(DocumentRepo documentRepo, LessonRepo lessonRepo, DocumentUseRepo documentUseRepo, CourseRepo courseRepo,
                            CourseCUDService courseCUDService, DocumentUseCUDService documentUseCUDService, LessonCUDService lessonCUDService)
        {
            _lessonRepo = lessonRepo;
            _documentUseRepo = documentUseRepo;
            _courseRepo = courseRepo;
            _courseCUDService = courseCUDService;
            _documentRepo = documentRepo;
            _documentUseCUDService = documentUseCUDService;
            _lessonCUDService = lessonCUDService;
        }

        public Document CreateOrUpdateDocument(string name, Guid lessonId, Guid userId, string fileName, bool isReference,
                                bool isInstructorOnly, Guid documentUseId, string modificationNotes)
        {
            Guid? originalDocId = null;
            if(documentUseId != Guid.Empty)
            {
                DocumentUse fromDocUse = _documentUseRepo.GetById(documentUseId);
                if(fromDocUse != null)
                {
                    originalDocId = fromDocUse.Document.OriginalDocumentId  == null ? fromDocUse.DocumentId : fromDocUse.Document.OriginalDocumentId;
                }
                
            }

            Document document = new Document();
            document.Name = name;
            document.ModifiedByUserId = userId;
            document.Path = "Add this later.";
            document.FileName = fileName;
            document.ModificationNotes = modificationNotes;
            document.OriginalDocumentId = originalDocId;
            DateTime timeNow = DateTime.Now;
            document.DateModified = timeNow;

            _documentRepo.Insert(document);

            _documentUseCUDService.CreateDocumentUse(lessonId, document.Id, isReference, isInstructorOnly);
            _lessonCUDService.UpdateDateDocumentsModified(lessonId, timeNow);

            if (documentUseId != Guid.Empty)
            {
                _documentUseCUDService.SetIsActiveFromId(documentUseId, false); //This inactivates our connection to the old one.

                //This sets the booleans that control the "Out for Edit" icon.
                Lesson lesson = _lessonRepo.GetById(lessonId);
                if (lesson != null)
                {
                    _lessonCUDService.SetLessonDocumentsBoolean(lesson);
                    _courseCUDService.SetCourseDocumentsBoolean(lesson.Course);
                }
            }
            return document;
        }

        /// <summary>
        /// Adds the file name of a pdf copy to a Document
        /// </summary>
        /// <param name="documentUseId">DocumentUse that points to document to be modified</param>
        public void AddPdfCopy(Guid documentUseId, string fileNameOfPdfCopy)
        {
            Document document = _documentRepo.DocumentForDocumentUse(documentUseId);
            document.FileNameOfPdfCopy = fileNameOfPdfCopy;

            _documentRepo.Update(document);
        }

        /// <summary>
        /// Generates the StorageFileName used in Blob storage
        /// </summary>
        public string UserFileNameToStorageFileName(string userFileName)
        {
            var extension = Path.GetExtension(userFileName);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(userFileName);
            // This is deliberately two underscores and SHOULD NOT BE ALTERED
            return fileNameWithoutExtension + "__" + DateTime.Now.ToString("yyyyMMddHHmmssfffff") + extension;
        }

        private string SubstringBeforeLast(string input, string lookFor)
        {
            var pos = input.LastIndexOf(lookFor);
            return input.Substring(0, pos);
        }

        /// <summary>
        /// Calls AddDocumentToLesson, but takes in a documentUseId instead of a documentId
        /// </summary>
        public void AddDocFromDocUseIdToLesson(Guid lessonId, Guid documentUseId)
        {
            DocumentUse docUse = _documentUseRepo.GetById(documentUseId);
            AddDocumentToLesson(lessonId, docUse.DocumentId);
        }

        /// <summary>
        /// Looks for a DocumentUse for Lesson and Document.  If one is found, it is activated, if not, one is created. 
        /// The funky thing with the lists is because we can't get the date of the new document for the new DocUse in this thread without the direct query here
        /// </summary>
        public void AddDocumentToLesson(Guid lessonId, Guid documentId)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            List<DateTime> dateList = lesson.DocumentUses.Select(x => x.Document.DateModified.GetValueOrDefault()).ToList();

            DocumentUse docUse = _documentUseRepo.DocumentUseForDocumentAndLesson(lessonId, documentId);
            if (docUse != null)
            {
                _documentUseCUDService.SetIsActive(docUse, true);
                dateList.Add(docUse.Document.DateModified.GetValueOrDefault());
            }
            else
            {
                _documentUseCUDService.CreateDocumentUse(lessonId, documentId, false, false);
                Document document = _documentRepo.GetById(documentId);
                if(document != null)
                {
                    dateList.Add(document.DateModified.GetValueOrDefault());
                }
                else//which means this is a new document that can't be found by EF yet.
                {
                    dateList.Add(DateTime.Now);
                }
            }

            dateList = dateList.OrderByDescending(x => x.Ticks).ToList();
            DateTime modDate = dateList.FirstOrDefault();

            _lessonCUDService.UpdateDateDocumentsModified(lessonId, modDate);
        }





        /// <summary>
        /// Sets the IsActive boolean on a DocumentUse to false
        /// </summary>
        public void RemoveDocumentFromLesson(Guid documentUseId)
        {
            DocumentUse docUse = _documentUseRepo.GetById(documentUseId);
            docUse.IsActive = false;
            _documentUseRepo.Update(docUse);
        }

        /// <summary>
        /// Sets the IsReference and IsInstructorOnly booleans on a DocumentUse
        /// </summary>
        public void MoveDocument(Guid documentUseId, bool isReference, bool isInstructorOnly)
        {
            DocumentUse docUse = _documentUseRepo.GetById(documentUseId);
            if (docUse != null)
            {
                docUse.IsInstructorOnly = isInstructorOnly;
                docUse.IsReference = isReference;
                _documentUseRepo.Update(docUse);
            }
        }


        /// <summary>
        /// Renames a Document, but takes in a DocumentUseId to find the document
        /// </summary>
        public void RenameDocument(Guid documentUseId, string newName, string newModificationNotes)
        {
            Document document = _documentRepo.DocumentForDocumentUse(documentUseId);
            document.Name = newName;
            document.ModificationNotes = newModificationNotes;
            _documentRepo.Update(document);
        }

        /// <summary>
        /// Determines whether a file extension is valid for upload.  
        /// </summary>
        //TODO: This is a bit of a hack.
        public bool ValidateExtension(string fileName)
        {

            string extension = fileName.Substring(fileName.LastIndexOf(".") + 1); ;
            if (extension == "pdf" || extension == "doc" || extension == "docx" || extension == "xls" || extension == "xlsx" || extension == "ggb" ||
                extension == "ggt" || extension == "jpg" || extension == "png" || extension == "pps" || extension == "ppsx" || extension == "ppt" ||
                extension == "pptx" || extension == "tex")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateIsPdf(string fileName)
        {

            string extension = fileName.Substring(fileName.LastIndexOf(".") + 1); ;
            if (extension == "pdf")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sets IsOutForEdit on a documentUse
        /// </summary>
        public void SetIsOutForEdit(Guid documentUseId, bool isOutForEdit)
        {
            DocumentUse docUse = _documentUseRepo.GetById(documentUseId);
            docUse.IsOutForEdit = isOutForEdit;
            _documentUseRepo.Update(docUse);
            UpdateDocumentsBoolean(docUse.LessonId);
        }

        private void UpdateDocumentsBoolean(Guid lessonId)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            if (lesson != null)
            {
                _lessonCUDService.SetLessonDocumentsBoolean(lesson);
                _courseCUDService.SetCourseDocumentsBoolean(lesson.Course);

            }
        }

        /// <summary>
        /// Used when deleting Lessons.
        /// </summary>
        public void RemoveDocumentsFromLesson(Lesson lesson)
        {
            foreach (var xDocUse in lesson.DocumentUses)
            {
                _documentUseRepo.Delete(xDocUse.Id);
            }
        }
    }//End Class
}

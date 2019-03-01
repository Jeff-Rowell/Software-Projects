using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.ViewObjectBuilder;
using Ninject;
using CWMasterTeacherService;
using CWMasterTeacherDomain;
using CWMasterTeacherService.CUDServices;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherService.CUDRouters;

namespace CWMasterTeacher3
{
    [Authorize]
    public class CurriculumController : BaseController
    {

        private UserDomainObjBuilder _userObjBuilder;
        private DocumentStorageService _documentStorageService;
        private MessageBoardViewObjBuilder _messageBoardViewObjBuilder;
        private CurriculumViewObjBuilder _curriculumViewObjBuilder;
        private DocumentsTabViewObjBuilder _documentsTabViewObjBuilder;
        private LessonPlanViewObjBuilder _lessonPlanViewObjBuilder;
        private NarrativeViewObjBuilder _narrativeViewObjBuilder;
        private LessonDomainObjBuilder _lessonObjBuilder;
        private MessageUseDomainObjBuilder _messageUseObjBuilder;
        private DocumentDomainObjBuilder _documentObjBuilder;

        private CurriculumCUDRouter _curriculumCUDRouter;
        private NarrativeTabCUDRouter _narrativeTabCUDRouter;
        private LessonPlanTabCUDRouter _lessonPlanTabCUDRouter;
        private MessageBoardTabCUDRouter _messageBoardTabCUDRouter;
        private DocumentsTabCUDRouter _documentsTabCUDRouter;

        public CurriculumController(UserDomainObjBuilder userObjBuilder,  
                DocumentStorageService documentStorageService,
                MessageBoardViewObjBuilder messageBoardViewObjBuilder, 
                DocumentsTabViewObjBuilder documentsTabViewObjBuilder, CurriculumViewObjBuilder curriculumViewObjBuilder,
                LessonPlanViewObjBuilder lessonPlanViewObjBuilder, NarrativeViewObjBuilder narrativeViewObjBuilder,
                LessonDomainObjBuilder lessonObjBuilder, MessageUseDomainObjBuilder messageUseObjBuilder,
                DocumentDomainObjBuilder documentObjBuilder, CurriculumCUDRouter curriculumCUDRouter,
                NarrativeTabCUDRouter narrativeTabCUDRouter, LessonPlanTabCUDRouter lessonPlanTabCUDRouter,
                MessageBoardTabCUDRouter messageBoardTabCUDRouter, DocumentsTabCUDRouter documentsTabCUDRouter)

            : base(userObjBuilder)
        {
            _userObjBuilder = userObjBuilder;
            _documentStorageService = documentStorageService;
            _documentsTabViewObjBuilder = documentsTabViewObjBuilder;
            _messageBoardViewObjBuilder = messageBoardViewObjBuilder;
            _curriculumViewObjBuilder = curriculumViewObjBuilder;
            _lessonPlanViewObjBuilder = lessonPlanViewObjBuilder;
            _narrativeViewObjBuilder = narrativeViewObjBuilder;

            _documentObjBuilder = documentObjBuilder;
            _lessonObjBuilder = lessonObjBuilder;
            _messageUseObjBuilder = messageUseObjBuilder;

            _curriculumCUDRouter = curriculumCUDRouter;
            _narrativeTabCUDRouter = narrativeTabCUDRouter;
            _lessonPlanTabCUDRouter = lessonPlanTabCUDRouter;
            _messageBoardTabCUDRouter = messageBoardTabCUDRouter;
            _documentsTabCUDRouter = documentsTabCUDRouter;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseId">Only relevant when a Lesson has not been selected. Otherwise will default to Course containing Lesson</param>
        /// <param name="lessonId">If Lesson is selected</param>
        /// <param name="showMasters">Show master courses in dropdown</param>
        /// <param name="showAll">Show all courses in dropdown</param>
        /// <param name="displayMessageUseId">Pass to Message Board</param>
        /// <param name="showArchivedMessages">Pass to Message Board</param>
        /// <param name="selectedDocumentUseId">Pass to Documents Tab</param>
        /// <param name="selectedDocumentId">Pass to Documents Tab</param>
        /// <param name="showUploadDialog">Pass to Documents Tab</param>
        /// <param name="showArchivedDocs">Pass to Documents Tab</param>
        /// <param name="showThisManyDocs">Pass to Documents Tab</param>
        /// <param name="isPdfCopyUpload">Pass to Documents Tab</param>
        /// <param name="fileValidationMessage">Pass to Documents Tab</param>
        /// <param name="showGroupLessons">Pass to Group Tab</param>
        /// <param name="showGroupNarrative">Pass to Group Tab</param>
        /// <param name="showGroupLessonPlan">Pass to Group Tab</param>
        /// <param name="showGroupDocuments">Pass to Group Tab</param>
        /// <param name="groupSelectedLessonId">Pass to Group Tab</param>
        /// <param name="groupReferenceLessonId">Pass to Group Tab</param>
        /// <param name="groupSelectedDocumentUseId">Pass to Group Tab</param>
        /// <param name="showGroupTab">Pass to Group Tab</param>
        /// <param name="showGroupDocStorage">Pass to Group Tab</param>
        /// <returns></returns>

        public ActionResult Index(Guid? courseId = null, Guid? lessonId = null, bool showMasters = false,
                                bool showAll = false, Guid? displayMessageUseId = null, bool showArchivedMessages = false,
                                Guid? selectedDocumentUseId = null, Guid? selectedDocumentId = null, bool showUploadDialog = false,
                                bool showArchivedDocs = false, int showThisManyDocs = -1, bool isPdfCopyUpload = false,
                                string fileValidationMessage = "", bool showGroupLessons = false, bool showGroupNarrative = false, 
                                bool showGroupLessonPlan = false, bool showGroupDocuments = false, Guid? groupSelectedLessonId = null, Guid? groupReferenceLessonId = null,
                                Guid? groupSelectedDocumentUseId = null, bool showGroupTab = false, bool showGroupDocStorage = false)
        {

            try
            {
                //I have been told that best practice is that all optional integers have the same default value, I'm using -1 as the default.  Hence the following code
                if (showThisManyDocs < 0)
                {
                    showThisManyDocs = 10;
                }

                Guid lastDisplayedCourseId = CurrentUserObj.LastDisplayedCourseId;


                CurriculumViewObj viewObj = _curriculumViewObjBuilder.RetrieveCurriculumViewObj(currentUserName: CurrentUserName, 
                                                                                        courseId: courseId ?? Guid.Empty, 
                                                                                        lessonId: lessonId ?? Guid.Empty, 
                                                                                        showMasters: showMasters, 
                                                                                        showAll: showAll, 
                                                                                        displayMessageUseId: displayMessageUseId ?? Guid.Empty,
                                                                                        showArchivedMessages: showArchivedMessages,
                                                                                        selectedDocumentUseId: selectedDocumentUseId ?? Guid.Empty,
                                                                                        selectedDocumentId: selectedDocumentId ?? Guid.Empty,
                                                                                        showUploadDialog: showUploadDialog,
                                                                                        showArchivedDocs: showArchivedDocs,
                                                                                        showThisManyDocs: showThisManyDocs,
                                                                                        isPdfCopyUpload: isPdfCopyUpload,
                                                                                        fileValidationMessage: fileValidationMessage,
                                                                                        showGroupDocStorage: showGroupDocStorage);



                //ViewData["ComboAction"] = "CoursesAll";
                //ViewData["ComboController"] = "CourseInfo";
                ModelState.Clear();
                return View(viewObj);
            }
            catch (Exception e)
            {
                WebLogger.Log.CurriculumIndexFailed(e);
                throw;
            }
        }

        public ActionResult SetCourseListDisplayValues(bool showMasters = false, bool showAll = false, Guid? selectedLessonId = null)
        {

            _curriculumCUDRouter.SetCourseListDisplayValues(currentUserId: CurrentUserId, showMasters: showMasters, showAll: showAll);

            return RedirectToAction("Index", new {lessonId = selectedLessonId ?? Guid.Empty });
        }

        /// <summary>
        /// Toggles whether we are editing an individual Lesson or master Lesson.  Should only available to those with Master Edit credentials
        /// </summary>
        /// <param name="isMasterEdit"></param>
        /// <param name="selectedLessonId"></param>
        /// <param name="returnLessonId">Id of individual Lesson to return to</param>
        /// <returns></returns>
        public ActionResult ToggleMasterEdit(bool isMasterEdit, Guid selectedLessonId, Guid returnLessonId)
        {
            try
            {
                Guid redirectLessonId = _curriculumViewObjBuilder.GetToggleMasterEditReturnLessonId(isMasterEdit, selectedLessonId, returnLessonId);
                return RedirectToAction("Index", new { lessonId = redirectLessonId });
            }
            catch (Exception e)
            {
                WebLogger.Log.ToggleMasterEditFailed(e);
                throw;
            }
        }

        public ActionResult ToggleLessonIsCollapsed(Guid lessonId)
        {
            _curriculumCUDRouter.ToggleLessonIsCollapsed(lessonId);
            return RedirectToAction("Index", new { lessonId = lessonId});
        }



        /// <summary>
        /// Toggles whether to show Lessons marked as "folders".  Boolean showFolders is in Course and isFolder is in Lesson
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public ActionResult ToggleShowFolders(Guid courseId, Guid lessonId)
        {
            try
            {
                _curriculumCUDRouter.ToggleShowFolders(courseId);
                return RedirectToAction("Index", new { lessonId = lessonId, courseId = courseId });
            }
            catch (Exception e)
            {
                WebLogger.Log.ToggleShowFoldersFailed(e);
                throw;
            }
        }

        /// <summary>
        /// Toggles whether to show Lessons with "isOptional = true."  showOptionalLessons is a bool in Course object.
        /// This is deprecated and should be deleted as soon as I have the time to remove all references.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public ActionResult ToggleShowOptionalLessons(Guid courseId, Guid lessonId)
        {
            try
            {
                _curriculumCUDRouter.ToggleShowOptionalLessons(courseId);
                return RedirectToAction("Index", new { lessonId = lessonId, courseId = courseId });
            }
            catch (Exception e)
            {
                WebLogger.Log.ToggleShowOptionalLessonsFailed(e);
                throw;
            }
        }

        //********************************************************************************
        public ActionResult Narrative(Guid lessonId, bool hasBasicEditRights, bool hasMasterEditRights, bool doShowNarrativeNotifications)
        {
            try
            {
                NarrativeViewObj viewObj = _narrativeViewObjBuilder.RetrieveNarrativeViewObj(lessonId: lessonId, 
                                                                                                currentUserId: CurrentUserId,
                                                                                                hasBasicEditRights: hasBasicEditRights,
                                                                                                hasMasterEditRights: hasMasterEditRights,
                                                                                                doShowNarrativeNotifications: doShowNarrativeNotifications);
                return View(viewObj);
            }
            catch (Exception e)
            {
                WebLogger.Log.NarrativeFailed(e);
                throw;
            }
        }

        public ActionResult NarrativeUpdate(TextEditorReturnModel model, Guid objectId, Guid lessonId)
        {
            try
            {
                _narrativeTabCUDRouter.UpdateNarrative(narrativeId: objectId, 
                                                        text: model.Text, 
                                                        lessonId: lessonId);
                return RedirectToAction("Index", new { lessonId = lessonId });
            }
            catch (Exception e)
            {
                WebLogger.Log.NarrativeUpdateFailed(e);
                throw;
            }
        }

        public ActionResult NarrativeUpdateCancel(Guid lessonId)
        {
            try
            {
                return RedirectToAction("Index", new { lessonId = lessonId });
            }
            catch (Exception e)
            {
                WebLogger.Log.NarrativeUpdateCancelFailed(e);
                throw;
            }
        }

        public ActionResult NarrativeDismissChangeNotification(Guid selectedLessonId)
        {
            _narrativeTabCUDRouter.UpdateNarrativeDateChoiceConfirmedToGroupDate(selectedLessonId);
            return RedirectToAction("Index", new { lessonId = selectedLessonId });
        }

        public ActionResult StashNarrative(Guid narrativeId, Guid lessonId)
        {
            try
            {
                _narrativeTabCUDRouter.CreateSavedStashedNarrative(lessonId: lessonId, narrativeId: narrativeId);
                return RedirectToAction("Index", new
                {
                    lessonId = lessonId
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }


        public ActionResult LessonPlan(Guid lessonId, bool isMasterEdit, bool hasBasicEditRights, bool hasMasterEditRights, bool doShowLessonPlanNotifications)
        {
            try
            {
                LessonPlanViewObj viewObj = _lessonPlanViewObjBuilder.Retrieve(selectedLessonId: lessonId, 
                                                                                isMasterEdit: isMasterEdit,
                                                                                hasBasicEditRights: hasBasicEditRights,
                                                                                hasMasterEditRights: hasMasterEditRights,
                                                                                doShowLessonPlanNotifications: doShowLessonPlanNotifications);
                return View(viewObj);
            }
            catch (Exception e)
            {
                WebLogger.Log.LessonPlanFailed(e);
                throw;
            }
        }

        public ActionResult StashLessonPlan(Guid lessonPlanId, Guid lessonId)
        {
            try
            {
                _lessonPlanTabCUDRouter.CreateSavedStashedLessonPlan(lessonId: lessonId, lessonPlanId: lessonPlanId);
                return RedirectToAction("Index", new
                {
                    lessonId = lessonId
                });
            }
            catch (Exception e)
            {
                WebLogger.Log.GenericFailure(e);
                throw;
            }
        }

        public ActionResult LessonPlanUpdate(TextEditorReturnModel model, Guid objectId, Guid lessonId,
                                               bool isPreNote = false, bool isPostNote = false)
        {
            try
            {
                _lessonPlanTabCUDRouter.AddOrUpdateLessonPlan(lessonPlanId: objectId, lessonId: lessonId, userId: CurrentUserId, 
                                                            text: model.Text, name: model.NameOrSubject, isPreNote: isPreNote,
                                                            isPostNote: isPostNote );
                return RedirectToAction("Index", new { lessonId = lessonId });
            }
            catch (Exception e)
            {
                WebLogger.Log.LessonPlanUpdateFailed(e);
                throw;
            }
        }

        public ActionResult LessonPlanUpdateCancel(Guid lessonId)
        {
            try
            {
                return RedirectToAction("Index", new { lessonId = lessonId });
            }
            catch (Exception e)
            {
                WebLogger.Log.LessonPlanUpdateCancelFailed(e);
                throw;
            }
        }

        public ActionResult SetLessonIsHidden(Guid lessonId, bool isHidden)
        {
            try
            {
                _curriculumCUDRouter.SetLessonIsHidden(lessonId: lessonId, isHidden: isHidden);

                return RedirectToAction("Index", new { selectedLessonId = lessonId });
            }
            catch (Exception e)
            {
                WebLogger.Log.LessonManager_LowerSequenceNumberFailed(e);
                throw;
            }
        }

        public ActionResult SetCourseShowHiddenLessons(Guid lessonId, Guid courseId, bool doShowHiddenLessons)
        {
            try
            {
                _curriculumCUDRouter.SetShowHiddenLessons(courseId: courseId, doShowHidden: doShowHiddenLessons);

                return RedirectToAction("Index", new { selectedLessonId = lessonId });
            }
            catch (Exception e)
            {
                WebLogger.Log.LessonManager_LowerSequenceNumberFailed(e);
                throw;
            }
        }


        //**************************************************************
        /// <summary>
        /// Displays the message board
        /// </summary>
        /// <param name="lessonId"></param>
        /// <param name="selectedMessageUseId"></param>
        /// <param name="showArchivedMessages">For archived messages</param>
        /// <returns></returns>
        public ActionResult MessageBoard(Guid lessonId, Guid selectedMessageUseId, bool showArchivedMessages)
        {
            try
            {
                MessageBoardViewObj viewObj = _messageBoardViewObjBuilder.Retrieve(lessonId, selectedMessageUseId, CurrentUserId, showArchivedMessages);
                return View(viewObj);
            }
            catch (Exception e)
            {
                WebLogger.Log.MessageBoardFailed(e);
                throw;
            }
        }

        public ActionResult PostMessage(TextEditorReturnModel model, Guid objectId, Guid lessonId)
        {
            try
            {
                _messageBoardTabCUDRouter.PostMessage(lessonId: model.SelectedLessonId, 
                                                    parentMessageUseId: model.ParentMessageUseId, 
                                                    authorId: CurrentUserId, 
                                                    subject: model.NameOrSubject, 
                                                    text: model.Text, 
                                                    isToSelf: model.IsToSelf, 
                                                    isToStorage: model.IsToStorage);

                return RedirectToAction("Index", new { lessonId = lessonId });
            }
            catch (Exception e)
            {
                WebLogger.Log.PostMessageFailed(e);
                throw;
            }
        }

        public ActionResult PostMessageFromClassroomWrap(TextEditorReturnModel model, Guid objectId, Guid lessonId)
        {
            try
            {
                _messageBoardTabCUDRouter.PostMessage(lessonId: model.SelectedLessonId,
                                                    parentMessageUseId: model.ParentMessageUseId,
                                                    authorId: CurrentUserId,
                                                    subject: model.NameOrSubject,
                                                    text: model.Text,
                                                    isToSelf: model.IsToSelf,
                                                    isToStorage: model.IsToStorage);

                return RedirectToAction("Index", "Classroom", new { });
            }
            catch (Exception e)
            {
                WebLogger.Log.PostMessageFailed(e);
                throw;
            }
        }

        public ActionResult PostMessageCancel(Guid lessonId)
        {
            try
            {
                return RedirectToAction("Index", new { lessonId = lessonId });
            }
            catch (Exception e)
            {
                WebLogger.Log.PostMessageCancelFailed(e);
                throw;
            }
        }

        public ActionResult PostMessageCancelFromClassroomWrap()
        {
            try
            {
                return RedirectToAction("Index", "Classroom", new { });
            }
            catch (Exception e)
            {
                WebLogger.Log.PostMessageCancelFailed(e);
                throw;
            }
        }


        public ActionResult UpdateMessageUseIsStored(Guid lessonId, Guid messageUseId, bool isStored)
        {
            try
            {
                _messageBoardTabCUDRouter.UpdateIsStored(messageUseId, isStored);
                return RedirectToAction("Index", new { lessonId = lessonId, displayMessageUseId = messageUseId });
            }
            catch (Exception e)
            {
                WebLogger.Log.UpdateMessageUseIsStoredFailed(e);
                throw;
            }
        }

        public ActionResult UpdateMessageUseIsImportant(Guid lessonId, Guid messageUseId, bool isImportant)
        {
            try
            {
                _messageBoardTabCUDRouter.UpdateIsImportant(messageUseId, isImportant);
                return RedirectToAction("Index", new { lessonId = lessonId, displayMessageUseId = messageUseId });
            }
            catch (Exception e)
            {
                WebLogger.Log.UpdateMessageUseIsImportantFailed(e);
                throw;
            }
        }

        public ActionResult UpdateMessageUseIsArchived(Guid lessonId, Guid messageUseId, bool isArchived, bool isShowArchived)
        {
            try
            {
                //archived values are coming in as they currently exist. The call is for what we want the Archived value to be. 
                _messageBoardTabCUDRouter.UpdateIsArchived(messageUseId, isArchived, isShowArchived);

                return RedirectToAction("Index", new { lessonId = lessonId, displayMessageUseId = messageUseId });
            }
            catch (Exception e)
            {
                WebLogger.Log.UpdateMessageUseIsArchivedFailed(e);
                throw;
            }
        }

        public ActionResult UpdateMessageUseIsForEndOfSemRev(Guid lessonId, Guid messageUseId, bool isForEndOfSemRev)
        {
            try
            {
                _messageBoardTabCUDRouter.UpdateIsForEndOfSemRev(messageUseId, isForEndOfSemRev);
                return RedirectToAction("Index", new { lessonId = lessonId, displayMessageUseId = messageUseId });
            }
            catch (Exception e)
            {
                WebLogger.Log.UpdateMessageUseIsForEndOfSemRevFailed(e);
                throw;
            }
        }

        public ActionResult ShowArchivedMessages(Guid lessonId, bool doShowArchivedMessages)
        {
            try
            {
                return RedirectToAction("Index", new { lessonId = lessonId, showArchivedMessages = doShowArchivedMessages });
            }
            catch (Exception e)
            {
                WebLogger.Log.ShowArchivedMessagesFailed(e);
                throw;
            }
        }

        public ActionResult UpdateAllMessageBooleans(Guid lessonId)
        {
            try
            {
                _messageBoardTabCUDRouter.UpdateAllMessageBooleansForLessonsSharingCourse(lessonId);
                return RedirectToAction("Index", new { lessonId = lessonId });
            }
            catch (Exception e)
            {
                throw;
            }
        }

        
        /// <summary>
        /// Displays Documents Tab
        /// </summary>
        /// <param name="lessonId"></param>
        /// <param name="selectedDocumentUseId"></param>
        /// <param name="selectedDocumentId">For non-active documents</param>
        /// <param name="showUploadDialog"></param>
        /// <param name="includeArchivedDocs"></param>
        /// <param name="showThisManyDocs"></param>
        /// <param name="isPdfCopyUpload">Only relevant with showUploadDialog</param>
        /// <param name="isRename">Only relevant with showUploadDialog</param>
        /// <param name="fileValidationMessage"></param>
        /// <param name="showGroupDocStorage">Tells factory to include group documents in ViewModel</param>
        /// <returns></returns>
        public ActionResult Documents(Guid lessonId, Guid selectedDocumentUseId, Guid selectedDocumentId, bool showUploadDialog,
                                    bool includeArchivedDocs, int showThisManyDocs, bool isRename = false, 
                                    string fileValidationMessage = "", bool showGroupDocStorage = false, bool hasComparisonDocsAvailable = false,
                                    bool hasBasicEditRights = false, bool hasMasterEditRights = false, bool isPdfCopyUpload = false)
        {
            try
            {                
                var documentsTabViewObj = _documentsTabViewObjBuilder.Retrieve(lessonId: lessonId, 
                                                                                selectedDocumentUseId: selectedDocumentUseId,
                                                                                selectedDocumentId: selectedDocumentId, 
                                                                                includeArchived: includeArchivedDocs, 
                                                                                showThisManyDocs: showThisManyDocs,
                                                                                 showGroupDocStorage: showGroupDocStorage, 
                                                                                 showUploadDialog: showUploadDialog, 
                                                                                isRename: isRename, 
                                                                                isPdfCopyUpload: isPdfCopyUpload,
                                                                                fileValidationMessage: fileValidationMessage, 
                                                                                hasComparisonDocsAvailable: hasComparisonDocsAvailable,
                                                                                hasBasicEditRights: hasBasicEditRights,
                                                                                hasMasterEditRights: hasMasterEditRights);

                //DocumentsTabViewModel model = _factory.BuildDocumentsTabViewModel(lessonId, selectedDocumentUseId, selectedDocumentId, 
                //                                showUploadDialog, includeArchivedDocs, showThisManyDocs, isPdfCopyUpload, isRename, 
                //                                fileValidationMessage, showGroupDocStorage, documentsTabViewObj);
                return View(documentsTabViewObj);
            }
            catch (Exception e)
            {
                WebLogger.Log.DocumentsFailed(e);
                throw;
            }
        }

        /// <summary>
        /// Downloads a document
        /// </summary>
        /// <param name="documentUseId"></param>
        /// <param name="isForEdit">This will mark isOutForEdit bool as true</param>
        /// <param name="skipSave">This is for pdfs.  If true will display in a new window, not saving, which is easier for user.</param>
        /// <param name="downloadPdfCopy">This is for documents that have both an original and a pdfCopy.</param>
        /// <returns></returns>
        public FileStreamResult DownloadDocument(Guid? documentUseId = null, Guid? documentId = null, bool isForEdit = false, bool skipSave = false, bool downloadPdfCopy = false)
        {
            try
            {
                DocumentDomainObj document = null;
                if(documentUseId != null)
                {
                    document = documentUseId == null ? null : _documentObjBuilder.DocumentForDocumentUse(documentUseId.Value);
                }
                else if(documentId != null)
                {
                    document = _documentObjBuilder.BuildFromId(documentId.Value);
                }
                



                if (documentUseId != Guid.Empty && document != null)
                {
                    if (isForEdit && documentUseId != null)
                    {
                        _documentsTabCUDRouter.SetIsOutForEdit(documentUseId.Value, true);
                    }

                    string storageFileName;
                    if (document != null)
                    {
                        if (document.HasPdfCopy && downloadPdfCopy)
                        {
                            storageFileName = document.FileNameOfPdfCopy;
                            skipSave = true;
                        }
                        else 
                        {
                            storageFileName = document.FileName;
                            skipSave = document.IsPdf;
                        }

                        string extension = Path.GetExtension(storageFileName);
                        Stream stream =  _documentStorageService.GetDownloadStream(storageFileName, GetServerPath());

                        //I'm keeping this here in case we get a .pdf converter someday.
                        //if (downloadPdfCopy)
                        //{
                        //    Stream pdfStream = _documentStorageService.DocOrDocxToPDF(stream, extension);
                        //    if(pdfStream != null)
                        //    {
                        //        extension = ".pdf";
                        //        stream = pdfStream;
                        //    }

                        //}

                        var userFileName = document.Name + extension;
                        var result = new FileStreamResult(stream, userFileName);
                        if (!skipSave) 
                        {
                            result.FileDownloadName = userFileName;
                        }
                        return result;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                WebLogger.Log.DownloadDocumentFailed(e);
                throw;
            }
        }

        public ActionResult RenameDocument(DocumentsTabReturnModel model)
        {
            try
            {
                _documentsTabCUDRouter.RenameDocument(model.SelectedDocumentUseId, model.SelectedDocumentName, model.SelectedDocumentUseModificationNotes);

                return RedirectToAction("Index", new { lessonId = model.SelectedLessonId });
            }
            catch (Exception e)
            {
                WebLogger.Log.RenameDocumentFailed(e);
                throw;
            }
        }

        [HttpPost]
        public ActionResult UploadNewDocument(HttpPostedFileBase file, DocumentsTabReturnModel model)
        {
            //Note that this also uploads edited documents
            try
            {
                Guid selectedLessonId = model.SelectedLessonId;
                if (file != null)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    if (_documentsTabCUDRouter.ValidateExtension(fileName))
                    {
                        Guid selectedDocumentUseId = model.SelectedDocumentUseId;
                        bool isReference = model.SelectedDocumentUseIsReference;
                        bool isInstructorOnly = model.SelectedDocumentUseIsInstructorOnly;
                        string name = model.SelectedDocumentName;
                        string modificationNotes = model.SelectedDocumentUseModificationNotes;
                        if (file.ContentLength > 0)
                        {
                            fileName = _documentsTabCUDRouter.UserFileNameToStorageFileName(fileName);
                            _documentStorageService.UploadFile(fileName, GetServerPath(), file);
                            if (model.IsPdfCopyUpload)
                            {
                                if (_documentsTabCUDRouter.ValidateIsPdf(fileName))
                                {
                                    _documentsTabCUDRouter.AddPdfCopy(selectedDocumentUseId, fileName);
                                }
                                else
                                {
                                    return RedirectToAction("Index", new { lessonId = selectedLessonId, showUploadDialog = true,
                                                                            fileValidationMessage = "Selected file is not a PDF.",
                                                                            isPdfCopyUpload = true,
                                                                            selectedDocumentUseId = selectedDocumentUseId 
                                                                          });
                                }
                            }
                            else
                            {
                                _documentsTabCUDRouter.CreateOrUpdateDocument(name, selectedLessonId, CurrentUserId, fileName, isReference, isInstructorOnly,
                                        selectedDocumentUseId, modificationNotes);
                            }
                        }
                        ViewBag.Message = "Upload successful";
                        return RedirectToAction("Index", new { lessonId = selectedLessonId });
                    }
                    else
                    {
                        return RedirectToAction("Index", new { lessonId = selectedLessonId, showUploadDialog = true, fileValidationMessage = "File extension is not supported." });
                    }

                }
                return RedirectToAction("Index", new { lessonId = selectedLessonId, showUploadDialog = true, fileValidationMessage = "Select a file to upload." });
                
            }
            catch (Exception e)
            {
                WebLogger.Log.UploadNewDocumentFailed(e);
                throw;
            }
        }

        

        private string GetServerPath()
        {
            return Server.MapPath("~/App_Data/Docs");
        }


        /// <summary>
        /// This just changes the isReference and isInstructorOnly booleans.  Thus "moves" from one category to another
        /// </summary>
        /// <param name="lessonId"></param>
        /// <param name="documentUseId"></param>
        /// <param name="isReference"></param>
        /// <param name="isInstructorOnly"></param>
        /// <returns></returns>
        public ActionResult MoveDocument(Guid lessonId, Guid documentUseId, bool isReference, bool isInstructorOnly)
        {

            try
            {
                _documentsTabCUDRouter.MoveDocument(documentUseId, isReference, isInstructorOnly);
                return RedirectToAction("Index", new { lessonId = lessonId });
            }
            catch (Exception e)
            {
                WebLogger.Log.MoveDocumentFailed(e);
                throw;
            }
        }

        public ActionResult RemoveDocumentFromLesson(Guid lessonId, Guid documentUseId)
        {
            try
            {
                _documentsTabCUDRouter.RemoveDocumentFromLesson(documentUseId);
                return RedirectToAction("Index", new { lessonId = lessonId });
            }
            catch (Exception e)
            {
                WebLogger.Log.RemoveDocumentFromLessonFailed(e);
                throw;
            }
        }

        public ActionResult AddDocumentToLesson(Guid lessonId, Guid documentId)
        {
            try
            {
                _documentsTabCUDRouter.AddDocumentToLesson(lessonId, documentId);
                return RedirectToAction("Index", new { lessonId = lessonId });
            }
            catch (Exception e)
            {
                WebLogger.Log.AddDocumentToLessonFailed(e);
                throw;
            }
        }

        /// <summary>
        /// This just kills the isOutForEdit boolean on the DocUse
        /// </summary>
        /// <param name="lessonId"></param>
        /// <param name="documentUseId"></param>
        /// <returns></returns>
        public ActionResult CancelEdit(Guid lessonId, Guid documentUseId)
        {
            try
            {
                _documentsTabCUDRouter.SetIsOutForEdit(documentUseId, false);
                return RedirectToAction("Index", new { lessonId = lessonId, selectedDocumentUseId = documentUseId });
            }
            catch (Exception e)
            {
                WebLogger.Log.CancelEditFailed(e);
                throw;
            }
        }



    }//End Class
}

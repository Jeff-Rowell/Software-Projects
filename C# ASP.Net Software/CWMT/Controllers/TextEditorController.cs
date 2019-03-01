using CWMasterTeacherDataModel;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.ViewObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain;

namespace CWMasterTeacher3
{
    [Authorize]
    public class TextEditorController : BaseController
    {
        NarrativeRepo _narrativeRepo;
        TextEditorViewObjBuilder _viewObjBuilder;

        public TextEditorController(NarrativeRepo narrativeRepo, UserDomainObjBuilder userObjBuilder, TextEditorViewObjBuilder textEditorViewObjBuilder) : base(userObjBuilder)
        {
            _narrativeRepo = narrativeRepo;
            _viewObjBuilder = textEditorViewObjBuilder;
        }
        
        public ActionResult Index(string editType, Guid objectId, Guid lessonId,  
                                    int lessonUseSequenceNumber = -1, bool isFromClassroomWrap = false, Guid? selectedClassMeetingId = null)
        {
            try
            {
                if (editType == DomainWebUtilities.TextEditType.Narrative )
                {
                    TextEditorViewObj viewObj = _viewObjBuilder.RetrieveViewObjForNarrative(lessonId, CurrentUserId);
                    return View(viewObj);
                }
                else if (editType == DomainWebUtilities.TextEditType.LessonPlan)
                {
                    TextEditorViewObj viewObj = _viewObjBuilder.RetrieveViewObjForLessonPlan(objectId, lessonId, CurrentUserId);
                    return View(viewObj);
                }
                else if (editType == DomainWebUtilities.TextEditType.Message)
                {
                    TextEditorViewObj viewObj = _viewObjBuilder.RetrieveViewObjForMessage(parentMessageUseId: objectId, 
                                                                                           selectedLessonId: lessonId, 
                                                                                           currentUserId: CurrentUserId, 
                                                                                           isToSelf: false, 
                                                                                           isToStorage: false,
                                                                                           isFromClassroomWrap: isFromClassroomWrap );
                    return View(viewObj);
                }

                else if (editType == DomainWebUtilities.TextEditType.MessageToSelf )
                {
                    TextEditorViewObj viewObj = _viewObjBuilder.RetrieveViewObjForMessage(parentMessageUseId: objectId,
                                                                                           selectedLessonId: lessonId,
                                                                                           currentUserId: CurrentUserId,
                                                                                           isToSelf: true,
                                                                                           isToStorage: false,
                                                                                           isFromClassroomWrap: isFromClassroomWrap);
                    return View(viewObj);
                }
                else if (editType == DomainWebUtilities.TextEditType.MessageToSelfStorage)
                {
                    TextEditorViewObj viewObj = _viewObjBuilder.RetrieveViewObjForMessage(parentMessageUseId: objectId,
                                                                                           selectedLessonId: lessonId,
                                                                                           currentUserId: CurrentUserId,
                                                                                           isToSelf: true,
                                                                                           isToStorage: true,
                                                                                           isFromClassroomWrap: isFromClassroomWrap);
                    return View(viewObj);
                }
                else if (editType == DomainWebUtilities.TextEditType.CustomLessonPlan)
                {
                    TextEditorViewObj viewObj = _viewObjBuilder.RetrieveViewObjForCustomLessonPlan(lessonUseId: objectId, 
                                                                    selectedLessonId: lessonId, 
                                                                    currentUserId: CurrentUserId,
                                                                    selectedClassMeetingId: selectedClassMeetingId == null ? Guid.Empty : selectedClassMeetingId.Value);
                    return View(viewObj);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                WebLogger.Log.TextEditorIndexFailed(e);
                throw;
            }


        }

    }
}

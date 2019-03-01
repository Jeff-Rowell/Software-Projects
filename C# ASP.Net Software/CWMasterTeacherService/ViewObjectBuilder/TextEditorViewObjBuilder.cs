using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain;
using CWMasterTeacherDataModel.Interfaces;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    public class TextEditorViewObjBuilder
    {
        private IUserDomainObjBuilder _userObjBuilder;
        private ICourseDomainObjBuilder _courseObjBuilder;
        private ILessonDomainObjBuilder _lessonObjBuilder;
        private ITermDomainObjBuilder _termObjBuilder;
        private ILessonPlanDomainObjBuilder _lessonPlanObjBuilder;
        private IMessageUseDomainObjBuilder _messageUseObjBuilder;
        private IMessageDomainObjBuilder _messageObjBuilder;
        private IDocumentUseDomainObjBuilder _documentUseObjBuilder;
        private IDocumentDomainObjBuilder _documentObjBuilder;
        private ILessonUseDomainObjBuilder _lessonUseObjBuilder;

        public TextEditorViewObjBuilder (IUserDomainObjBuilder userObjBuilder,
                                         ICourseDomainObjBuilder courseObjBuilder,
                                         ILessonDomainObjBuilder lessonObjBuilder,
                                         ITermDomainObjBuilder termObjBuilder,
                                         ILessonPlanDomainObjBuilder lessonPlanObjBuilder,
                                         IMessageUseDomainObjBuilder messageUseObjBuilder,
                                         IMessageDomainObjBuilder messageObjBuilder,
                                         IDocumentUseDomainObjBuilder documentUseObjBuilder,
                                         IDocumentDomainObjBuilder documentObjBuilder,
                                         ILessonUseDomainObjBuilder lessonUseObjBuilder
                                         )
        {
            _userObjBuilder = userObjBuilder;
            _courseObjBuilder = courseObjBuilder;
            _lessonObjBuilder = lessonObjBuilder;
            _termObjBuilder = termObjBuilder;
            _lessonPlanObjBuilder = lessonPlanObjBuilder;
            _messageUseObjBuilder = messageUseObjBuilder;
            _messageObjBuilder = messageObjBuilder;
            _documentUseObjBuilder = documentUseObjBuilder;
            _documentObjBuilder = documentObjBuilder;
            _lessonUseObjBuilder = lessonUseObjBuilder;
        }

        public TextEditorViewObj RetrieveViewObjForLessonPlan(Guid lessonPlanId, Guid selectedLessonId, 
                                                              Guid currentUserId)
        {
            TextEditorViewObj viewObj = new TextEditorViewObj();
            LessonDomainObj lessonObj = _lessonObjBuilder.BuildFromId(selectedLessonId );
            viewObj.IsMasterEdit = lessonObj.IsMaster;
            string masterSuffix = lessonObj.IsMaster ? " (Master)" : "";

            if (lessonPlanId != Guid.Empty)
            {
                LessonPlanDomainObj lessonPlanObj  = _lessonPlanObjBuilder.BuildFromId(lessonPlanId);
                viewObj.Text = lessonPlanObj.Text;
                viewObj.NameOrSubject = lessonPlanObj.Name;
                viewObj.ScreenTitle = "Edit " + DomainWebUtilities.LessonPlanTypeName +
                    " for: " + lessonObj.Name + masterSuffix;
            }
            else
            {
                viewObj.Text = "";
                viewObj.NameOrSubject = lessonObj.Name;
                viewObj.ScreenTitle = "Create " + DomainWebUtilities.LessonPlanTypeName +
                    " for: " + lessonObj.Name + masterSuffix;
            }

            viewObj.SelectedLessonId = selectedLessonId;
            viewObj.Id = lessonPlanId;
            viewObj.ControllerName = "Curriculum";
            viewObj.ActionName = "LessonPlanUpdate";
            viewObj.CancelActionName = "LessonPlanUpdateCancel";
            viewObj.NameOrSubjectCaption = DomainWebUtilities.LessonPlanTypeName + " Name";
            viewObj.CurrentUserObj = _userObjBuilder.BuildFromId(currentUserId);

            return viewObj;
        }

        public TextEditorViewObj RetrieveViewObjForCustomLessonPlan(Guid lessonUseId, Guid selectedLessonId, 
                                                                    Guid currentUserId, Guid selectedClassMeetingId)
        {
            TextEditorViewObj viewObj = new TextEditorViewObj();

            if (lessonUseId != Guid.Empty)
            {
                LessonUseDomainObj lessonUseObj = _lessonUseObjBuilder.BuildFromId(lessonUseId);
                viewObj.Text = _lessonUseObjBuilder.GetTextForLessonUse(lessonUseObj.Id);
                viewObj.NameOrSubject = lessonUseObj.Name;
                viewObj.ScreenTitle = "Edit " + DomainWebUtilities.LessonPlanTypeName + " for: " + lessonUseObj.Name;
            }
            else
            {
                viewObj.Text = "";
                viewObj.NameOrSubject = "";
                viewObj.ScreenTitle = "Create Custom " + DomainWebUtilities.LessonPlanTypeName;
            }

            viewObj.SelectedLessonId = selectedLessonId;
            viewObj.Id = lessonUseId;
            viewObj.ControllerName = "DailyPlanning";
            viewObj.ActionName = "CustomLessonPlanUpdate";
            viewObj.CancelActionName = "CustomLessonPlanUpdateCancel";
            viewObj.NameOrSubjectCaption = DomainWebUtilities.LessonPlanTypeName + " Name";
            viewObj.CurrentUserObj = _userObjBuilder.BuildFromId(currentUserId);

            return viewObj;
        }

        public TextEditorViewObj RetrieveViewObjForMessage(Guid parentMessageUseId, Guid selectedLessonId,
                                                           Guid currentUserId, bool isToSelf, bool isToStorage,
                                                           bool isFromClassroomWrap)
        {
            LessonDomainObj lessonObj = _lessonObjBuilder.BuildFromId(selectedLessonId);
            MessageUseDomainObj parentMessageUseObj = _messageUseObjBuilder.BuildFromId(parentMessageUseId);
            Guid parentMessageId = Guid.Empty;

            if (parentMessageUseObj != null)
            {
                parentMessageId = parentMessageUseObj.MessageId;
            }

            else
            {
                parentMessageUseId = Guid.Empty;
            }

            TextEditorViewObj viewObj = new TextEditorViewObj();

            if (parentMessageId != Guid.Empty) // Which means this is a reply
            {
                MessageDomainObj messageObj = _messageObjBuilder.BuildFromId(parentMessageId);
                viewObj.ParentMessageUseId = parentMessageUseId;
                viewObj.NameOrSubject = "Re: " + messageObj.Subject;
                viewObj.ScreenTitle = "Reply to Message for Lesson: " + lessonObj.Name;
            }

            else
            {
                viewObj.ScreenTitle = "Compose Message for Lesson: " + lessonObj.Name;
                viewObj.ParentMessageUseId = Guid.Empty;
                viewObj.NameOrSubject = "";
            }

            viewObj.IsToSelf = isToSelf;
            viewObj.IsToStorage = isToStorage;
            viewObj.SelectedLessonId = selectedLessonId;
            viewObj.Id = Guid.Empty;
            viewObj.ControllerName = "Curriculum";
            viewObj.ActionName = isFromClassroomWrap ? "PostMessageFromClassroomWrap" :  "PostMessage";
            viewObj.CancelActionName = isFromClassroomWrap ? "PostMessageCancelFromClassroomWrap" : "PostMessageCancel";
            viewObj.NameOrSubjectCaption = "Subject";
            viewObj.CurrentUserObj = _userObjBuilder.BuildFromId(currentUserId);

            return viewObj;
        }

        public TextEditorViewObj RetrieveViewObjForNarrative(Guid selectedLessonId, Guid currentUserId)
        {
            TextEditorViewObj viewObj = new TextEditorViewObj();
            LessonDomainObj lessonObj = _lessonObjBuilder.BuildFromId(selectedLessonId);
            viewObj.IsMasterEdit = lessonObj.IsMaster;
            string masterSuffix = lessonObj.IsMaster ? " (Master)" : "";

            string narrativeType = lessonObj.IsMaster ? DomainWebUtilities.NarrativeTypeName : 
                                                        DomainWebUtilities.NarrativeCommentTypeName;

            viewObj.ScreenTitle = "Edit " + narrativeType + " for: " + lessonObj.Name + masterSuffix;
            viewObj.Text = lessonObj.NarrativeForEditorText;
            viewObj.Id = lessonObj.NarrativeId;
            viewObj.ControllerName = "Curriculum";
            viewObj.ActionName = "NarrativeUpdate";
            viewObj.CancelActionName = "NarrativeUpdateCancel";
            viewObj.SelectedLessonId = selectedLessonId;
            viewObj.NameOrSubjectCaption = "Name";
            viewObj.CurrentUserObj = _userObjBuilder.BuildFromId(currentUserId);

            return viewObj;
        }
    }
}

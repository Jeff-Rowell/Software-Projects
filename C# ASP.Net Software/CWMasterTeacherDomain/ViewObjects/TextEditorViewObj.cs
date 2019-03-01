using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class TextEditorViewObj
    {
        public int LessonUseSequenceNumber { get; set; }
        public string CancelActionName { get; set; }
        public Guid SelectedClassMeetingId { get; set; }
        public Guid SelectedClassSectionId { get; set; }
        public string NameOrSubjectCaption { get; set; }


        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Required(ErrorMessage = "This field must be at least 3 characters.")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "This field must be at least 3 characters.")]
        public string NameOrSubject { get; set; }

        public UserDomainObj CurrentUserObj { get; set; }

        //This is the id of whatever is being edited, Narrative, LessonPlan, etc.
        public Guid Id { get; set; }

        //These are the controller and action to call when the submit button is clicked
        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        //This is so we can re-draw the screen correctly when we are done with the edit.
        public Guid SelectedLessonId { get; set; }

        public bool UserIsApplicationAdmin
        {
            get { return CurrentUserObj.IsApplicationAdmin; }
        }

        public bool UserIsWorkingGroupAdmin
        {
            get { return CurrentUserObj == null ? false : CurrentUserObj.IsWorkingGroupAdmin; }
        }

        public Guid ParentMessageUseId { get; set; }  //Only used for Message

        public bool IsToSelf { get; set; }

        public bool IsToStorage { get; set; }

        public string ScreenTitle { get; set; }

        public bool IsMasterEdit { get; set; }

        public string ScreenTitleCSS
        {
            get { return DomainWebUtilities.CourseLessonTitleClass(IsMasterEdit, true); }
        }

        public bool DoShowNameOrSubject
        {
            get { return ActionName == "LessonPlanUpdate" || ActionName == "PostMessage" || 
                    ActionName == "PostMessageFromClassroomWrap" || ActionName == "CustomLessonPlanUpdate"; }
        }

    }//End Class
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDataModel;

namespace CWMasterTeacher3
{
    public class WebLogger 
    {

        protected static WebLogger _log;

        private WebLogger() { }

        public  static WebLogger Log
        {
            get
            {
                if (_log == null)
                {
                    _log = new WebLogger();
                }
                return _log;
            }
        }

        protected void Write(string message)
        {
            Console.WriteLine(message);
            System.Diagnostics.Debug.Write(message);
        }


        public void ClassMeetingIndexFailed(Exception e)
        {
            Write("ClassMeetingIndexFailed: Error: " +  e.ToString() );
        }

        public void AddSingleClassMeetingFailed(Exception e)
        {
            Write("AddSingleClassMeetingFailed: " + e.ToString());
        }

        public void DeleteClassMeetingFailed(Exception e)
        {
            Write("DeleteClassMeetingFailed: " + e.ToString());
        }

        public void MarkClassMeetingAsNoClassFailed(Exception e)
        {
            Write(" MarkClassMeetingAsNoClassFailed: " + e.ToString());
        }
        public void ClassroomControllerIndexFailed(Exception e)
        {
            Write("ClassroomControllerIndexFailed: " + e.ToString());
        }
        public void ClassroomPlanFailed(Exception e)
        {
            Write(" ClassroomPlanFailed: " + e.ToString());
        }
        public void PreviewClassroomPlanFailed(Exception e)
        {
            Write("PreviewClassroomPlanFailed: " + e.ToString());
        }
        public void ClassroomDocumentsFailed(Exception e)
        {
            Write("ClassroomDocumentsFailed: " + e.ToString());
        }
        public void SetClassMeetingIsReadyFailed(Exception e)
        {
            Write("SetClassMeetingIsReadyFailed: " + e.ToString());
        }

        public void ClassSectionIndexFailed(Exception e)
        {
            Write("ClassSectionIndexFailed: " + e.ToString());
        }

        public void CreateClassSectionFailed(Exception e)
        {
            Write("CreateClassSectionFailes: " + e.ToString());
        }
        public void CourseCopyIndexFailed(Exception e)
        {
            Write("CourseCopyIndexFailed: " + e.ToString());
        }
        public void CopyCourseFailed(Exception e)
        {
            Write("CopyCourseFailed: " + e.ToString());
        }
        public void CourseCreateIndexFailed(Exception e)
        {
            Write("CourseCreateIndexFailed: " + e.ToString());
        }

        public void CreateCourseFailed(Exception e)
        {
            Write("CreateCourseFailed: " + e.ToString());
        }
        public void RenameCourseFailed(Exception e)
        {
            Write("RenameCourseFailed: " + e.ToString());
        }
        public void CheckIfCourseDeletableFailed(Exception e)
        {
            Write("CheckIfCourseDeletableFailed: " + e.ToString());
        }
        public void DeleteCourseFailed(Exception e)
        {
            Write("DeleteCourseFailed: " + e.ToString());
        }
        public void CreateWorkingGroupFailed(Exception e)
        {
            Write("CreateWorkingGroupFailed: " + e.ToString());
        }
        public void DeleteWorkingGroupFailed(Exception e)
        {
            Write("DelecteWorkingGroupFailed: " + e.ToString());
        }
        public void CurriculumIndexFailed(Exception e)
        {
            Write("CurriculumIndexFailed: " + e.ToString());
        }
        public void ToggleMasterEditFailed(Exception e)
        {
            Write("ToggleMasterEditFailed: " + e.ToString());
        }
        public void NarrativeFailed(Exception e)
        {
            Write("NarrativeFailed: " + e.ToString());
        }
        public void NarrativeUpdateFailed(Exception e)
        {
            Write("NarrativeUpdateFailed: " + e.ToString());
        }
        public void NarrativeUpdateCancelFailed(Exception e)
        {
            Write("NarrativeUpdateCancelFailed: " + e.ToString());
        }
        public void LessonPlanFailed(Exception e)
        {
            Write("LessonPlanFailed: " + e.ToString());
        }

        public void LessonPlanUpdateFailed(Exception e)
        {
            Write("LessonPlanUpdateFailed: " + e.ToString());
        }

        public void LessonPlanUpdateCancelFailed(Exception e)
        {
            Write("LessonPlanUpdateCancelFailed: " + e.ToString());
        }

        public void MessageBoardFailed(Exception e)
        {
            Write("MessageBoardFailed: " + e.ToString());
        }

        public void PostMessageFailed(Exception e)
        {
            Write("PostMessageFailed: " + e.ToString());
        }
        public void PostMessageCancelFailed(Exception e)
        {
            Write("PostMessageCancelFailed: " + e.ToString());
        }
        public void UpdateMessageUseIsStoredFailed(Exception e)
        {
            Write("UpdateMessageUseIsStoredFailed: " + e.ToString());
        }
        public void UpdateMessageUseIsImportantFailed(Exception e)
        {
            Write("UpdateMessageUseIsImportantFailed: " + e.ToString());
        }
        public void UpdateMessageUseIsArchivedFailed(Exception e)
        {
            Write("UpdateMessageUseIsArchivedFailed: " + e.ToString());
        }
        public void UpdateMessageUseIsForEndOfSemRevFailed(Exception e)
        {
            Write("UpdateMessageUseIsForEndOfSemRevFailed: " + e.ToString());
        }
        public void ShowArchivedMessagesFailed(Exception e)
        {
            Write("ShowArchivedMessagesFailed: " + e.ToString());
        }
        public void DocumentsFailed(Exception e)
        {
            Write("DocumentsFailed: " + e.ToString());
        }
        public void DownloadDocumentFailed(Exception e)
        {
            Write("DownloadDocumentFailed: " + e.ToString());
        }
        public void UploadNewDocumentFailed(Exception e)
        {
            Write("UploadNewDocumentFailed: " + e.ToString());
        }
        public void MoveDocumentFailed(Exception e)
        {
            Write("MoveDocumentFailed: " + e.ToString());
        }
        public void RemoveDocumentFromLessonFailed(Exception e)
        {
            Write("RemoveDocumentFromLessonFailed: " + e.ToString());
        }
        public void AddDocumentToLessonFailed(Exception e)
        {
            Write("AddDocumentToLessonFailed: " + e.ToString());
        }
        public void CancelEditFailed(Exception e)
        {
            Write("CancelEditFailed: " + e.ToString());
        }
        //End Curriculum Controller Exceptions
        public void DailyPlanningIndexFailed(Exception e)
        {
            Write("DailyPlanningIndexFailed: " + e.ToString());
        }

        public void DailyPlanning_AddLessonFailed(Exception e)
        {
            Write("DailyPlanning_AddLessonFailed: " + e.ToString());
        }

        public void DailyPlanning_RemoveLessonFailed(Exception e)
        {
            Write("DailyPlanning_RemoveLessonFailed: " + e.ToString());
        }
        public void DailyPlanning_MoveLessonBackFailed(Exception e)
        {
            Write("DailyPlanning_MoveLessonBackFailed: " + e.ToString());
        }
        public void DailyPlanning_MoveLessonForwardFailed(Exception e)
        {
            Write("DailyPlanning_MoveLessonForwardFailed: " + e.ToString());
        }
        public void DailyPlanning_RaiseSequenceNumberFailed(Exception e)
        {
            Write("DailyPlanning_RaiseSequenceNumberFailed: " + e.ToString());
        }
        public void DailyPlanning_LowerSequenceNumberFailed(Exception e)
        {
            Write("DailyPlanning_LowerSequenceNumberFailed: " + e.ToString());
        }
        public void DailyPlanning_AddReferenceCalendarFailed(Exception e)
        {
            Write("DailyPlanning_AddReferenceCalendarFailed: " + e.ToString());
        }
        public void DailyPlanning_CusomizePlanFailed(Exception e)
        {
            Write("DailyPlanning_CusomizePlanFailed: " + e.ToString());
        }
        public void DailyPlanning_CustomLessonPlanUpdateFailed(Exception e)
        {
            Write("DailyPlanning_CustomLessonPlanUpdateFailed: " + e.ToString());
        }
        public void DailyPlanning_CustomLessonPlanUpdateCancelFailed(Exception e)
        {
            Write("DailyPlanning_CustomLessonPlanUpdateCancelFailed: " + e.ToString());
        }
        public void DailyPlanning_SetClassMeetingIsReadyFailed(Exception e)
        {
            Write("DailyPlanning_SetClassMeetingIsReadyFailed: " + e.ToString());
        }

        //End Daily Planning controller exceptions
        public void LessonManagerIndexFailed(Exception e)
        {
            Write("LessonManagerIndexFailed: " + e.ToString());
        }
        public void LessonManager_CreateOrEditLessonFailed(Exception e)
        {
            Write("LessonManager_CreateOrEditLessonFailed: " + e.ToString());
        }
        public void LessonManager_DeleteLessonFailed(Exception e)
        {
            Write("LessonManager_DeleteLessonFailed: " + e.ToString());
        }
        public void LessonManager_RaiseSequenceNumberFailed(Exception e)
        {
            Write("LessonManager_RaiseSequenceNumberFailed: " + e.ToString());
        }
        public void LessonManager_LowerSequenceNumberFailed(Exception e)
        {
            Write("LessonManager_LowerSequenceNumberFailed: " + e.ToString());
        }
        //End LessonManager Exceptions

        public void LevelSet_IndexFailed(Exception e)
        {
            Write("LevelSet_IndexFailed: " + e.ToString());
        }
        public void LevelSet_CreateLevelSetFailed(Exception e)
        {
            Write("LevelSet_CreateLevelSetFailed: " + e.ToString());
        }
        public void LevelSet_AddLevelFailed(Exception e)
        {
            Write("LevelSet_AddLevelFailed: " + e.ToString());
        }
        public void LevelSet_RenameLevelFailed(Exception e)
        {
            Write("LevelSet_RenameLevelFailed: " + e.ToString());
        }
        public void LevelSet_DeleteLevelFailed(Exception e)
        {
            Write("LevelSet_DeleteLevelFailed: " + e.ToString());
        }
        //End Level Set Exceptions
        public void Term_IndexFailed(Exception e)
        {
            Write("Term_IndexFailed: " + e.ToString());
        }
        public void Term_CreateOrEditTermFailed(Exception e)
        {
            Write("Term_CreateOrEditTermFailed: " + e.ToString());
        }
        public void Term_AddHolidayFailed(Exception e)
        {
            Write("Term_AddHolidayFailed: " + e.ToString());
        }
        public void Term_DeleteHolidayFailed(Exception e)
        {
            Write("Term_DeleteHolidayFailed: " + e.ToString());
        }
        public void Term_MarkTermAsCurrentFailed(Exception e)
        {
            Write("Term_MarkTermAsCurrentFailed: " + e.ToString());
        }
        //End Term Exceptions
        public void TextEditorIndexFailed(Exception e)
        {
            Write("TextEditorIndexFailed: " + e.ToString());
        }
        //End Text Editor Exceptions
        public void UserIndexFailed(Exception e)
        {
            Write("UserIndexFailed: " + e.ToString());
        }
        public void User_EditUserFailed(Exception e)
        {
            Write("User_EditUserFailed: " + e.ToString());
        }
        public void User_AdminChangePasswordFailed(Exception e)
        {
            Write("User_AdminChangePasswordFailed: " + e.ToString());
        }

        public void ToggleLessonOptionalFailed(Exception e)
        {
            Write("ToggleLessonOptionalFailed: " + e.ToString());
        }

        public void ToggleShowFoldersFailed(Exception e)
        {
            Write("ToggleShowFoldersFailed: " + e.ToString());
        }
        public void ToggleShowOptionalLessonsFailed(Exception e)
        {
            Write("ToggleShowOptionalLessonsFailed: " + e.ToString());
        }
        public void RenameWorkingGroupFailed(Exception e)
        {
            Write("RenameWorkingGroupFailed: " + e.ToString());
        }
        public void RenameDocumentFailed(Exception e)
        {
            Write("RenameDocumentFailed: " + e.ToString());
        }
        public void GroupTabFailed(Exception e)
        {
            Write("GroupTabFailed: " + e.ToString());
        }
        public void GroupTabInactiveFailed(Exception e)
        {
            Write("GroupTabInactiveFailed: " + e.ToString());
        }
        public void GroupLessonsFailed(Exception e)
        {
            Write("GroupLessonsFailed: " + e.ToString());
        }
        public void GroupNarrativeFailed(Exception e)
        {
            Write("GroupNarrativeFailed: " + e.ToString());
        }
        public void GroupLessonPlanFailed(Exception e)
        {
            Write("GroupLessonPlanFailed: " + e.ToString());
        }
        public void GroupDocumentsFailed(Exception e)
        {
            Write("GroupDocumentsFailed: " + e.ToString());
        }
        public void GroupAddDocumentsToLessonFailed(Exception e)
        {
            Write("GroupAddDocumentsToLessonFailed: " + e.ToString());
        }
        public void ShowGroupTabFailed(Exception e)
        {
            Write("ShowGroupTabFailed: " + e.ToString());
        }
        public void GenericFailure(Exception e)
        {
            Write("GenericFailure: " + e.ToString());
        }


        //public void FooFailed(Exception e)
        //{
        //    Write("FooFailed: " + e.ToString());
        //}
        //public void FooFailed(Exception e)
        //{
        //    Write("FooFailed: " + e.ToString());
        //}
        //public void FooFailed(Exception e)
        //{
        //    Write("FooFailed: " + e.ToString());
        //}
        //public void FooFailed(Exception e)
        //{
        //    Write("FooFailed: " + e.ToString());
        //}
        //public void FooFailed(Exception e)
        //{
        //    Write("FooFailed: " + e.ToString());
        //}
        //public void FooFailed(Exception e)
        //{
        //    Write("FooFailed: " + e.ToString());
        //}
        //public void FooFailed(Exception e)
        //{
        //    Write("FooFailed: " + e.ToString());
        //}
        //public void FooFailed(Exception e)
        //{
        //    Write("FooFailed: " + e.ToString());
        //}

    }//End Class
}

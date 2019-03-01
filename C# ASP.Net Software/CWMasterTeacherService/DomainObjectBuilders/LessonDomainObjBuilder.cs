using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CWMasterTeacherDomain;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherService.CUDServices;

namespace CWMasterTeacherDataModel.ObjectBuilders
{
    /// <summary>
    /// A class to build lesson domain objects.
    /// </summary>
    public class LessonDomainObjBuilder : Interfaces.ILessonDomainObjBuilder
    {
        private readonly LessonRepo _lessonRepo;
        private readonly DocumentUseRepo _documentUseRepo;

        public LessonDomainObjBuilder(LessonRepo lessonRepo, DocumentUseRepo documentUseRepo)
        {
            _lessonRepo = lessonRepo;
            _documentUseRepo = documentUseRepo;
        }


        public static LessonDomainObj Build(Lesson lesson, bool doShowHidden)
        {
            return Build(lesson, doShowHidden, Guid.Empty, -1);
        }

        /// <summary>
        /// A method to build a lesson domain object.
        /// </summary>
        /// <param name="lesson"></param>
        /// <param name="referenceCourseId">This  is for the comparison tree.</param> 
        /// <param name="editableLessonId">This is fo the comparison tree</param>
        /// <param name="doShowHidden">used to show hidden Lessons in Master Course</param> 
        /// <returns></returns>
        public static LessonDomainObj Build(Lesson lesson, bool doShowHidden, Guid referenceCourseId,
                                         int comparisonModeInt)
        {
            var basic = BuildBasic(lesson);

            LessonDomainObj lessonObj = new LessonDomainObj(basic);

            lessonObj.ChangeNotes = lesson.ChangeNotes;
            lessonObj.ContainerLessonId = lesson.ContainerLessonId;
            lessonObj.EstimatedTimeMin = lesson.EstimatedTimeMin;
            lessonObj.NarrativeForEditorText = lesson.Narrative.Text;
            lessonObj.HasActiveMessages = lesson.HasActiveMessages;
            lessonObj.HasActiveStoredMessages = lesson.HasActiveStoredMessages;
            lessonObj.HasImportantMessages = lesson.HasImportantMessages;
            lessonObj.HasNewMessages = lesson.HasNewMessages;
            lessonObj.HasOutForEditDocuments = lesson.HasOutForEditDocuments;
            lessonObj.HasStoredMessages = lesson.HasStoredMessages;
            lessonObj.IsActive = lesson.IsActive;
            lessonObj.IsFolder = lesson.IsFolder;
            lessonObj.MasterLessonId = lesson.MasterLessonId.GetValueOrDefault();
            lessonObj.PredecessorLessonId = lesson.PredecessorLessonId;
            lessonObj.SequenceNumber = lesson.SequenceNumber;
            lessonObj.MasterCourseId = lesson.Course.MasterCourseId.GetValueOrDefault();
            lessonObj.WorkingGroupId = lesson.Course.WorkingGroupId;
            lessonObj.ComparisonModeInt = comparisonModeInt;
            lessonObj.MetaLessonId = lesson.MetaLessonId;
            lessonObj.IsHidden = lesson.IsHidden;
            lessonObj.ContainerChildren = GetContainerLessonChildren(lesson: lesson, doShowHidden: doShowHidden,
                                                            referenceCourseId: referenceCourseId,
                                                            comparisonModeInt: comparisonModeInt);
            lessonObj.HasCustomMasterEditRights = lesson.Course.HasMasterEditRights;

            //Here we are initializing some values that do not come from the Db
            lessonObj.SelectedLessonId = Guid.Empty;
            lessonObj.IsForPlanning = false;
            lessonObj.IsUsedInPlan = false;
            lessonObj.IsForLessonManager = false;
            lessonObj.IsForCompareSelect = referenceCourseId != Guid.Empty;

            if (referenceCourseId != Guid.Empty)
            {
                Lesson refLesson = null;
                if (referenceCourseId == lesson.Course.Id)
                {
                    refLesson = lesson;
                }
                else 
                {
                    Lesson masterLesson = lesson.Course.IsMaster ? lesson : lesson.MasterLesson;
                    if(masterLesson != null)
                    {
                        List<Lesson> lessonList = masterLesson.MasterLessonChildren.ToList();
                        lessonList.Add(masterLesson);

                        var refQuery = from x in lessonList
                                       where x.CourseId == referenceCourseId && x.IsActive
                                       select x;
                        refLesson = refQuery.FirstOrDefault();
                    }
                }

                if (refLesson != null)
                {
                    lessonObj.EditableLessonObj = BuildBasic(refLesson);
                }
            }

            return lessonObj;
        }

        public static LessonDomainObjBasic BuildBasic(Lesson lesson)
        {
            if (lesson == null)
            {
                return null;
            }
            else
            {
                LessonDomainObjBasic basicObj = new LessonDomainObjBasic();

                basicObj.IsMaster = lesson.Course.IsMaster;
                basicObj.Name = lesson.Name;
                basicObj.Id = lesson.Id;
                basicObj.CourseId = lesson.CourseId;
                basicObj.NarrativeDateChoiceConfirmed = lesson.NarrativeDateChoiceConfirmed.GetValueOrDefault();
                basicObj.NarrativeGroupMostRecentModDate = GetNarrativeGroupMostRecentModDate(lesson);
                basicObj.NarrativeDateModified = lesson.Narrative.DateModified;
                basicObj.LessonPlanDateChoiceConfirmed = lesson.LessonPlanDateChoiceConfirmed.GetValueOrDefault();
                basicObj.LessonPlanGroupMostRecentModDate = GetLessonPlanGroupMostRecentModDate(lesson);
                basicObj.LessonPlanDateModified = lesson.LessonPlan.DateModified;
                basicObj.DocumentsDateChoiceConfirmed = lesson.DateTimeDocumentsChoiceConfirmed == null ? new DateTime() : lesson.DateTimeDocumentsChoiceConfirmed.Value;
                basicObj.DocumentsGroupMostRecentModDate = GetDocumentsGroupMostRecentModDate(lesson);
                basicObj.DocumentsDateModified = GetDocumentsDateModified(lesson);
                basicObj.IsHidden = lesson.IsHidden;
                basicObj.IsCollapsed = lesson.IsCollapsed;
                basicObj.UserDisplayName = lesson.Course.User.DisplayName;
                basicObj.ActiveDocumentCount = lesson.DocumentUses.Where(x => x.IsActive).Count();
                basicObj.LessonPlanId = lesson.LessonPlanId;
                basicObj.NarrativeId = lesson.NarrativeId;
                basicObj.NarrativeTextLength = lesson.Narrative.Text.Length;
                basicObj.DoShowDocumentsNotifications = lesson.Course.CoursePreference.DoShowDocumentNotifications;
                basicObj.DoShowLessonPlanNotifications = lesson.Course.CoursePreference.DoShowLessonPlanNotifications;
                basicObj.DoShowNarrativeNotifications = lesson.Course.CoursePreference.DoShowNarrativeNotifications;
                    basicObj.FullNarrative = GetFullNarrative(lesson);
                basicObj.MasterDocumentsDateModified = lesson.Course.MasterCourseId == null || lesson.Course.MasterCourseId == Guid.Empty ?
                                    lesson.DateDocumentsModified.GetValueOrDefault() : lesson.MasterLesson.DateDocumentsModified.GetValueOrDefault();
                basicObj.MasterLessonPlanDateModified = lesson.Course.MasterCourseId == null || lesson.Course.MasterCourseId == Guid.Empty ?
                                    lesson.LessonPlan.DateModified : lesson.MasterLesson.LessonPlan.DateModified;
                basicObj.MasterNarrativeDateModified = lesson.Course.MasterCourseId == null || lesson.Course.MasterCourseId == Guid.Empty ?
                                    lesson.Narrative.DateModified : lesson.MasterLesson.LessonPlan.DateModified;
                return basicObj;
            }

        }

        public static LessonDomainObjBasic BuildBasicEmpty()
        {
            return new LessonDomainObjBasic()
            {
                IsMaster = false,
                Name = "_",
                Id = Guid.Empty,
                CourseId = Guid.Empty,
                NarrativeDateChoiceConfirmed = new DateTime(),
                NarrativeGroupMostRecentModDate = new DateTime(),
                LessonPlanDateChoiceConfirmed = new DateTime(),
                LessonPlanGroupMostRecentModDate = new DateTime(),
                DocumentsDateChoiceConfirmed = new DateTime(),
                DocumentsGroupMostRecentModDate = new DateTime()

            };
        }

        private static DateTime GetNarrativeGroupMostRecentModDate(Lesson lesson)
        {
            Lesson masterLesson = lesson.MasterLesson == null ? lesson : lesson.MasterLesson;
            List<DateTime> dateList = masterLesson.MasterLessonChildren
                                    .Where(x => x.Id != lesson.Id && x.Narrative.Text.Length > 2)
                                    .OrderByDescending(x => x.Narrative.DateModified)
                                    .Select(x => x.Narrative.DateModified).ToList();
            return dateList.Count == 0 ? new DateTime() : dateList.FirstOrDefault();
        }

        private static DateTime GetLessonPlanGroupMostRecentModDate(Lesson lesson)
        {
            Lesson masterLesson = lesson.MasterLesson == null ? lesson : lesson.MasterLesson;
            List<DateTime> dateList = masterLesson.MasterLessonChildren
                                    .Where(x => x.Id != lesson.Id)
                                    .OrderByDescending(x => x.LessonPlan.DateModified)
                                    .Select(x => x.LessonPlan.DateModified).ToList();
            return dateList.Count == 0 ? new DateTime() : dateList.FirstOrDefault();
        }

        private static DateTime GetDocumentsGroupMostRecentModDate(Lesson lesson)
        {
            Lesson masterLesson = lesson.MasterLesson == null ? lesson : lesson.MasterLesson;
            List<DateTime> dateList = masterLesson.MasterLessonChildren.Where(x => x.Id != lesson.Id)
                                    .OrderByDescending(x => x.DateDocumentsModified)
                                    .Select(x => x.DateDocumentsModified.GetValueOrDefault()).ToList();
            return dateList.Count == 0 ? new DateTime() : dateList.FirstOrDefault();
        }

        private static DateTime GetDocumentsDateModified(Lesson lesson)
        {
            List<DateTime> dateList = lesson.DocumentUses
                                            .OrderByDescending(x => x.Document.DateModified.GetValueOrDefault())
                                            .Select(x => x.Document.DateModified.GetValueOrDefault())
                                            .ToList();
            return dateList.Count == 0 ? new DateTime() : dateList.FirstOrDefault();
        }

        private static string GetFullNarrative(Lesson lesson)
        {
            User user = lesson.Course.User;
            Narrative masterNarrative = null;
            if (lesson.Course.IsMaster)
            {
                masterNarrative = lesson.Narrative;
            }
            else if (lesson.MasterLesson != null)
            {
                masterNarrative = lesson.MasterLesson.Narrative;
            }

            string fullText = "";

            if (masterNarrative != null)
            {
                string sharedHeading = "<h5>Master " + DomainWebUtilities.NarrativeTypeName + "____" + masterNarrative.DateModified.Date.ToString("d") + "</h5>";
                fullText = fullText + sharedHeading + masterNarrative.Text;
                List<Narrative> commentNarratives = masterNarrative.CommentNarratives.OrderByDescending(x => x.DateModified).ToList();
                foreach (var xNarrative in commentNarratives)
                {
                    if (xNarrative != null && xNarrative.Text.Length > 3)
                    {
                        string xHeading = "<h5>" + xNarrative.User.DisplayName + "'s " + DomainWebUtilities.NarrativeCommentTypeName + "____" + xNarrative.DateModified.Date.ToString("d") + "</h5>";
                        fullText = fullText + xHeading + xNarrative.Text;
                    }
                }
            }

            return fullText;
        }


        public LessonDomainObj BuildFromId(Guid id)
        {
            LessonDomainObj lessonObj = BuildFromId(id, false);
            return lessonObj;
        }

        /// <summary>
        /// A method to build a lesson domain object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LessonDomainObj BuildFromId(Guid id, bool doShowHidden)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            Lesson lesson = _lessonRepo.GetById(id);
            if (lesson == null)
            {
                return null;
            }
            else
            {
                LessonDomainObj lessonObj = Build(lesson, doShowHidden);
                lessonObj.ActiveDocumentCount = _documentUseRepo.ActiveDocumentUsesForLesson(id).Count();

                return lessonObj;
            }
        }

        /// <summary>
        /// A method to build a basic lesson domain object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LessonDomainObjBasic BuildBasicFromId(Guid id)
        {
            Lesson lesson = _lessonRepo.GetById(id);
            if (lesson == null)
            {
                return null;
            }
            else
            {
                return BuildBasic(lesson);
            }
        }

        public Guid ReturnLessonIdForMasterLesson(Guid lessonId, Guid userId)
        {
            Lesson returnLesson = _lessonRepo.LessonForMasterLessonAndUser(lessonId, userId);
            return returnLesson == null ? Guid.Empty : returnLesson.Id;
        }

        public int SiblingCountForLesson(Guid lessonId)
        {
            return _lessonRepo.LessonsForContainerLesson(lessonId).Count();
        }

        /// <summary>
        /// This is where we deal with the "DoShowHidden" question
        /// </summary>
        private static List<LessonDomainObj> GetContainerLessonChildren(Lesson lesson, bool doShowHidden, Guid referenceCourseId,
                                                                         int comparisonModeInt)
        {
            List<Lesson> childList = lesson.ContainerLessonChildren.ToList();
            if (!doShowHidden)
            {
                childList = childList.Where(x => !x.IsHidden).ToList();
            }
            return childList.Select(childLesson => Build(lesson: childLesson, doShowHidden: doShowHidden,
                                                          referenceCourseId: referenceCourseId,
                                                          comparisonModeInt: comparisonModeInt)).ToList();
        }

        public Guid GetMasterCourseIdForLessonId(Guid lessonId)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            Guid returnVal = new Guid();
            if (lesson != null)
            {
                returnVal = lesson.Course.MasterCourseId == null ? lesson.CourseId : lesson.Course.MasterCourseId.Value;
            }

            return returnVal;
        }

        public Guid GetCourseIdForLessonId(Guid lessonId)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);

            return lesson.CourseId;
        }

    }
}
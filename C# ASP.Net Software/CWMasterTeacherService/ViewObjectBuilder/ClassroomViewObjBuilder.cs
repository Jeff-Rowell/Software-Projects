using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherService.DomainObjectBuilders;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    public class ClassroomViewObjBuilder
    {
        private IClassMeetingDomainObjBuilder _classMeetingObjBuilder;
        private ILessonUseDomainObjBuilder _lessonUseObjBuilder;
        private ILessonPlanDomainObjBuilder _lessonPlanObjBuilder;
        private IDocumentUseDomainObjBuilder _documentUseObjBuilder;
        private IClassSectionDomainObjBuilder _classSectionObjBuilder;
        private ITermDomainObjBuilder _termObjBuilder;
        private IUserDomainObjBuilder _userObjBuilder;
        private ReleasedDocumentDomainObjBuilder _releasedDocumentObjBuilder;
        private CourseDomainObjBuilder _courseObjBuilder;

        public ClassroomViewObjBuilder(IClassMeetingDomainObjBuilder classMeetingObjBuilder, 
                                ILessonUseDomainObjBuilder lessonUseDomainObjBuilder,
                                ILessonPlanDomainObjBuilder lessonPlanObjBuilder,
                                IDocumentUseDomainObjBuilder documentUseObjBuilder,
                                IClassSectionDomainObjBuilder classSectionObjBuilder,
                                ITermDomainObjBuilder termObjBuilder,
                                IUserDomainObjBuilder userObjBuilder,
                                ReleasedDocumentDomainObjBuilder releasedDocumentObjBuilder,
                                CourseDomainObjBuilder courseObjBuilder)
        {
            _classMeetingObjBuilder = classMeetingObjBuilder;
            _lessonUseObjBuilder = lessonUseDomainObjBuilder;
            _lessonPlanObjBuilder = lessonPlanObjBuilder;
            _documentUseObjBuilder = documentUseObjBuilder;
            _classSectionObjBuilder = classSectionObjBuilder;
            _termObjBuilder = termObjBuilder;
            _userObjBuilder = userObjBuilder;
            _releasedDocumentObjBuilder = releasedDocumentObjBuilder;
            _courseObjBuilder = courseObjBuilder;
        }

        public ClassroomViewObj RetrieveClassroomViewObj(Guid currentUserId, Guid selectedTermId, Guid selectedClassMeetingId,
            bool showAllDates, DateTime? selectedDate, bool showReferenceDocs, bool doShowStudentManager, Guid classSectionId,
            bool showAllTerms)
        {
            //If we don't have a class section but we have a class meeting, we set the class section from the class meeting
            if (classSectionId == Guid.Empty && selectedClassMeetingId != Guid.Empty)
            {
                ClassMeetingDomainObj classMeeting = _classMeetingObjBuilder.BuildFromId(selectedClassMeetingId);
                classSectionId = classMeeting.ClassSectionId;
            }

            Guid lastDisplayedClassSectionId = _courseObjBuilder.GetLastDisplayedClassSectionId(currentUserId);
            //If we don't have a class section but we have a lastDisplayedClassSectionId, set class section to that
            if (classSectionId == Guid.Empty && lastDisplayedClassSectionId != Guid.Empty)
            {
                classSectionId = lastDisplayedClassSectionId;
            }


            ClassroomViewObj viewObj = new ClassroomViewObj();
            viewObj.CurrentUserObj = _userObjBuilder.BuildFromId(currentUserId);

            ClassSectionDomainObj classSectionObj = _classSectionObjBuilder.BuildFromId(classSectionId);
            classSectionObj = DailyPlanningViewObjBuilder.SetClassMeetingValues(classSectionObj, selectedClassMeetingId, Guid.Empty);
            viewObj.ClassSectionObj = classSectionObj;
            viewObj.SelectedClassSectionId = classSectionId;

            Guid lastDisplayedClassMeetingId = _classSectionObjBuilder.GetLastDisplayedClassMeetingId(currentUserId);
            if (selectedClassMeetingId == Guid.Empty && lastDisplayedClassMeetingId != Guid.Empty)
            {
                selectedClassMeetingId = lastDisplayedClassMeetingId;
            }

            if (selectedClassMeetingId != Guid.Empty)
            {
                ClassMeetingDomainObjBasic classMeetingObj = _classMeetingObjBuilder.BuildBasicFromId(selectedClassMeetingId);
                _classSectionObjBuilder.SetLastDisplayedClassMeetingId(classMeetingObj.Id, classMeetingObj.ClassSectionId );
                viewObj.SelectedClassMeetingObj = classMeetingObj;
            }

            List<ClassSectionDomainObjBasic> classSectionObjBasicList = new List<ClassSectionDomainObjBasic>();
            if (showAllTerms)
            {
                classSectionObjBasicList = _classSectionObjBuilder.ClassSectionsForUser(currentUserId).ToList();
            }
            else
            {
                classSectionObjBasicList = _classSectionObjBuilder.ClassSectionsForUserAndCurrentTerm(currentUserId).ToList();
            }
            viewObj.ClassSectionDomainObjBasicList = classSectionObjBasicList;



            DateTime date = new DateTime();

            TermDomainObj termObj;
            if (selectedTermId != Guid.Empty)
            {
                termObj = _termObjBuilder.BuildFromId(selectedTermId);
            }
            else
            {
                termObj = _termObjBuilder.CurrentTerm();
            }

            if (selectedDate != null)
            {
                date = selectedDate.Value;
            }
            else
            {
                date = DomainWebUtilities.Now;
            }

            List<DateViewObj> dateObjList = new List<DateViewObj>();
            if (showAllDates)
            {
                dateObjList = DateListToDateObjList(_classMeetingObjBuilder.DatesForUserAndTerm(currentUserId, termObj.Id), date);
            }
            else
            {
                dateObjList = DateListToDateObjList(_classMeetingObjBuilder.SevenCurrentDates(currentUserId, termObj.Id), date);
            }


            if (selectedDate == null)
            {
                var dateModelQuery = from x in dateObjList
                                     where x.Date > DomainWebUtilities.Now
                                     orderby x.Date
                                     select x;
                if (dateModelQuery.Count() > 0)
                {
                    date = dateModelQuery.FirstOrDefault().Date;
                }
            }

            viewObj.PossibleDatesObjList = dateObjList;
            viewObj.ShowAllDates = showAllDates;
            viewObj.SelectedTermId = selectedTermId;
            viewObj.DateLabelString = "Classes for " + DomainWebUtilities.DateTime_ToLongDateString(date);
            viewObj.SelectedDate = date;
            viewObj.ShowReferenceDocs = showReferenceDocs;
            viewObj.LessonPlanTypeName = DomainWebUtilities.LessonPlanTypeName;
            viewObj.DoShowStudentManager = doShowStudentManager;
            viewObj.ShowAllTerms = showAllTerms;

            return viewObj;
        }

        public ClassroomPlanViewObj RetrieveClassroomPlanViewObj(Guid selectedClassMeetingId, bool isPreview)
        {
            ClassroomPlanViewObj viewObj = new ClassroomPlanViewObj();
            viewObj.SelectedClassMeetingId = selectedClassMeetingId;
            viewObj.IsPreview = isPreview;


            string lessonPlanText = "";
            if (selectedClassMeetingId != Guid.Empty)
            {
                ClassMeetingDomainObj classMeetingObj = _classMeetingObjBuilder.BuildFromId(selectedClassMeetingId);
                string mainHeading = "<h3>" + classMeetingObj.ClassSectionName + "........" +
                                            DomainWebUtilities.DateTime_ToLongDateString(classMeetingObj.MeetingDate) + "</h3>";

                lessonPlanText = mainHeading;

                List<LessonUseDomainObj> lessonUseObjList = _lessonUseObjBuilder.LessonUseObjsForClassMeeting(selectedClassMeetingId).ToList();
                foreach (var xLessonUseObj in lessonUseObjList)
                {
                    string heading = "<h4>" + xLessonUseObj.Name + "<h4>";
                    string text = "";
                    if (xLessonUseObj.HasCustomText)
                    {
                        if (xLessonUseObj.CustomText != null)
                        {
                            text = xLessonUseObj.CustomText;
                        }

                        heading = "<h4>" + xLessonUseObj.CustomName + "<h4>";
                    }
                    else if (xLessonUseObj.LessonId != Guid.Empty && xLessonUseObj.LessonId != Guid.Empty)
                    {
                        heading = "<h4>" + xLessonUseObj.Name + "<h4>";
                        text = _lessonPlanObjBuilder.GetLessonPlanTextForLesson(xLessonUseObj.LessonId);
                    }
                    lessonPlanText = lessonPlanText + heading + text;
                }
                viewObj.LessonPlanText = lessonPlanText;
            }
            return viewObj;
        }

        public ClassroomDocsLessonViewObj RetrieveClassroomDocsLessonViewObj(Guid lessonUseId, bool showReferenceDocs)
        {
            ClassroomDocsLessonViewObj viewObj = new ClassroomDocsLessonViewObj();
            
            LessonUseDomainObj lessonUseObj = _lessonUseObjBuilder.BuildFromId(lessonUseId);
            viewObj.LessonUseObj = lessonUseObj;

            if(lessonUseObj.LessonId != Guid.Empty)
            {
                viewObj.TeachingDocs = _documentUseObjBuilder.TeachingDocumentUsesForLessonUse(lessonUseId);
                viewObj.InstructorOnlyDocs = _documentUseObjBuilder.InstructorOnlyDocumentUsesForLesson(lessonUseId);
                viewObj.ReferenceDocs = _documentUseObjBuilder.ReferenceDocumentUsesForLesson(lessonUseId);
                viewObj.ReleasedDocs = _releasedDocumentObjBuilder.ReleasedDocumentsForLessonUse(lessonUseId);

                if (lessonUseObj.CustomName != null && lessonUseObj.CustomName.Length > 2)
                {
                    viewObj.LessonName = lessonUseObj.CustomName;
                }
                else
                {
                    viewObj.LessonName = lessonUseObj.DisplayName;
                }
            }
            else
            {
                viewObj.LessonName = lessonUseObj.CustomName;
                viewObj.TeachingDocs = new List<DocumentUseDomainObj>();
                viewObj.InstructorOnlyDocs = new List<DocumentUseDomainObj>();
                viewObj.ReferenceDocs = new List<DocumentUseDomainObj>();
            }

            return viewObj;
        }

        private List<DateViewObj> DateListToDateObjList(List<DateTime> dateList, DateTime selectedDate)
        {
            List<DateViewObj> dateModelList = new List<DateViewObj>();
            bool gotIt = false;
            foreach (var xDate in dateList)
            {
                DateViewObj dateModel = new DateViewObj(xDate);
                if (xDate >= selectedDate && !gotIt)
                {
                    dateModel.IsSelected = true;
                    gotIt = true;
                }
                else
                {
                    dateModel.IsSelected = false;
                }
                dateModelList.Add(dateModel);
            }
            return dateModelList;
        }

        public ClassroomDocsMeetingViewObj RetrieveClassroomDocsMeetingViewObj(Guid selectedClassMeetingId,
                                                                                bool showReferenceDocs)
        {

            ClassroomDocsMeetingViewObj viewObj = new ClassroomDocsMeetingViewObj();
            if (selectedClassMeetingId != Guid.Empty)
            {
                ClassMeetingDomainObj meetingObj = _classMeetingObjBuilder.BuildFromId(selectedClassMeetingId);

                viewObj.DocsLessonList = GetClassroomDocsLessonList(meetingObj, showReferenceDocs);

                viewObj.Heading = meetingObj.LongDisplayName;
                viewObj.ClassMeetingId = selectedClassMeetingId;
            }
            else
            {
                viewObj.DocsLessonList = new List<ClassroomDocsLessonViewObj>();
                viewObj.Heading = "";
                viewObj.ClassMeetingId = Guid.Empty;
            }

            viewObj.ShowReferenceDocs = showReferenceDocs;

            return viewObj;

        }

        public ClassroomWrapViewObj RetrieveClassroomWrapViewObj(Guid selectedClassMeetingId, Guid classSectionId, 
                                                                    int awaitingApprovalCount, bool doAllowEditing)
        {
            ClassroomWrapViewObj viewObj = new ClassroomWrapViewObj();

            viewObj.ClassSectionId = classSectionId;
            ClassMeetingDomainObj classMeetingObj = _classMeetingObjBuilder.BuildFromId(selectedClassMeetingId);
            viewObj.SelectedClassMeetingObj = classMeetingObj;
            viewObj.DocsLessonList = GetClassroomDocsLessonList(classMeetingObj, false);
            viewObj.AwaitingApprovalCount = awaitingApprovalCount;
            viewObj.DoAllowEditing = doAllowEditing;

            return viewObj;
        }

         public List<ClassroomDocsLessonViewObj>GetClassroomDocsLessonList(ClassMeetingDomainObj classMeetingObj, bool showReferenceDocs)
        {
            List<ClassroomDocsLessonViewObj> docsLessonList = new List<ClassroomDocsLessonViewObj>();
            if(classMeetingObj != null)
            {
                foreach (var xLessonUseObj in classMeetingObj.LessonUseList)
                {
                    docsLessonList.Add(RetrieveClassroomDocsLessonViewObj(xLessonUseObj.Id, showReferenceDocs));
                }
                return docsLessonList;
            }
            else
            {
                return new List<ClassroomDocsLessonViewObj>();
            }

        }


    }//End Class
}

using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    public class DailyPlanningViewObjBuilder
    {
        private IClassSectionDomainObjBuilder _classSectionObjBuilder;
        private IClassMeetingDomainObjBuilder _classMeetingObjBuilder;
        private ICourseDomainObjBuilder _courseObjBuilder;
        private ILessonUseDomainObjBuilder _lessonUseObjBuilder;
        private IUserDomainObjBuilder _userObjBuilder;
        private ILessonDomainObjBuilder _lessonObjBuilder;

        public DailyPlanningViewObjBuilder(IClassSectionDomainObjBuilder classSectionObjBuilder,
                                    IClassMeetingDomainObjBuilder classMeetingObjBuilder,
                                    ICourseDomainObjBuilder courseObjBuilder,
                                    ILessonUseDomainObjBuilder lessonUseObjBuilder,
                                    IUserDomainObjBuilder userObjBuilder,
                                    ILessonDomainObjBuilder lessonObjBuilder
                                    )
        {
            _classSectionObjBuilder = classSectionObjBuilder;
            _classMeetingObjBuilder = classMeetingObjBuilder;
            _courseObjBuilder = courseObjBuilder;
            _lessonUseObjBuilder = lessonUseObjBuilder;
            _userObjBuilder = userObjBuilder;
            _lessonObjBuilder = lessonObjBuilder;
        }

        public DailyPlanViewObj RetrieveDailyPlanViewObj(Guid classSectionId, Guid currentUserId, Guid selectedLessonId, 
            Guid referenceClassSectionId, bool showAllTerms, Guid selectedLessonUseId, bool showAddReferenceCalendar, 
            int numberOfPossibleRefCalSections, Guid selectedClassMeetingId)
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

            DailyPlanViewObj viewObj = new DailyPlanViewObj();

            viewObj.SelectedClassSectionId = classSectionId;
            viewObj.SelectedLessonUseId = selectedLessonUseId;
            viewObj.CurrentUserObj = _userObjBuilder.BuildFromId(currentUserId);
            
            List<ClassSectionDomainObjBasic> classSectionObjBasicList = new List<ClassSectionDomainObjBasic>();
            if (showAllTerms)
            {
                classSectionObjBasicList = _classSectionObjBuilder.ClassSectionsForUser(currentUserId).ToList();
            }
            else
            {
                classSectionObjBasicList = _classSectionObjBuilder
                    .ClassSectionsForUserAndCurrentTerm(currentUserId).ToList();
            }
            viewObj.ClassSectionDomainObjBasicList = classSectionObjBasicList;

            if (classSectionId != Guid.Empty)
            {
                ClassSectionDomainObj classSectionObj = _classSectionObjBuilder.BuildFromId(classSectionId);
                viewObj.ClassSectionObj = classSectionObj;
                if(classSectionObj != null)
                {
                    CourseDomainObj courseObj = _courseObjBuilder.BuildFromId(classSectionObj.CourseId);
                    HashSet<Guid> usedLessonIdSet = GetUsedLessonIdSet(classSectionObj);
                    courseObj.SetIsUsedInPlanBooleansForAllLessons(usedLessonIdSet);
                    //If we didn't get passed in a selected lesson, let's try to get the last one displayed for the course.
                    if (selectedLessonId == Guid.Empty)
                    {
                        selectedLessonId = courseObj.LastDisplayedLessonId;
                    }
                    viewObj.SelectedLessonObjBasic = _lessonObjBuilder.BuildBasicFromId(selectedLessonId);

                    if (classSectionId != lastDisplayedClassSectionId)
                    {
                        _courseObjBuilder.SetLastDisplayedClassSectionId(classSectionId);
                    }


                    if (selectedLessonUseId != Guid.Empty)
                    {
                        LessonUseDomainObj lessonUseObj = _lessonUseObjBuilder.BuildFromId(selectedLessonUseId);
                        selectedClassMeetingId = lessonUseObj.ClassMeetingId;
                        viewObj.SelectedLessonUseSequencNumber = lessonUseObj.SequenceNumber.Value;
                        if (lessonUseObj.LessonId != Guid.Empty)
                        {
                            _courseObjBuilder.SetLastDisplayedLessonId(lessonUseObj.LessonId);
                            selectedLessonId = lessonUseObj.LessonId;
                        }
                    }
                    else if (selectedLessonId != Guid.Empty)
                    {
                        _courseObjBuilder.SetLastDisplayedLessonId(selectedLessonId);
                    }

                    courseObj.SelectedLessonId = selectedLessonId;
                    viewObj.CourseObj = courseObj;

                    classSectionObj = SetClassMeetingValues(classSectionObj, selectedClassMeetingId, selectedLessonId);

                    //Now Build the Reference Calendar Section List
                    List<ClassSectionDomainObjBasic> referenceSectionList = _classSectionObjBuilder
                        .ReferenceSectionsForClassSection(classSectionId).ToList();
                    viewObj.ReferenceCalendarClassSectionSelectList = new SelectList(referenceSectionList, "Id", "DisplayName");

                    //And the possible Reference Calendar selection List
                    if (showAddReferenceCalendar)
                    {
                        List<ClassSectionDomainObjBasic> refSectionList = _classSectionObjBuilder
                            .PossibleReferenceClassSections(classSectionId, numberOfPossibleRefCalSections).ToList();
                        viewObj.ReferenceCalendarPossibleClassSectionSelectList = new SelectList(refSectionList, "Id", "DisplayName");
                    }

                    //And the reference calendar classMeetingList
                    if (referenceClassSectionId != Guid.Empty)
                    {
                        ClassSectionDomainObj referenceSection = _classSectionObjBuilder.BuildFromId(referenceClassSectionId);
                        referenceSection = SetClassMeetingValues(referenceSection, selectedClassMeetingId, selectedLessonId);
                        viewObj.ReferenceCalendarClassMeetingObjList = referenceSection.ClassMeetingList;
                        viewObj.ReferenceCalDropdownHeading = "Hide Reference Calendar";
                        viewObj.ReferenceClassSectionId = referenceClassSectionId;
                    }
                    else
                    {
                        viewObj.ReferenceCalDropdownHeading = "Select Reference Calendar";
                    }

                    viewObj.ShowAddReferenceCalendar = showAddReferenceCalendar;
                    viewObj.ShowAllTerms = showAllTerms;
                }//End if (classSection Obj != null)
            } //End If (classSectionId > 0)

            return viewObj;
        }

        public static ClassSectionDomainObj SetClassMeetingValues(ClassSectionDomainObj classSectionObj,
                                                            Guid selectedClassMeetingId,
                                                            Guid selectedLessonId)
        {
            //Now  set the variables such as class number, allowing for holidays
            List<ClassMeetingDomainObj> classMeetingList = classSectionObj == null 
                ? new List<ClassMeetingDomainObj> () : classSectionObj.ClassMeetingList;
            classMeetingList = classMeetingList.OrderBy(x => x.MeetingDate).ThenBy(x => x.StartTime).ToList();
            if (classMeetingList != null && classMeetingList.Count > 0)
            {
                int classIndex = 0;
                int weekIndex = 1;
                bool haveNext = false;
                foreach (var xMeeting in classMeetingList)
                {
                    xMeeting.IsNextClass = false;
                    xMeeting.IsLessonUseSelected = xMeeting.Id == selectedClassMeetingId;
                    xMeeting.IsALessonSelected = selectedLessonId != Guid.Empty;

                    if (!xMeeting.IsExamDay && !xMeeting.IsNoClass)
                    {
                        classIndex = classIndex + 1;
                        xMeeting.ClassNumber = classIndex;
                        xMeeting.WeekNumber = weekIndex;
                        if (xMeeting.IsBeginningOfWeek)
                        {
                            weekIndex = weekIndex + 1;
                        }
                        if (!haveNext && xMeeting.StartTime.CompareTo(DateTime.Now) >= 0)
                        {
                            xMeeting.IsNextClass = true;
                            haveNext = true;
                        }
                    }
                    else
                    {
                        xMeeting.ClassNumber = -1;
                    }
                }

                foreach (var xMeetingModel in classMeetingList)
                {
                    xMeetingModel.ClassCount = classIndex;
                }
                classSectionObj.ClassMeetingList = classMeetingList;
            }//End if classMeetingList != null
            
            return classSectionObj;
        }

        private HashSet<Guid> GetUsedLessonIdSet(ClassSectionDomainObj classSectionObj)
        {
            HashSet<Guid> usedLessonIdSet = new HashSet<Guid>();

            foreach (var xClassMeeting in classSectionObj.ClassMeetingList)
            {
                foreach (var xLessonUse in xClassMeeting.LessonUseList)
                {
                    usedLessonIdSet.Add(xLessonUse.LessonId);
                }
            }
            return usedLessonIdSet;
        }
    }//End Class
}

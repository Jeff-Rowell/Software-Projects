using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class StudentHomeViewObj
    {
        public List<WorkingGroupDomainObjBasic> WorkingGroupList { get; set; }
        public IEnumerable<SelectListItem> WorkingGroupSelectList
        {
            get { return new SelectList(WorkingGroupList, "Id", "Name"); }
        }
        public bool DoShowSelectWorkingGroup
        {
            get { return WorkingGroupList != null && WorkingGroupList.Count > 1; }
        }


        public List<UserDomainObjBasic> InstructorList { get; set; }
        public UserDomainObjBasic SelectedInstructor { get; set; }
        [Display(Name = "Select Instructor")]
        public Guid InstructorUserId { get; set; }
        public IEnumerable<SelectListItem> InstructorSelectList
        {
            get { return new SelectList(InstructorList, "Id", "Name"); }
        }

        public List<ClassSectionDomainObjBasic> ClassSectionList { get; set; }

        /// <summary>
        /// This is the class section for requesting access.
        /// </summary>
        public ClassSectionDomainObjBasic SelectedClassSection { get; set; }
        [Display(Name = "Select Class Section")]
        public Guid ClassSectionId { get; set; }
        public IEnumerable<SelectListItem> ClassSectionSelectList
        {
            get { return new SelectList(ClassSectionList, "Id", "Name"); }
        }

        public List<ClassSectionStudentDomainObjBasic> ClassesAwaitingApprovalClassSecStdt { get; set; }
        public List<ClassSectionStudentDomainObjBasic> ClassesDeniedClassSecStdt { get; set; }
        public List<ClassSectionStudentDomainObjBasic> CurrentClassesClassSecStdt { get; set; }


        /// <summary>
        /// This is the class section that will display meetings and documents
        /// </summary>
        public Guid DisplayedClassSectionId { get; set; }



        public bool DoShowRequestCourseAccess { get; set; }
        public bool DoShowRequestGroupAccess { get; set; }

        [Display(Name = "Enter Group Access Code")]
        public string AccessCode { get; set; }

        public string AccessRequestMessage { get; set; }
        public string CourseRequestMessage { get; set; }

        public List<ClassMeetingDomainObjBasic> ClassMeetingList { private get; set; }

        public List<ClassMeetingDomainObjBasic> ClassMeetingSelectionList
        {
            get
            {
                if (ClassMeetingList != null && ClassMeetingList.Count > 0)
                {
                    return ShowAllDates && ClassMeetingList != null ? MarkLastClassMeeting(ClassMeetingList) : CurrentNineMeetingsList;
                }
                else
                {
                    return new List<ClassMeetingDomainObjBasic>();
                }
            }

        }

        public ClassMeetingDomainObj SelectedClassMeetingObj { get; set; }

        public Guid SelectedClassMeetingId
        {
            get { return SelectedClassMeetingObj == null ? Guid.Empty : SelectedClassMeetingObj.Id; }
        }

        public string SelectedMeetingDateString
        {
            get { return SelectedClassMeetingObj == null ? "" : SelectedClassMeetingObj.MeetingDateString; }
        }

        public List<ClassroomDocsLessonViewObj> LessonList { get; set; }

        public List<ClassroomDocsLessonViewObj> LessonDisplayList
        {
            get
            {
                return LessonList != null && LessonList.Count > 0 ? LessonList: new List<ClassroomDocsLessonViewObj>();
            }
        }

        public bool UserIsApplicationAdmin
        {
            get { return false; }
        }

        public bool UserIsWorkingGroupAdmin
        {
            get { return false; }
        }

        public bool UserIsEditor
        {
            get { return false; }
        }

        public StudentUserDomainObj StudentUserObj { get; set; }
        public string WelcomeMessage
        {
            get { return StudentUserObj == null ? "" : "Welcome, " + StudentUserObj.FullName + "."; }
        }

        public bool DoShowWaitingForApprovalList
        {
            get { return ClassesAwaitingApprovalClassSecStdt == null ? false : ClassesAwaitingApprovalClassSecStdt.Count > 0 && !DoShowRequestGroupAccess && !DoShowRequestCourseAccess; }
        }

        public bool DoShowDeniedList
        {
            get { return ClassesDeniedClassSecStdt == null ? false : ClassesDeniedClassSecStdt.Count > 0 && !DoShowRequestGroupAccess && !DoShowRequestCourseAccess; }
        }

        public bool DoShowCurrentClasses
        {
            get { return CurrentClassesClassSecStdt == null ? false : CurrentClassesClassSecStdt.Count > 0 && !DoShowRequestCourseAccess && !DoShowRequestGroupAccess; }
        }

        //public string CourseRequestCSS
        //{
        //    get
        //    {
        //        if (DoShowCurrentClasses || DoShowDeniedList || DoShowWaitingForApprovalList)
        //        {
        //            return "cw-underline";
        //        }
        //        else
        //        {
        //            return "cw-underline cw-bold";
        //        }
        //    }
        //}

        public bool ShowAllDates { get; set; }

        private List<ClassMeetingDomainObjBasic> CurrentNineMeetingsList
        {
            get
            {
                List<ClassMeetingDomainObjBasic> pastMeetings = ClassMeetingList.Where(x => x.MeetingDate <= DateTime.Now).OrderByDescending(x => x.MeetingDate).ToList();
                pastMeetings = pastMeetings.Take(5).ToList();
                //ClassMeetingDomainObjBasic lastMeeting = pastMeetings != null && pastMeetings.Count > 0 ? pastMeetings.FirstOrDefault() : ClassMeetingList.FirstOrDefault();
                //if (lastMeeting != null)
                //{
                //    lastMeeting.IsCurrent = true;
                //}
                List<ClassMeetingDomainObjBasic> futureMeetings = ClassMeetingList.Where(x => x.MeetingDate > DateTime.Now).ToList();
                futureMeetings = futureMeetings.Take(4).ToList();

                List<ClassMeetingDomainObjBasic> returnList = pastMeetings.Concat(futureMeetings).OrderBy(x => x.MeetingDate).ToList();
                return MarkLastClassMeeting(returnList);
            }
        }

        private List<ClassMeetingDomainObjBasic> MarkLastClassMeeting(List<ClassMeetingDomainObjBasic> meetingList)
        {
            ClassMeetingDomainObjBasic lastMeeting = meetingList.Where(x => x.MeetingDate <= DateTime.Now).OrderByDescending(x => x.MeetingDate).FirstOrDefault();
            lastMeeting.IsCurrent = true;
            return meetingList;
        }

        public bool DoShowGroupAccessInstructions
        {
            get { return WorkingGroupList == null || WorkingGroupList.Count == 0; }
        }
        public string GroupAccessInstructions
        {
            get { return "From the 'Request Access' dropdown above, click on 'Request Group Access.'  You will need the access code that you got from your instructor."; }
        }

        public bool DoShowCourseAccessInstructions
        {
            get { return CurrentClassesClassSecStdt.Count == 0 && ClassesAwaitingApprovalClassSecStdt.Count == 0 && 
                    ClassesDeniedClassSecStdt.Count == 0 && !DoShowGroupAccessInstructions  && !DoShowRequestCourseAccess; }
        }
        public string CourseAccessInstructions
        {
            get { return "From the 'Request Access' dropdown above, click on 'Request Course Access.'"; }
        }

        [DataType(DataType.MultilineText)]
        public string NotesForStudents
        {
            get
            {
                if(SelectedClassMeetingObj != null)
                {
                    return SelectedClassMeetingObj.NotesForStudents != null && SelectedClassMeetingObj.NotesForStudents.Length > 2 ? SelectedClassMeetingObj.NotesForStudents : "Instructor has not entered any notes.";
                }
                else
                {
                    return "_";
                }
            }
        }



    }
}

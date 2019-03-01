using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWMasterTeacherDomain.ViewObjects;

namespace CWMasterTeacher3
{
    public class CourseCreateAdminReturnModel
    {

        public CourseCreateAdminReturnModel()
        {
        }

        //public bool ShowRenameWorkingGroup
        //{
        //    get { return _viewObj.ShowRenameWorkingGroup; }
        //    set { _viewObj.ShowRenameWorkingGroup = value; }
        //}

        //public bool IsCourseDeletable
        //{
        //    get { return _viewObj.IsCourseDeletable; }
        //    set { _viewObj.IsCourseDeletable = value; }
        //}

        [Display(Name = "Show All Courses")]
        public bool ShowAllCourses { get; set; }

        //public bool ShowDeleteWorkingGroup
        //{
        //    get { return _viewObj.ShowDeleteWorkingGroup; }
        //    set { _viewObj.ShowDeleteWorkingGroup = value; }
        //}


        [Required(ErrorMessage = "This field must be at least 5 characters.")]
        [MinLength(5)]
        public string WorkingGroupName { get; set; }

        //public bool ShowNewWorkingGroup
        //{
        //    get { return _viewObj.ShowNewWorkingGroup; }
        //    set { _viewObj.ShowNewWorkingGroup = value; }
        //}

        public bool ShowDeleteCourse { get; set; }

        //public IEnumerable<SelectListItem> CourseSelectList
        //{
        //    get { return _viewObj.CourseSelectList; }
        //}

        //public string Message
        //{
        //    get { return _viewObj.Message; }
        //    set { _viewObj.Message = value; }
        //}

        //private bool _userIsEditor;
        //private bool _userIsAdmin;

        //public bool UserIsAdmin
        //{
        //    get { return _userIsAdmin; }
        //    set { _userIsAdmin = value; }
        //}

        //public bool UserIsEditor
        //{
        //    get { return _userIsEditor; }
        //    set { _userIsEditor = value; }
        //}


        //public IEnumerable<SelectListItem> UserSelectList
        //{
        //    get { return _viewObj.UserSelectList; }
        //}

        public Guid UserId { get; set; }


        //public IEnumerable<SelectListItem> TermSelectList
        //{
        //    get { return _viewObj.TermSelectList; }
        //}

        public Guid TermId { get; set; }


        //public IEnumerable<SelectListItem> WorkingGroupSelectList
        //{
        //    get { return _viewObj.WorkingGroupSelectList; }
        //}
        public Guid WorkingGroupId { get; set; }




        [Required(ErrorMessage = "This field must be at least 5 characters.")]
        [MinLength(5)]
        public string CourseName { get; set; }


        public Guid CourseId { get; set; }


        //public string NarrativeMessage
        //{
        //    get
        //    { return _viewObj.NarrativeMessage; }

        //    set
        //    {
        //        _viewObj.NarrativeMessage = value;
        //    }
        //}

    }
}
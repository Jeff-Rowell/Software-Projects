using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CWMasterTeacherDomain.DomainObjects;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class CourseCopyAdminViewObj
    {
        //private bool _isMaster;
        //private string _successMessage;
        //private bool _isAdmin;
        //private IEnumerable<SelectListItem> _possibleMasterCourses;
        //private IEnumerable<SelectListItem> _possibleFromCourses;
        //private IEnumerable<SelectListItem> _possibleToUsers;
        //private IEnumerable<SelectListItem> _possibleFromTerms;
        //private IEnumerable<SelectListItem> _possibleToTerms;

        /// <summary>
        ///  Constructor that initializes a new instance of CourseCopyViewObj using the values of the provided arguments.
        /// </summary>
        /// <param name="isMaster">Indicates if the course is the master course</param>
        /// <param name="successMessage">Message indicating success of copying the course</param>
        /// <param name="isAdmin">A boolean flag dictating whether the user is an Admin.</param>
        /// <param name="masterCourseForWorkingGroupList">List of master courses that are associated with the current workingGroup.</param>
        /// <param name="coursesAllForWorkingGroupList">List of courses that are associated with the current workingGroup.</param>
        /// <param name="usersForWorkingGroupList">List of users that are associated with the current workingGroup.</param> 
        /// <param name="termsForWorkingGroupList">List of terms that are associated with the current workingGroup.</param>
        public CourseCopyAdminViewObj(bool isMaster, string successMessage, UserDomainObj currentUserObj, IEnumerable<SelectListItem> masterCourseForWorkingGroupList, 
            IEnumerable<SelectListItem> coursesAllForWorkingGroupList, IEnumerable<SelectListItem> usersForWorkingGroupList, 
            IEnumerable<SelectListItem> termsForWorkingGroupList)
        {
            IsMaster = isMaster;
            SuccessMessage = successMessage;
            CurrentUserObj = currentUserObj;
            PossibleMasterCourses = masterCourseForWorkingGroupList;
            PossibleToUsers = usersForWorkingGroupList;
            PossibleFromTerms = termsForWorkingGroupList;
            PossibleToTerms = termsForWorkingGroupList;
            MasterCourseForWorkingGroupList = masterCourseForWorkingGroupList;
            CoursesAllForWorkingGroupList = coursesAllForWorkingGroupList;
        }

        private IEnumerable<SelectListItem> MasterCourseForWorkingGroupList { get; }
        private IEnumerable<SelectListItem> CoursesAllForWorkingGroupList { get; }

        public bool IsMaster { get; }


        public string SuccessMessage { get;  }

        public UserDomainObj CurrentUserObj { get;  }

        public bool UserIsApplicationAdmin
        {
            get { return CurrentUserObj == null ? false : CurrentUserObj.IsApplicationAdmin; }
        }
        public bool UserIsWorkingGroupAdmin
        {
            get { return CurrentUserObj == null ? false : CurrentUserObj.IsWorkingGroupAdmin; }
        }

        public IEnumerable<SelectListItem> PossibleMasterCourses { get; set; }

        public IEnumerable<SelectListItem> PossibleFromCourses
        {
            get
            {
                if (IsMaster)
                {
                    return MasterCourseForWorkingGroupList;
                }
                else
                {
                    return CoursesAllForWorkingGroupList;
                }
            }
        }

        public IEnumerable<SelectListItem> PossibleToUsers { get; set; }

        public IEnumerable<SelectListItem> PossibleFromTerms { get; set; }

        public IEnumerable<SelectListItem> PossibleToTerms { get; set; }

        [Required(ErrorMessage = "Select a Master Course.")]
        public Guid MasterCourseId { get; set; }

        [Required(ErrorMessage = "Select a course to copy from.")]
        public Guid FromCourseId { get; set; }

        [Required(ErrorMessage = "Select a user.")]
        public Guid ToUserId { get; set; }

        [Required(ErrorMessage = "Select a term.")]
        public Guid ToTermId { get; set; }




    }// End Class

}

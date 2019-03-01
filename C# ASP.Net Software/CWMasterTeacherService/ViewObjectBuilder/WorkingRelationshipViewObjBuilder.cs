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
    public class WorkingRelationshipViewObjBuilder
    {
        ITermDomainObjBuilder _termObjBuilder;
        ICourseDomainObjBuilder _courseObjBuilder;
        IWorkingRelationshipDomainObjBuilder _workingRelObjBuilder;
        IUserDomainObjBuilder _userObjBuilder;

        public WorkingRelationshipViewObjBuilder(ITermDomainObjBuilder termObjBuilder,
                                            ICourseDomainObjBuilder courseObjBuilder,
                                            IWorkingRelationshipDomainObjBuilder workingRelObjBuilder,
                                            IUserDomainObjBuilder userObjBuilder)
        {
            _termObjBuilder = termObjBuilder;
            _courseObjBuilder = courseObjBuilder;
            _workingRelObjBuilder = workingRelObjBuilder;
            _userObjBuilder = userObjBuilder;
        }


        //public WorkingRelationshipViewObj RetrieveWorkingRelService(Guid courseId, Guid currentUserId, Guid selectedWorkRelId)
        //{
        //    WorkingRelationshipViewObj viewObj = new WorkingRelationshipViewObj();

        //    TermDomainObj currentTerm = _termObjBuilder.CurrentTerm();
        //    int sequenceNumberMax = 1;

        //    List<CourseDomainObjBasic> courseList = _courseObjBuilder.CoursesForTermAndUser(currentTerm.Id, currentUserId);
        //    viewObj.CourseSelectList = new SelectList(courseList, "Id", "DisplayName");

        //    if (courseId != Guid.Empty)
        //    {
        //        _courseObjBuilder.BuildWorkingRelationshipsForCourse(courseId);
        //        CourseDomainObjBasic course = _courseObjBuilder.BuildBasicFromId(courseId);
        //        viewObj.CourseId = courseId;
        //        viewObj.CourseName = course.Name;
        //        sequenceNumberMax = workRelList.Count + 1;
        //    }

        //    viewObj.SelectedWorkRelId = selectedWorkRelId;
        //    if (selectedWorkRelId != Guid.Empty)
        //    {
        //        WorkingRelationshipDomainObj workRel = _workingRelObjBuilder.BuildFromId(selectedWorkRelId);
        //        viewObj.SelectedWorkRelName = workRel.Name;
        //        viewObj.SelectedWorkRelUserName = workRel.User.DisplayName;
        //        viewObj.FollowNarrative = workRel.FollowUserNarrative;
        //        viewObj.SequenceNumber = workRel.UserNarrativeSequenceNumber;
        //        viewObj.SequenceNumberSelectList = Enumerable.Range(1, sequenceNumberMax).Select(x => new SelectListItem { Text = x.ToString() });
        //    }

        //    viewObj.CurrentUser = _userObjBuilder.BuildFromId(currentUserId);
        //    return viewObj;
        //}
    }
}

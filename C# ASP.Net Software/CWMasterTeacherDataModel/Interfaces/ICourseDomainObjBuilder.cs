using System;
using System.Collections.Generic;
using CWMasterTeacherDomain.DomainObjects;

namespace CWMasterTeacherDataModel.Interfaces
{
    /// <summary>
    /// Interface to be implemented by classes wishing to provide services that build and return CourseDomainObjs.
    /// </summary>
    public interface ICourseDomainObjBuilder : Interfaces.IDomainObjBuilder<CourseDomainObj,
                                                                            CourseDomainObjBasic,
                                                                            Course>
    {
        List<CourseDomainObjBasic> GetCoursesForTermWorkingGroupList(Guid termId, Guid workingGroupId);
        List<CourseDomainObjBasic> GetCoursesAllForWorkingGroupList(Guid workingGroupId);
        List<CourseDomainObjBasic> GetMasterCourseForWorkingGroupList(Guid workingGroupId);
        List<CourseDomainObjBasic> GetCoursesForTermAndUserList(Guid selectedTermId, Guid selectedUserId);
        List<CourseDomainObjBasic> GetIndividualCoursesForUserList(Guid selectedUserId);
        List<CourseDomainObjBasic> GetIndividualCoursesForTermList(Guid selectedTermId);
        List<CourseDomainObjBasic> GetDisplayCourseListForUser(Guid userId);
        void SetLastDisplayedLessonId(Guid lastDisplayedLessonId);
        void SetLastDisplayedClassSectionId(Guid lastDisplayedLessonId);
        Guid GetLastDisplayedClassSectionId(Guid userId);
        List<CourseDomainObjBasic> CoursesForTermAndUser(Guid termId, Guid userId);
        List<CourseDomainObjBasic> CoursesThatShareMaster(Guid courseId);
    }
}
using System;
using System.Collections.Generic;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface ICourseRepo : IRepository<Course>
    {
        List<Course> CoursesForTermAndUser(Guid termId, Guid userId);
        List<Course> MasterCoursesForTermAndMetaCourse(Guid termId, Guid metaCourseId);
        List<Course> SingleCourseList(Guid courseId);
        List<Course> CoursesForTermAndWorkingGroup(Guid termId, Guid workingGroupId);
        List<Course> MasterCoursesForWorkingGroup(Guid workingGroupId);
        List<Course> MasterCoursesForTermAndWorkingGroup(Guid termId, Guid workingGroupId);
        List<Course> CoursesAllForUser(Guid userId);
        List<Course> IndividualCoursesForUser(Guid userId);
        List<Course> IndividualCoursesForUserAndMasterCoursesForWorkingGroup(Guid userId, Guid workingGroupId);
        List<Course> IndividualCoursesForTerm(Guid termId);
        List<Course> IndividualCoursesForTermAndUser(Guid termId, Guid userId);
        List<Course> CoursesAllForWorkingGroup(Guid workingGroupId);
        List<Course> CoursesForMetaCourse(Guid metaCourseId);
        List<Course> CoursesThatShareMaster(Guid masterCourseId);
        Course MostRecentCourseForUserAndMetaCourse(Guid userId, Guid metaCourseId);
        Guid GetLastDisplayedClassSectionIdFromUserId(Guid userId);
        Guid GetLastDisplayedClassSectionId(User user);
        List<Course> AllCurrentCoursesForWorkingGroupList(List<Guid> workingGroupIds);
    }
}

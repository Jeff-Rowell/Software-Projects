using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.DomainObjectBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    public class CompareSelectViewObjBuilder
    {
        private CourseDomainObjBuilder _courseDomainObjBuilder;
        private LessonDomainObjBuilder _lessonDomainObjBuilder;

        private LessonRepo _lessonRepo;
        private MetaCourseRepo _metaCourseRepo;

        public CompareSelectViewObjBuilder(CourseDomainObjBuilder courseDomainObjBuilder, LessonRepo lessonRepo,
                                            LessonDomainObjBuilder lessonDomainObjBuilder, MetaCourseRepo metaCourseRepo)
        {
            _courseDomainObjBuilder = courseDomainObjBuilder;
            _lessonRepo = lessonRepo;
            _lessonDomainObjBuilder = lessonDomainObjBuilder;
            _metaCourseRepo = metaCourseRepo;
        }


        public CompareSelectViewObj Retrieve(Guid referenceEditableLessonId, Guid courseId, Guid selectedMetaCourseId, Guid selectedLessonId, 
                                                UserDomainObj currentUserObj, bool isDisplayModeNarrative, 
                                                bool isDisplayModeLessonPlan, bool isDisplayModeDocuments,
                                                bool doLockEditableLesson, int comparisonModeInt)
        {
            CompareSelectViewObj viewObj = new CompareSelectViewObj();

            LessonDomainObj referenceEditableLessonObj = _lessonDomainObjBuilder.BuildFromId(referenceEditableLessonId);


            Guid selectedEditableLessonId = doLockEditableLesson ? referenceEditableLessonId : Guid.Empty;

            if(courseId == Guid.Empty && referenceEditableLessonObj != null)
            {
                courseId = referenceEditableLessonObj.IsMaster ? referenceEditableLessonObj.CourseId : referenceEditableLessonObj.MasterCourseId;
            }

            viewObj.ReferenceEditableLessonId = referenceEditableLessonObj.Id;
            viewObj.ReferenceCourseId = referenceEditableLessonObj.CourseId;
            CourseDomainObj courseObj = _courseDomainObjBuilder.BuildFromIdForComparison(id: courseId, doShowHidden: true, 
                                                                                referenceCourseId: referenceEditableLessonObj.CourseId,
                                                                                 selectedEditableLessonId: selectedEditableLessonId, 
                                                                                 doShowLessonComparisons: !doLockEditableLesson,
                                                                                 comparisonModeInt: comparisonModeInt);

            viewObj.CourseObj = courseObj;

            if(selectedMetaCourseId != Guid.Empty)
            {
                viewObj.SelectedMetaCourseId = selectedMetaCourseId;
            }
            else if (courseObj != null)
            {
                selectedMetaCourseId = courseObj.MetaCourseId;
                viewObj.SelectedMetaCourseId = selectedMetaCourseId;
            }
            else
            {
                viewObj.SelectedMetaCourseId = Guid.Empty;
            }
            viewObj.IsDisplayModeNarrative = isDisplayModeNarrative;
            viewObj.IsDisplayModeLessonPlan = isDisplayModeLessonPlan;
            viewObj.IsDisplayModeDocuments = isDisplayModeDocuments;
            viewObj.SelectedLessonId = selectedLessonId;
            viewObj.CurrentUserObj = currentUserObj;
            viewObj.DoLockEditableLesson = doLockEditableLesson;
            viewObj.ComparisonModeInt = comparisonModeInt;

            viewObj.MetaCourses = referenceEditableLessonObj == null ? null : _metaCourseRepo.MetaCoursesForWorkingGroup(referenceEditableLessonObj.WorkingGroupId)
                                                                                .Select(x => MetaCourseDomainObjBuilder.BuildBasic(x)).ToList();

            viewObj.CoursesByTerm = selectedMetaCourseId == Guid.Empty ? null : _courseDomainObjBuilder.CoursesForMetaCourseByTerm(selectedMetaCourseId);

            return viewObj;
        }

    }
}

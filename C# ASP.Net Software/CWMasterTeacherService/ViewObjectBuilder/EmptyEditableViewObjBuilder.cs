using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain;
using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    public class EmptyEditableViewObjBuilder
    {
        private CourseDomainObjBuilder _courseObjBuilder;
        private LessonDomainObjBuilder _lessonObjBuilder;

        public EmptyEditableViewObjBuilder(CourseDomainObjBuilder courseObjBuilder, LessonDomainObjBuilder lessonObjBuilder)
        {
            _courseObjBuilder = courseObjBuilder;
            _lessonObjBuilder = lessonObjBuilder;
        }

        public EmptyEditableViewObj Retrieve(Guid comparisonLessonId, Guid lastEditableLessonId,
                                              bool isDisplayModeNarrative, bool isDisplayModeLessonPlan, bool isDisplayModeDocuments,
                                              Guid containerLessonId, bool doLockEditableLesson, bool doLockComparisonLesson,
                                              Guid containerCourseId, int comparisonModeInt)
        {
            EmptyEditableViewObj viewObj = new EmptyEditableViewObj();
            LessonDomainObj lastEditableLesson = lastEditableLessonId == Guid.Empty ? null : _lessonObjBuilder.BuildFromId(lastEditableLessonId);
            if(lastEditableLesson != null)
            {
                Guid courseId = lastEditableLesson.CourseId;
                viewObj.CourseObj = _courseObjBuilder.BuildFromIdForComparison(id: courseId, doShowHidden: false, 
                                                                                referenceCourseId: Guid.Empty, 
                                                                                selectedEditableLessonId:containerLessonId, 
                                                                                doShowLessonComparisons: false,
                                                                                comparisonModeInt: comparisonModeInt);
            }

            if(containerLessonId != Guid.Empty)
            {
                viewObj.ContainerLessonObj = _lessonObjBuilder.BuildBasicFromId(containerLessonId);
            }
            else if (containerCourseId != Guid.Empty)
            {
                viewObj.ContainerCourseObj = _courseObjBuilder.BuildBasicFromId(containerCourseId);
            }
            

            viewObj.ComparisonLessonId = comparisonLessonId;
            viewObj.LastEditableLessonId = lastEditableLessonId;
            viewObj.IsDisplayModeLessonPlan = isDisplayModeLessonPlan;
            viewObj.IsDisplayModeNarrative = isDisplayModeNarrative;
            viewObj.IsDisplayModeDocuments = isDisplayModeDocuments;

            viewObj.DoLockEditableLesson = doLockEditableLesson;
            viewObj.DoLockComparisonLesson = doLockComparisonLesson;

            return viewObj;
        }
    }
}

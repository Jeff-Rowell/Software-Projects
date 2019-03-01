using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain
{
    public class EmptyEditableViewObj
    {

        public CourseDomainObj CourseObj { get; set; }
        public LessonDomainObjBasic ContainerLessonObj { get; set; }
        public CourseDomainObjBasic ContainerCourseObj { get; set; }
        public Guid ComparisonLessonId { get; set; }
        public Guid LastEditableLessonId { get; set; }

        public bool IsDisplayModeLessonPlan { get; set; }
        public bool IsDisplayModeNarrative { get; set; }
        public bool IsDisplayModeDocuments { get; set; }

        [Display(Name = "Lock Last Displayed Lesson")]
        public bool DoLockEditableLesson { get; set; }
        public bool DoLockComparisonLesson { get; set; }

        public Guid CourseId
        {
            get { return CourseObj == null ? Guid.Empty : CourseObj.Id; }
        }
        public Guid ContainerLessonId
        {
            get { return ContainerLessonObj == null ? Guid.Empty : ContainerLessonObj.Id; }
        }

        public string ContainerLessonName
        {
            get { return ContainerLessonObj == null ? "" : ContainerLessonObj.Name; }
        }


        public string CourseName
        {
            get { return CourseObj == null ? "" : CourseObj.DisplayName; }
        }

        public string HeadingText1
        {
            get { return "No Version of This Lesson Could Be Found in "; }
        }

        public string HeadingText2
        {
            get { return CourseName; }
        }

        public string HeadingCSS
        {
            get { return CourseObj == null ? "" : DomainWebUtilities.CourseLessonTitleClass(CourseObj.IsMaster, false); }
        }


        public bool DoShowImportLesson
        {
            get { return ContainerLessonObj != null; }
        }



    }
}

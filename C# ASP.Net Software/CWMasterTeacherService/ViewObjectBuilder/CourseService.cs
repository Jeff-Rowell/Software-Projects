using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.ViewObjects;

namespace CWMasterTeacherService.RetrieveServices
{
    public class CourseService
    {
        private readonly ICourseDomainObjBuilder _courseBuilder;

        public CourseService(ICourseDomainObjBuilder courseBuilder)
        {
            this._courseBuilder = courseBuilder;
        }

        /// <summary>
        /// A method to return a course view object based on a provided course id
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>a course view object</returns>
        public CourseViewObject Retrieve(int courseId)
        {
            var courseDomainObj = this._courseBuilder.BuildFromId(courseId);
           
            var courseViewObject = new CourseViewObject
            {
                CourseDomainObj = courseDomainObj
            };
            
            return courseViewObject;
        }
    }
}
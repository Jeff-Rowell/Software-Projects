using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.RetrieveServices;
using Moq;
using NUnit.Framework;

namespace CWTesting.Tests.CWMasterTeacherService
{
    [TestFixture()]
    public class CourseServiceTests
    {
        [Test]
        public void Can_Get_Course_View_Object()
        {
            // Arrange
            const int courseId = 1;
            var expected = new CourseViewObject();
            expected.CourseDomainObj= new CourseDomainObj();
            expected.CourseDomainObj.CourseDomainObjBasic = new CourseDomainObjBasic();
            expected.CourseDomainObj.CourseDomainObjBasic.CourseId = courseId;
            var courseBuilder = new Mock<ICourseDomainObjBuilder>();
            courseBuilder.Setup(x => x.BuildFromId(1)).Returns(expected.CourseDomainObj);

            // Act
            var service = new CourseService(courseBuilder.Object);
            var result = service.Retrieve(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.CourseDomainObj);
            Assert.IsNotNull(result.CourseDomainObj.CourseDomainObjBasic);
            Assert.AreEqual(1, result.CourseDomainObj.CourseDomainObjBasic.Id);

        }
    }
}
using CWMasterTeacherDomain.DomainObjects;
using NUnit.Framework;

namespace CWTesting.Tests.CWMasterTeacherDomain
{
    [TestFixture]
    public class CourseObjBasicTests
    {
        [Test]
        public void Can_Get_Id()
        {
            // Arrange
            var expected = 1;

            // Act
            var courseObjBasic = new CourseObjBasic
            {
                Id = expected
            };

            // Assert
            Assert.AreEqual(expected, courseObjBasic.Id);
        }

        [Test]
        public void Can_Get_Name()
        {
            // Arrange
            var expected = "test course 1";

            // Act
            var courseObjBasic = new CourseObjBasic
            {
                Name = expected
            };

            // Assert
            Assert.AreEqual(expected, courseObjBasic.Name);
        }

        [Test]
        public void Can_Get_IsMaster_When_MasterCourse_Exists()
        {
            // Arrange

            // Act
            var courseObjBasic = new CourseObjBasic
            {
                MasterCourse = new CourseObj()
            };

            // Assert
            Assert.IsFalse(courseObjBasic.IsMaster);
        }

        //[Test]
        public void Can_Get_IsMaster_When_MasterCourse_Does_Not_Exixt()
        {
            // Arrange

            // Act
            var courseObjBasic = new CourseObjBasic();

            // Assert
            Assert.IsTrue(courseObjBasic.IsMaster);
        }

        [Test]
        public void Can_Get_MasterCourse()
        {
            // Arrange
            var expected = new CourseObj();

            // Act
            var courseObjBasic = new CourseObjBasic
            {
                MasterCourse = expected
            };

            // Assert
            Assert.AreEqual(expected, courseObjBasic.MasterCourse);
        }
    }
}
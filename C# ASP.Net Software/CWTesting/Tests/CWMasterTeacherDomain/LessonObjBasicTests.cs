using CWMasterTeacherDomain.DomainObjects;
using NUnit.Framework;

namespace CWTesting.Tests.CWMasterTeacherDomain
{
    [TestFixture]
    public class LessonObjBasicTests
    {
        [Test]
        public void Can_Get_Id()
        {
            // Arrange
            var expected = 1;
            
            // Act
            var lesonObjBasic = new LessonObjBasic
            {
                Id = expected
            };

            // Assert
            Assert.AreEqual(expected,lesonObjBasic.Id);
        }

        [Test]
        public void Can_Get_Name()
        {
            // Arrange
            var expected = "test 1";

            // Act
            var lesonObjBasic = new LessonObjBasic
            {
                Name = expected
            };

            // Assert
            Assert.AreEqual(expected, lesonObjBasic.Name);
        }

        [Test]
        public void Can_Get_IsOptional()
        {
            // Arrange
            var expected = true;

            // Act
            var lesonObjBasic = new LessonObjBasic
            {
                IsOptional = expected
            };

            // Assert
            Assert.AreEqual(expected, lesonObjBasic.IsOptional);
        }

        [Test]
        public void Can_Get_DisplayName_When_Optional()
        {
            // Arrange
            var expected = "abc (Opt)";

            // Act
            var lesonObjBasic = new LessonObjBasic
            {
                Name = "abc",
                IsOptional = true
            };

            // Assert
            Assert.AreEqual(expected, lesonObjBasic.DisplayName);
        }

        [Test]
        public void Can_Get_DisplayName_When_Not_Optional()
        {
            // Arrange
            var expected = "abc";

            // Act
            var lesonObjBasic = new LessonObjBasic
            {
                Name = "abc",
                IsOptional = false
            };

            // Assert
            Assert.AreEqual(expected, lesonObjBasic.DisplayName);
        }
    }
}
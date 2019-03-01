using System;
using System.Collections.ObjectModel;
using CWMasterTeacherDomain.DomainObjects;
using NUnit.Framework;

namespace CWTesting.Tests.CWMasterTeacherDomain
{
    [TestFixture]
    public class CourseObjTests
    {
        [Test]
        public void Can_Get_CourseBasicObject()
        {
            // Arrange
            var expected = new CourseObjBasic();

            // Act
            var courseObj = new CourseObj
            {
                CourseObjBasic = expected
            };

            // Assert
            Assert.AreEqual(expected, courseObj.CourseObjBasic);
        }

        [Test]
        public void Can_Get_HasImportantMessgaes()
        {
            var expected = true;

            var courseObj = new CourseObj
            {
                HasImportantMessages = expected
            };

            Assert.AreEqual(expected, courseObj.HasImportantMessages);
        }

        [Test]
        public void Can_Get_HasNewMessgaes()
        {
            var expected = true;

            var courseObj = new CourseObj
            {
                HasNewMessages = expected
            };

            Assert.AreEqual(expected, courseObj.HasNewMessages);
        }

        [Test]
        public void Can_Get_HasActiveMessages()
        {
            var expected = true;

            var courseObj = new CourseObj
            {
                HasActiveMessages = expected
            };

            Assert.AreEqual(expected, courseObj.HasActiveMessages);
        }

        [Test]
        public void Can_Get_HasActiveStoredMessages()
        {
            var expected = true;

            var courseObj = new CourseObj
            {
                HasActiveStoredMessages = expected
            };

            Assert.AreEqual(expected, courseObj.HasActiveStoredMessages);
        }

        [Test]
        public void Can_Get_HasStoredMessages()
        {
            var expected = true;

            var courseObj = new CourseObj
            {
                HasStoredMessages = expected
            };

            Assert.AreEqual(expected, courseObj.HasStoredMessages);
        }

        [Test]
        public void Can_Get_HasOutForEditDocuments()
        {
            var expected = true;

            var courseObj = new CourseObj
            {
                HasOutForEditDocuments = expected
            };

            Assert.AreEqual(expected, courseObj.HasOutForEditDocuments);
        }

        [Test]
        public void Can_Get_ShowFolders()
        {
            var expected = true;

            var courseObj = new CourseObj
            {
                ShowFolders = expected
            };

            Assert.AreEqual(expected, courseObj.ShowFolders);
        }

        [Test]
        public void Can_Get_ShowOptionalLessons()
        {
            var expected = true;

            var courseObj = new CourseObj
            {
                ShowOptionalLessons = expected
            };

            Assert.AreEqual(expected, courseObj.ShowOptionalLessons);
        }

        [Test]
        public void Can_Get_LastDisplayedLessonId()
        {
            var expected = 89;

            var courseObj = new CourseObj
            {
                LastDisplayedLessonId = expected
            };

            Assert.AreEqual(expected, courseObj.LastDisplayedLessonId);
        }

        [Test]
        public void Can_Get_InstitutionId()
        {
            var expected = 89;

            var courseObj = new CourseObj
            {
                InstitutionId = expected
            };

            Assert.AreEqual(expected, courseObj.InstitutionId);
        }

        [Test]
        public void Can_Get_TermId()
        {
            var expected = 402;

            var courseObj = new CourseObj
            {
                TermId = expected
            };

            Assert.AreEqual(expected, courseObj.TermId);
        }

        [Test]
        public void Can_Get_UserId()
        {
            var expected = 6002;

            var courseObj = new CourseObj
            {
                UserId = expected
            };

            Assert.AreEqual(expected, courseObj.UserId);
        }

        [Test]
        public void Can_Get_LevelSetId()
        {
            var expected = 6;

            var courseObj = new CourseObj
            {
                LevelSetId = expected
            };

            Assert.AreEqual(expected, courseObj.LevelSetId);
        }

        [Test]
        public void Can_Get_MasterCourseId()
        {
            var expected = 36;

            var courseObj = new CourseObj
            {
                MasterCourseId = expected
            };

            Assert.AreEqual(expected, courseObj.MasterCourseId);
        }

        [Test]
        public void Can_Get_PredecessorCourseId()
        {
            var expected = 892333;

            var courseObj = new CourseObj
            {
                PredecessorCourseId = expected
            };

            Assert.AreEqual(expected, courseObj.PredecessorCourseId);
        }

        [Test]
        public void Can_Get_OriginalCourseId()
        {
            var expected = 1200;

            var courseObj = new CourseObj
            {
                OriginalCourseId = expected
            };

            Assert.AreEqual(expected, courseObj.OriginalCourseId);
        }

        [Test]
        public void Can_Get_LastDisplayedClassSectionId()
        {
            var expected = 2;

            var courseObj = new CourseObj
            {
                LastDisplayedClassSectionId = expected
            };

            Assert.AreEqual(expected, courseObj.LastDisplayedClassSectionId);
        }

        [Test]
        public void Can_Get_DateCreated()
        {
            var expected = DateTime.Now;

            var courseObj = new CourseObj
            {
                DateCreated = expected
            };

            Assert.AreEqual(expected, courseObj.DateCreated);
        }

        [Test]
        public void Can_Get_DateModified()
        {
            var expected = DateTime.Now;

            var courseObj = new CourseObj
            {
                DateModified = expected
            };

            Assert.AreEqual(expected, courseObj.DateModified);
        }

        [Test]
        public void Can_Get_IsActive()
        {
            var expected = true;

            var courseObj = new CourseObj
            {
                IsActive = expected
            };

            Assert.AreEqual(expected, courseObj.IsActive);
        }

        [Test]
        public void Can_Get_UploadPath()
        {
            var expected = "some path";

            var courseObj = new CourseObj
            {
                UploadPath = expected
            };

            Assert.AreEqual(expected, courseObj.UploadPath);
        }

        [Test]
        public void Can_Get_DownloadPath()
        {
            var expected = "some dl path";

            var courseObj = new CourseObj
            {
                DownloadPath = expected
            };

            Assert.AreEqual(expected, courseObj.DownloadPath);
        }

        [Test]
        public void Can_Get_Lessons()
        {
            var expected = new Collection<LessonObj>();

            var courseObj = new CourseObj
            {
                Lessons = expected
            };

            Assert.AreEqual(expected, courseObj.Lessons);
        }

        [Test]
        public void Can_Get_MasterCourseChildren()
        {
            var expected = new Collection<CourseObj>();

            var courseObj = new CourseObj
            {
                MasterCourseChildren = expected
            };

            Assert.AreEqual(expected, courseObj.MasterCourseChildren);
        }

        [Test]
        public void Can_Get_OriginalCourseChildren()
        {
            var expected = new Collection<CourseObj>();

            var courseObj = new CourseObj
            {
                OriginalCourseChildren = expected
            };

            Assert.AreEqual(expected, courseObj.OriginalCourseChildren);
        }

        [Test]
        public void Can_Get_OriginalCourse()
        {
            var expected = new CourseObj();

            var courseObj = new CourseObj
            {
                OriginalCourse = expected
            };

            Assert.AreEqual(expected, courseObj.OriginalCourse);
        }

        [Test]
        public void Can_Get_PredecessorCourseChildren()
        {
            var expected = new Collection<CourseObj>();

            var courseObj = new CourseObj
            {
                PredecessorCourseChildren = expected
            };

            Assert.AreEqual(expected, courseObj.PredecessorCourseChildren);
        }

        [Test]
        public void Can_Get_PredecessorCourse()
        {
            var expected = new CourseObj();

            var courseObj = new CourseObj
            {
                PredecessorCourse = expected
            };

            Assert.AreEqual(expected, courseObj.PredecessorCourse);
        }

        [Test]
        public void Can_Get_ContainerChildLessons_When_No_Container_Lessons()
        {
            var containerLesson = new LessonObj();
            
            var expected = new Collection<LessonObj>{containerLesson};

            var courseObj = new CourseObj
            {
                Lessons = expected
            };

            Assert.AreEqual(expected, courseObj.ContainerChildLessons);
        }
    }
}
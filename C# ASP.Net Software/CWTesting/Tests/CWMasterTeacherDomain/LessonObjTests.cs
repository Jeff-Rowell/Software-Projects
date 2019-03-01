using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using CWMasterTeacherDomain.DomainObjects;
using NUnit.Framework;

namespace CWTesting.Tests.CWMasterTeacherDomain
{
    [TestFixture]
    public class LessonObjTests
    {
        [Test]
        public void Can_Get_LessonObjBasic()
        {
            // Arrange
            const int id = 2;
            const string name = "Another test";
            var expected = new LessonObjBasic
            {
                Id = id,
                Name = name
            };

            // Act
            var lessonObj = new LessonObj
            {
                LessonObjBasic = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.LessonObjBasic);

            // These next tests are covered in the lesson obj basic unit test class 
            // but are included here for completness and additional discussion.
            Assert.AreEqual(id, lessonObj.LessonObjBasic.Id);
            Assert.AreEqual(name, lessonObj.LessonObjBasic.Name);
        }

        [Test]
        public void Can_Get_MasterLessonId()
        {
            // Arrange
            var expected = 3;
            
            // Act
            var lessonObj = new LessonObj
            {
                MasterLessonId = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.MasterLessonId);
        }

        [Test]
        public void Can_Get_LevelId()
        {
            // Arrange
            var expected = 12;

            // Act
            var lessonObj = new LessonObj
            {
                LevelId = expected
            };

            // Assert
           Assert.AreEqual(expected, lessonObj.LevelId);
        }

        [Test]
        public void Can_Get_DateModified()
        {
            // Arrange
            var expected = DateTime.Now;

            // Act
            var lessonObj = new LessonObj
            {
                DateModified = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.DateModified);
        }

        [Test]
        public void Can_Get_CourseId()
        {
            // Arrange
            var expected = 99;

            // Act
            var lessonObj = new LessonObj
            {
                CourseId= expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.CourseId);
        }

        [Test]
        public void Can_Get_IsActive()
        {
            // Arrange
            var expected = true;

            // Act
            var lessonObj = new LessonObj
            {
                IsActive = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.IsActive);
        }

        [Test]
        public void Can_Get_CourseObj()
        {
            // Arrange
            var expected = new CourseObj();

            // Act
            var lessonObj = new LessonObj
            {
                CourseObj = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.CourseObj);
        }

        [Test]
        public void Can_Get_Container_Lesson_Children()
        {
            // Arrange
            var expected = new Collection<LessonObj>();

            // Act
            var lessonObj = new LessonObj
            {
                ContainerLessonChildren = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.ContainerLessonChildren);
        }

        [Test]
        public void Can_Get_Master_Lesson_Children()
        {
            // Arrange
            var expected = new Collection<LessonObj>();

            // Act
            var lessonObj = new LessonObj
            {
                MasterLessonChildren = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.MasterLessonChildren);
        }

        [Test]
        public void Can_Get_Original_Lesson_Children()
        {
            // Arrange
            var expected = new Collection<LessonObj>();

            // Act
            var lessonObj = new LessonObj
            {
                OriginalLessonChildren = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.OriginalLessonChildren);
        }

        [Test]
        public void Can_Get_Predecessor_Lesson_Children()
        {
            // Arrange
            var expected = new Collection<LessonObj>();

            // Act
            var lessonObj = new LessonObj
            {
                PredecessorLessonChildren = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.PredecessorLessonChildren);
        }

        [Test]
        public void Can_Get_Container_LessonId()
        {
            // Arrange
            var expected = 14;

            // Act
            var lessonObj = new LessonObj
            {
                ContainerLessonId = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.ContainerLessonId);
        }

        [Test]
        public void Can_Get_Original_LessonId()
        {
            // Arrange
            var expected = 7823;

            // Act
            var lessonObj = new LessonObj
            {
                OriginalLessonId = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.OriginalLessonId);
        }

        [Test]
        public void Can_Get_Predecessor_LessonId()
        {
            // Arrange
            var expected = 3;

            // Act
            var lessonObj = new LessonObj
            {
                PredecessorLessonId = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.PredecessorLessonId);
        }

        [Test]
        public void Can_Get_Estimated_Time_Min()
        {
            // Arrange
            var expected = 84;

            // Act
            var lessonObj = new LessonObj
            {
                EstimatedTimeMin = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.EstimatedTimeMin);
        }

        [Test]
        public void Can_Get_Date_Choice_Confirmed()
        {
            // Arrange
            var expected = DateTime.Now;

            // Act
            var lessonObj = new LessonObj
            {
                DateChoiceConfirmed = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.DateChoiceConfirmed);
        }

        [Test]
        public void Can_Get_Sequence_Number()
        {
            // Arrange
            var expected = 9;

            // Act
            var lessonObj = new LessonObj
            {
                SequenceNumber = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.SequenceNumber);
        }

        [Test]
        public void Can_Get_IsOptional()
        {
            // Arrange
            var expected = true;

            // Act
            var lessonObj = new LessonObj
            {
                IsOptional = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.IsOptional);
        }

        [Test]
        public void Can_Get_IsFolder()
        {
            // Arrange
            var expected = true;

            // Act
            var lessonObj = new LessonObj
            {
                IsFolder = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.IsFolder);
        }

        [Test]
        public void Can_Get_Change_Notes()
        {
            // Arrange
            var expected = "These are my change notes";

            // Act
            var lessonObj = new LessonObj
            {
                ChangeNotes = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.ChangeNotes);
        }

        [Test]
        public void Can_Get_HasActiveMessages()
        {
            // Arrange
            var expected = true;

            // Act
            var lessonObj = new LessonObj
            {
                HasActiveMessages = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.HasActiveMessages);
        }

        [Test]
        public void Can_Get_HasActiveStoredMessages()
        {
            // Arrange
            var expected = true;

            // Act
            var lessonObj = new LessonObj
            {
                HasActiveStoredMessages = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.HasActiveStoredMessages);
        }

        [Test]
        public void Can_Get_HasStoredMessages()
        {
            // Arrange
            var expected = true;

            // Act
            var lessonObj = new LessonObj
            {
                HasStoredMessages = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.HasStoredMessages);
        }

        [Test]
        public void Can_Get_HasImportantMessages()
        {
            // Arrange
            var expected = true;

            // Act
            var lessonObj = new LessonObj
            {
                HasImportantMessages = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.HasImportantMessages);
        }

        [Test]
        public void Can_Get_HasNewMessages()
        {
            // Arrange
            var expected = true;

            // Act
            var lessonObj = new LessonObj
            {
                HasNewMessages = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.HasNewMessages);
        }

        [Test]
        public void Can_Get_HasOutForEditDocuments()
        {
            // Arrange
            var expected = true;

            // Act
            var lessonObj = new LessonObj
            {
                HasOutForEditDocuments = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.HasOutForEditDocuments);
        }

        [Test]
        public void Can_Get_Full_Narrative()
        {
            // Arrange
            var expected = "dfgfkg dlfkg skg lssg ksl";

            // Act
            var lessonObj = new LessonObj
            {
                FullNarrative = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.FullNarrative);
        }

        [Test]
        public void Can_Get_Container_Lesson()
        {
            // Arrange
            var expected = new LessonObj();

            // Act
            var lessonObj = new LessonObj
            {
                ContainerLesson = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.ContainerLesson);
        }

        [Test]
        public void Can_Get_Original_Lesson()
        {
            // Arrange
            var expected = new LessonObj();

            // Act
            var lessonObj = new LessonObj
            {
                OriginalLesson = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.OriginalLesson);
        }

        [Test]
        public void Can_Get_Predecessor_Lesson()
        {
            // Arrange
            var expected = new LessonObj();

            // Act
            var lessonObj = new LessonObj
            {
                PredecessorLesson = expected
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.PredecessorLesson);
        }

        [Test]
        public void Can_Get_IsMaster_When_Master_Not_Exists()
        {
            // Arrange
            var expected = true;

            // Act
            var lessonObj = new LessonObj
            {
                CourseObj = new CourseObj { CourseObjBasic = new CourseObjBasic()}
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.IsMaster);
        }

        [Test]
        public void Can_Get_IsMaster_When_Master_Exists()
        {
            // Arrange
            var expected = false;

            // Act
            var lessonObj = new LessonObj
            {
                CourseObj = new CourseObj { CourseObjBasic = new CourseObjBasic {MasterCourse = new CourseObj()} }
            };

            // Assert
            Assert.AreEqual(expected, lessonObj.IsMaster);
        }

        [Test]
        public void Can_Get_IsMaster_When_CourseObj_Not_Exists()
        {
            // Arrange
            var expected = false;

            // Act
            var lessonObj = new LessonObj();

            // Assert
            Assert.AreEqual(expected, lessonObj.IsMaster);
        }
    }
}
using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using CWMasterTeacherDomain.DomainObjects;
using NUnit.Framework;

namespace CWTesting.Tests.CWMasterTeacherDomain
{
    //[TestFixture]
    //public class LessonObjTests
    //{
    //    [Test]
    //    public void Can_Get_LessonObjBasic()
    //    {
    //        // Arrange
    //        const int id = 2;
    //        const string name = "Another test";
    //        var expected = new LessonDomainObjBasic
    //        {
    //            Id = id,
    //            Name = name
    //        };

    //        // Act
    //        var lessonObj = new LessonDomainObj(expected);

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.LessonObjBasic);

    //        // These next tests are covered in the lesson obj basic unit test class 
    //        // but are included here for completness and additional discussion.
    //        Assert.AreEqual(id, lessonObj.LessonObjBasic.Id);
    //        Assert.AreEqual(name, lessonObj.LessonObjBasic.Name);
    //    }

    //    [Test]
    //    public void Can_Get_MasterLessonId()
    //    {
    //        // Arrange
    //        var expected = 3;
            
    //        // Act
    //        var lessonObj = new LessonDomainObj(null)
    //        {
    //            MasterLessonId = expected
    //        };

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.MasterLessonId);
    //    }

    //    [Test]
    //    public void Can_Get_DateModified()
    //    {
    //        // Arrange
    //        var expected = DateTime.Now;

    //        // Act
    //        var lessonObj = new LessonDomainObj(null)
    //        {
    //            DateModified = expected
    //        };

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.DateModified);
    //    }

    //    [Test]
    //    public void Can_Get_CourseId()
    //    {
    //        // Arrange
    //        var expected = 99;

    //        // Act
    //        var lessonObj = new LessonDomainObj(null)
    //        {
    //            CourseId= expected
    //        };

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.CourseId);
    //    }

    //    [Test]
    //    public void Can_Get_IsActive()
    //    {
    //        // Arrange
    //        var expected = true;

    //        // Act
    //        var lessonObj = new LessonDomainObj(null)
    //        {
    //            IsActive = expected
    //        };

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.IsActive);
    //    }
       
    //    [Test]
    //    public void Can_Get_Container_LessonId()
    //    {
    //        // Arrange
    //        var expected = 14;

    //        // Act
    //        var lessonObj = new LessonDomainObj(null)
    //        {
    //            ContainerLessonId = expected
    //        };

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.ContainerLessonId);
    //    }

    //    [Test]
    //    public void Can_Get_Original_LessonId()
    //    {
    //        // Arrange
    //        var expected = 7823;

    //        // Act
    //        var lessonObj = new LessonDomainObj(null)
    //        {
    //            OriginalLessonId = expected
    //        };

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.OriginalLessonId);
    //    }

    //    [Test]
    //    public void Can_Get_Predecessor_LessonId()
    //    {
    //        // Arrange
    //        var expected = 3;

    //        // Act
    //        var lessonObj = new LessonDomainObj(null)
    //        {
    //            PredecessorLessonId = expected
    //        };

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.PredecessorLessonId);
    //    }

    //    [Test]
    //    public void Can_Get_Estimated_Time_Min()
    //    {
    //        // Arrange
    //        var expected = 84;

    //        // Act
    //        var lessonObj = new LessonDomainObj(null)
    //        {
    //            EstimatedTimeMin = expected
    //        };

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.EstimatedTimeMin);
    //    }

    //    [Test]
    //    public void Can_Get_Date_Choice_Confirmed()
    //    {
    //        // Arrange
    //        var expected = DateTime.Now;

    //        // Act
    //        var lessonObj = new LessonDomainObj(null)
    //        {
    //            DateChoiceConfirmed = expected
    //        };

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.DateChoiceConfirmed);
    //    }

    //    [Test]
    //    public void Can_Get_Sequence_Number()
    //    {
    //        // Arrange
    //        var expected = 9;

    //        // Act
    //        var lessonObj = new LessonDomainObj(null)
    //        {
    //            SequenceNumber = expected
    //        };

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.SequenceNumber);
    //    }
        
    //    [Test]
    //    public void Can_Get_IsFolder()
    //    {
    //        // Arrange
    //        var expected = true;

    //        // Act
    //        var lessonObj = new LessonDomainObj(null)
    //        {
    //            IsFolder = expected
    //        };

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.IsFolder);
    //    }

    //    [Test]
    //    public void Can_Get_Change_Notes()
    //    {
    //        // Arrange
    //        var expected = "These are my change notes";

    //        // Act
    //        var lessonObj = new LessonDomainObj(null)
    //        {
    //            ChangeNotes = expected
    //        };

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.ChangeNotes);
    //    }

    //    [Test]
    //    public void Can_Get_HasActiveMessages()
    //    {
    //        // Arrange
    //        var expected = true;

    //        // Act
    //        var lessonObj = new LessonDomainObj(null)
    //        {
    //            HasActiveMessages = expected
    //        };

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.HasActiveMessages);
    //    }

    //    [Test]
    //    public void Can_Get_HasActiveStoredMessages()
    //    {
    //        // Arrange
    //        var expected = true;

    //        // Act
    //        var lessonObj = new LessonDomainObj(null)
    //        {
    //            HasActiveStoredMessages = expected
    //        };

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.HasActiveStoredMessages);
    //    }

    //    [Test]
    //    public void Can_Get_HasStoredMessages()
    //    {
    //        // Arrange
    //        var expected = true;

    //        // Act
    //        var lessonObj = new LessonDomainObj(null)
    //        {
    //            HasStoredMessages = expected
    //        };

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.HasStoredMessages);
    //    }

    //    [Test]
    //    public void Can_Get_HasImportantMessages()
    //    {
    //        // Arrange
    //        var expected = true;

    //        // Act
    //        var lessonObj = new LessonDomainObj(null)
    //        {
    //            HasImportantMessages = expected
    //        };

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.HasImportantMessages);
    //    }

    //    [Test]
    //    public void Can_Get_HasNewMessages()
    //    {
    //        // Arrange
    //        var expected = true;

    //        // Act
    //        var lessonObj = new LessonDomainObj(null)
    //        {
    //            HasNewMessages = expected
    //        };

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.HasNewMessages);
    //    }

    //    [Test]
    //    public void Can_Get_HasOutForEditDocuments()
    //    {
    //        // Arrange
    //        var expected = true;

    //        // Act
    //        var lessonObj = new LessonDomainObj(null)
    //        {
    //            HasOutForEditDocuments = expected
    //        };

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.HasOutForEditDocuments);
    //    }

    //    [Test]
    //    public void Can_Get_Full_Narrative()
    //    {
    //        // Arrange
    //        var expected = "dfgfkg dlfkg skg lssg ksl";

    //        // Act
    //        var lessonObj = new LessonDomainObj(null)
    //        {
    //            FullNarrative = expected
    //        };

    //        // Assert
    //        Assert.AreEqual(expected, lessonObj.FullNarrative);
    //    }

        
    //}
}
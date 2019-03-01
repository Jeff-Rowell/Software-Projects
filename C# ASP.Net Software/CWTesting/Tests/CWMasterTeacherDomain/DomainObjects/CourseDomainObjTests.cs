using System;
using System.Collections.ObjectModel;
using CWMasterTeacherDomain.DomainObjects;
using NUnit.Framework;

namespace CWTesting.Tests.CWMasterTeacherDomain
{
    //[TestFixture]
    //public class CourseObjTests
    //{


    //    [Test]
    //    public void Can_Get_HasImportantMessgaes()
    //    {
    //        var expected = true;

    //        var courseObjBasic = new CourseDomainObjBasic
    //        {
    //            HasImportantMessages = expected
    //        };

    //        Assert.AreEqual(expected, courseObjBasic.HasImportantMessages);
    //    }

    //    [Test]
    //    public void Can_Get_HasNewMessgaes()
    //    {
    //        var expected = true;

    //        var courseObjBasic = new CourseDomainObjBasic
    //        {
    //            HasNewMessages = expected
    //        };

    //        Assert.AreEqual(expected, courseObjBasic.HasNewMessages);
    //    }

    //    [Test]
    //    public void Can_Get_HasActiveMessages()
    //    {
    //        var expected = true;

    //        var courseObjBasic = new CourseDomainObjBasic
    //        {
    //            HasActiveMessages = expected
    //        };

    //        Assert.AreEqual(expected, courseObjBasic.HasActiveMessages);
    //    }

    //    [Test]
    //    public void Can_Get_HasActiveStoredMessages()
    //    {
    //        var expected = true;

    //        var courseObjBasic = new CourseDomainObjBasic
    //        {
    //            HasActiveStoredMessages = expected
    //        };

    //        Assert.AreEqual(expected, courseObjBasic.HasActiveStoredMessages);
    //    }

    //    [Test]
    //    public void Can_Get_HasStoredMessages()
    //    {
    //        var expected = true;

    //        var courseObjBasic = new CourseDomainObjBasic
    //        {
    //            HasStoredMessages = expected
    //        };

    //        Assert.AreEqual(expected, courseObjBasic.HasStoredMessages);
    //    }

    //    [Test]
    //    public void Can_Get_HasOutForEditDocuments()
    //    {
    //        var expected = true;

    //        var courseObjBasic = new CourseDomainObjBasic
    //        {
    //            HasOutForEditDocuments = expected
    //        };

    //        Assert.AreEqual(expected, courseObjBasic.HasOutForEditDocuments);
    //    }

    //    [Test]
    //    public void Can_Get_ShowFolders()
    //    {
    //        var expected = true;

    //        var courseObj = new CourseDomainObj
    //        {
    //            ShowFolders = expected
    //        };

    //        Assert.AreEqual(expected, courseObj.ShowFolders);
    //    }

    //    [Test]
    //    public void Can_Get_ShowOptionalLessons()
    //    {
    //        var expected = true;

    //        var courseObj = new CourseDomainObj
    //        {
    //            ShowOptionalLessons = expected
    //        };

    //        Assert.AreEqual(expected, courseObj.ShowOptionalLessons);
    //    }

    //    [Test]
    //    public void Can_Get_LastDisplayedLessonId()
    //    {
    //        var expected = 89;

    //        var courseObj = new CourseDomainObj
    //        {
    //            LastDisplayedLessonId = expected
    //        };

    //        Assert.AreEqual(expected, courseObj.LastDisplayedLessonId);
    //    }

    //    [Test]
    //    public void Can_Get_InstitutionId()
    //    {
    //        var expected = 89;

    //        var courseObj = new CourseDomainObj
    //        {
    //            InstitutionId = expected
    //        };

    //        Assert.AreEqual(expected, courseObj.InstitutionId);
    //    }

    //    [Test]
    //    public void Can_Get_TermId()
    //    {
    //        var expected = 402;

    //        var courseObjBasic = new CourseDomainObjBasic
    //        {
    //            TermId = expected
    //        };

    //        Assert.AreEqual(expected, courseObjBasic.TermId);
    //    }

    //    [Test]
    //    public void Can_Get_UserId()
    //    {
    //        var expected = 6002;

    //        var courseObj = new CourseDomainObj
    //        {
    //            UserId = expected
    //        };

    //        Assert.AreEqual(expected, courseObj.UserId);
    //    }

    //    [Test]
    //    public void Can_Get_LevelSetId()
    //    {
    //        var expected = 6;

    //        var courseObj = new CourseDomainObj
    //        {
    //            LevelSetId = expected
    //        };

    //        Assert.AreEqual(expected, courseObj.LevelSetId);
    //    }

    //    [Test]
    //    public void Can_Get_MasterCourseId()
    //    {
    //        var expected = 36;

    //        var courseObj = new CourseDomainObj
    //        {
    //            MasterCourseId = expected
    //        };

    //        Assert.AreEqual(expected, courseObj.MasterCourseId);
    //    }

    //    [Test]
    //    public void Can_Get_PredecessorCourseId()
    //    {
    //        var expected = 892333;

    //        var courseObj = new CourseDomainObj
    //        {
    //            PredecessorCourseId = expected
    //        };

    //        Assert.AreEqual(expected, courseObj.PredecessorCourseId);
    //    }

    //    [Test]
    //    public void Can_Get_OriginalCourseId()
    //    {
    //        var expected = 1200;

    //        var courseObj = new CourseDomainObj
    //        {
    //            OriginalCourseId = expected
    //        };

    //        Assert.AreEqual(expected, courseObj.OriginalCourseId);
    //    }

    //    [Test]
    //    public void Can_Get_LastDisplayedClassSectionId()
    //    {
    //        var expected = 2;

    //        var courseObj = new CourseDomainObj
    //        {
    //            LastDisplayedClassSectionId = expected
    //        };

    //        Assert.AreEqual(expected, courseObj.LastDisplayedClassSectionId);
    //    }

    //    [Test]
    //    public void Can_Get_DateCreated()
    //    {
    //        var expected = DateTime.Now;

    //        var courseObj = new CourseDomainObj
    //        {
    //            DateCreated = expected
    //        };

    //        Assert.AreEqual(expected, courseObj.DateCreated);
    //    }

    //    [Test]
    //    public void Can_Get_DateModified()
    //    {
    //        var expected = DateTime.Now;

    //        var courseObj = new CourseDomainObj
    //        {
    //            DateModified = expected
    //        };

    //        Assert.AreEqual(expected, courseObj.DateModified);
    //    }

    //    [Test]
    //    public void Can_Get_IsActive()
    //    {
    //        var expected = true;

    //        var courseObj = new CourseDomainObj
    //        {
    //            IsActive = expected
    //        };

    //        Assert.AreEqual(expected, courseObj.IsActive);
    //    }

    //    [Test]
    //    public void Can_Get_UploadPath()
    //    {
    //        var expected = "some path";

    //        var courseObj = new CourseDomainObj
    //        {
    //            UploadPath = expected
    //        };

    //        Assert.AreEqual(expected, courseObj.UploadPath);
    //    }

    //    [Test]
    //    public void Can_Get_DownloadPath()
    //    {
    //        var expected = "some dl path";

    //        var courseObj = new CourseDomainObj
    //        {
    //            DownloadPath = expected
    //        };

    //        Assert.AreEqual(expected, courseObj.DownloadPath);
    //    }

    //}
}
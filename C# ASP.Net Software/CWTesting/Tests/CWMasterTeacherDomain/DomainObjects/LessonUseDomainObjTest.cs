using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CWMasterTeacherDomain.DomainObjects;

namespace CWTesting.Tests.CWMasterTeacherDomain
{
    //[TestFixture]
    //public class LessonUseDomainObjectTest
    //{
    //    private int testID = 0;
    //    private string testName = "testName";
    //    private int testSetID = 1;
    //    private string testSetName = "setname";
    //    int testSequenceNumber = 12;
    //    string testCustomName = "test custom name";
    //    string testCustomText = "test custom text";
    //    string testShortDisplayText = "td";
    //    int testCustomTime = 56;
    //    int testClassMeetingId = 13;
    //    int testLessonId = 32;
    //    bool testHasCustomTime = true;

    //    private LessonUseDomainObj ludo;

    //    [SetUp]
    //    public void Setup()
    //    {
    //        LessonUseDomainObjBasic ludob = new LessonUseDomainObjBasic();
    //        ludob.Name = testName;
    //        ludob.Id = testID;
    //        ludo = new LessonUseDomainObj(ludob);
    //        ludo.ClassMeetingId = testClassMeetingId;
    //        ludo.LessonId = testLessonId;
    //        ludo.HasCustomTime = testHasCustomTime;
    //        ludo.CustomName = testCustomName;
    //        ludo.CustomText = testCustomText;
    //        ludo.CustomTime = testCustomTime;
    //        ludo.HasCustomTime = testHasCustomTime;
    //        ludo.SequenceNumber = testSequenceNumber;
    //        ludo.Name = testName;
    //    }

    //    [Test]
    //    public void LessonUseIdTest()
    //    {
    //        Assert.AreEqual(ludo.Id, ludo.LessonUseId);
    //        Assert.AreEqual(testID, ludo.LessonUseId);
    //    }

    //    [Test]
    //    public void GetIdTest()
    //    {
    //        Assert.AreEqual(ludo.Id, testID);
    //    }

    //    [Test]
    //    public void GetNameTest()
    //    {
    //        Assert.AreEqual(ludo.Name, testName);
    //    }

    //    [Test]
    //    public void GetClassMeetingIdTest()
    //    {
    //        Assert.AreEqual(testClassMeetingId, ludo.ClassMeetingId);
    //    }

    //    [Test]
    //    public void GetLessonIdTest()
    //    {
    //        Assert.AreEqual(testLessonId, ludo.LessonId);
    //    }

    //    [Test]
    //    public void GetSequenceNumberTest()
    //    {
    //        Assert.AreEqual(testSequenceNumber, ludo.SequenceNumber);
    //    }

    //    [Test]
    //    public void GetCustomNameTest()
    //    {
    //        Assert.AreEqual(testCustomName, ludo.CustomName);
    //    }

    //    [Test]
    //    public void GetCustomTextTest()
    //    {
    //        Assert.AreEqual(testCustomText, ludo.CustomText);
    //    }

    //    [Test]
    //    public void GetCustomTimeTest()
    //    {
    //        Assert.AreEqual(testCustomTime, ludo.CustomTime);
    //    }

    //    [Test]
    //    public void GetHasCustomTimeTest()
    //    {
    //        Assert.AreEqual(testHasCustomTime, ludo.HasCustomTime);
    //    }

    //    [Test]
    //    public void SetIdTest()
    //    {
    //        ludo.Id = testSetID;
    //        Assert.AreEqual(testSetID, ludo.Id);
    //    }

    //    [Test]
    //    public void SetNameTest()
    //    {
    //        ludo.Name = testSetName;
    //        Assert.AreEqual(testSetName, ludo.Name);
    //    }


    //    [Test]
    //    public void SetClassMeetingIdTest()
    //    {
    //        ludo.ClassMeetingId = testSetID;
    //        Assert.AreEqual(testSetID, ludo.ClassMeetingId);
    //    }

    //    [Test]
    //    public void SetLessonIdTest()
    //    {
    //        ludo.LessonId = testSetID;
    //        Assert.AreEqual(testSetID, ludo.LessonId);
    //    }

    //    [Test]
    //    public void SetSequenceNumberTest()
    //    {
    //        ludo.SequenceNumber = 101;
    //        Assert.AreEqual(101, ludo.SequenceNumber);
    //    }

    //    [Test]
    //    public void SetCustomNameTest()
    //    {
    //        ludo.CustomName = "Some new custom name";
    //        Assert.AreEqual("Some new custom name", ludo.CustomName);
    //    }

    //    [Test]
    //    public void SetCustomTextTest()
    //    {
    //        ludo.CustomText = "Some new custom text";
    //        Assert.AreEqual("Some new custom text", ludo.CustomText);
    //    }

    //    [Test]
    //    public void SetCustomTimeTest()
    //    {
    //        ludo.CustomTime = 8;
    //        Assert.AreEqual(8, ludo.CustomTime);
    //    }

    //    [Test]
    //    public void SetHasCustomTime()
    //    {
    //        ludo.HasCustomTime = false;
    //        Assert.AreEqual(false, ludo.HasCustomTime);
    //    }

    //    [Test]
    //    public void DisplayNameTest()
    //    {
    //        //first case
    //        ludo.CustomText = testShortDisplayText;
    //        ludo.LessonId = testID;
    //        Assert.AreEqual(testName, ludo.DisplayName);

    //        //second case
    //        ludo.CustomText = null;
    //        Assert.AreEqual("*" + ludo.CustomName, ludo.DisplayName);

    //        //last case
    //        ludo.CustomName = null;
    //        Assert.AreEqual(ludo.DisplayName, "*No Custom Name Found*");
    //    }

    //    [Test]
    //    public void GetShortDisplayNameTest()
    //    {
    //        // First case
    //        ludo.CustomText = testShortDisplayText;
    //        Assert.AreEqual(ludo.DisplayName, ludo.ShortDisplayName);

    //        // Second case
    //        ludo.CustomText = null;
    //        Assert.AreEqual(ludo.CustomName, ludo.ShortDisplayName);

    //        // Third case
    //        ludo.CustomName = null;
    //        Assert.AreEqual("*No Custom Name Found*", ludo.ShortDisplayName);
    //    }
        
    //    [Test]
    //    public void SetLessonUseIdTest()
    //    {
    //        ludo.LessonUseId = testID;
    //        Assert.AreEqual(ludo.LessonUseId, testID);
    //    }
        
    //    /* blanks to be filled in when more is known
    //    [Test]
    //    public void Test()
    //    {
            
    //    }
        
    //    /* blanks to be filled in when more is known
    //    [Test]
    //    public void Test()
    //    {
            
    //    }
        
    //    /* blanks to be filled in when more is known
    //    [Test]
    //    public void Test()
    //    {
            
    //    }
        
    //    /* blanks to be filled in when more is known
    //    [Test]
    //    public void Test()
    //    {
            
    //    }
    //     */
    //}
}
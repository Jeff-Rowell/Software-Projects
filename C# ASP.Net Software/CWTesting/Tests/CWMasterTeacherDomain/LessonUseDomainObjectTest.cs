using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CWMasterTeacherDomain.DomainObjects;

namespace CWTesting.Tests.CWMasterTeacherDomain
{
    [TestFixture]
    public class LessonUseDomainObjectTest
    {
        private int testID;
        private string testName;
        private int testSetID;
        private string testSetName;
        int testSequenceNumber;
        string testCustomName = "test custom name";
        string testCustomText = "test custom text";
        string testShortDisplayText = "td";
        string testLongDisplayText = "test long display text";
        int testCustomTime = 56;
        

        private LessonUseDomainObjBasic ludob;
        private LessonUseDomainObj ludo;

        [SetUp]
        public void Setup()
        {
            testID = 0;
            testName = "testName";
            testSetID = 1;
            testSetName = "setname";
            ludob = new LessonUseDomainObjBasic();
            ludob.Name = testName;
            ludob.Id = testID;
            ludo = new LessonUseDomainObj(ludob);
            testSequenceNumber = 12;
        }

        [Test]
        public void BasicGetIdTest()
        {
            Assert.AreEqual(ludob.Id, testID);
        }

        [Test]
        public void BasicGetNameTest()
        {
            Assert.AreEqual(ludob.Name, testName);
        }

        [Test]
        public void GetIdTest()
        {
            Assert.AreEqual(ludo.Id, testID);
        }

        [Test]
        public void GetNameTest()
        {
            Assert.AreEqual(ludo.Name, testName);
        }

        [Test]
        public void BasicSetIdTest()
        {
            ludob.Id = testSetID;
            Assert.AreEqual(testSetID, ludob.Id);
        }

        [Test]
        public void BasicSetNameTest()
        {
            ludob.Name = testSetName;
            Assert.AreEqual(testSetName, ludob.Name);
        }

        [Test]
        public void SetIdTest()
        {
            ludo.Id = testSetID;
            Assert.AreEqual(testSetID, ludo.Id);
        }

        [Test]
        public void SetNameTest()
        {
            ludo.Name = testSetName;
            Assert.AreEqual(testSetName, ludo.Name);
        }

        
        [Test]
        public void SetClassMeetingIdTest()
        {
            ludo.ClassMeetingId = testSetID;
            Assert.AreEqual(testSetID, ludo.ClassMeetingId);
        }
        
        [Test]
        public void SetLessonIdTest()
        {
            ludo.LessonId = testSetID;
            Assert.AreEqual(testSetID, ludo.LessonId);
        }
        
        [Test]
        public void SetSequenceNumberTest()
        {
            ludo.SequenceNumber = testSequenceNumber;
            Assert.AreEqual(ludo.SequenceNumber, testSequenceNumber);
        }
        
        [Test]
        public void SetCustomNameTest()
        {
            ludo.CustomName = testCustomName;
            Assert.AreEqual(ludo.CustomName, testCustomName);
        }
        
        [Test]
        public void SetCustomTextTest()
        {
            ludo.CustomText = testCustomText;
            Assert.AreEqual(ludo.CustomText, testCustomText);
        }
        
        [Test]
        public void SetCustomTimeTest()
        {
            ludo.CustomTime = testCustomTime;
            Assert.AreEqual(ludo.CustomTime, testCustomTime);
        }
        
        [Test]
        public void DisplayNameTest()
        {
            //last case
            Assert.AreEqual(ludo.DisplayName, "*No Custom Name Found*");
            //third case
            ludo.CustomName = testCustomName;
            Assert.AreEqual(ludo.DisplayName, "*"+testCustomName);
            //first case
            ludo.CustomText = testShortDisplayText;
            ludo.LessonId = testID;
            Assert.AreEqual(ludo.DisplayName, testName);
        }
        
        /* blanks to be filled in when more is known
        [Test]
        public void ShortDisplayNameTest()
        {
            
        }
        
        /* blanks to be filled in when more is known
        [Test]
        public void Test()
        {
            
        }
        
        /* blanks to be filled in when more is known
        [Test]
        public void Test()
        {
            
        }
        
        /* blanks to be filled in when more is known
        [Test]
        public void Test()
        {
            
        }
        
        /* blanks to be filled in when more is known
        [Test]
        public void Test()
        {
            
        }
         */
    }
}

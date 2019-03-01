using System;
using System.Collections.Generic;
using NUnit.Framework;
using CWMasterTeacherDomain.DomainObjects;

namespace CWTesting.Tests.CWMasterTeacherDomain
{
    [TestFixture]
    class MessageUseDomainObjTest
    {
        //MessageUseDomainObj testMessageUseDomainObj;
        //MessageDomainObj testMessageDomainObj;
        //MessageUseDomainObjBasic testMessageUseDomainObjBasic;

        //int testMessageId;
        //int testLessonId;
        //bool testIsNew;
        //DateTime? testStorageReferenceTime;
        //bool testIsArchived;
        //bool testIsForEndOfSemRev;
        //bool testIsImportant;
        //bool testIsStored;
        //DateTime testTermStartDate;

        //[SetUp]
        //public void SetUp()
        //{
        //    MessageDomainObjBasic testMessageDomainObjBasic = new MessageDomainObjBasic(888888, "You got a message!",
        //                                                        "Mr.UserName", new DateTime(2016, 11, 11, 12, 0, 0));

        //    testMessageDomainObj = new MessageDomainObj(testMessageDomainObjBasic, 23232, "Hello how are you? I am fine, thank you!",
        //                                                "Message Just for You!", new DateTime(2016, 11, 11, 12, 0, 0));

        //    testMessageUseDomainObjBasic = new MessageUseDomainObjBasic(1010101);

        //    testMessageId = 808080;
        //    testLessonId = 12345;
        //    testIsNew = true;
        //    testStorageReferenceTime = new DateTime(2016, 4, 9, 12, 0, 0); //04-09-2016 12:00:00
        //    testIsArchived = false;
        //    testIsForEndOfSemRev = false;
        //    testIsImportant = true;
        //    testIsStored = false;
        //    testTermStartDate = new DateTime(2017, 1, 22, 12, 0, 0); //1-22-2017 12:00:00
        //    testMessageUseDomainObj = new MessageUseDomainObj(testMessageUseDomainObjBasic, testMessageDomainObj, testMessageId, testLessonId,
        //                                testIsNew, testStorageReferenceTime, testIsArchived, testIsForEndOfSemRev, testIsImportant, testIsStored,
        //                                    testTermStartDate);
        //}

        //private static DateTime?[] storageReferenceTimes =
        //{
        //    new DateTime(2016, 4, 9, 12, 0, 0),
        //    new DateTime(2016, 4, 9, 12, 0, 0),
        //    new DateTime(2017, 2, 22, 12, 0, 0),
        //    new DateTime(2017, 2, 22, 12, 0, 0),
        //    null,
        //    null,
        //};

        //[Test]
        //[Sequential]
        //[Author("Stefany Segovia")]
        //public void MessageUseDomainObj_SetIsActiveStoredCorrectly(
        //    [Values(false, true, true, false, false, true)] bool isStored,
        //    [ValueSource("storageReferenceTimes")] DateTime? storageReferenceTime,
        //    [Values(false, true, false, false, false, false)] bool expectedIsActiveStored)
        //{
        //    testMessageUseDomainObj = new MessageUseDomainObj(testMessageUseDomainObjBasic, testMessageDomainObj, testMessageId, testLessonId,
        //                    testIsNew, storageReferenceTime, testIsArchived, testIsForEndOfSemRev, testIsImportant, isStored,
        //                        testTermStartDate);
        //    Assert.That(testMessageUseDomainObj.IsActiveStored, Is.EqualTo(expectedIsActiveStored));
        //}
    }
}
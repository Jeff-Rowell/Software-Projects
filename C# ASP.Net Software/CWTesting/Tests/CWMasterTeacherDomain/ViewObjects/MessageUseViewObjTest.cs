using Moq;
using NUnit.Framework;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;

[TestFixture]
public class MessageUseViewObjTest
{

    //private MessageUseViewObj testMessageUseViewObj;
    //private MessageUseDomainObj testMessageUseDomainObj01, testMessageUseDomainObj02, testMessageUseDomainObj03;
    //private MessageUseViewObj testChildMessageUseViewObj;
    //private List<MessageUseViewObj> childMessageUsesList = new List<MessageUseViewObj>();


    //[SetUp]
    //public void SetUp()
    //{
    //    testChildMessageUseViewObj = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(12345),
    //                                        new MessageDomainObj((new MessageDomainObjBasic(111, "Hello", "TestUserName", new DateTime(2016, 12, 6, 12, 0, 0))),
    //                                            2345, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 12, 6, 12, 0, 0)),
    //                                                78931, 4250, true, new DateTime(2016, 11, 12, 12, 0, 0), false, true, true, false, new DateTime(2017, 2, 2, 12, 0, 0)), new List<MessageUseViewObj>());

    //    childMessageUsesList.Add(testChildMessageUseViewObj);

    //    testMessageUseDomainObj01 = new MessageUseDomainObj(new MessageUseDomainObjBasic(1010101),
    //                                    new MessageDomainObj(new MessageDomainObjBasic(888888, "You got a message!", "Mr.UserName", new DateTime(2016, 11, 11, 12, 0, 0)),
    //                                        0, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 11, 11, 12, 0, 0)),
    //                                            808080, 12345, true, new DateTime(2016, 4, 9, 12, 0, 0), false, false, true, false, new DateTime(2017, 1, 22, 12, 0, 0));

    //    testMessageUseDomainObj02 = new MessageUseDomainObj(new MessageUseDomainObjBasic(1010101),
    //                                    new MessageDomainObj(new MessageDomainObjBasic(888888, "You got a message!", "Mr.UserName", new DateTime(2016, 11, 11, 12, 0, 0)),
    //                                        null, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 11, 11, 12, 0, 0)),
    //                                            808080, 12345, true, new DateTime(2016, 4, 9, 12, 0, 0), false, false, true, false, new DateTime(2017, 1, 22, 12, 0, 0));

    //    testMessageUseDomainObj03 = new MessageUseDomainObj(new MessageUseDomainObjBasic(1010101),
    //                                    new MessageDomainObj(new MessageDomainObjBasic(888888, "You got a message!", "Mr.UserName", new DateTime(2016, 11, 11, 12, 0, 0)),
    //                                        15970255, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 11, 11, 12, 0, 0)),
    //                                            808080, 12345, true, new DateTime(2016, 4, 9, 12, 0, 0), false, false, true, false, new DateTime(2017, 1, 22, 12, 0, 0));
    //}

    //private MessageUseDomainObj testMessageUseDomainObjToUse(int id)
    //{
    //    switch (id)
    //    {
    //        default:
    //            return testMessageUseDomainObj01;
    //        case 2:
    //            return testMessageUseDomainObj02;
    //        case 3:
    //            return testMessageUseDomainObj03;
    //    }
    //}

    //[TestCase(1, -1)]
    //[TestCase(2, -1)]
    //[TestCase(3, 15970255)]
    //public void Verify_CorrectThreadParentId(int idForTestMessageUseDomainObj, int expectedId)
    //{
    //    testMessageUseViewObj = new MessageUseViewObj(testMessageUseDomainObjToUse(idForTestMessageUseDomainObj), childMessageUsesList);
    //    MessageUseDomainObj messageUseDomainObj = testMessageUseDomainObjToUse(idForTestMessageUseDomainObj);
    //    MessageUseViewObj MessageUseViewObj = new MessageUseViewObj(messageUseDomainObj, new List<MessageUseViewObj>());
    //    int actualId = testMessageUseViewObj.ThreadParentId;
    //    Assert.That(expectedId == actualId);
    //}

    //[TestCase]
    //public void Verify_IsNew_ReturnsTrue()
    //{
    //    bool modifiedValue = true;
    //    MessageUseViewObj childMessageUseViewObj = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(12345),
    //                                new MessageDomainObj((new MessageDomainObjBasic(111, "Hello", "TestUserName", new DateTime(2016, 12, 6, 12, 0, 0))),
    //                                    2345, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 12, 6, 12, 0, 0)),
    //                                        78931, 4250, modifiedValue, new DateTime(2016, 11, 12, 12, 0, 0), false, true, true, false, new DateTime(2017, 2, 2, 12, 0, 0)), new List<MessageUseViewObj>());
    //    Assert.IsTrue(childMessageUseViewObj.IsNew);
    //    Assert.IsTrue(childMessageUseViewObj.IsThisOrChildrenNew);
    //}

    //[TestCase]
    //public void Verify_IsNew_ReturnsFalse()
    //{
    //    bool modifiedValue = false;
    //    MessageUseViewObj childMessageUseViewObj = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(12345),
    //                        new MessageDomainObj((new MessageDomainObjBasic(111, "Hello", "TestUserName", new DateTime(2016, 12, 6, 12, 0, 0))),
    //                            2345, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 12, 6, 12, 0, 0)),
    //                                78931, 4250, modifiedValue, new DateTime(2016, 11, 12, 12, 0, 0), false, true, true, false, new DateTime(2017, 2, 2, 12, 0, 0)), new List<MessageUseViewObj>());
    //    Assert.IsFalse(childMessageUseViewObj.IsNew);
    //    Assert.IsFalse(childMessageUseViewObj.IsThisOrChildrenNew);
    //}

    //[TestCase]
    //public void Verify_IsNewChildren()
    //{
    //    MessageUseViewObj newChildViewObj = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(12345),
    //                new MessageDomainObj((new MessageDomainObjBasic(111, "Hello", "TestUserName", new DateTime(2016, 12, 6, 12, 0, 0))),
    //                    2345, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 12, 6, 12, 0, 0)),
    //                        78931, 4250, true, new DateTime(2016, 11, 12, 12, 0, 0), false, true, true, false, new DateTime(2017, 2, 2, 12, 0, 0)), new List<MessageUseViewObj>());

    //    MessageUseViewObj parentMessageUseViewObj = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(12345),
    //                new MessageDomainObj((new MessageDomainObjBasic(111, "Hello", "TestUserName", new DateTime(2016, 12, 6, 12, 0, 0))),
    //                    0, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 12, 6, 12, 0, 0)),
    //                        78931, 4250, false, new DateTime(2016, 11, 12, 12, 0, 0), false, true, true, false, new DateTime(2017, 2, 2, 12, 0, 0)), 
    //                        new List<MessageUseViewObj>() { newChildViewObj });

    //    Assert.IsFalse(parentMessageUseViewObj.IsNew);
    //    Assert.IsTrue(parentMessageUseViewObj.IsThisOrChildrenNew);
    //}

    //[TestCase]
    //public void Verify_IsImportant_ReturnsTrue()
    //{
    //    bool modifiedValue = true;
    //    MessageUseViewObj childMessageUseViewObj = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(12345),
    //                                new MessageDomainObj((new MessageDomainObjBasic(111, "Hello", "TestUserName", new DateTime(2016, 12, 6, 12, 0, 0))),
    //                                    2345, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 12, 6, 12, 0, 0)),
    //                                        78931, 4250, true, new DateTime(2016, 11, 12, 12, 0, 0), false, true, modifiedValue, false, new DateTime(2017, 2, 2, 12, 0, 0)), new List<MessageUseViewObj>());
    //    Assert.IsTrue(childMessageUseViewObj.IsImportant);
    //    Assert.IsTrue(childMessageUseViewObj.IsThisOrChildrenImportant);
    //}

    //[TestCase]
    //public void Verify_IsImportant_ReturnsFalse()
    //{
    //    bool modifiedValue = false;
    //    MessageUseViewObj childMessageUseViewObj = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(12345),
    //                        new MessageDomainObj((new MessageDomainObjBasic(111, "Hello", "TestUserName", new DateTime(2016, 12, 6, 12, 0, 0))),
    //                            0, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 12, 6, 12, 0, 0)),
    //                                78931, 4250, true, new DateTime(2016, 11, 12, 12, 0, 0), false, true, modifiedValue, false, new DateTime(2017, 2, 2, 12, 0, 0)), new List<MessageUseViewObj>());
    //    Assert.IsFalse(childMessageUseViewObj.IsImportant);
    //}

    //[TestCase]
    //public void Verify_IsImportantChildren()
    //{
    //    MessageUseViewObj newChildViewObj = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(12345),
    //        new MessageDomainObj((new MessageDomainObjBasic(111, "Hello", "TestUserName", new DateTime(2016, 12, 6, 12, 0, 0))),
    //            2345, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 12, 6, 12, 0, 0)),
    //                78931, 4250, true, new DateTime(2016, 11, 12, 12, 0, 0), false, true, true, false, new DateTime(2017, 2, 2, 12, 0, 0)), new List<MessageUseViewObj>());

    //    MessageUseViewObj parentMessageUseViewObj = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(12345),
    //                new MessageDomainObj((new MessageDomainObjBasic(111, "Hello", "TestUserName", new DateTime(2016, 12, 6, 12, 0, 0))),
    //                    0, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 12, 6, 12, 0, 0)),
    //                        78931, 4250, false, new DateTime(2016, 11, 12, 12, 0, 0), false, true, false, false, new DateTime(2017, 2, 2, 12, 0, 0)),
    //                        new List<MessageUseViewObj>() { newChildViewObj });

    //    Assert.IsFalse(parentMessageUseViewObj.IsImportant);
    //    Assert.IsTrue(parentMessageUseViewObj.IsThisOrChildrenImportant);
    //}

    //[TestCase]
    //public void Verify_IsForEndOfSemRev_ReturnsTrue()
    //{
    //    bool modifiedValue = true;
    //    MessageUseViewObj childMessageUseViewObj = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(12345),
    //                        new MessageDomainObj((new MessageDomainObjBasic(111, "Hello", "TestUserName", new DateTime(2016, 12, 6, 12, 0, 0))),
    //                            0, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 12, 6, 12, 0, 0)),
    //                                78931, 4250, true, new DateTime(2016, 11, 12, 12, 0, 0), false, modifiedValue, false, false, new DateTime(2017, 2, 2, 12, 0, 0)), new List<MessageUseViewObj>());
    //    Assert.IsTrue(childMessageUseViewObj.IsForEndOfSemRev);
    //    Assert.IsTrue(childMessageUseViewObj.IsThisOrChildrenForEndOfSemRev);
    //}

    //[TestCase]
    //public void Verify_IsForEndOfSemRev_ReturnsFalse()
    //{
    //    bool modifiedValue = false;
    //    MessageUseViewObj childMessageUseViewObj = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(12345),
    //                        new MessageDomainObj((new MessageDomainObjBasic(111, "Hello", "TestUserName", new DateTime(2016, 12, 6, 12, 0, 0))),
    //                            0, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 12, 6, 12, 0, 0)),
    //                                78931, 4250, true, new DateTime(2016, 11, 12, 12, 0, 0), false, modifiedValue, true, false, new DateTime(2017, 2, 2, 12, 0, 0)), new List<MessageUseViewObj>());
    //    Assert.IsFalse(childMessageUseViewObj.IsForEndOfSemRev);
    //    Assert.IsFalse(childMessageUseViewObj.IsThisOrChildrenForEndOfSemRev);
    //}

    //[TestCase]
    //public void Verify_IsForEndOfSemRevChildren()
    //{
    //    MessageUseViewObj newChildViewObj = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(12345),
    //        new MessageDomainObj((new MessageDomainObjBasic(111, "Hello", "TestUserName", new DateTime(2016, 12, 6, 12, 0, 0))),
    //            2345, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 12, 6, 12, 0, 0)),
    //                78931, 4250, true, new DateTime(2016, 11, 12, 12, 0, 0), false, true, true, false, new DateTime(2017, 2, 2, 12, 0, 0)), new List<MessageUseViewObj>());

    //    MessageUseViewObj parentMessageUseViewObj = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(12345),
    //                new MessageDomainObj((new MessageDomainObjBasic(111, "Hello", "TestUserName", new DateTime(2016, 12, 6, 12, 0, 0))),
    //                    0, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 12, 6, 12, 0, 0)),
    //                        78931, 4250, false, new DateTime(2016, 11, 12, 12, 0, 0), false, false, true, false, new DateTime(2017, 2, 2, 12, 0, 0)),
    //                        new List<MessageUseViewObj>() { newChildViewObj });

    //    Assert.IsFalse(parentMessageUseViewObj.IsForEndOfSemRev);
    //    Assert.IsTrue(parentMessageUseViewObj.IsThisOrChildrenForEndOfSemRev);
    //}

    //[TestCase]
    //public void Verify_IsThisOrChildrenStoredButNotArchived()
    //{
    //    MessageUseViewObj newChildViewObj = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(12345),
    //new MessageDomainObj((new MessageDomainObjBasic(111, "Hello", "TestUserName", new DateTime(2016, 12, 6, 12, 0, 0))),
    //    2345, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 12, 6, 12, 0, 0)),
    //        78931, 4250, true, new DateTime(2016, 11, 12, 12, 0, 0), false, true, true, true, new DateTime(2017, 2, 2, 12, 0, 0)), new List<MessageUseViewObj>());

    //    MessageUseViewObj parentMessageUseViewObj = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(12345),
    //                new MessageDomainObj((new MessageDomainObjBasic(111, "Hello", "TestUserName", new DateTime(2016, 12, 6, 12, 0, 0))),
    //                    0, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 12, 6, 12, 0, 0)),
    //                        78931, 4250, false, new DateTime(2016, 11, 12, 12, 0, 0), true, false, true, false, new DateTime(2017, 2, 2, 12, 0, 0)),
    //                        new List<MessageUseViewObj>() { newChildViewObj });

    //    Assert.IsTrue(parentMessageUseViewObj.IsThisOrChildrenStoredButNotArchived);
    //}
}

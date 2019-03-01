using CWMasterTeacherDataModel.Interfaces;
using NUnit.Framework;
using Moq;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherService.ViewObjectBuilder;
using System;
using System.Collections.Generic;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDataModel;

namespace CWTesting.Tests.CWMasterTeacherService.RetrieveServices
{
    [TestFixture]
    class MessageBoardServiceTest
    {
        //private MessageUseDomainObj testMessageUseDomainObj;
        //private MessageUse testMessageUse = new MessageUse();
        //private MessageDomainObj testMessageDomainObj;
        //private MessageUseService messageUseTestService;
        //private MessageBoardService messageBoardTestService;
        //private MessageUseViewObj testMessageUseViewObj;

        //[OneTimeSetUp]
        //public void SetUp()
        //{
        //    testMessageUseDomainObj = new MessageUseDomainObj(new MessageUseDomainObjBasic(1010101),
        //                                new MessageDomainObj(new MessageDomainObjBasic(888888, "You got a message!", "Mr.UserName", new DateTime(2016, 11, 11, 12, 0, 0)),
        //                                    23232, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 11, 11, 12, 0, 0)),
        //                                        808080, 12345, true, new DateTime(2016, 4, 9, 12, 0, 0), false, false, true, false, new DateTime(2017, 1, 22, 12, 0, 0));

        //    testMessageDomainObj = new MessageDomainObj(new MessageDomainObjBasic(122321, "This the subject", "TheUserName", new DateTime(2015, 11, 10, 12, 0, 0)),
        //                                111, "You the the best!", "This the subject", new DateTime(2015, 11, 10, 12, 0, 0));

        //    testMessageUseViewObj = new MessageUseViewObj(testMessageUseDomainObj, new List<MessageUseViewObj>());

        //    //Mocking MessageUseDomainObjBuilder
        //    Mock<IMessageUseDomainObjBuilder> MockMessageUseDomainObjBuilder = new Mock<IMessageUseDomainObjBuilder>();
        //    MockMessageUseDomainObjBuilder.Setup(x => x.GetChildMessageUsesForMessageUseList(testMessageUse.Id)).Returns(new List<MessageUseDomainObj>());
        //    MockMessageUseDomainObjBuilder.Setup(x => x.GetParentMessageUsesList(It.IsAny<int>())).Returns(new List<MessageUseDomainObj>() { testMessageUseDomainObj });
        //    MockMessageUseDomainObjBuilder.Setup(x => x.BuildFromId(It.IsAny<int>())).Returns(testMessageUseDomainObj);
        //    IMessageUseDomainObjBuilder mockMessageUseDomainObjBuilder = MockMessageUseDomainObjBuilder.Object;

        //    //Mocking MessageDomainObjBuilder
        //    Mock<IMessageDomainObjBuilder> MockMessageDomainObjBuilder = new Mock<IMessageDomainObjBuilder>();
        //    MockMessageDomainObjBuilder.Setup(x => x.BuildFromId(It.IsAny<int>())).Returns(testMessageDomainObj);
        //    IMessageDomainObjBuilder mockMessageDomainObjBuilder = MockMessageDomainObjBuilder.Object;

        //    messageUseTestService = new MessageUseService(mockMessageUseDomainObjBuilder);
        //    messageBoardTestService = new MessageBoardService(mockMessageUseDomainObjBuilder, mockMessageDomainObjBuilder);
        //}

        //[Test]
        //[Author("Stefany Segovia")]
        //public void Retrieve_MessageBoardViewObj()
        //{
        //    int expectedLessonId = 12345;
        //    int expectedSelectedMessageUseId = 1010101;
        //    MessageUseViewObj expectedMessageUseViewObj = new MessageUseViewObj(testMessageUseDomainObj, new List<MessageUseViewObj>());

        //    MessageBoardViewObj messageBoardViewObj = messageBoardTestService.Retrieve(12345, 1010101);

        //    Assert.AreEqual(messageBoardViewObj.SelectedLessonId, expectedLessonId);
        //    Assert.AreEqual(messageBoardViewObj.DisplayMessageUseId, expectedSelectedMessageUseId);
        //    Assert.AreEqual(messageBoardViewObj.SelectedMessageUseObj.MessageUseDomainObj, expectedMessageUseViewObj.MessageUseDomainObj);
        //}

        //[Test]
        //[Author("Stefany Segovia")]
        //public void Retrieve_MessageBoardViewObjNull()
        //{
        //    int expectedLessonId = 12345;
        //    int expectedSelectedMessageUseId = -1;

        //    MessageBoardViewObj messageBoardViewObj = messageBoardTestService.Retrieve(12345, -1);

        //    Assert.AreEqual(messageBoardViewObj.SelectedLessonId, expectedLessonId);
        //    Assert.AreEqual(messageBoardViewObj.DisplayMessageUseId, expectedSelectedMessageUseId);
        //    Assert.AreEqual(messageBoardViewObj.SelectedMessageUseObj, null);
        //}
    }
}

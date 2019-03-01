using CWMasterTeacherDataModel.Interfaces;
using NUnit.Framework;
using Moq;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherService.ViewObjectBuilder;
using System;
using CWMasterTeacherDomain.ViewObjects;

namespace CWTesting.Tests.CWMasterTeacherService.RetrieveServices
{
    [TestFixture]
    class MessageUseServiceTest
    {
        //private static MessageUseDomainObj testMessageUseDomainObj;
        //private MessageUseService testService;

        //[OneTimeSetUp]
        //public void SetUp()
        //{
        //    testMessageUseDomainObj = new MessageUseDomainObj(new MessageUseDomainObjBasic(1010101),
        //                                    new MessageDomainObj(new MessageDomainObjBasic(888888, "You got a message!", "Mr.UserName", new DateTime(2016, 11, 11, 12, 0, 0)),
        //                                        23232, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 11, 11, 12, 0, 0)),
        //                                            808080, 12345, true, new DateTime(2016, 4, 9, 12, 0, 0), false, false, true, false, new DateTime(2017, 1, 22, 12, 0, 0));

        //    //Mocking MessageUseDomainObjBuilder
        //    Mock<IMessageUseDomainObjBuilder> MockMessageUseDomainObjBuilder = new Mock<IMessageUseDomainObjBuilder>();
        //    MockMessageUseDomainObjBuilder.Setup(x => x.BuildFromId(It.IsAny<int>())).Returns(testMessageUseDomainObj);
        //    IMessageUseDomainObjBuilder mockMessageUseDomainObjBuilder = MockMessageUseDomainObjBuilder.Object;

        //    testService = new MessageUseService(mockMessageUseDomainObjBuilder);
        //}

        //[Test]
        //[Author("Stefany Segovia")]
        //public void Retrieve_InitializeMessageUse()
        //{
        //    MessageUseDomainObj expectedMessageUseDomainObj = testMessageUseDomainObj;
        //    MessageUseViewObj messageUseViewObj = testService.Retrieve(1010101);

        //    Assert.AreEqual(messageUseViewObj.MessageUseDomainObj, expectedMessageUseDomainObj);
        //    Assert.IsEmpty(messageUseViewObj.getChildMessageUses);
        //}
    }
}

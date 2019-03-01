using NUnit.Framework;

namespace CWTesting.Tests.CWMasterTeacherDomain
{
    [TestFixture]
    [Author("Luz Rodriguez, Carlos Rodriguez, Stefany Segovia, Andrew Becker")]
    public class MessageBoardViewObjTest
    {
        //private MessageBoardViewObj messageBoardViewObj;
        //private List<MessageUseViewObj> childMessageUsesList = new List<MessageUseViewObj>();
        //private MessageUseViewObj childMessageUseViewObj;
        //private MessageUseDomainObj testMessageUseDomainObj;

        //[SetUp]
        //public void SetUp()
        //{

        //    childMessageUseViewObj = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(12345),
        //                                       new MessageDomainObj((new MessageDomainObjBasic(111, "Hello", "TestUserName", new DateTime(2016, 12, 6, 12, 0, 0))),
        //                                            2345, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 12, 6, 12, 0, 0)),
        //                                                78931, 4250, true, new DateTime(2016, 11, 12, 12, 0, 0), false, true, true, false, new DateTime(2017, 2, 2, 12, 0, 0)), new List<MessageUseViewObj>());

        //    childMessageUsesList.Add(childMessageUseViewObj);

        //    testMessageUseDomainObj = new MessageUseDomainObj(new MessageUseDomainObjBasic(1010101),
        //                                                new MessageDomainObj(new MessageDomainObjBasic(888888, "You got a message!", "Mr.UserName", new DateTime(2016, 11, 11, 12, 0, 0)),
        //                                                    23232, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 11, 11, 12, 0, 0)),
        //                                                        808080, 12345, true, new DateTime(2016, 4, 9, 12, 0, 0), false, false, true, false, new DateTime(2017, 1, 22, 12, 0, 0));

        //    MessageUseViewObj testMessageUseViewObj = new MessageUseViewObj(testMessageUseDomainObj, childMessageUsesList);
        //    List<MessageUseViewObj> parentViewObjectList = new List<MessageUseViewObj>();
        //    parentViewObjectList.Add(new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(12345),
        //                                                    new MessageDomainObj((new MessageDomainObjBasic(111, "Hello", "TestUserName", new DateTime(2016, 12, 6, 12, 0, 0))),
        //                                                        2345, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 12, 6, 12, 0, 0)),
        //                                                            78931, 4250, true, new DateTime(2016, 11, 12, 12, 0, 0), false, true, true, false, new DateTime(2017, 2, 2, 12, 0, 0)), new List<MessageUseViewObj>()));

        //    messageBoardViewObj = new MessageBoardViewObj(1234, 1010101, testMessageUseViewObj, parentViewObjectList);
        //}

        //[Test]
        //public void Retrieve_LiveMessages()
        //{
        //    List<MessageUseViewObj> liveMessages = new List<MessageUseViewObj>();

        //    MessageUseViewObj liveChildMessage = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(0), 
        //                                                                new MessageDomainObj(null, 0, null, null, DateTime.Now), 
        //                                                                0, 0, false, DateTime.Now, false, false, false, false, DateTime.Now), new List<MessageUseViewObj>());
        //    MessageUseViewObj liveParentMessage = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(0), 
        //                                                                new MessageDomainObj(null, 0, null, null, DateTime.Now), 
        //                                                                0, 0, false, DateTime.Now, false, false, false, true, DateTime.Now), 
        //                                                                new List<MessageUseViewObj>() { liveChildMessage });
        //    liveMessages.Add(liveChildMessage);
        //    liveMessages.Add(liveParentMessage);

        //    MessageBoardViewObj messageBoard = new MessageBoardViewObj(0, 0, null, liveMessages);
        //    CollectionAssert.AreEqual(liveMessages, messageBoard.LiveMessages);
        //}

        //[Test]
        //public void Retrieve_StoredMessages()
        //{
        //    List<MessageUseViewObj> storedMessages = new List<MessageUseViewObj>();

        //    MessageUseViewObj storedChildMessage = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(0),
        //                                                    new MessageDomainObj(null, 0, null, null, DateTime.Now),
        //                                                    0, 0, false, DateTime.Now, false, false, false, true, DateTime.Now), new List<MessageUseViewObj>());
        //    storedMessages.Add(storedChildMessage);
        //    MessageBoardViewObj messageBoard = new MessageBoardViewObj(0, 0, null, storedMessages);
        //    CollectionAssert.AreEqual(messageBoard.StoredMessages, storedMessages);
        //}

        //[Test]
        //public void Retrieve_ArchivedMessages()
        //{
        //    List<MessageUseViewObj> archivedMessages = new List<MessageUseViewObj>();
        //    MessageUseViewObj archivedMessage = new MessageUseViewObj(new MessageUseDomainObj(new MessageUseDomainObjBasic(0),
        //                                    new MessageDomainObj(null, 0, null, null, DateTime.Now),
        //                                0, 0, false, DateTime.Now, true, false, false, true, DateTime.Now),
        //                                    new List<MessageUseViewObj>());
        //    archivedMessages.Add(archivedMessage);
        //    MessageBoardViewObj messageBoard = new MessageBoardViewObj(0, 0, null, archivedMessages);
        //    CollectionAssert.AreEqual(messageBoard.ArchivedMessages, archivedMessages);
        //}

        //[Test]
        //public void Verify_ExpectedMessage()
        //{
        //    string expectedMessageText = "<h4>You got a message!  (Mr.UserName__11/11/2016 12:00:00 PM)</h4>Hello how are you? I am fine, thank you!";
        //    StringAssert.AreEqualIgnoringCase(messageBoardViewObj.DisplayMessageText, expectedMessageText);
        //}

        //[Test]
        //public void Verify_ExpectedStoredMessages()
        //{
        //    bool expectedIsThisOrChildrenStoredButNotArchived = false;
        //    bool isThisOrChildrenStoredButNotArchived = messageBoardViewObj.SelectedMessageUseObj.IsThisOrChildrenStoredButNotArchived;
        //    Assert.AreEqual(expectedIsThisOrChildrenStoredButNotArchived, isThisOrChildrenStoredButNotArchived);
        //}

        //[Test]
        //public void Verify_ExpectedArchived()
        //{
        //    bool expectedIsThisOrChildrenStoredAndArchived = false;
        //    bool isThisOrChildrenStoredAndArchived = messageBoardViewObj.SelectedMessageUseObj.IsThisOrChildrenStoredButNotArchived;
        //    Assert.AreEqual(expectedIsThisOrChildrenStoredAndArchived, isThisOrChildrenStoredAndArchived);
        //}

        //[Test]
        //public void Verify_ExpectedLessonId()
        //{
        //    int expectedSelectedLessonId = 1234;
        //    int selectedLessonId = messageBoardViewObj.SelectedLessonId;
        //    Assert.AreEqual(expectedSelectedLessonId, selectedLessonId);
        //}

        //[Test]
        //public void Verify_ExpectedDisplayMessageId()
        //{
        //    int expectedDisplayMessageUseId = 1010101;
        //    int displayMessageUseId = messageBoardViewObj.DisplayMessageUseId;
        //    Assert.AreEqual(expectedDisplayMessageUseId, displayMessageUseId);
        //}

        //[Test]
        //public void Verify_CorrectIsDisplayArchived()
        //{
        //    bool expectedIsDisplayArchived = false;
        //    bool isDisplayArchived = messageBoardViewObj.IsDisplayArchived;
        //    Assert.AreEqual(expectedIsDisplayArchived, isDisplayArchived);
        //}

        //[Test]
        //public void Verify_CorrectDisplayStored()
        //{
        //    bool expectedIsDisplayStored = false;
        //    bool isDisplayStored = messageBoardViewObj.IsDisplayStored;
        //    Assert.AreEqual(expectedIsDisplayStored, isDisplayStored);
        //}

        //[Test]
        //public void Verify_CorrectIsDisplayImportant()
        //{
        //    bool expectedIsDisplayImportant = true;
        //    bool isDisplayImportant = messageBoardViewObj.IsDisplayImportant;
        //    Assert.AreEqual(expectedIsDisplayImportant, isDisplayImportant);
        //}

        //[Test]
        //public void Verify_CorrectEndOfSemRev()
        //{
        //    bool expectedIsDisplayForEndOfSemRev = false;
        //    bool isDisplayForEndOfSemRev = messageBoardViewObj.IsDisplayForEndOfSemRev;
        //    Assert.AreEqual(expectedIsDisplayForEndOfSemRev, isDisplayForEndOfSemRev);
        //}

        //[Test]
        //public void Retrieve_CorrectArchivedMenuText()
        //{
        //    StringAssert.IsMatch(messageBoardViewObj.ArchivedMenuText, "Archive message");
        //    bool modifier = true;

        //    testMessageUseDomainObj = new MessageUseDomainObj(new MessageUseDomainObjBasic(1010101),
        //                                    new MessageDomainObj(new MessageDomainObjBasic(888888, "You got a message!", "Mr.UserName", new DateTime(2016, 11, 11, 12, 0, 0)),
        //                                        23232, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 11, 11, 12, 0, 0)),
        //                                            808080, 12345, true, new DateTime(2016, 4, 9, 12, 0, 0), modifier, false, true, false, new DateTime(2017, 1, 22, 12, 0, 0));

        //    MessageUseViewObj testMessageUseViewObj = new MessageUseViewObj(testMessageUseDomainObj, new List<MessageUseViewObj>());
        //    messageBoardViewObj = new MessageBoardViewObj(1234, 1010101, testMessageUseViewObj, new List<MessageUseViewObj>());
        //    StringAssert.IsMatch(messageBoardViewObj.ArchivedMenuText, "Remove from archive");
        //}

        //[Test]
        //public void Retrieve_CorrectStoredMenuText()
        //{
        //    StringAssert.IsMatch(messageBoardViewObj.StoredMenuText, "Store message");
        //    bool modifier = true;

        //    testMessageUseDomainObj = new MessageUseDomainObj(new MessageUseDomainObjBasic(1010101),
        //                                    new MessageDomainObj(new MessageDomainObjBasic(888888, "You got a message!", "Mr.UserName", new DateTime(2016, 11, 11, 12, 0, 0)),
        //                                        23232, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 11, 11, 12, 0, 0)),
        //                                            808080, 12345, true, new DateTime(2016, 4, 9, 12, 0, 0), false, false, true, modifier, new DateTime(2017, 1, 22, 12, 0, 0));

        //    MessageUseViewObj testMessageUseViewObj = new MessageUseViewObj(testMessageUseDomainObj, new List<MessageUseViewObj>());
        //    messageBoardViewObj = new MessageBoardViewObj(1234, 1010101, testMessageUseViewObj, new List<MessageUseViewObj>());
        //    StringAssert.IsMatch(messageBoardViewObj.StoredMenuText, "Remove from storage");
        //}

        //[Test]
        //public void Retrieve_CorrectImportantMenuText()
        //{
        //    StringAssert.IsMatch(messageBoardViewObj.ImportantMenuText, "Remove 'Important' tag");
        //    bool modifier = false;

        //    testMessageUseDomainObj = new MessageUseDomainObj(new MessageUseDomainObjBasic(1010101),
        //                                    new MessageDomainObj(new MessageDomainObjBasic(888888, "You got a message!", "Mr.UserName", new DateTime(2016, 11, 11, 12, 0, 0)),
        //                                        23232, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 11, 11, 12, 0, 0)),
        //                                            808080, 12345, true, new DateTime(2016, 4, 9, 12, 0, 0), false, false, modifier, false, new DateTime(2017, 1, 22, 12, 0, 0));

        //    MessageUseViewObj testMessageUseViewObj = new MessageUseViewObj(testMessageUseDomainObj, new List<MessageUseViewObj>());
        //    messageBoardViewObj = new MessageBoardViewObj(1234, 1010101, testMessageUseViewObj, new List<MessageUseViewObj>());
        //    StringAssert.IsMatch(messageBoardViewObj.ImportantMenuText, "Mark as 'Important'");
        //}

        //[Test]
        //public void Retrieve_CorrectEndOfSemRevMenuText()
        //{
        //    StringAssert.IsMatch(messageBoardViewObj.EndOfSemRevMenuText, "Add to 'End of semester review'");
        //    bool modifier = true;

        //    testMessageUseDomainObj = new MessageUseDomainObj(new MessageUseDomainObjBasic(1010101),
        //                                    new MessageDomainObj(new MessageDomainObjBasic(888888, "You got a message!", "Mr.UserName", new DateTime(2016, 11, 11, 12, 0, 0)),
        //                                        23232, "Hello how are you? I am fine, thank you!", "Message Just for You!", new DateTime(2016, 11, 11, 12, 0, 0)),
        //                                            808080, 12345, true, new DateTime(2016, 4, 9, 12, 0, 0), false, modifier, true, false, new DateTime(2017, 1, 22, 12, 0, 0));

        //    MessageUseViewObj testMessageUseViewObj = new MessageUseViewObj(testMessageUseDomainObj, new List<MessageUseViewObj>());
        //    messageBoardViewObj = new MessageBoardViewObj(1234, 1010101, testMessageUseViewObj, new List<MessageUseViewObj>());
        //    StringAssert.IsMatch(messageBoardViewObj.EndOfSemRevMenuText, "Remove from 'End of semester review'");
        //}
    }
}
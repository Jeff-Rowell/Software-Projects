using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.ViewObjectBuilder;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWTesting.Tests.CWMasterTeacherDomain.ViewObjects
{
    [TestFixture]
    class TextEditorViewObjBuilderTest
    {
        LessonPlan _lessonPlan;
        LessonUse _lessonUse;
        Lesson _lesson;
        User _user;
        ClassMeeting _classMeeting;
        Course _course;
        Narrative _narrative;
        CoursePreference _coursePreference;
        WorkingGroup _workingGroup;
        MessageUse _messageUse;
        Message _message;
        Term _term;

        List<LessonUse> _lessonUseList;
        List<Lesson> _lessonList;
        List<User> _userList;
        List<LessonPlan> _lessonPlanList;
        List<MessageUse> _messageUseList;
        List<Message> _messageList;

        Mock<IUserDomainObjBuilder> _userBuilder;
        Mock<ICourseDomainObjBuilder> _courseBuilder;
        Mock<ILessonDomainObjBuilder> _lessonBuilder;
        Mock<ITermDomainObjBuilder> _termBuilder;
        Mock<ILessonPlanDomainObjBuilder> _lessonPlanBuilder;
        Mock<IMessageUseDomainObjBuilder> _messageUseBuilder;
        Mock<IMessageDomainObjBuilder> _messageBuilder;
        Mock<IDocumentUseDomainObjBuilder> _documentUseBuilder;
        Mock<IDocumentDomainObjBuilder> _documentBuilder;
        Mock<ILessonUseDomainObjBuilder> _lessonUseBuilder;
        Mock<LessonUseDomainObj> _lessonUseDomainObj;

        TextEditorViewObjBuilder _textEditorViewObjBuilder;

        [SetUp]
        public void SetUp()
        {
            InitializeObjsForObjBuilders();
            InitializeDomainObjBuilderMocksAndViewObject();
        }

        private void InitializeObjsForObjBuilders()
        {
            _lessonList = new List<Lesson>();
            _userList = new List<User>();
            _lessonPlanList = new List<LessonPlan>();
            _lessonUseList = new List<LessonUse>();
            _messageUseList = new List<MessageUse>();
            _messageList = new List<Message>();

            _course = new Course();
            _narrative = new Narrative();
            _lessonPlan = new LessonPlan();
            _coursePreference = new CoursePreference();
            _user = new User();
            _user.DisplayName = "some name";
            _workingGroup = new WorkingGroup();
            _lessonUse = new LessonUse();
            _classMeeting = new ClassMeeting();
            _messageUse = new MessageUse();
            _message = new Message();
            _messageUse = new MessageUse();
            _lesson = new Lesson();
            _term = new Term();

            _lesson.Id = Guid.NewGuid();
            _lesson.CourseId = Guid.NewGuid();
            _lesson.ContainerLessonId = Guid.NewGuid();
            _lesson.MasterLessonId = Guid.NewGuid();
            _lesson.PredecessorLessonId = Guid.NewGuid();
            _lesson.MirrorTargetLessonId = Guid.NewGuid();
            _lesson.MetaLessonId = Guid.NewGuid();
            _lesson.LessonPlanId = Guid.NewGuid();
            _lesson.NarrativeId = Guid.NewGuid();
            _lesson.EstimatedTimeMin = 12;
            _lesson.DateTimeCreated = new DateTime(12, 1, 1);
            _lesson.DateTimeDocumentsChoiceConfirmed = new DateTime(12, 1, 1);
            _lesson.ReferenceDateTimeDocChoiceConfirmed = new DateTime(12, 1, 1);
            _lesson.DateDocumentsModified = new DateTime(12, 1, 1);
            _lesson.LessonPlanDateChoiceConfirmed = new DateTime(12, 1, 1);
            _lesson.LessonPlanReferenceDateChoiceConfirmed = new DateTime(12, 1, 1);
            _lesson.NarrativeDateChoiceConfirmed = new DateTime(12, 1, 1);
            _lesson.NarrativeReferenceDateChoiceConfirmed = new DateTime(12, 1, 1);
            _lesson.Narrative = _narrative;
            _lesson.Narrative.Text = "some text";
            _lesson.Narrative.DateModified = new DateTime(12, 1, 1);
            _lesson.LessonPlan = _lessonPlan;
            _lesson.LessonPlan.DateModified = new DateTime(12, 1, 1);
            _lesson.DateCreated = new DateTime(12, 1, 1);
            _lesson.SequenceNumber = 13;
            _lesson.Course = _course;
            _lesson.Course.IsMaster = true;
            _lesson.Course.CoursePreference = _coursePreference;
            _lesson.Course.CoursePreference.DoShowDocumentNotifications = true;
            _lesson.Course.CoursePreference.DoShowLessonPlanNotifications = true;
            _lesson.Course.CoursePreference.DoShowNarrativeNotifications = true;
            _lesson.Course.Term = _term;
            _lesson.Course.Term.StartDate = new DateTime(12, 1, 1);
            _lesson.IsHidden = false;
            _lesson.IsCollapsed = false;
            _lesson.Course.User = _user;
            _lesson.Name = "some name";
            _lessonPlan.Id = Guid.NewGuid();

            _user.WorkingGroup = _workingGroup;
            _user.WorkingGroup.Name = "some name";
            _user.WorkingGroupId = Guid.NewGuid();
            _user.Id = Guid.NewGuid();
            _user.EmailAddress = "some email";
            _user.IsApplicationAdmin = false;
            _user.IsWorkingGroupAdmin = false;
            _user.IsNarrativeEditor = false;
            _user.IsActive = false;
            _user.HasAdminApproval = false;
            _user.ShowMastersInCourseList = false;
            _user.ShowAllInCourseList = false;
            _user.WorkingGroupId = Guid.NewGuid();

            _lessonUse.Id = Guid.NewGuid();
            _message.User = _user;
            _message.Subject = "some subject";
            _message.TimeStamp = new DateTime(12, 1, 1);
            _message.Id = Guid.NewGuid();
            _messageUse.Message = _message;
            _messageUse.MessageId = _message.Id;
            _messageUse.Id = Guid.NewGuid();
            _messageUse.LessonId = _lesson.Id;
            _messageUse.StorageReferenceTime = new DateTime(12, 1, 1);
            _messageUse.Lesson = _lesson;
            _messageUse.Message.ThreadParentId = Guid.NewGuid();
            _messageUse.Message.Text = "some text";

            _lessonList.Add(_lesson);
            _userList.Add(_user);
            _lessonPlanList.Add(_lessonPlan);
            _lessonUseList.Add(_lessonUse);
            _messageUseList.Add(_messageUse);
            _messageList.Add(_message);
        }

        private void InitializeDomainObjBuilderMocksAndViewObject()
        {
            _userBuilder = new Mock<IUserDomainObjBuilder>();
            _courseBuilder = new Mock<ICourseDomainObjBuilder>();
            _lessonBuilder = new Mock<ILessonDomainObjBuilder>();
            _termBuilder = new Mock<ITermDomainObjBuilder>();
            _lessonPlanBuilder = new Mock<ILessonPlanDomainObjBuilder>();
            _messageUseBuilder = new Mock<IMessageUseDomainObjBuilder>();
            _messageBuilder = new Mock<IMessageDomainObjBuilder>();
            _documentUseBuilder = new Mock<IDocumentUseDomainObjBuilder>();
            _documentBuilder = new Mock<IDocumentDomainObjBuilder>();
            _lessonUseBuilder = new Mock<ILessonUseDomainObjBuilder>();
            _lessonUseDomainObj = new Mock<LessonUseDomainObj>();

            _lessonBuilder.Setup(mock => mock.BuildFromId(It.IsAny<Guid>())).Returns(
                (Guid i) =>
                {
                    foreach (var x in _lessonList)
                        if (i.Equals(x.Id)) { return LessonDomainObjBuilder.Build(x, false); }
                    return null;
                }
            );

            _userBuilder.Setup(mock => mock.BuildFromId(It.IsAny<Guid>())).Returns(
                (Guid i) =>
                {
                    foreach (var x in _userList)
                        if (i.Equals(x.Id)) { return UserDomainObjBuilder.Build(x); }
                    return null;
                }
            );

            _lessonPlanBuilder.Setup(mock => mock.BuildFromId(It.IsAny<Guid>())).Returns(
                (Guid i) =>
                {
                    foreach (var x in _lessonPlanList)
                        if (i.Equals(x.Id)) { return LessonPlanDomainObjBuilder.Build(x); }
                    return null;
                }
            );

            _messageUseBuilder.Setup(mock => mock.BuildFromId(It.IsAny<Guid>())).Returns(
                (Guid i) =>
                {
                    foreach (var x in _messageUseList)
                        if (i.Equals(x.Id)) { return MessageUseDomainObjBuilder.Build(x); }
                    return null;
                }
            );

            _messageBuilder.Setup(mock => mock.BuildFromId(It.IsAny<Guid>())).Returns(
                (Guid i) =>
                {
                    foreach (var x in _messageList)
                        if (i.Equals(x.Id)) { return MessageDomainObjBuilder.Build(x); }
                    return null;
                }
            );

            _lessonUseBuilder.Setup(mock => mock.BuildFromId(It.IsAny<Guid>())).Returns(
                (Guid i) =>
                {
                    foreach (var x in _lessonUseList)
                        if (i.Equals(x.Id)) { return LessonUseDomainObjBuilder.Build(x); }
                    return null;
                }
            );

            _lessonUseBuilder.Setup(mock => mock.GetTextForLessonUse(It.IsAny<Guid>())).Returns(
                (Guid i) => { return "some string"; });

            _textEditorViewObjBuilder = new TextEditorViewObjBuilder
                (
                _userBuilder.Object, _courseBuilder.Object,
                _lessonBuilder.Object, _termBuilder.Object,
                _lessonPlanBuilder.Object, _messageUseBuilder.Object,
                _messageBuilder.Object, _documentUseBuilder.Object,
                _documentBuilder.Object, _lessonUseBuilder.Object
                );
        }

        [Test]
        public void RetrieveViewObjForLessonPlan()
        {
            TextEditorViewObj expectedViewObj = new TextEditorViewObj();
            expectedViewObj.ControllerName = "Curriculum";
            expectedViewObj.IsMasterEdit = _lesson.Course.IsMaster;
            expectedViewObj.Text = _lessonPlan.Text;
            expectedViewObj.NameOrSubject = _lessonPlan.Name;
            expectedViewObj.ScreenTitle = "Edit " + DomainWebUtilities.LessonPlanTypeName + " for: " + 
                _lesson.Name + " (Master)";
            expectedViewObj.ActionName = "LessonPlanUpdate";
            expectedViewObj.NameOrSubjectCaption = DomainWebUtilities.LessonPlanTypeName + " Name";
            expectedViewObj.SelectedLessonId = _lesson.Id;
            expectedViewObj.Id = _lessonPlan.Id;

            TextEditorViewObj actualViewObj = _textEditorViewObjBuilder.RetrieveViewObjForLessonPlan(_lessonPlan.Id,
                _lesson.Id, _user.Id);

            Assert.AreEqual(expectedViewObj.Text, actualViewObj.Text);
            Assert.AreEqual(expectedViewObj.ControllerName, actualViewObj.ControllerName);
            Assert.AreEqual(expectedViewObj.IsMasterEdit, actualViewObj.IsMasterEdit);
            Assert.AreEqual(expectedViewObj.NameOrSubject, actualViewObj.NameOrSubject);
            Assert.AreEqual(expectedViewObj.ScreenTitle, actualViewObj.ScreenTitle);
            Assert.AreEqual(expectedViewObj.ActionName, actualViewObj.ActionName);
            Assert.AreEqual(expectedViewObj.NameOrSubjectCaption, actualViewObj.NameOrSubjectCaption);
            Assert.AreEqual(expectedViewObj.SelectedLessonId, actualViewObj.SelectedLessonId);
            Assert.AreEqual(expectedViewObj.Id, actualViewObj.Id);

            _lessonPlan.Id = Guid.Empty;

            expectedViewObj.Text = "";
            expectedViewObj.NameOrSubject = _lesson.Name;
            expectedViewObj.ScreenTitle = "Create " + DomainWebUtilities.LessonPlanTypeName + 
                " for: " + _lesson.Name + " (Master)";

            actualViewObj = _textEditorViewObjBuilder.RetrieveViewObjForLessonPlan(_lessonPlan.Id, 
                _lesson.Id, _user.Id);

            Assert.AreEqual(expectedViewObj.Text, actualViewObj.Text);
            Assert.AreEqual(expectedViewObj.NameOrSubject, actualViewObj.NameOrSubject);
            Assert.AreEqual(expectedViewObj.ScreenTitle, actualViewObj.ScreenTitle);
        }

        [Test]
        public void RetrieveViewObjForCustomLessonPlan()
        {
            TextEditorViewObj expectedViewObj = new TextEditorViewObj();
            expectedViewObj.ControllerName = "DailyPlanning";
            expectedViewObj.ActionName = "CustomLessonPlanUpdate";
            expectedViewObj.CancelActionName = "CustomLessonPlanUpdateCancel";
            expectedViewObj.NameOrSubjectCaption = DomainWebUtilities.LessonPlanTypeName + " Name";
            expectedViewObj.SelectedLessonId = _lesson.Id;
            expectedViewObj.Id = _lessonUse.Id;
            expectedViewObj.Text = "some string";

            TextEditorViewObj actualViewObj = _textEditorViewObjBuilder.RetrieveViewObjForCustomLessonPlan(_lessonUse.Id,
                _lesson.Id, _user.Id, _classMeeting.Id);

            Assert.AreEqual(expectedViewObj.ControllerName, actualViewObj.ControllerName);
            Assert.AreEqual(expectedViewObj.ActionName, actualViewObj.ActionName);
            Assert.AreEqual(expectedViewObj.CancelActionName, actualViewObj.CancelActionName);
            Assert.AreEqual(expectedViewObj.NameOrSubjectCaption, actualViewObj.NameOrSubjectCaption);
            Assert.AreEqual(expectedViewObj.SelectedLessonId, actualViewObj.SelectedLessonId);
            Assert.AreEqual(expectedViewObj.Id, actualViewObj.Id);
            Assert.AreEqual(expectedViewObj.Text, actualViewObj.Text);

            _lessonUse.Id = Guid.Empty;
            expectedViewObj.Text = "";
            expectedViewObj.NameOrSubject = "";
            expectedViewObj.ScreenTitle = "Create Custom " + DomainWebUtilities.LessonPlanTypeName;

            actualViewObj = _textEditorViewObjBuilder.RetrieveViewObjForCustomLessonPlan(_lessonUse.Id, 
                _lesson.Id, _user.Id, _classMeeting.Id);

            Assert.AreEqual(expectedViewObj.Text, actualViewObj.Text);
            Assert.AreEqual(expectedViewObj.NameOrSubject, actualViewObj.NameOrSubject);
            Assert.AreEqual(expectedViewObj.ScreenTitle, actualViewObj.ScreenTitle);
        }

        [Test]
        public void RetrieveViewObjForMessage()
        {
            TextEditorViewObj expectedViewObj = new TextEditorViewObj();
            expectedViewObj.IsToSelf = false;
            expectedViewObj.IsToStorage = false;
            expectedViewObj.SelectedLessonId = _lesson.Id;
            expectedViewObj.Id = Guid.Empty;
            expectedViewObj.ControllerName = "Curriculum";
            expectedViewObj.ActionName = "PostMessage";
            expectedViewObj.CancelActionName = "PostMessageCancel";
            expectedViewObj.NameOrSubjectCaption = "Subject";
            expectedViewObj.ParentMessageUseId = _messageUse.Id;
            expectedViewObj.NameOrSubject = "Re: " + _message.Subject;
            expectedViewObj.ScreenTitle = "Reply to Message for Lesson: " + _lesson.Name;

            TextEditorViewObj actualViewObj = _textEditorViewObjBuilder.RetrieveViewObjForMessage(_messageUse.Id, 
                _lesson.Id, _user.Id, false, false, false);

            Assert.AreEqual(expectedViewObj.ControllerName, actualViewObj.ControllerName);
            Assert.AreEqual(expectedViewObj.ActionName, actualViewObj.ActionName);
            Assert.AreEqual(expectedViewObj.CancelActionName, actualViewObj.CancelActionName);
            Assert.AreEqual(expectedViewObj.NameOrSubjectCaption, actualViewObj.NameOrSubjectCaption);
            Assert.AreEqual(expectedViewObj.SelectedLessonId, actualViewObj.SelectedLessonId);
            Assert.AreEqual(expectedViewObj.Id, actualViewObj.Id);
            Assert.AreEqual(expectedViewObj.ParentMessageUseId, actualViewObj.ParentMessageUseId);
            Assert.AreEqual(expectedViewObj.NameOrSubject, actualViewObj.NameOrSubject);
            Assert.AreEqual(expectedViewObj.ScreenTitle, actualViewObj.ScreenTitle);

            _messageUse.MessageId = Guid.Empty;
            expectedViewObj.ParentMessageUseId = Guid.Empty;
            expectedViewObj.NameOrSubject = "";
            expectedViewObj.ScreenTitle = "Compose Message for Lesson: " + _lesson.Name;

            actualViewObj = _textEditorViewObjBuilder.RetrieveViewObjForMessage(Guid.NewGuid(),
                _lesson.Id, _user.Id, false, false, false);

            Assert.AreEqual(expectedViewObj.ParentMessageUseId, actualViewObj.ParentMessageUseId);
            Assert.AreEqual(expectedViewObj.NameOrSubject, actualViewObj.NameOrSubject);
            Assert.AreEqual(expectedViewObj.ScreenTitle, actualViewObj.ScreenTitle);
        }

        [Test]
        public void RetrieveViewObjForNarrative()
        {
            TextEditorViewObj expectedViewObj = new TextEditorViewObj();
            expectedViewObj.ScreenTitle = "Edit Narrative for: some name (Master)";
            expectedViewObj.Text = _lesson.Narrative.Text;
            expectedViewObj.Id = _lesson.NarrativeId;
            expectedViewObj.ControllerName = "Curriculum";
            expectedViewObj.ActionName = "NarrativeUpdate";
            expectedViewObj.CancelActionName = "NarrativeUpdateCancel";
            expectedViewObj.SelectedLessonId = _lesson.Id;
            expectedViewObj.NameOrSubjectCaption = "Name";

            TextEditorViewObj actualViewObj = _textEditorViewObjBuilder.RetrieveViewObjForNarrative(_lesson.Id, 
                _user.Id);

            Assert.AreEqual(expectedViewObj.ScreenTitle, actualViewObj.ScreenTitle);
            Assert.AreEqual(expectedViewObj.Text, actualViewObj.Text);
            Assert.AreEqual(expectedViewObj.Id, actualViewObj.Id);
            Assert.AreEqual(expectedViewObj.ControllerName, actualViewObj.ControllerName);
            Assert.AreEqual(expectedViewObj.ActionName, actualViewObj.ActionName);
            Assert.AreEqual(expectedViewObj.CancelActionName, actualViewObj.CancelActionName);
            Assert.AreEqual(expectedViewObj.SelectedLessonId, actualViewObj.SelectedLessonId);
            Assert.AreEqual(expectedViewObj.NameOrSubjectCaption, actualViewObj.NameOrSubjectCaption);
            Assert.AreEqual(expectedViewObj.ScreenTitle, actualViewObj.ScreenTitle);
        }
    }
}

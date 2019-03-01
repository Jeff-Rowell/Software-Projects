using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherService.CUDServices;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWTesting.Tests.CWMasterTeacherService.CUDServices
{
    [TestFixture]
    public class LessonCUDServiceTest
    {
        private Course _course0;
        private Course _course1;
        private Course _course2;
        private CoursePreference _coursePreference0;
        private User _user0;
        private Lesson _lesson0;
        private Lesson _lesson1;
        private Lesson _lesson2;
        private Lesson _lesson3;
        private LessonPlan _lessonPlan0;
        private LessonPlan _lessonPlan1;

        private DocumentUseCUDService _documentUseCUDService;
        private StashedLessonPlanCUDService _stashedLessonPlanCUDService;
        private MessageUseCUDService _messageUseCUDService;
        private LessonPlanCUDService _lessonPlanCUDService;
        private NarrativeCUDService _narrativeCUDService;
        private SharedCUDService _sharedCUDService;
        private StashedNarrativeCUDService _stashedNarrativeCUDService;
        private MocksForCUDs _mocks;
        private LessonCUDService _lessonCUDService;

        private Lesson _fromLesson;
        private Lesson _toCourseLesson;
        private Lesson _containerLesson;
        private Course _courseDiffMetaId;
        private Lesson _masterLesson;
        private Course _masterCourse;
        private Course _courseWithMaster;



        private DocumentUse _documentUse0;

        [SetUp]
        public void Setup()
        {
            _mocks = new MocksForCUDs();
            initializeObjects();
            initializeCUDServices();
        }

        [Test]
        public void testUpdateLesson()
        {
            _mocks.ClearLists();
            _mocks.mockLessonRepo.Object.Insert(_lesson0); _mocks.mockLessonRepo.Object.Insert(_lesson1); _mocks.mockLessonRepo.Object.Insert(_lesson2); _mocks.mockLessonRepo.Object.Insert(_lesson3);

            Lesson expected = _lessonCUDService.UpdateLesson(lessonId: _lesson0.Id, name: "newname0", containerLessonId: Guid.Empty, courseId: _course2.Id, masterLessonId: Guid.Empty,
                                           predecessorLessonId: Guid.NewGuid(), sequenceNumber: -1, isFolder: false, isHidden: false, dateModified: DateTime.Now);

            Assert.AreEqual(expected.Name, "newname0");
            Assert.AreEqual(_lesson0.CourseId, _course2.Id);
            Assert.AreNotEqual(_lesson0.PredecessorLessonId, Guid.Empty);
            Assert.AreNotEqual(expected.SequenceNumber, -1); //not set if under 1

            expected = _lessonCUDService.UpdateLesson(lessonId: _lesson0.Id, name: "newname0", containerLessonId: Guid.Empty, courseId: _course1.Id, masterLessonId: Guid.Empty,
                                           predecessorLessonId: Guid.NewGuid(), sequenceNumber: 4, isFolder: false, isHidden: false, dateModified: DateTime.Now);
            //Course container children are incremented and then reordered up from one.
            //Since lesson3's Seq number is higher initially than the target for lesson 0 (4)!
            //It is incremented
            Assert.AreEqual(_lesson0.SequenceNumber, 1);
            Assert.AreEqual(_lesson3.SequenceNumber, 2);

            _lesson1.SequenceNumber = 11;
            expected = _lessonCUDService.UpdateLesson(lessonId: _lesson2.Id, name: null, containerLessonId: _lesson0.Id, courseId: Guid.Empty, masterLessonId: _lesson0.Id,
                                           predecessorLessonId: Guid.Empty, sequenceNumber: 3, isFolder: false, isHidden: false, dateModified: DateTime.Now);

            //Container children are ordered starting at one from smallest to greatest according to the integer passed in as a parameter for sequence number.
            //The data are sorted, and then any gaps are removed, counting from 1 up with no interruptions. 
            Assert.AreEqual(expected.SequenceNumber, 1);
            Assert.AreEqual(_lesson1.SequenceNumber, 2);

        }

        [Test]
        public void testRaiseLessonSequenceNumberByOne()
        {
            int? tempSeq0 = _lesson0.SequenceNumber;
            int? tempSeq1 = _lesson1.SequenceNumber;
            Lesson tempLessonCont = _lesson1.ContainerLesson;
            Guid? tempLessonContId = _lesson1.ContainerLessonId;
            Guid tempCourseId = _lesson1.CourseId;

            _lesson0.SequenceNumber = null;
            _mocks.ClearLists();
            _mocks.mockLessonRepo.Object.Insert(_lesson0); _mocks.mockLessonRepo.Object.Insert(_lesson1);

            _lessonCUDService.RaiseLessonSequenceNumberByOne(_lesson0.Id);

            Assert.IsNull(_lesson0.SequenceNumber); //no changes if null

            _lesson0.SequenceNumber = 1;

            _lessonCUDService.RaiseLessonSequenceNumberByOne(_lesson0.Id);

            Assert.AreEqual(_lesson0.SequenceNumber, 1); //since there are no others in container, it's still 1

            _lesson1.ContainerLesson = null; _lesson1.ContainerLessonId = null; _lesson1.SequenceNumber = 2; _lesson1.IsActive = true;
            _lesson1.CourseId = _lesson0.CourseId;

            _lessonCUDService.RaiseLessonSequenceNumberByOne(_lesson0.Id);

            //now they are reordered since they had the same container (the course)
            Assert.AreEqual(_lesson1.SequenceNumber, 1);
            Assert.AreEqual(_lesson0.SequenceNumber, 2);

            _lesson1.SequenceNumber = 2; _lesson0.SequenceNumber = 1;
            _lesson0.ContainerLessonId = Guid.NewGuid(); _lesson1.ContainerLessonId = _lesson0.ContainerLessonId;

            _lessonCUDService.RaiseLessonSequenceNumberByOne(_lesson0.Id);

            //now they are reordered since they had the same container (the containerlesson)
            Assert.AreEqual(_lesson1.SequenceNumber, 1);
            Assert.AreEqual(_lesson0.SequenceNumber, 2);

            //return values to their initial state
            _lesson0.ContainerLessonId = null;
            _lesson0.SequenceNumber = tempSeq0; _lesson1.SequenceNumber = tempSeq1;
            _lesson1.ContainerLesson = tempLessonCont; _lesson1.ContainerLessonId = tempLessonContId;
            _lesson1.CourseId = tempCourseId;

            Lesson bsLesson = new Lesson() { Id = Guid.NewGuid(), SequenceNumber = 0 };

            _mocks.mockLessonRepo.Object.Insert(bsLesson);

            _lessonCUDService.RaiseLessonSequenceNumberByOne(bsLesson.Id); //just to hit the final branch! Nothing happens really
        }

        [Test]
        public void testLowerLessonSequenceNumberByOne()
        {
            int? tempSeq0 = _lesson0.SequenceNumber;
            int? tempSeq1 = _lesson1.SequenceNumber;
            Lesson tempLessonCont = _lesson1.ContainerLesson;
            Guid? tempLessonContId = _lesson1.ContainerLessonId;
            Guid tempCourseId = _lesson1.CourseId;

            _lesson0.SequenceNumber = null;
            _mocks.ClearLists();
            _mocks.mockLessonRepo.Object.Insert(_lesson0); _mocks.mockLessonRepo.Object.Insert(_lesson1);

            _lessonCUDService.LowerLessonSequenceNumberByOne(_lesson0.Id);

            Assert.IsNull(_lesson0.SequenceNumber); //no changes if null

            _lesson0.SequenceNumber = 1;

            _lessonCUDService.LowerLessonSequenceNumberByOne(_lesson0.Id);

            Assert.AreEqual(_lesson0.SequenceNumber, 1); //since there are no others in container, it's still 1

            _lesson1.ContainerLesson = null; _lesson1.ContainerLessonId = null; _lesson1.SequenceNumber = 1; _lesson1.IsActive = true;
            _lesson1.CourseId = _lesson0.CourseId; _lesson0.SequenceNumber = 2;

            _lessonCUDService.LowerLessonSequenceNumberByOne(_lesson0.Id);

            //now they are reordered since they had the same container (the course)
            Assert.AreEqual(_lesson1.SequenceNumber, 2);
            Assert.AreEqual(_lesson0.SequenceNumber, 1);

            _lesson1.SequenceNumber = 1; _lesson0.SequenceNumber = 2;
            _lesson0.ContainerLessonId = Guid.NewGuid(); _lesson1.ContainerLessonId = _lesson0.ContainerLessonId;

            _lessonCUDService.LowerLessonSequenceNumberByOne(_lesson0.Id);

            //now they are reordered since they had the same container (the containerlesson)
            Assert.AreEqual(_lesson1.SequenceNumber, 2);
            Assert.AreEqual(_lesson0.SequenceNumber, 1);

            //return values to their initial state
            _lesson0.ContainerLessonId = null;
            _lesson0.SequenceNumber = tempSeq0; _lesson1.SequenceNumber = tempSeq1;
            _lesson1.ContainerLesson = tempLessonCont; _lesson1.ContainerLessonId = tempLessonContId;
            _lesson1.CourseId = tempCourseId;

            Lesson bsLesson = new Lesson() { Id = Guid.NewGuid(), SequenceNumber = 0 };

            _mocks.mockLessonRepo.Object.Insert(bsLesson);

            _lessonCUDService.LowerLessonSequenceNumberByOne(bsLesson.Id); //just to hit the final branch! Nothing happens really
        }

        [Test]
        public void testImportLesson_ReturnId()
        {
            _mocks.ClearLists();
            _mocks.mockLessonRepo.Object.Insert(_fromLesson);
            _mocks.mockLessonRepo.Object.Insert(_toCourseLesson);
            _mocks.mockLessonRepo.Object.Insert(_masterLesson);

            Guid returnVal = _lessonCUDService.ImportLesson_ReturnId(_fromLesson.Id, _containerLesson.Id, _toCourseLesson.Id);

            Assert.IsNotNull(_mocks.mockLessonRepo.Object.GetById(returnVal));

            _fromLesson.Course = _courseDiffMetaId;
            _mocks.mockLessonRepo.Object.Update(_fromLesson);

            returnVal = _lessonCUDService.ImportLesson_ReturnId(_fromLesson.Id, _containerLesson.Id, _toCourseLesson.Id);

            Assert.IsNotNull(_mocks.mockLessonRepo.Object.GetById(returnVal));

            _toCourseLesson.MasterLessonId = _masterLesson.Id;
            _toCourseLesson.Course = _courseWithMaster;
            _mocks.mockLessonRepo.Object.Update(_toCourseLesson);

            returnVal = _lessonCUDService.ImportLesson_ReturnId(_fromLesson.Id, _containerLesson.Id, _toCourseLesson.Id);

            Assert.IsNotNull(_mocks.mockLessonRepo.Object.GetById(returnVal));
        }

        private void initializeCUDServices()
        {
            _documentUseCUDService = new DocumentUseCUDService(_mocks.mockDocumentUseRepo.Object, _mocks.mockLessonRepo.Object);
            _messageUseCUDService = new MessageUseCUDService(_mocks.mockMessageUseRepo.Object);
            _stashedLessonPlanCUDService = new StashedLessonPlanCUDService(_mocks.mockStashedLessonPlanRepo.Object, _mocks.mockLessonRepo.Object, _mocks.mockLessonPlanRepo.Object);
            _lessonPlanCUDService = new LessonPlanCUDService(_mocks.mockLessonPlanRepo.Object, _stashedLessonPlanCUDService, _mocks.mockLessonRepo.Object);
            _sharedCUDService = new SharedCUDService(_mocks.mockLessonRepo.Object, _mocks.mockCourseRepo.Object, _mocks.mockMessageUseRepo.Object);
            _stashedNarrativeCUDService = new StashedNarrativeCUDService(_mocks.mockStashedNarrativeRepo.Object, _mocks.mockLessonRepo.Object);
            _narrativeCUDService = new NarrativeCUDService(_mocks.mockNarrativeRepo.Object, _mocks.mockLessonRepo.Object, _mocks.mockStashedNarrativeRepo.Object, _mocks.mockCourseRepo.Object, _stashedNarrativeCUDService, _sharedCUDService);

            _lessonCUDService = new LessonCUDService(null, _mocks.mockLessonRepo.Object, _mocks.mockNarrativeRepo.Object, _mocks.mockDocumentUseRepo.Object,
                _mocks.mockLessonPlanRepo.Object, _mocks.mockStashedLessonPlanRepo.Object, _mocks.mockMessageUseRepo.Object, _mocks.mockTermRepo.Object, _mocks.mockMetaLessonRepo.Object, _mocks.mockCourseRepo.Object,
                _documentUseCUDService, _mocks.mockStashedNarrativeRepo.Object, _stashedLessonPlanCUDService, _messageUseCUDService, _lessonPlanCUDService, _narrativeCUDService,
                _mocks.mockMessageUseDomainObjBuilder.Object, _sharedCUDService, _mocks.mockLessonDomainObjBuilder.Object);

        }

        private void initializeObjects()
        {
            _user0 = new User();
            _user0.Id = Guid.NewGuid();
            _user0.DisplayName = "User 0 Display Name";

            _course0 = new Course();
            _course1 = new Course();
            _course2 = new Course();
            _course0.Id = Guid.NewGuid();
            _course1.Id = Guid.NewGuid();
            _course2.Id = Guid.NewGuid();

            _course0.Name = "Course 0";
            _course0.WorkingGroupId = Guid.NewGuid();
            _course0.TermId = Guid.NewGuid();
            _course0.User = _user0;
            _course0.UserId = Guid.NewGuid();
            _course0.Term = new Term();
            _course0.Term.StartDate = new DateTime();
            _course0.MasterCourseId = Guid.NewGuid();
            _course0.ShowOptionalLessons = false;
            _course0.ShowFolders = false;
            _course0.PredecessorCourseChildren = new List<Course>();
            _course0.ClassSections = new List<ClassSection>();
            _course0.MasterCourseChildren = new List<Course>();

            _course0.MetaCourseId = Guid.NewGuid();

            _coursePreference0 = new CoursePreference();
            _coursePreference0.Id = _course0.Id;
            _course0.CoursePreference = _coursePreference0;
            _course0.CoursePreferenceId = _coursePreference0.Id;
            _coursePreference0.Name = "Course Preference 0";
            _coursePreference0.DoShowNarrativeNotifications = true;
            _coursePreference0.DoShowLessonPlanNotifications = true;
            _coursePreference0.DoShowDocumentNotifications = true;

            _course1.Name = "Course 1";
            _course1.UserId = Guid.NewGuid();
            _course1.TermId = Guid.NewGuid();
            _course1.Term = new Term();
            _course1.Term.StartDate = new DateTime();
            _course1.MasterCourseId = null;

            _course2.Name = "Course 2";
            _course2.UserId = _course0.UserId;
            _course2.TermId = _course0.TermId;
            _course2.Term = new Term();
            _course2.Term.StartDate = new DateTime();
            _course2.MasterCourseId = null;

            _lesson0 = new Lesson();
            _lesson1 = new Lesson();
            _lesson2 = new Lesson();
            _lesson3 = new Lesson();

            _lesson0.Id = Guid.NewGuid();
            _lesson1.Id = Guid.NewGuid();
            _lesson2.Id = Guid.NewGuid();
            _lesson3.Id = Guid.NewGuid();

            _lesson0.Course = _course0;
            _lesson0.CourseId = _course0.Id;
            _lesson0.IsActive = true;
            _lesson0.HasOutForEditDocuments = true;
            _lesson0.ContainerLessonChildren.Add(_lesson1);
            _lesson0.MetaLessonId = Guid.NewGuid();

            _lesson0.Name = "oldname0";
            _lesson1.Name = "oldname1";
            _lesson2.Name = "oldname2";
            _lesson3.Name = "oldname3";

            _lesson0.ContainerLesson = null;
            _lesson0.SequenceNumber = 0;
            _lesson0.StashedLessonPlans = new List<StashedLessonPlan>() { new StashedLessonPlan() };
            _lesson0.StashedNarratives = new List<StashedNarrative>() { new StashedNarrative() };
            _lesson0.MessageUses = new List<MessageUse>() { new MessageUse() };

            _lesson1.ContainerLesson = _lesson0;
            _lesson1.ContainerLessonId = _lesson0.Id;
            _lesson1.SequenceNumber = 1;
            _lesson1.StashedLessonPlans = new List<StashedLessonPlan>() { new StashedLessonPlan() };
            _lesson1.StashedNarratives = new List<StashedNarrative>() { new StashedNarrative() };
            _lesson1.MessageUses = new List<MessageUse>() { new MessageUse() };
            _lesson1.IsActive = true;

            _lesson2.ContainerLesson = null;
            _lesson2.SequenceNumber = 0;
            _lesson2.StashedLessonPlans = new List<StashedLessonPlan>() { new StashedLessonPlan() };
            _lesson2.StashedNarratives = new List<StashedNarrative>() { new StashedNarrative() };
            _lesson2.MessageUses = new List<MessageUse>() { new MessageUse() };

            _lesson3.Course = _course1; _lesson3.CourseId = _course1.Id;
            _lesson3.CourseId = _course1.Id;
            _lesson3.IsActive = true;
            _lesson3.HasOutForEditDocuments = true;
            _lesson3.SequenceNumber = 6;

            _lessonPlan0 = new LessonPlan();
            _lessonPlan0.Id = Guid.NewGuid();
            _lessonPlan0.DoSuggestUsingMaster = false;

            _lessonPlan1 = new LessonPlan();
            _lessonPlan1.Id = Guid.NewGuid();
            _lessonPlan1.DoSuggestUsingMaster = false;

            Guid testGuid = Guid.NewGuid();
            _masterCourse = new Course() { Id = Guid.NewGuid() };

            _documentUse0 = new DocumentUse();
            _documentUse0.LessonId = _lesson0.Id;
            _documentUse0.IsActive = true;
            _documentUse0.Document = new Document();
            _documentUse0.Document.Name = "document name";
            _documentUse0.IsOutForEdit = true;

            _courseWithMaster = new Course()
            {
                MetaCourseId = testGuid,
                Name = "some name",
                MetaCourse = new MetaCourse() { Name = "some name", },
                MasterCourse = _masterCourse,
                MasterCourseId = _masterCourse.Id
            };


            _fromLesson = new Lesson();
            _fromLesson.Id = Guid.NewGuid();
            _fromLesson.Narrative = new Narrative();
            _fromLesson.Course = new Course() { MetaCourseId = testGuid };
            _fromLesson.MetaLesson = new MetaLesson() { Name = "some name", Id = Guid.NewGuid() };

            _toCourseLesson = new Lesson();
            _toCourseLesson.Id = Guid.NewGuid();
            _toCourseLesson.Course = new Course()
            {
                MetaCourseId = testGuid,
                Name = "some name",
                MetaCourse = new MetaCourse() { Name = "some name", },

            };
            _toCourseLesson.MetaLesson = new MetaLesson() { Name = "some name", Id = Guid.NewGuid() };

            _containerLesson = new Lesson();
            _containerLesson.Id = Guid.NewGuid();
            _containerLesson.MetaLesson = new MetaLesson() { Name = "some name", Id = Guid.NewGuid() };

            _masterLesson = new Lesson() { Id = Guid.NewGuid(),
                                           LessonPlan = new LessonPlan() { DateModified = DateTime.Now },
                                           Narrative = new Narrative() { DateModified = DateTime.Now }
            };

            _courseDiffMetaId = new Course() { MetaCourseId = Guid.NewGuid(), Name = "some name" };

            _masterCourse.Lessons.Add(_toCourseLesson);
            _masterCourse.Lessons.Add(_masterLesson);
            _masterCourse.Lessons.Add(_fromLesson);
        }

        [Test]
        public void testRenumberAllLessonsInCourse()
        {
            _lesson0.IsActive = true;
            _mocks.mockLessonRepo.Object.Insert(_lesson0);
            _lessonCUDService.RenumberAllLessonsInCourse(_course0.Id);

            Assert.AreEqual(_lesson0.SequenceNumber, 1);
        }

        [Test]
        public void testReturnToReferenceChoiceConfirmedDate()
        {
            _mocks.ClearLists();
            _lesson0.LessonPlanReferenceDateChoiceConfirmed = null;
            _mocks.mockLessonRepo.Object.Insert(_lesson0);

            _lessonCUDService.ReturnToReferenceChoiceConfirmedDate(_lesson0.Id);

            DateTime? expectedNull = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id).LessonPlanDateChoiceConfirmed;
            Assert.Null(_lesson0.LessonPlanDateChoiceConfirmed);
            Assert.IsNull(expectedNull);

            _mocks.ClearLists();
            _lesson0.LessonPlanReferenceDateChoiceConfirmed = new DateTime();
            _mocks.mockLessonRepo.Object.Insert(_lesson0);

            _lessonCUDService.ReturnToReferenceChoiceConfirmedDate(_lesson0.Id);

            DateTime expected = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id).LessonPlanDateChoiceConfirmed.GetValueOrDefault();
            Assert.AreEqual(expected, _lesson0.LessonPlanReferenceDateChoiceConfirmed);
        }

        [Test]
        public void testSetLessonPlanDateChoiceConfirmedBackInTime()
        {
            _mocks.ClearLists();
            _lesson0.LessonPlanDateChoiceConfirmed = DateTime.Now;
            _mocks.mockLessonRepo.Object.Insert(_lesson0);
            DateTime actual = _lesson0.LessonPlanDateChoiceConfirmed.GetValueOrDefault().Subtract(TimeSpan.FromDays(4));


            _lessonCUDService.SetLessonPlanDateChoiceConfirmedBackInTime(_lesson0.Id, 4);

            DateTime expected = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id).LessonPlanDateChoiceConfirmed.GetValueOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testUpdateSuggestUsingMasterLessonPlan()
        {
            _mocks.ClearLists();
            _mocks.mockLessonPlanRepo.Object.Insert(_lessonPlan0);

            _lessonCUDService.UpdateSuggestUsingMasterLessonPlan(_lessonPlan0.Id, true);

            bool expected = _mocks.mockLessonPlanRepo.Object.GetById(_lessonPlan0.Id).DoSuggestUsingMaster;
            Assert.IsTrue(expected);
        }

        [Test]
        public void testSetLessonPlanDateChoiceConfirmed()
        {
            _mocks.ClearLists();
            DateTime testDateTime = DateTime.Now;
            _lessonPlan0.DoSuggestUsingMaster = false;
            _mocks.mockLessonRepo.Object.Insert(_lesson0);
            _mocks.mockLessonPlanRepo.Object.Insert(_lessonPlan0);
            DateTime actual = testDateTime.AddMilliseconds(3);

            _lessonCUDService.SetLessonPlanDateChoiceConfirmed(_lesson0.Id, _lessonPlan0.Id, testDateTime, true);

            DateTime expected = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id).LessonPlanDateChoiceConfirmed.GetValueOrDefault();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testUpdateLessonPlanDateChoiceConfirmed()
        {
            _mocks.ClearLists();
            DateTime testDateTime = DateTime.Now;
            DateTime actual = testDateTime.AddMilliseconds(3);
            _lessonPlan0.DoSuggestUsingMaster = false;
            _lessonPlan0.DateModified = actual;
            _lesson0.LessonPlanDateChoiceConfirmed = actual;
            _mocks.mockLessonPlanRepo.Object.Insert(_lessonPlan0);
            _mocks.mockLessonPlanRepo.Object.Insert(_lessonPlan1);
            _mocks.mockLessonRepo.Object.Insert(_lesson0);


            _lessonCUDService.UpdateLessonPlanDateChoiceConfirmed(_lesson0.Id, _lessonPlan1.Id, _lessonPlan0.Id, true);
            DateTime expected = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id).LessonPlanDateChoiceConfirmed.GetValueOrDefault();
            Assert.AreEqual(expected, actual.AddMilliseconds(3));
        }

        [Test]
        public void testSetLessonDocumentsBoolean()
        {
            _mocks.ClearLists();
            _mocks.mockLessonRepo.Object.Insert(_lesson0);
            _mocks.mockDocumentUseRepo.Object.Insert(_documentUse0);
            _lessonCUDService.SetLessonDocumentsBoolean(_lesson0);
            Lesson expected = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id);
            Assert.IsTrue(expected.HasOutForEditDocuments);
            _lesson0.HasOutForEditDocuments = false;
            _documentUse0.IsOutForEdit = false;
            _lessonCUDService.SetLessonDocumentsBoolean(_lesson0);
            expected = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id);
            Assert.IsFalse(expected.HasOutForEditDocuments);
        }

        [Test]
        public void testDeleteLessonReturnMessageAndId()
        {
            _mocks.ClearLists();
            _lesson0.MasterLessonChildren = new List<Lesson>() { new Lesson() };
            _lesson0.PredecessorLessonChildren = new List<Lesson>();
            _lesson0.LessonUses = new List<LessonUse>();
            _mocks.mockLessonRepo.Object.Insert(_lesson0);

            var expected = _lessonCUDService.DeleteLessonReturnMessageAndId(_lesson0.Id);

            Assert.AreEqual(expected.Item1, "Lesson is not deletable due to dependencies.");
            Assert.AreEqual(expected.Item2, _lesson0.Id);

            _mocks.ClearLists();

            expected = _lessonCUDService.DeleteLessonReturnMessageAndId(Guid.Empty);

            Assert.AreEqual(expected.Item1, "");
            Assert.AreEqual(expected.Item2, Guid.Empty);

            _mocks.ClearLists();
            _lesson0.MasterLessonChildren = new List<Lesson>();
            _lesson0.PredecessorLessonChildren = new List<Lesson>();
            _lesson0.LessonUses = new List<LessonUse>();
            _lesson0.StashedLessonPlans = new List<StashedLessonPlan>();
            _lesson0.StashedNarratives = new List<StashedNarrative>();
            _lesson0.DocumentUses = new List<DocumentUse>();
            _lesson0.MessageUses = new List<MessageUse>();
            _lesson0.ContainerLessonChildren = new List<Lesson>();

            _mocks.mockLessonRepo.Object.Insert(_lesson0);

            expected = _lessonCUDService.DeleteLessonReturnMessageAndId(_lesson0.Id);
            Assert.AreEqual(expected.Item1, "Lesson has been deleted.");
            Assert.AreEqual(expected.Item2, Guid.Empty);

        }

        [Test]
        public void testUpdateDateDocumentsModified()
        {
            _mocks.ClearLists();
            DateTime? testDateTime = DateTime.Now;
            _mocks.mockLessonRepo.Object.Insert(_lesson0);

            _lessonCUDService.UpdateDateDocumentsModified(_lesson0.Id, testDateTime.Value);

            Lesson expected = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id);
            Assert.AreEqual(expected.DateDocumentsModified, testDateTime);


            testDateTime = null;
            _lessonCUDService.UpdateDateDocumentsModified(Guid.Empty, testDateTime.GetValueOrDefault());
            Assert.IsNull(testDateTime);
        }

        [Test]
        public void testSetIsHidden()
        {
            _mocks.ClearLists();
            _lesson0.IsHidden = false;
            _mocks.mockLessonRepo.Object.Insert(_lesson0);
            _lessonCUDService.SetIsHidden(_lesson0.Id, true);

            Lesson expected = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id);

            Assert.IsTrue(expected.IsHidden);

            _lesson0.IsHidden = false;
            _mocks.mockLessonRepo.Object.Insert(_lesson0);
            _lessonCUDService.SetIsHidden(Guid.Empty, true);

            expected = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id);

            Assert.IsFalse(expected.IsHidden);
        }

        [Test]
        public void testToggleIsCollapsed()
        {
            _mocks.ClearLists();
            _lesson0.IsCollapsed = true;
            _mocks.mockLessonRepo.Object.Insert(_lesson0);

            _lessonCUDService.ToggleIsCollapsed(_lesson0.Id);

            Lesson expected = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id);
            Assert.IsFalse(expected.IsCollapsed);

        }

        [Test]
        public void testUpdateNarrativeDateChoiceConfirmed()
        {
            _mocks.ClearLists();
            _lesson0.Narrative = new Narrative();
            _mocks.mockLessonRepo.Object.Insert(_lesson0);
            DateTime date = DateTime.Now;
            _lesson1.Narrative = new Narrative();
            _lesson1.Narrative.DateModified = DateTime.Now;
            _mocks.mockLessonRepo.Object.Insert(_lesson1);

            _lessonCUDService.UpdateNarrativeDateChoiceConfirmed(_lesson0.Id, _lesson1.Id, true);
            Lesson expected = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id);
            Assert.AreEqual(expected.NarrativeDateChoiceConfirmed, _lesson1.Narrative.DateModified.AddMilliseconds(1));
            Assert.IsTrue(expected.Narrative.DoSuggestRemovingComment);

            _lessonCUDService.UpdateNarrativeDateChoiceConfirmed(_lesson0.Id, _lesson1.Id, false);
            expected = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id);
            Assert.AreEqual(expected.NarrativeDateChoiceConfirmed, date.AddMilliseconds(1));
            Assert.IsFalse(expected.Narrative.DoSuggestRemovingComment);
        }

        [Test]
        public void testSetNarrativeDateChoiceConfirmed()
        {
            _mocks.ClearLists();
            _lesson0.Narrative = new Narrative();
            _mocks.mockLessonRepo.Object.Insert(_lesson0);
            DateTime date = DateTime.Now;

            _lessonCUDService.SetNarrativeDateChoiceConfirmed(_lesson0.Id, date, true);

            Lesson expected = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id);
            Assert.AreEqual(expected.NarrativeDateChoiceConfirmed, date.AddMilliseconds(1));
            Assert.IsTrue(expected.Narrative.DoSuggestRemovingComment);
        }

        [Test]
        public void testDeleteLesson()
        {
            _mocks.ClearLists();
            _mocks.mockLessonRepo.Object.Insert(_lesson1);
            _mocks.mockLessonRepo.Object.Insert(_lesson0);
            _lessonCUDService.DeleteLesson(_lesson0);

            Assert.IsEmpty(_mocks.repoLessonList);
        }

        [Test]
        public void testReturnToReferenceNarrativeChoiceConfirmedDate()
        {
            _mocks.ClearLists();
            _lesson0.NarrativeDateChoiceConfirmed = null;
            _lesson0.NarrativeReferenceDateChoiceConfirmed = null;
            _mocks.mockLessonRepo.Object.Insert(_lesson0);

            _lessonCUDService.ReturnToReferenceNarrativeChoiceConfirmedDate(_lesson0.Id);

            Lesson expected = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id);
            Assert.IsNull(expected.NarrativeDateChoiceConfirmed);

            _mocks.ClearLists();
            DateTime? actual = DateTime.Now;
            _lesson0.NarrativeReferenceDateChoiceConfirmed = actual;
            _mocks.mockLessonRepo.Object.Insert(_lesson0);

            _lessonCUDService.ReturnToReferenceNarrativeChoiceConfirmedDate(_lesson0.Id);

            expected = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id);
            Assert.AreEqual(actual, expected.NarrativeDateChoiceConfirmed);
        }

        [Test]
        public void testUpdateDocumentsChoiceConfirmedDateToComparisonLesson()
        {
            _mocks.ClearLists();
            DateTime testTime = DateTime.Now;
            _lesson0 = new Lesson() { Id = Guid.NewGuid() };
            _lesson1 = new Lesson() { Id = Guid.NewGuid(), DateDocumentsModified = testTime };
            _mocks.mockLessonRepo.Object.Insert(_lesson0);
            _mocks.mockLessonRepo.Object.Insert(_lesson1);

            _lessonCUDService.UpdateDocumentsChoiceConfirmedDateToComparisonLesson(_lesson0.Id, _lesson1.Id);

            _lesson1.DateDocumentsModified = testTime.AddMilliseconds(3);
            Assert.AreEqual(_lesson0.DateTimeDocumentsChoiceConfirmed.Value, _lesson1.DateDocumentsModified.Value);

        }

        [Test]
        public void testSetDateDocChoiceConfirmedBackInTime()
        {
            _mocks.ClearLists();
            DateTime testTime = DateTime.Now;
            _lesson0 = new Lesson() { Id = Guid.NewGuid(), DateTimeDocumentsChoiceConfirmed = testTime };
            _mocks.mockLessonRepo.Object.Insert(_lesson0);

            _lessonCUDService.SetDateDocChoiceConfirmedBackInTime(_lesson0.Id, 5);

            testTime = testTime.Subtract(TimeSpan.FromDays(5));
            Assert.AreEqual(testTime, _lesson0.DateTimeDocumentsChoiceConfirmed.Value);
        }
    

        [Test]
        public void testUpdateNarrativeDateChoiceConfirmedToGroupDate()
        {
            _mocks.ClearLists();

            ClassSection  _classSection0 = new ClassSection() { Id = Guid.NewGuid(), CourseId = _course0.Id, Course = _course0 };
            _lesson0 = new Lesson()
            {
                Course = _course0,
                CourseId = _course0.Id,
                Id = Guid.NewGuid(),
                LessonPlan = new LessonPlan() { Id = Guid.NewGuid() },
                Narrative = new Narrative() { Id = Guid.NewGuid() }
            };
            _lesson0.LessonPlanId = _lesson0.LessonPlan.Id;
            _lesson0.NarrativeId = _lesson0.Narrative.Id;
            _lesson0.Narrative.Text = "Some Text";

            _lesson1 = new Lesson()
            {
                Course = _course0,
                CourseId = _course0.Id,
                Id = Guid.NewGuid(),
                Narrative = _lesson0.Narrative,
                NarrativeId = _lesson0.NarrativeId,
                LessonPlan = _lesson0.LessonPlan,
                LessonPlanId = _lesson0.LessonPlanId,
                MasterLesson = _masterLesson
            };

            _mocks.mockLessonRepo.Object.Insert(_lesson0);
            _mocks.mockLessonRepo.Object.Insert(_lesson1);
            
            
            _lessonCUDService.UpdateNarrativeDateChoiceConfirmedToGroupDate(_lesson1.Id);

            Lesson expected = _mocks.mockLessonRepo.Object.GetById(_lesson1.Id);
            Assert.AreEqual(expected, _lesson1);

            _mocks.ClearLists();
            _lesson0.NarrativeDateChoiceConfirmed = null;
            _mocks.mockLessonRepo.Object.Insert(_lesson0);
            
            _lessonCUDService.UpdateNarrativeDateChoiceConfirmedToGroupDate(Guid.Empty);

            expected = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id);
            Assert.IsNull(expected.NarrativeDateChoiceConfirmed);
        }

        [Test]
        public void testReturnToReferenceDateDocumentChoiceConfirmed()
        {
            _mocks.ClearLists();
            _lesson0.ReferenceDateTimeDocChoiceConfirmed = null;
            _mocks.mockLessonRepo.Object.Insert(_lesson0);
            _lessonCUDService.ReturnToReferenceDateDocumentChoiceConfirmed(_lesson0.Id);
            Lesson expected = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id);
            Assert.IsNull(expected.DateTimeDocumentsChoiceConfirmed);

            _mocks.ClearLists();
            _lesson0.ReferenceDateTimeDocChoiceConfirmed = DateTime.Now;
            _mocks.mockLessonRepo.Object.Insert(_lesson0);
            _lessonCUDService.ReturnToReferenceDateDocumentChoiceConfirmed(_lesson0.Id);
            expected = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id);
            Assert.AreEqual(expected.DateTimeDocumentsChoiceConfirmed, _lesson0.ReferenceDateTimeDocChoiceConfirmed.Value);
        }

        [Test]
        public void testSetNarrativeDateChoiceConfirmedBackInTime()
        {
            _mocks.ClearLists();
            DateTime? actual = DateTime.Now;
            _lesson0.NarrativeDateChoiceConfirmed = actual;
            _mocks.mockLessonRepo.Object.Insert(_lesson0);
            _lessonCUDService.SetNarrativeDateChoiceConfirmedBackInTime(_lesson0.Id, 3);
            Lesson expected = _mocks.mockLessonRepo.Object.GetById(_lesson0.Id);
            Assert.AreEqual(expected.NarrativeDateChoiceConfirmed, actual.GetValueOrDefault().Subtract(TimeSpan.FromDays(3)));

        }



    }

}

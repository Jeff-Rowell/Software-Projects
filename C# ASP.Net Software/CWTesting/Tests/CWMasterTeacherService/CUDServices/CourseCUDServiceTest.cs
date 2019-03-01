using CWMasterTeacherDataModel;
using CWMasterTeacherService.CUDServices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CWTesting.Tests.CWMasterTeacherService.CUDServices
{
    [TestFixture]
    class CourseCUDServiceTest
    {
        private MocksForCUDs mfc;

        private CourseCUDService _courseCUDService;
        private LessonCUDService _lessonCUDService;
        private UserCUDService _userCUDService;
        private CoursePreferenceCUDService _coursePreferenceCUDService;
        private SharedCUDService _sharedCUDService;
        private DocumentUseCUDService _documentUseCUDService;

        private Course _course0;
        private Course _course1;
        private Course _course2;
        private User _user0;
        private ClassSection _classSection0;
        private Lesson _lesson0;
        private Lesson _lesson1;
        private Lesson _lesson2;
        private CoursePreference _coursePreference0;

        [SetUp]
        public void setup()
        {
            mfc = new MocksForCUDs();
            setupUserData();
            setupCourseData();
            setupLessondata();
            InitializeCUDServices();
        }

        public void setupUserData()
        {
            _user0 = new User();
            _user0.Id = Guid.NewGuid();
            _user0.DisplayName = "User 0 Display Name";
        }

        public void setupCourseData()
        {
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
        }

        public void setupLessondata()
        {
            _lesson0 = new Lesson();
            _lesson1 = new Lesson();
            _lesson2 = new Lesson();

            _lesson0.Id = Guid.NewGuid();
            _lesson1.Id = Guid.NewGuid();
            _lesson2.Id = Guid.NewGuid();

            _lesson0.Course = _course0;
            _lesson0.CourseId = _course0.Id;
            _lesson0.IsActive = false;
            _lesson0.HasOutForEditDocuments = true;

            _lesson0.ContainerLesson = null;
            _lesson0.SequenceNumber = 0;
            _lesson0.StashedLessonPlans = new List<StashedLessonPlan>()
            {
                new StashedLessonPlan()
            };

            _lesson0.StashedNarratives = new List<StashedNarrative>()
            {
                new StashedNarrative()
            };

            _lesson0.MessageUses = new List<MessageUse>()
            {
                new MessageUse()
            };

            _lesson1.ContainerLesson = null;
            _lesson1.SequenceNumber = 1;
            _lesson1.StashedLessonPlans = new List<StashedLessonPlan>()
            {
                new StashedLessonPlan()
            };

            _lesson1.StashedNarratives = new List<StashedNarrative>()
            {
                new StashedNarrative()
            };

            _lesson1.MessageUses = new List<MessageUse>()
            {
                new MessageUse()
            };

            _lesson2.ContainerLesson = null;
            _lesson2.SequenceNumber = 2;
            _lesson2.StashedLessonPlans = new List<StashedLessonPlan>()
            {
                new StashedLessonPlan()
            };

            _lesson2.StashedNarratives = new List<StashedNarrative>()
            {
                new StashedNarrative()
            };

            _lesson2.MessageUses = new List<MessageUse>()
            {
                new MessageUse()
            };
        }

        public void InitializeCUDServices()
        {
            _documentUseCUDService = new DocumentUseCUDService(mfc.mockDocumentUseRepo.Object, 
                                                               mfc.mockLessonRepo.Object);
            _lessonCUDService = new LessonCUDService(null, mfc.mockLessonRepo.Object, null, 
                                                     mfc.mockDocumentUseRepo.Object,
                                                     null, mfc.mockStashedLessonPlanRepo.Object, 
                                                     mfc.mockMessageUseRepo.Object, 
                                                     mfc.mockTermRepo.Object, mfc.mockMetaLessonRepo.Object, null,
                                                     _documentUseCUDService, mfc.mockStashedNarrativeRepo.Object, null, 
                                                     null, null, null, mfc.mockMessageUseDomainObjBuilder.Object, null, 
                                                     null);
            _userCUDService = new UserCUDService(mfc.mockUserRepo.Object, null, null);
            _coursePreferenceCUDService = new CoursePreferenceCUDService(mfc.mockCoursePreferenceRepo.Object);
            _sharedCUDService = new SharedCUDService(mfc.mockLessonRepo.Object, mfc.mockCourseRepo.Object, null);

            _courseCUDService = new CourseCUDService(mfc.mockCourseRepo.Object, mfc.mockLessonRepo.Object, 
                                                     mfc.mockUserRepo.Object, mfc.mockMetaCourseRepo.Object, 
                                                     _lessonCUDService, _userCUDService,
                                                     mfc.mockCoursePreferenceRepo.Object, _coursePreferenceCUDService,
                                                     _sharedCUDService);
        }

        [Test]
        public void testCopyCourseWithoutMasterId()
        {
            mfc.mockCourseRepo.Object.Insert(_course0);

            Course expected = _courseCUDService.CopyCourse(_course0.Id, null, _course0.UserId, _course0.TermId);

            Assert.NotNull(mfc.mockCourseRepo.Object.GetById(_course0.Id));
            Assert.IsTrue(expected.IsMaster);
            Assert.IsFalse(expected.CoursePreferenceId.Equals(Guid.Empty));
            Assert.AreEqual(expected.MetaCourseId, _course0.MetaCourseId);
            Assert.AreEqual(expected.WorkingGroupId, _course0.WorkingGroupId);
            Assert.AreEqual(expected.TermId, _course0.TermId);
            Assert.AreEqual(expected.UserId, _course0.UserId);
            Assert.IsNull(expected.MasterCourseId);
            Assert.AreEqual(expected.PredecessorCourseId, _course0.Id);
            Assert.AreEqual(expected.Name, _course0.Name);
            Assert.IsTrue(expected.IsActive);

            expected = _courseCUDService.CopyCourse(Guid.NewGuid(), null, _course0.UserId, _course0.TermId);
            Assert.Null(expected);

            _course0.MasterCourseId = Guid.NewGuid();
            Course masterCourse0 = new Course()
            {
                Id = _course0.MasterCourseId.Value
            };

            mfc.mockCourseRepo.Object.Insert(masterCourse0);
            expected = _courseCUDService.CopyCourse(_course0.Id, _course0.MasterCourseId, _course0.UserId, 
                                                    _course0.TermId);
            Assert.NotNull(mfc.mockCourseRepo.Object.GetById(_course0.MasterCourseId.Value));
        }

        [Test]
        public void testRenameCourse()
        {
            mfc.mockCourseRepo.Object.Insert(_course0);
            Course expected = _courseCUDService.RenameCourse(_course0.Id, "New Name");
            Assert.AreEqual(expected.Name, _course0.Name);
        }

        [Test]
        public void testToggleShowFolders()
        {
            mfc.mockCourseRepo.Object.Insert(_course0);
            _courseCUDService.ToggleShowFolders(_course0.Id);
            Assert.IsTrue(_course0.ShowFolders);
            _courseCUDService.ToggleShowFolders(_course0.Id);
            Assert.IsFalse(_course0.ShowFolders);
        }

        [Test]
        public void testToggleShowOptionalLessons()
        {
            mfc.mockCourseRepo.Object.Insert(_course0);
            _courseCUDService.ToggleShowOptionalLessons(_course0.Id);
            Assert.IsTrue(_course0.ShowOptionalLessons);
            _courseCUDService.ToggleShowOptionalLessons(_course0.Id);
            Assert.IsFalse(_course0.ShowOptionalLessons);
        }

        [Test]
        public void testIsThisADuplicateCourse()
        {
            mfc.mockCourseRepo.Object.Insert(_course0);
            mfc.mockCourseRepo.Object.Insert(_course1);
            mfc.mockCourseRepo.Object.Insert(_course2);

            bool is_duplicate = _courseCUDService.IsThisADuplicateCourse(_course0.Id, _course0.MasterCourseId, 
                                                                         _course2.UserId, _course2.TermId);
            Assert.IsTrue(is_duplicate);

            is_duplicate = _courseCUDService.IsThisADuplicateCourse(_course0.Id, Guid.NewGuid(), 
                                                                    _course0.UserId, _course0.TermId);
            Assert.IsFalse(is_duplicate);



            mfc.ClearLists();
            _course0.MasterCourseId = null;
            mfc.mockCourseRepo.Object.Insert(_course0);
            is_duplicate = _courseCUDService.IsThisADuplicateCourse(_course0.Id, null, _course0.UserId, 
                                                                    _course0.TermId);
            Assert.IsTrue(is_duplicate);

            is_duplicate = _courseCUDService.IsThisADuplicateCourse(_course0.Id, null, Guid.NewGuid(), Guid.NewGuid());
            Assert.IsFalse(is_duplicate);

            is_duplicate = _courseCUDService.IsThisADuplicateCourse(Guid.NewGuid(), null, _course0.UserId, 
                                                                    _course0.TermId);
            Assert.IsFalse(is_duplicate);
        }

        [Test]
        public void testCheckIfCourseDeletable_AreLessonsDeletable()
        {
            mfc.mockCourseRepo.Object.Insert(_course0);

            List<Lesson> masterLessonChildren = new List<Lesson>()
            {
                new Lesson()
            };

            List<Lesson> predecessorLessonChildren = new List<Lesson>();
            List<LessonUse> lessonUseList = new List<LessonUse>();

            _lesson0.MasterLessonChildren = masterLessonChildren;
            _lesson0.PredecessorLessonChildren = predecessorLessonChildren;
            _lesson0.LessonUses = lessonUseList;
            List<Lesson> lessonList = new List<Lesson>
            {
                _lesson0
            };

            _course0.Lessons = lessonList;

            string actual = _courseCUDService.CheckIfCourseDeletable(_course0.Id);
            string expected = "OK  Lessons have attachments.";
            Assert.AreEqual(expected, actual);

            lessonList = new List<Lesson> { null };
            _course0.Lessons = lessonList;

            actual = _courseCUDService.CheckIfCourseDeletable(_course0.Id);

            expected = "OK";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testCheckIfCourseDeletable()
        {
            mfc.mockCourseRepo.Object.Insert(_course0);
            List<Course> masterCourseChildrenList = new List<Course>
            {
                new Course(),
                new Course()
            };

            _course0.MasterCourseChildren = masterCourseChildrenList;

            string actual = _courseCUDService.CheckIfCourseDeletable(_course0.Id);

            string expected = "  Course has Master Child Courses.  ";
            Assert.AreEqual(expected, actual);

            List<ClassSection> classSectionList = new List<ClassSection>
            {
                new ClassSection(),
                new ClassSection()
            };

            mfc.mockCourseRepo.Object.Insert(_course1);
            _course1.ClassSections = classSectionList;

            actual = _courseCUDService.CheckIfCourseDeletable(_course1.Id);

            expected = "Course has Class Sections.  ";
            Assert.AreEqual(expected, actual);

            List<Course> predecessorCourseChildrenList = new List<Course>
            {
                new Course(),
                new Course()
            };

            mfc.mockCourseRepo.Object.Insert(_course2);
            _course2.PredecessorCourseChildren = predecessorCourseChildrenList;
        
            actual = _courseCUDService.CheckIfCourseDeletable(_course2.Id);
            expected = "Course has PredecessorCourseChildren.  ";
            Assert.AreEqual(expected, actual);

            _course0.ClassSections = classSectionList;
            actual = _courseCUDService.CheckIfCourseDeletable(_course0.Id);
            expected = "  Course has Master Child Courses.  " + "Course has Class Sections.  ";
            Assert.AreEqual(expected, actual);

            _course0.PredecessorCourseChildren = predecessorCourseChildrenList;
            actual = _courseCUDService.CheckIfCourseDeletable(_course0.Id);
            expected = "  Course has Master Child Courses.  " + "Course has Class Sections.  " + 
                       "Course has PredecessorCourseChildren.  ";
            Assert.AreEqual(expected, actual);

            _course0.PredecessorCourseChildren = predecessorCourseChildrenList;
            _course0.ClassSections = new List<ClassSection>();
            actual = _courseCUDService.CheckIfCourseDeletable(_course0.Id);
            expected = "  Course has Master Child Courses.  " + "Course has PredecessorCourseChildren.  ";
            Assert.AreEqual(expected, actual);

            _course0.PredecessorCourseChildren = predecessorCourseChildrenList;
            _course0.ClassSections = classSectionList;
            _course0.MasterCourseChildren = new List<Course>();
            actual = _courseCUDService.CheckIfCourseDeletable(_course0.Id);
            expected = "Course has Class Sections.  " + "Course has PredecessorCourseChildren.  ";
            Assert.AreEqual(expected, actual);

            _course0.PredecessorCourseChildren = new List<Course>();
            _course0.ClassSections = new List<ClassSection>();
            _course0.MasterCourseChildren = new List<Course>();
            actual = _courseCUDService.CheckIfCourseDeletable(_course0.Id);
            expected = "OK";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testSetShowHiddenLessons()
        {
            mfc.mockCourseRepo.Object.Insert(_course0);
            _courseCUDService.SetShowHiddenLessons(_course0.Id, true);
            Assert.IsTrue(_course0.ShowHiddenLessons);

            mfc.ClearLists();
            _course0.ShowHiddenLessons = false;
            _courseCUDService.SetShowHiddenLessons(_course0.Id, true); 
            Assert.IsFalse(_course0.ShowHiddenLessons); 
        }

        [Test]
        public void testAddLessonAndChildrenToIdSet()
        {
            _lesson0.ContainerLessonChildren.Add(_lesson1);
            _course0.Lessons.Add(_lesson0);
            mfc.mockCourseRepo.Object.Insert(_course0);

            List<Guid> listGuids = _courseCUDService.GetAllLessonIdsForCourse(_course0.Id);

            Assert.Contains(_lesson0.Id, listGuids);
            Assert.Contains(_lesson1.Id, listGuids);
        }

        [Test]
        public void testSetLastDisplayedClassSectionId()
        {
            _classSection0 = new ClassSection();
            _classSection0.Course = _course0;
            _classSection0.Id = Guid.NewGuid();
            mfc.mockCourseRepo.Object.Insert(_course0);
            mfc.mockUserRepo.Object.Insert(_user0);
            
            _courseCUDService.SetLastDisplayedClassSectionId(_classSection0);
            Course expectedCourse = mfc.mockCourseRepo.Object.GetById(_course0.Id);
            User expectedUser = mfc.mockUserRepo.Object.GetById(_user0.Id);
            _courseCUDService.SetLastDisplayedClassSectionId(null);

            Assert.AreEqual(expectedCourse.LastDisplayedClassSectionId, _classSection0.Id);
            Assert.AreEqual(expectedUser.LastDisplayedCourseId, _course0.Id);
        }

        [Test]
        public void testCopyCourseReturnMessage_WithIsDuplicate()
        {
            _course0.MasterCourseId = null;
            mfc.mockCourseRepo.Object.Insert(_course0);
            string expected = _courseCUDService.CopyCourseReturnMessage(_course0.Id, null, _course0.UserId, 
                                                                        _course0.TermId);
            Assert.AreEqual(expected, "We already have a master course for this MetaCourse and Term.");

            _course0.MasterCourseId = Guid.NewGuid();
            expected = _courseCUDService.CopyCourseReturnMessage(_course0.Id, _course0.MasterCourseId, _course0.UserId, 
                                                                 _course0.TermId);
            Assert.AreEqual(expected, "There is already a course for this master course, user, and term.");
        }

        [Test]
        public void testCopyCourseReturnMessage_WithoutIsDuplicate()
        {
            mfc.ClearLists();
            _coursePreference0.Name = "Course Preference 0";
            mfc.mockCoursePreferenceRepo.Object.Insert(_coursePreference0);
            mfc.mockCourseRepo.Object.Insert(_course0);

            string expected = _courseCUDService.CopyCourseReturnMessage(_course0.Id, null, _course0.UserId,
                                                                        _course0.TermId);
            CoursePreference expectedCoursePreference = mfc.repoCoursePreferenceList.ElementAt(1); 

            Assert.IsTrue(expectedCoursePreference.DoShowNarrativeNotifications);
            Assert.IsTrue(expectedCoursePreference.DoShowLessonPlanNotifications);
            Assert.IsTrue(expectedCoursePreference.DoShowDocumentNotifications);
            Assert.AreEqual(expectedCoursePreference.Name, _coursePreference0.Name);
            Assert.AreEqual(expected, "Alert!!!!  New Course was created but UserID is not valid.");

            mfc.ClearLists();
            mfc.mockUserRepo.Object.Insert(_user0);
            _course0.User = _user0;
            _course0.UserId = _user0.Id;
            mfc.mockCourseRepo.Object.Insert(_course0);
            expected = _courseCUDService.CopyCourseReturnMessage(_course0.Id, null, _course0.UserId, _course0.TermId);
            Assert.AreEqual(expected, "Course: " + _course0.Name + " for " + _course0.User.DisplayName + 
                                      " was successfully created.");

            mfc.ClearLists();
            mfc.mockUserRepo.Object.Insert(_user0);
            mfc.mockCourseRepo.Object.Insert(_course0);
            expected = _courseCUDService.CopyCourseReturnMessage(Guid.Empty, null, _course0.UserId, _course0.TermId);
            Assert.AreEqual(expected, "Alert!!!.  There were problems creating Course.");
        }

        [Test]
        public void testDeleteCourse()
        {
            List<Lesson> lessonList = new List<Lesson>()
            {
                _lesson0,
                _lesson1,
                _lesson2
            };

            mfc.mockLessonRepo.Object.Insert(_lesson0);
            mfc.mockLessonRepo.Object.Insert(_lesson1);
            mfc.mockLessonRepo.Object.Insert(_lesson2);
            _course0.Lessons = lessonList;
            mfc.mockCourseRepo.Object.Insert(_course0);
            _course0.MasterCourseChildren = new List<Course>()
            {
                new Course()
            };

            string expected = _courseCUDService.DeleteCourse(_course0.Id);
            Assert.AreEqual(expected, "  Course has Master Child Courses.  ");

            _course0.MasterCourseChildren = new List<Course>();
            expected = _courseCUDService.DeleteCourse(_course0.Id);
            Assert.IsFalse(mfc.repoCourseList.Contains(_course0));
            Assert.AreEqual(expected, "Course has been deleted.");

            expected = _courseCUDService.DeleteCourse(Guid.Empty);
            Assert.AreEqual(expected, "Course with that Id does not exist.");
        }

        [Test]
        public void testSetLastDisplayedLessonId()
        {
            mfc.ClearLists();
            _lesson0.Course = _course0;
            mfc.mockLessonRepo.Object.Insert(_lesson0);
            mfc.mockCourseRepo.Object.Insert(_course0);

            _courseCUDService.SetLastDisplayedLessonId(_lesson0.Id);

            Course expected = mfc.mockCourseRepo.Object.GetById(_course0.Id);
            Assert.AreEqual(expected.LastDisplayedLessonId, _lesson0.Id);
        }

        [Test]
        public void testSetCourseDocumentsBoolean()
        {
            mfc.mockCourseRepo.Object.Insert(_course0);
            mfc.mockLessonRepo.Object.Insert(_lesson0);

            _courseCUDService.SetCourseDocumentsBoolean(_course0);

            Assert.IsFalse(_course0.HasOutForEditDocuments);

            _lesson0.IsActive = true;
            mfc.mockLessonRepo.Object.Insert(_lesson0);

            _courseCUDService.SetCourseDocumentsBoolean(_course0);

            Assert.IsTrue(_course0.HasOutForEditDocuments);

            _lesson0.HasOutForEditDocuments = false;
            _course0.HasOutForEditDocuments = false; 
            mfc.mockLessonRepo.Object.Insert(_lesson0);

            _courseCUDService.SetCourseDocumentsBoolean(_course0);

            Assert.IsFalse(_course0.HasOutForEditDocuments);
        }

        [Test]
        public void testCreateMasterCourseReturnIdAndMessage()
        {
            mfc.ClearLists();
            var expected = _courseCUDService.CreateMasterCourseReturnIdAndMessage("Empty Working Group Id", Guid.Empty,
                                                                                  Guid.NewGuid(), Guid.NewGuid());

            Assert.AreEqual(expected.Item1, Guid.Empty);
            Assert.AreEqual(expected.Item2, "Some values were missing");
            
            Guid workingGroupId = Guid.NewGuid();
            Guid termId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            expected = _courseCUDService.CreateMasterCourseReturnIdAndMessage("SomeName", workingGroupId, userId, 
                                                                              termId);

            Assert.NotNull(mfc.repoCoursePreferenceList.Count > 0);
            Assert.AreEqual(mfc.repoCoursePreferenceList.ElementAt(0).Name, "SomeName_Preferences");
            Assert.IsTrue(mfc.repoMetaCourseList.Count > 0);
            Assert.AreEqual(mfc.repoMetaCourseList.ElementAt(0).Name, "SomeName");
            Assert.AreEqual(mfc.repoMetaCourseList.ElementAt(0).WorkingGroupId, workingGroupId);
            Assert.IsTrue(mfc.repoCourseList.Count > 0);
            Assert.AreEqual(mfc.repoCourseList.ElementAt(0).Name, "SomeName");
            Assert.AreEqual(mfc.repoCourseList.ElementAt(0).WorkingGroupId, workingGroupId);
            Assert.IsFalse(mfc.repoCourseList.ElementAt(0).MetaCourseId == Guid.Empty);
            Assert.AreEqual(mfc.repoCourseList.ElementAt(0).TermId, termId);
            Assert.AreEqual(mfc.repoCourseList.ElementAt(0).UserId, userId);
            Assert.NotNull(mfc.repoCourseList.ElementAt(0).DateCreated);
            Assert.NotNull(mfc.repoCourseList.ElementAt(0).DateModified);
            Assert.IsTrue(mfc.repoCourseList.ElementAt(0).IsActive);
            Assert.IsTrue(mfc.repoCourseList.ElementAt(0).IsMaster);
            Assert.IsFalse(expected.Item1 == Guid.Empty);
            Assert.AreEqual(expected.Item2, "Course successfully created.");
        }
    }
}

using NUnit.Framework;
using Moq;
using CWMasterTeacherService.ViewObjectBuilder;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using System.Collections.Generic;

namespace CWTesting.Tests.CWMasterTeacherService.RetrieveServices
{
//    [TestFixture]
//    class ClassMeetingServiceTest
//    {
//        private static int _testId = 1;
//        private static int currentInstitutionId = _testId;
//        private static int selectedUserId = _testId;
//        private static int selectedTermId = _testId;
//        private static int selectedClassSectionId = _testId;
//        private static int selectedCourseId = _testId;
//        private static int selectedClassMeetingId = _testId;
        
//        private static ClassMeetingAdminViewObjBuilder _classMeetingService;
//        private static string _testName = "test name";
//        private static string _testFirstName = "testFirstName";
//        private static string _testLastName = "testLastName";
//        private static ClassMeetingDomainObjBasic _classMeetingDomainObjectBasic = new ClassMeetingDomainObjBasic();
//        private static ClassMeetingDomainObj _classMeetingDomainObj;
//        private static UserDomainObj _userDomainObj = new UserDomainObj(_testId, _testName, _testFirstName, _testLastName );
//        private static UserDomainObjBasic _userDomainObjBasic = new UserDomainObjBasic(_testId, _testName, _testFirstName, _testLastName);
//        private static TermDomainObj _termDomainObj = new TermDomainObj(new TermDomainObjBasic(_testId, _testName));
//        private static CourseDomainObj _courseDomainObj = new CourseDomainObj();
//        private static CourseDomainObjBasic _courseDomainObjBasic = new CourseDomainObjBasic();
//        private static ClassSectionDomainObjBasic _classSectionDomainObjBasic = new ClassSectionDomainObjBasic();

//        [SetUp]
//        public void setup()
//        {
//            _classMeetingDomainObjectBasic.Id = _testId;
//            _classMeetingDomainObj = new ClassMeetingDomainObj(_classMeetingDomainObjectBasic);
//            _classSectionDomainObjBasic.Id = _testId;
//            _classSectionDomainObjBasic.Name = _testName;

//            Mock<IClassMeetingDomainObjBuilder> MockClassMeetingDomainObjectBuilder = new Mock<IClassMeetingDomainObjBuilder>();
//            MockClassMeetingDomainObjectBuilder.Setup(x => x.GetClassMeetingsForClassSection(It.IsAny<int>()))
//                .Returns(new List<ClassMeetingDomainObj>() { _classMeetingDomainObj });
//            MockClassMeetingDomainObjectBuilder.Setup(x => x.BuildFromId(It.IsAny<int>())).Returns(_classMeetingDomainObj);

//            Mock<IUserDomainObjBuilder> MockUserDomainObjectBuilder = new Mock<IUserDomainObjBuilder>();
//            MockUserDomainObjectBuilder.Setup(x => x.GetUsersForInstitutionList(It.IsAny<int>()))
//                .Returns(new List<UserDomainObjBasic>() { _userDomainObjBasic });

//            Mock<ITermDomainObjBuilder> MockTermDomainObjectBuilder = new Mock<ITermDomainObjBuilder>();
//            MockTermDomainObjectBuilder.Setup(x => x.GetTermsForInstitutionList(It.IsAny<int>()))
//                .Returns(new List<TermDomainObj>() { _termDomainObj });

//            Mock<ICourseDomainObjBuilder> MockCourseDomainObjectBuilder = new Mock<ICourseDomainObjBuilder>();
//            MockCourseDomainObjectBuilder.Setup(x => x.GetCoursesForTermAndUserList(It.IsAny<int>(), It.IsAny<int>()))
//                .Returns(new List<CourseDomainObjBasic>() { _courseDomainObjBasic});
//            MockCourseDomainObjectBuilder.Setup(x => x.GetIndividualCoursesForUserList(It.IsAny<int>()))
//                .Returns(new List<CourseDomainObjBasic>() { _courseDomainObjBasic });
//            MockCourseDomainObjectBuilder.Setup(x => x.GetIndividualCoursesForTermList(It.IsAny<int>()))
//                .Returns(new List<CourseDomainObjBasic>() { _courseDomainObjBasic });
//            MockCourseDomainObjectBuilder.Setup(x => x.GetCoursesAllForInstitutionList(It.IsAny<int>()))
//                .Returns(new List<CourseDomainObjBasic>() { _courseDomainObjBasic });

//            Mock<IClassSectionDomainObjBuilder> MockClassSectionDomainObjectBuilder = new Mock<IClassSectionDomainObjBuilder>();
//            MockClassSectionDomainObjectBuilder.Setup(x => x.GetClassSectionsForCourseList(It.IsAny<int>()))
//                .Returns(new List<ClassSectionDomainObjBasic>() { _classSectionDomainObjBasic });
//            MockClassSectionDomainObjectBuilder.Setup(x => x.BuildBasicFromId(It.IsAny<int>())).Returns(_classSectionDomainObjBasic);
            
//            _classMeetingService = new ClassMeetingAdminViewObjBuilder(MockClassMeetingDomainObjectBuilder.Object, MockUserDomainObjectBuilder.Object,
//                MockTermDomainObjectBuilder.Object, MockCourseDomainObjectBuilder.Object, MockClassSectionDomainObjectBuilder.Object);
//        }

//        //[Test]
//        //[Sequential]
//        //[Author("Ivan Gajic")]
//        //public void ClassMeetingService_ConstructorTest()
//        //{
//        //    Assert.That(null != _classMeetingService);
//        //}

//        //[Test]
//        //[Sequential]
//        //[Author("Ivan Gajic")]
//        //public void buildClassMeetingViewObj_Test()
//        //{
//        //    Assert.That(null != _classMeetingService.buildClassMeetingViewObj
//        //        (currentInstitutionId, selectedUserId, selectedTermId, selectedClassSectionId, selectedCourseId, selectedClassMeetingId));
//        //}

//        //[Test]
//        //[Sequential]
//        //[Author("Kyle L Frisbie")]
//        //public void ClassMeetingService_buildClasMeetingViewObj_existingUser_existingTerm_Test()
//        //{
//        //    selectedUserId = 1;
//        //    selectedTermId = 1;
//        //    ClassMeetingAdminViewObj viewObj = _classMeetingService.buildClassMeetingViewObj(
//        //        currentInstitutionId, selectedUserId, selectedTermId, selectedClassSectionId, selectedCourseId, selectedClassMeetingId);
//        //    Assert.That(null != viewObj);
//        //}

//        //[Test]
//        //[Sequential]
//        //[Author("Kyle L Frisbie")]
//        //public void ClassMeetingService_buildClasMeetingViewObj_existingUser_nonexistingTerm_Test()
//        //{
//        //    selectedUserId = 1;
//        //    selectedTermId = -1;
//        //    ClassMeetingAdminViewObj viewObj = _classMeetingService.buildClassMeetingViewObj(
//        //        currentInstitutionId, selectedUserId, selectedTermId, selectedClassSectionId, selectedCourseId, selectedClassMeetingId);
//        //    Assert.That(null != viewObj);

//        //    selectedTermId = 1;
//        //}

//        //[Test]
//        //[Sequential]
//        //[Author("Kyle L Frisbie")]
//        //public void ClassMeetingService_buildClasMeetingViewObj_nonexistingUser_existingTerm_Test()
//        //{
//        //    selectedUserId = -1;
//        //    selectedTermId = 1;
//        //    ClassMeetingAdminViewObj viewObj = _classMeetingService.buildClassMeetingViewObj(
//        //        currentInstitutionId, selectedUserId, selectedTermId, selectedClassSectionId, selectedCourseId, selectedClassMeetingId);
//        //    Assert.That(null != viewObj);

//        //    selectedUserId = 1;
//        //}

//        //[Test]
//        //[Sequential]
//        //[Author("Kyle L Frisbie")]
//        //public void ClassMeetingService_buildClasMeetingViewObj_nonexistinguser_nonexistingTerm_Test()
//        //{
//        //    selectedUserId = -1;
//        //    selectedTermId = -1;
//        //    ClassMeetingAdminViewObj viewObj = _classMeetingService.buildClassMeetingViewObj(
//        //        currentInstitutionId, selectedUserId, selectedTermId, selectedClassSectionId, selectedCourseId, selectedClassMeetingId);
//        //    Assert.That(null != viewObj);

//        //    selectedUserId = 1;
//        //    selectedTermId = 1;
//        //}

//        //[Test]
//        //[Sequential]
//        //[Author("Kyle L Frisbie")]
//        //public void ClassMeetingService_buildClassMeetingViewObj_existingSection_Test()
//        //{
//        //    selectedClassSectionId = 1;
//        //    ClassMeetingAdminViewObj viewObj = _classMeetingService.buildClassMeetingViewObj(
//        //        currentInstitutionId, selectedUserId, selectedTermId, selectedClassSectionId, selectedCourseId, selectedClassMeetingId);
//        //    Assert.That(null != viewObj);
//        //}

//        //[Test]
//        //[Sequential]
//        //[Author("Kyle L Frisbie")]
//        //public void ClassMeetingService_buildClassMeetingViewObj_nonexistingSection_Test()
//        //{
//        //    selectedClassSectionId = -1;
//        //    ClassMeetingAdminViewObj viewObj = _classMeetingService.buildClassMeetingViewObj(
//        //        currentInstitutionId, selectedUserId, selectedTermId, selectedClassSectionId, selectedCourseId, selectedClassMeetingId);
//        //    Assert.That(null != viewObj);
//        //    selectedClassSectionId = 1;
//        //}

//        //[Test]
//        //[Sequential]
//        //[Author("Kyle L Frisbie")]
//        //public void ClassMeetingService_buildClassMeetingViewObj_existingCourse_Test()
//        //{
//        //    selectedCourseId = 1;
//        //    ClassMeetingAdminViewObj viewObj = _classMeetingService.buildClassMeetingViewObj(
//        //        currentInstitutionId, selectedUserId, selectedTermId, selectedClassSectionId, selectedCourseId, selectedClassMeetingId);
//        //    Assert.That(null != viewObj);
//        //}

//        //[Test]
//        //[Sequential]
//        //[Author("Kyle L Frisbie")]
//        //public void ClassMeetingService_buildClassMeetingViewObj_nonexistingCourse_Test()
//        //{

//        //    selectedCourseId = -1;
//        //    ClassMeetingAdminViewObj viewObj = _classMeetingService.buildClassMeetingViewObj(
//        //        currentInstitutionId, selectedUserId, selectedTermId, selectedClassSectionId, selectedCourseId, selectedClassMeetingId);
//        //    Assert.That(null != viewObj);
//        //    selectedCourseId = 1;
//        //}

//        //[Test]
//        //[Sequential]
//        //[Author("Kyle L Frisbie")]
//        //public void ClassMeetingService_buildClassMeetingViewObj_existingClassMeeting_Test()
//        //{

//        //    selectedClassMeetingId = 1;
//        //    ClassMeetingAdminViewObj viewObj = _classMeetingService.buildClassMeetingViewObj(
//        //        currentInstitutionId, selectedUserId, selectedTermId, selectedClassSectionId, selectedCourseId, selectedClassMeetingId);
//        //    Assert.That(null != viewObj);
//        //}

//        //[Test]
//        //[Sequential]
//        //[Author("Kyle L Frisbie")]
//        //public void ClassMeetingService_buildClassMeetingViewObj_existingClassMeeting_hasComment_Test()
//        //{
//        //    selectedClassMeetingId = 1;
//        //    _classMeetingDomainObj.Comment = "comment";
//        //    ClassMeetingAdminViewObj viewObj = _classMeetingService.buildClassMeetingViewObj(
//        //        currentInstitutionId, selectedUserId, selectedTermId, selectedClassSectionId, selectedCourseId, selectedClassMeetingId);
//        //    Assert.That(null != viewObj);
//        //    Assert.That(_classMeetingDomainObj.Comment == viewObj.NoClassComment);
//        //}

//        //[Test]
//        //[Sequential]
//        //[Author("Kyle L Frisbie")]
//        //public void ClassMeetingService_buildClassMeetingViewObj_existingClassMeeting_hasNoComment_Test()
//        //{

//        //    selectedClassMeetingId = 1;
//        //    _classMeetingDomainObj.Comment = null;
//        //    ClassMeetingAdminViewObj viewObj = _classMeetingService.buildClassMeetingViewObj(
//        //        currentInstitutionId, selectedUserId, selectedTermId, selectedClassSectionId, selectedCourseId, selectedClassMeetingId);
//        //    Assert.That(null != viewObj);
//        //    Assert.That("NA" == viewObj.NoClassComment);
//        //}

//        //[Test]
//        //[Sequential]
//        //[Author("Kyle L Frisbie")]
//        //public void ClassMeetingService_buildClassMeetingViewObj_nonexistingClassMeeting_Test()
//        //{

//        //    selectedClassMeetingId = -1;
//        //    ClassMeetingAdminViewObj viewObj = _classMeetingService.buildClassMeetingViewObj(
//        //        currentInstitutionId, selectedUserId, selectedTermId, selectedClassSectionId, selectedCourseId, selectedClassMeetingId);
//        //    Assert.That(null != viewObj);
//        //    selectedClassMeetingId = 1;
//        //}
//    }
}

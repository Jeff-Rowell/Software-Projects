using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherService.ViewObjectBuilder;
using CWMasterTeacherDomain.ViewObjects;

namespace CWTesting.Tests.CWMasterTeacherService.RetrieveServices
{
//    [Author("Nick Beier")]
//    [TestFixture]
//    class CourseCreateServiceTest
//    {
//        private static WorkingGroupDomainObjBasic testworkingGroupBasic = new WorkingGroupDomainObjBasic(0, "test");
//        private static WorkingGroupDomainObj testWorkingGroupDomainObj = new WorkingGroupDomainObj(testworkingGroupBasic);
//        private static List<WorkingGroupDomainObjBasic> workingGroupBasicList = new List<WorkingGroupDomainObjBasic>() { testworkingGroupBasic };

//        private static List<CourseDomainObjBasic> testCourseDomainObjList = new List<CourseDomainObjBasic>();

//        private static UserDomainObj testUserDomainObj = new UserDomainObj(0, "test", "testFirstName", "TestLastName");
//        private static UserDomainObjBasic testUserDomainObjBasic = new UserDomainObjBasic(0, "test", "testFirstName", "TestLastName");
//        private static List<UserDomainObjBasic> testUserDomainObjList = new List<UserDomainObjBasic>() { testUserDomainObjBasic };

//        private static TermDomainObj testTermDomainObj = new TermDomainObj();
//        private static List<TermDomainObj> testTermDomainObjList = new List<TermDomainObj>() { testTermDomainObj };


//        private CourseCreateAdminViewObjBuilder testService;

//        [OneTimeSetUp]
//        public void Setup()
//        {
//            Mock<IWorkingGroupDomainObjBuilder> workingGroupBuilderMock = new Mock<IWorkingGroupDomainObjBuilder>();
//            Mock<ICourseDomainObjBuilder> courseBuilderMock = new Mock<ICourseDomainObjBuilder>();
//            Mock<IUserDomainObjBuilder> userBuilderMock = new Mock<IUserDomainObjBuilder>();
//            Mock<ITermDomainObjBuilder> termBuilderMock = new Mock<ITermDomainObjBuilder>();

//            workingGroupBuilderMock.Setup(x => x.GenWorkingGroupBasicList()).Returns(workingGroupBasicList);
//            workingGroupBuilderMock.Setup(x => x.BuildFromId(It.IsAny<int>())).Returns(testWorkingGroupDomainObj);


//            //Set up the courseList 
//            DateTime startDateTerm1 = new DateTime(2015, 1, 1);
//            DateTime endDateTerm1 = new DateTime(2015, 5, 1);
//            CourseDomainObj testCourseDomainObj = new CourseDomainObj();
//            CourseDomainObjBasic testCourseDomainObjBasic = new CourseDomainObjBasic(courseId: 0,
//                                                                                       name: "Course1",
//                                                                                       isMaster: false,
//                                                                                       termName: "Term1",
//                                                                                       userDisplayName: "UserName",
//                                                                                       termId: 0,
//                                                                                       hasActiveMessages: false,
//                                                                                       hasActiveStoredMessages: false,
//                                                                                       hasNewMessages: false,
//                                                                                       hasStoredMessages: false,
//                                                                                       hasImportantMessages: false,
//                                                                                       hasOutForEditDocuments: false,
//                                                                                       termEndDate: new DateTime(2016, 01, 01));

//            testCourseDomainObjBasic.Name = "Course1";
//            testCourseDomainObj.CourseDomainObjBasic = testCourseDomainObjBasic;
//            testCourseDomainObjList.Add(testCourseDomainObjBasic);

//            courseBuilderMock.Setup(x => x.GetCoursesForTermInstitutionList(It.IsAny<int>(), It.IsAny<int>())).Returns(testCourseDomainObjList);
//            courseBuilderMock.Setup(x => x.GetMasterCourseForInstitutionList(It.IsAny<int>())).Returns(testCourseDomainObjList);
//            courseBuilderMock.Setup(x => x.GetCoursesAllForInstitutionList(It.IsAny<int>())).Returns(testCourseDomainObjList);
//            courseBuilderMock.Setup(x => x.BuildFromId(It.IsAny<int>())).Returns(testCourseDomainObj);


//            userBuilderMock.Setup(x => x.GetUsersForInstitutionList(It.IsAny<int>())).Returns(testUserDomainObjList);
//            termBuilderMock.Setup(x => x.GetTermsForInstitutionList(It.IsAny<int>())).Returns(testTermDomainObjList);


//            testService = new CourseCreateAdminViewObjBuilder(workingGroupBuilderMock.Object, courseBuilderMock.Object, userBuilderMock.Object, termBuilderMock.Object);
//        }

//        //Parameters to Retrieve method
//        //passthroughs
//        private int userId = 0;
//        private String message = "Test message";
//        private bool showDeleteCourse = true;
//        private bool isCourseDeletable = true;
//        private bool showRenameInstitution = true;
//        private String narrativeMessage = "Test narative";


//        //[TestCase(0,0,0,true,true,true)]
//        //[TestCase(1,1,1,true,true,true)]
//        //[TestCase(1,1,0,true,true,true)]
//        //[TestCase(1,1,1,false,false,false)]
//        //[TestCase(1,1,0,false,false,false)]
//        //[Author("Nick Beier")]
//        //public void CourseCreateService_RetrieveTest(int courseId, int institutionId, int termId, bool showNewInstitution, bool showDeleteInstitution, bool showAllCourses)
//        //{
//        //    CourseCreateAdminViewObj testViewObj = testService.Retrieve(courseId, institutionId, userId, termId, message, showDeleteCourse,
//        //                                                           showNewInstitution, showDeleteInstitution, showAllCourses, isCourseDeletable,
//        //                                                           showRenameInstitution, narrativeMessage);

//        //    //Not currently testing beyond object creation.
//        //    //Since everything to test would be dependent on the CourseCreateViewObj methods
//        //    //this really seems like more of an integration test.
//        //    Assert.That(testViewObj, Is.Not.Null);
//        //}
//    }
}

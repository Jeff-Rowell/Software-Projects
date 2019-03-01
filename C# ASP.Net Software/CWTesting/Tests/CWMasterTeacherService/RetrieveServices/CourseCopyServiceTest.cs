using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.ViewObjectBuilder;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace CWTesting.Tests.CWMasterTeacherService.RetrieveServices
{
    //[TestFixture]
    //class CourseCopyServiceTest
    //{
    //    private IEnumerable<SelectListItem> testMasterCourseForInstitutionList = null;
    //    private IEnumerable<SelectListItem> testCoursesAllForInstitutionList = null;
    //    private IEnumerable<SelectListItem> testUsersForInstitutionList = null;
    //    private IEnumerable<SelectListItem> testTermsForInstitutionList = null;
    //    CourseDomainObj testCourseDomainObj;
    //    CourseDomainObjBasic testCourseDomainObjBasic;
    //    UserDomainObj testUserDomainObj;
    //    UserDomainObjBasic testUserDomainObjBasic;
    //    TermDomainObj testTermDomainObj;
    //    private CourseCopyAdminViewObjBuilder courseCopyTestService;
    //    private CourseCopyAdminViewObj testCourseCopyViewObj;

    //    [OneTimeSetUp]
    //    public void SetUp()
    //    {

    //        //Mocking CourseDomainObjBuilder
    //        testCourseDomainObjBasic = new CourseDomainObjBasic(courseId: 11001,
    //                                                               name: "TestCourse",
    //                                                               isMaster: false,
    //                                                               termName: "This Term",
    //                                                               userDisplayName: "User",
    //                                                               termId: 0,
    //                                                               hasActiveMessages: false,
    //                                                               hasActiveStoredMessages: false,
    //                                                               hasNewMessages: false,
    //                                                               hasStoredMessages: false,
    //                                                               hasImportantMessages: false,
    //                                                               hasOutForEditDocuments: false,
    //                                                               termEndDate: new DateTime(2016, 01, 01));

    //        testCourseDomainObj = new CourseDomainObj { CourseDomainObjBasic = testCourseDomainObjBasic };

    //        Mock<ICourseDomainObjBuilder> MockCourseDomainObjBuilder = new Mock<ICourseDomainObjBuilder>();
    //        MockCourseDomainObjBuilder.Setup(x => x.GetMasterCourseForInstitutionList(It.IsAny<int>())).Returns(new List<CourseDomainObjBasic>() { testCourseDomainObjBasic });
    //        MockCourseDomainObjBuilder.Setup(x => x.GetCoursesAllForInstitutionList(It.IsAny<int>())).Returns(new List<CourseDomainObjBasic>() { testCourseDomainObjBasic });
    //        MockCourseDomainObjBuilder.Setup(x => x.BuildFromId(It.IsAny<int>())).Returns(testCourseDomainObj);
    //        ICourseDomainObjBuilder mockCourseDomainObjBuilder = MockCourseDomainObjBuilder.Object;

    //        //Mocking UserDomainObjBuilder
    //        testUserDomainObj = new UserDomainObj { BasicObj = new UserDomainObjBasic(880, "TestUser", "TestUserFirstName", "TestUserLastName") };

    //        Mock<IUserDomainObjBuilder> MockUserDomainObjBuilder = new Mock<IUserDomainObjBuilder>();
    //        MockUserDomainObjBuilder.Setup(x => x.GetUsersForInstitutionList(It.IsAny<int>())).Returns(new List<UserDomainObjBasic>() { testUserDomainObjBasic});
    //        MockUserDomainObjBuilder.Setup(x => x.BuildFromId(It.IsAny<int>())).Returns(testUserDomainObj);
    //        IUserDomainObjBuilder mockUserDomainObjBuilder = MockUserDomainObjBuilder.Object;

    //        //Mocking TermDomainObjBuilder
    //        testTermDomainObj = new TermDomainObj(new TermDomainObjBasic(2016, "TestTerm"));

    //        Mock<ITermDomainObjBuilder> MockTermDomainObjBuilder = new Mock<ITermDomainObjBuilder>();
    //        MockTermDomainObjBuilder.Setup(x => x.GetTermsForInstitutionList(It.IsAny<int>())).Returns(new List<TermDomainObj>() { testTermDomainObj });
    //        MockTermDomainObjBuilder.Setup(x => x.BuildFromId(It.IsAny<int>())).Returns(testTermDomainObj);
    //        ITermDomainObjBuilder mockTermDomainObjBuilder = MockTermDomainObjBuilder.Object;

    //        //Create test CourseCopyViewObj
    //        testCourseCopyViewObj = new CourseCopyAdminViewObj(false, "Success Message!", true, testMasterCourseForInstitutionList, 
    //                                                        testCoursesAllForInstitutionList, testUsersForInstitutionList,
    //                                                            testTermsForInstitutionList);

    //        courseCopyTestService = new CourseCopyAdminViewObjBuilder(mockCourseDomainObjBuilder, mockUserDomainObjBuilder, mockTermDomainObjBuilder);
    //}

    //    [Test]
    //    [Author("Stefany Segovia")]
    //    public void Retrieve_CourseCopyViewObj_MasterFalse()
    //    {
    //        CourseCopyAdminViewObj courseCopyViewObj = courseCopyTestService.Retrieve(false, "Success Message!", true, 1010101);

    //        Assert.AreEqual(courseCopyViewObj.IsMaster, false);
    //        Assert.AreEqual(courseCopyViewObj.SuccessMessage, "Success Message!");
    //        Assert.AreEqual(courseCopyViewObj.UserIsAdmin, true);
    //        Assert.That(courseCopyViewObj.PossibleMasterCourses.Count(item => item.Value == string.Empty), Is.EqualTo(0));
    //        Assert.That(courseCopyViewObj.PossibleFromCourses.Count(item => item.Value == string.Empty), Is.EqualTo(0));
    //        Assert.That(courseCopyViewObj.PossibleToUsers.Count(item => item.Value == string.Empty), Is.EqualTo(0));
    //        Assert.That(courseCopyViewObj.PossibleFromTerms.Count(item => item.Value == string.Empty), Is.EqualTo(0));
    //        Assert.That(courseCopyViewObj.PossibleToTerms.Count(item => item.Value == string.Empty), Is.EqualTo(0));
    //    }

    //    [Test]
    //    [Author("Stefany Segovia")]
    //    public void Retrieve_CourseCopyViewObj_MasterTrue()
    //    {
    //        CourseCopyAdminViewObj courseCopyViewObj = courseCopyTestService.Retrieve(true, "Success Message!", true, 1010101);

    //        Assert.AreEqual(courseCopyViewObj.IsMaster, true);
    //        Assert.AreEqual(courseCopyViewObj.SuccessMessage, "Success Message!");
    //        Assert.AreEqual(courseCopyViewObj.UserIsAdmin, true);
    //        Assert.That(courseCopyViewObj.PossibleMasterCourses.Count(item => item.Value == string.Empty), Is.EqualTo(0));
    //        Assert.That(courseCopyViewObj.PossibleFromCourses.Count(item => item.Value == string.Empty), Is.EqualTo(0));
    //        Assert.That(courseCopyViewObj.PossibleToUsers.Count(item => item.Value == string.Empty), Is.EqualTo(0));
    //        Assert.That(courseCopyViewObj.PossibleFromTerms.Count(item => item.Value == string.Empty), Is.EqualTo(0));
    //        Assert.That(courseCopyViewObj.PossibleToTerms.Count(item => item.Value == string.Empty), Is.EqualTo(0));
    //    }
    //}
}

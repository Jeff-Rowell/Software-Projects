using NUnit.Framework;
using Moq;
using CWMasterTeacherService.ViewObjectBuilder;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using System.Collections.Generic;

namespace CWTesting.Tests.CWMasterTeacherService.RetrieveServices
{
    [TestFixture]
    class UserServiceTest
    {
        //private UserAdminViewObjBuilder _userService;

        //private static int _userId = 1;
        //private static int _workingGroupId = 2;
        //private static string _currentUserName = "Current User Name";
        //private static string _userName = "User Name";
        //private static string _firstName = "FirstName";
        //private static string _lastName = "LastName";
        //private static UserDomainObjBasic _sampleUserDomainObjBasic = new UserDomainObjBasic(_userId, _userName, _firstName, _lastName);
        //private static UserDomainObj _sampleUserDomainObj = new UserDomainObj(_userId, _userName, _firstName, _lastName);
        //private static WorkingGroupDomainObjBasic _sampleWorkingGroupObjBasic = new WorkingGroupDomainObjBasic(_userId, _firstName);

        //[SetUp]
        //public void SetUp()
        //{
        //    Mock<IUserDomainObjBuilder> mockUserDomainObjBuilder = new Mock<IUserDomainObjBuilder>();
        //    mockUserDomainObjBuilder.Setup(x => x.GetUsersForInstitutionList(It.IsAny<int>()))
        //        .Returns(new List<UserDomainObjBasic>() { _sampleUserDomainObjBasic });

        //    mockUserDomainObjBuilder.Setup(x => x.BuildFromId(It.IsAny<int>())).
        //        Returns(_sampleUserDomainObj);

        //    Mock<IWorkingGroupDomainObjBuilder> mockWorkingGroupDomainObjBuilder = new Mock<IWorkingGroupDomainObjBuilder>();
        //    mockWorkingGroupDomainObjBuilder.Setup(x => x.GenWorkingGroupBasicList())
        //        .Returns(new List<WorkingGroupDomainObjBasic>() { _sampleWorkingGroupObjBasic });
        //    mockWorkingGroupDomainObjBuilder.Setup(x => x.BuildBasicFromId(It.IsAny<int>()))
        //        .Returns(_sampleWorkingGroupObjBasic);

        //    _userService = new UserAdminViewObjBuilder(mockUserDomainObjBuilder.Object, mockWorkingGroupDomainObjBuilder.Object);
        //}

        //[Test]
        //[Sequential]
        //[Author("Kyle L Frisbie")]
        //public void UserService_ConstructorTest()
        //{
        //    Assert.That(null != _userService);
        //}

        ////[Test]
        ////[Sequential]
        ////[Author("Kyle L Frisbie")]
        ////public void UserService_BuildUserViewObject_VaildUser_Test()
        ////{
        ////    UserAdminViewObj userViewObj = _userService.RetrieveUserAdminViewObject(_userId, _workingGroupId, _currentUserName );
        ////    Assert.That(null != userViewObj);
        ////    Assert.That(_userId == userViewObj.UserObj.Id);
        ////    Assert.That(_userName == userViewObj.UserObj.BasicObj.UserName);
        ////    Assert.That((_firstName + " " + _lastName) == userViewObj.UserObj.BasicObj.FullName);
        ////}

        ////[Test]
        ////[Sequential]
        ////[Author("Kyle L Frisbie")]
        ////public void UserService_BuildUserViewObject_InvaildUser_Test()
        ////{
        ////    int invalidUserId = -1;
        ////    string emptyUserName = "";
        ////    UserAdminViewObj userViewObj = _userService.RetrieveUserAdminViewObject(invalidUserId, _workingGroupId, _currentUserName);
        ////    Assert.That(null != userViewObj);
        ////    Assert.That(invalidUserId == userViewObj.UserObj.Id);
        ////    Assert.That(emptyUserName == userViewObj.UserObj.BasicObj.UserName);
        ////}
    }
}

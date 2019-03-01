using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDomain.DomainObjects;

namespace CWTesting.Tests.CWMasterTeacherDomain.ViewObjects
{
    //[TestFixture]
    //class UserViewObjTest
    //{
    //    UserAdminViewObj _userViewObj;
    //    private static int _userId = 1;
    //    private static string _userName = "User Name";
    //    private static string _firstName = "FirstName";
    //    private static string _lastName = "LastName";
    //    private static UserDomainObj _sampleUserDomainObj = new UserDomainObj(_userId, _userName, _firstName, _lastName);

    //    [SetUp]
    //    public void Setup()
    //    {
    //        _userViewObj = new UserAdminViewObj();
    //        _userViewObj.UserObj = _sampleUserDomainObj;
    //    }

    //    [Test]
    //    [Sequential]
    //    [Author("Kyle L Frisbie")]
    //    public void UserViewObj_Constructor_VaildUser_Test()
    //    {
    //        Assert.That(null != _userViewObj);
    //        Assert.That(null != _userViewObj.UserObj);
    //        Assert.That(_userId == _userViewObj.UserObj.BasicObj.Id);
    //        Assert.That(_userName == _userViewObj.UserObj.BasicObj.UserName);
    //        Assert.That("EditUser" == _userViewObj.ActionName);
    //        Assert.That("User" == _userViewObj.ControllerName);
    //    }

    //    [Test]
    //    [Sequential]
    //    [Author("Kyle L Frisbie")]
    //    public void UserViewObj_Constructor_InaildUser_Test()
    //    {
    //        int invalidUserId = -1;
    //        _userViewObj = new UserAdminViewObj();
    //        _userViewObj.UserObj = new UserDomainObj(invalidUserId, _userName, _firstName, _lastName);
    //        Assert.That(null != _userViewObj);
    //        Assert.That(null != _userViewObj.UserObj);
    //        Assert.That(invalidUserId == _userViewObj.UserObj.BasicObj.Id);
    //        Assert.That(_userName == _userViewObj.UserObj.BasicObj.UserName);
    //        Assert.That("RegisterCWUser" == _userViewObj.ActionName);
    //        Assert.That("Account" == _userViewObj.ControllerName);
    //    }

    //    [Test]
    //    [Sequential]
    //    [Author("Kyle L Frisbie")]
    //    public void UserViewObj_genSelectLists_Test()
    //    {
    //        Assert.That(null != _userViewObj);
    //        int workingGroupId = 1;
    //        string workingGroupName = "working group name";

    //        List<UserDomainObjBasic> userDomainObjBasicList = new List<UserDomainObjBasic>()
    //        {
    //            new UserDomainObjBasic(_userId, _userName, _firstName, _lastName)
    //        };
    //        List<WorkingGroupDomainObjBasic> workingGroupDomainObjBasicList = new List<WorkingGroupDomainObjBasic>
    //        {
    //            new WorkingGroupDomainObjBasic(workingGroupId, workingGroupName)
    //        };

    //        _userViewObj.genSelectLists(userDomainObjBasicList, workingGroupDomainObjBasicList);
    //        Assert.That(null != _userViewObj.WorkingGroupSelectList);
    //        Assert.That(null != _userViewObj.UserSelectList);
    //    }
    //}
}

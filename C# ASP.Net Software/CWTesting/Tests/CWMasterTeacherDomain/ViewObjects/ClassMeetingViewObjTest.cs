using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDomain.DomainObjects;
using System.Linq;

namespace CWTesting.Tests.CWMasterTeacherDomain.ViewObjects
{
    //[TestFixture]
    //class ClassMeetingViewObjTest
    //{
    //    ClassMeetingAdminViewObj _classMeetingViewObj;
    //    ClassMeetingAdminViewObj _emptyClassMeetingViewObj;
    //    private static ClassMeetingDomainObjBasic _mockClassMeetingDomainObjBasic =
    //        new ClassMeetingDomainObjBasic();
    //    private static ClassMeetingDomainObj _mockClassMeetingDomainObj =
    //        new ClassMeetingDomainObj(_mockClassMeetingDomainObjBasic);


    //    [SetUp]
    //    public void Setup()
    //    {
    //        _emptyClassMeetingViewObj = new ClassMeetingAdminViewObj();

    //        _classMeetingViewObj = new ClassMeetingAdminViewObj();
    //        _classMeetingViewObj.StartTimeLocal = "startTime";
    //        _classMeetingViewObj.EndTimeLocal = "endTime";
    //    }

    //    [Test]
    //    [Sequential]
    //    [Author("Patrick Flanagan")]
    //    public void ClassMeetingViewObj_Constructor_ValidClassMeeting_Test()
    //    {
    //        Assert.That(_emptyClassMeetingViewObj != null);
    //        Assert.That(_classMeetingViewObj != null);
    //    }

    //    [Test]
    //    [Sequential]
    //    [Author("Patrick Flanagan")]
    //    public void setStartAndEndTime_EmptyMeeting_Test()
    //    {
    //        Assert.That(_emptyClassMeetingViewObj.StartTimeLocal == null);
    //        Assert.That(_emptyClassMeetingViewObj.EndTimeLocal == null);
    //        _emptyClassMeetingViewObj.setStartAndEndTime(null);
    //        Assert.That(_emptyClassMeetingViewObj.StartTimeLocal != null);
    //        Assert.That(_emptyClassMeetingViewObj.EndTimeLocal != null);
    //    }

    //    [Test]
    //    [Sequential]
    //    [Author("Patrick Flanagan")]
    //    public void setStartAndEndTime_NonEmptyMeeting_Test()
    //    {
    //        Assert.That(_classMeetingViewObj.StartTimeLocal != null);
    //        Assert.That(_classMeetingViewObj.EndTimeLocal != null);

    //        string initialStart = _classMeetingViewObj.StartTimeLocal;
    //        string initialEnd = _classMeetingViewObj.EndTimeLocal;

    //        _classMeetingViewObj.setStartAndEndTime(_mockClassMeetingDomainObj);
            
    //        Assert.That(_classMeetingViewObj.StartTimeLocal != initialStart);
    //        Assert.That(_classMeetingViewObj.EndTimeLocal != initialEnd);
    //    }

    //    [Test]
    //    [Sequential]
    //    [Author("Mike Chambliss")]
    //    public void GenerateCourseSelectList_Test()
    //    {           
    //        List<CourseDomainObjBasic> courseList = new List<CourseDomainObjBasic>();
    //        SelectList courseSelectList = new SelectList(courseList, "Id", "DisplayName");

    //        _classMeetingViewObj.generateCourseSelectList(courseList);
    //        Assert.That(_classMeetingViewObj.CourseSelectList, Is.EqualTo(courseSelectList));
    //    }

    //    [Test]
    //    [Sequential]
    //    [Author("Mike Chambliss")]
    //    public void GenerateUserAndTermSelectList_Test()
    //    {
    //        List<UserDomainObjBasic> userList = new List<UserDomainObjBasic>();
    //        SelectList userSelectList = new SelectList(userList, "Id", "DisplayName");

    //        List<TermDomainObj> termList = new List<TermDomainObj>();
    //        SelectList termSelectList = new SelectList(termList, "Id", "Name");

    //        _classMeetingViewObj.genUserAndTermSelectLists(userList, termList);
    //        Assert.That(_classMeetingViewObj.UserSelectList, Is.EqualTo(userSelectList));
    //        Assert.That(_classMeetingViewObj.TermSelectList, Is.EqualTo(termSelectList));
    //    }

    //    [Test]
    //    [Sequential]
    //    [Author("Mike Chambliss")]
    //    public void GenerateClassMeetingSelectList_NullTest()
    //    {
    //        _classMeetingViewObj.genClassMeetingSelectList(null);
    //        Assert.That(_classMeetingViewObj.ClassMeetingSelectList, Is.EqualTo(Enumerable.Empty<SelectListItem>()));
    //    }

    //    [Test]
    //    [Sequential]
    //    [Author("Mike Chambliss")]
    //    public void GenerateClassMeetingSelectList_Test()
    //    {
    //        List<ClassMeetingDomainObj> classMeetingList = new List<ClassMeetingDomainObj>();
    //        SelectList classMeetingSelectList = new SelectList(classMeetingList, "Id", "Name");
            
    //        _classMeetingViewObj.genClassMeetingSelectList(classMeetingList);
    //        Assert.That(_classMeetingViewObj.ClassMeetingSelectList, Is.EqualTo(classMeetingSelectList));
    //    }

    //    [Test]
    //    [Sequential]
    //    [Author("Mike Chambliss")]
    //    public void GenerateClassMeetingSectionList_NullTest()
    //    {
    //        _classMeetingViewObj.genClassSectionSelectList(null);
    //        Assert.That(_classMeetingViewObj.ClassSectionSelectList, Is.EqualTo(Enumerable.Empty<SelectListItem>()));
    //    }

    //    [Test]
    //    [Sequential]
    //    [Author("Mike Chambliss")]
    //    public void GenerateClassSectionSelectList_Test()
    //    {
    //        List<ClassSectionDomainObjBasic> classSectionList = new List<ClassSectionDomainObjBasic>();
    //        SelectList classSectionSelectList = new SelectList(classSectionList, "Id", "Name");

    //        _classMeetingViewObj.genClassSectionSelectList(classSectionList);
    //        Assert.That(_classMeetingViewObj.ClassSectionSelectList, Is.EqualTo(classSectionSelectList));
    //    }

    //    [Test]
    //    [Author("Mike Chambliss")]
    //    public void getSetTests()
    //    {
    //        _emptyClassMeetingViewObj.SelectedClassSectionName = "someName";
    //        Assert.That(_emptyClassMeetingViewObj.SelectedClassSectionName, Is.EqualTo("someName"));
    //        _emptyClassMeetingViewObj.SelectedClassSectionId = 42;
    //        Assert.That(_emptyClassMeetingViewObj.SelectedClassSectionId, Is.EqualTo(42));
    //        _emptyClassMeetingViewObj.SelectedCourseId = 21;
    //        Assert.That(_emptyClassMeetingViewObj.SelectedCourseId, Is.EqualTo(21));
    //    }
    //}
}

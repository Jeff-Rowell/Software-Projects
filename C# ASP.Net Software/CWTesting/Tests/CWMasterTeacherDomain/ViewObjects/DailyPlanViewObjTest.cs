using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDomain.DomainObjects;
using System.Web.Mvc;
using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.ObjectBuilders;

namespace CWTesting.Tests.CWMasterTeacherDomain.ViewObjects
{
    [TestFixture]
    class DailyPlanViewObjTest
    {
        private ClassSectionDomainObjBasic _basicObj;
        private ClassSectionDomainObj _domainObj;
        private UserDomainObj _userObj;
        private DailyPlanViewObj _testViewObj;
        private LessonDomainObjBasic _testLessonBasicObj;
        private LessonDomainObj _testLessonDomainObj;
        
        [SetUp]
        protected void Setup()
        {
            _basicObj = new ClassSectionDomainObjBasic();
            _domainObj = new ClassSectionDomainObj(_basicObj);
            _userObj = new UserDomainObj();
            _testViewObj = new DailyPlanViewObj();
            _testLessonBasicObj = new LessonDomainObjBasic();
            _testLessonDomainObj = new LessonDomainObj(_testLessonBasicObj);
            _testLessonBasicObj.Id = Guid.NewGuid();
            _domainObj = null;
        }

        [Test]
        public void GetClassMeetingObjList()
        {
            _testViewObj.ClassSectionObj = _domainObj;
            var expected = new List<ClassMeetingDomainObj>();
            Assert.AreEqual(expected, _testViewObj.ClassMeetingObjList);
        }

        [Test]
        public void GetClassSectionSelectList()
        {
            User user = new User() { DisplayName = "some name" };
            Course course = new Course() { User = user };
            ClassSection classSection = new ClassSection() { Id = Guid.NewGuid(), Name = "some name", Course = course };
            List<ClassSectionDomainObjBasic> listClassSectionBasics = new List<ClassSectionDomainObjBasic>();
            listClassSectionBasics.Add(ClassSectionDomainObjBuilder.BuildBasic(classSection));
            _testViewObj.ClassSectionDomainObjBasicList = listClassSectionBasics;
            List<String> listString = new List<string>() { classSection.Id.ToString() };
            Assert.Contains(_testViewObj.ClassSectionSelectList.ElementAt(0).Value, listString);
        }
        
        [Test]
        public void GetSelectedClassSectionName()
        {
            _testViewObj.ClassSectionObj = _domainObj;
            var expected = "";
            Assert.AreEqual(expected, _testViewObj.SelectedClassSectionName);
        }
        
        [Test]
        public void GetSelectedLessonId()
        {
            LessonDomainObjBasic _basicObj = null;
            _testViewObj.SelectedLessonObjBasic = _basicObj;
            var expected = Guid.Empty;
            Assert.AreEqual(expected, _testViewObj.SelectedLessonId);
        }

        [Test]
        public void GetLessonCollapseLinkText()
        {
            LessonDomainObjBasic _basicObj = null;
            _testViewObj.SelectedLessonObjBasic = _basicObj;
            var expected = "Expand Lesson";
            Assert.AreEqual(expected, _testViewObj.LessonCollapseLinkText);
            _basicObj = _testLessonBasicObj;
            _testViewObj.SelectedLessonObjBasic = _basicObj;
            _basicObj.IsCollapsed = true;
            Assert.AreEqual(expected, _testViewObj.LessonCollapseLinkText);
            expected = "Collapse Lesson";
            _basicObj.IsCollapsed = false;
            Assert.AreEqual(expected, _testViewObj.LessonCollapseLinkText);
        }

        [Test]
        public void GetShowLessonCollapseLink()
        {
            LessonDomainObjBasic _basicObj = _testLessonBasicObj;
            _testViewObj.SelectedLessonObjBasic = _basicObj;
            var expected = true;
            Assert.AreEqual(expected, _testViewObj.ShowLessonCollapseLink);
        }

        [Test]
        public void GetLessonCollapseButtonCss()
        {
            _testLessonBasicObj = null;
            _testViewObj.SelectedLessonObjBasic = null;
            var expected = "cw-plus";
            Assert.AreEqual(expected, _testViewObj.LessonCollapseButtonCSS);
        }
        
        [Test]
        public void GetUserIsEditor()
        {
            _testViewObj.CurrentUserObj = _userObj;
            var expected = false;
            Assert.AreEqual(expected, _testViewObj.UserIsEditor);
        }

        [Test]
        public void GetUserIsApplicationAdmin()
        {
            _testViewObj.CurrentUserObj = _userObj;
            var expected = false;
            Assert.AreEqual(expected, _testViewObj.UserIsApplicationAdmin);
        }

        [Test]
        public void GetUserIsWorkingGroupAdmin()
        {
            _testViewObj.CurrentUserObj = _userObj;
            var expected = false;
            Assert.AreEqual(expected, _testViewObj.UserIsWorkingGroupAdmin);
        }

        [Test]
        public void GetSelectedCourseId()
        {
            _testViewObj.ClassSectionObj = _domainObj;
            var expected = Guid.Empty;
            Assert.AreEqual(expected, _testViewObj.SelectedCourseId);
        }

        [Test]
        public void GetTermName()
        {
            _testViewObj.ClassSectionObj = _domainObj;
            var expected = "";
            Assert.AreEqual(expected, _testViewObj.TermName);
        }

        [Test]
        public void GetLessonObsForTreeList()
        {
            _testViewObj.CourseObj = null;
            var expected = new List<LessonDomainObj>();
            Assert.AreEqual(expected, _testViewObj.LessonObjsForTreeList);
        }

        [Test]
        public void GetDoAllowEditing()
        {
            _testViewObj.ClassSectionObj = _domainObj;
            var expected = false;
            Assert.AreEqual(expected, _testViewObj.DoAllowEditing);
        }

    }
}

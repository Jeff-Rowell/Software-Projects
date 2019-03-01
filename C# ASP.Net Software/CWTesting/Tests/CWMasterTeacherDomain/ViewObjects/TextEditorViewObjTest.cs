using CWMasterTeacherDomain.DomainObjects;
using NUnit.Framework;
using CWMasterTeacherDomain.ViewObjects;

namespace CWTesting.Tests.CWMasterTeacherDomain.ViewObjects
{
    [TestFixture]
    class TextEditorViewObjTest
    {
        private TextEditorViewObj _testViewObj = new TextEditorViewObj();
        private UserDomainObj _currentUserObj = new UserDomainObj();

        [SetUp]
        protected void Setup()
        {
            _currentUserObj.IsWorkingGroupAdmin = true;
            _testViewObj.CurrentUserObj = _currentUserObj;
            _testViewObj.CurrentUserObj.IsApplicationAdmin = true;
            _testViewObj.IsMasterEdit = true;
            _testViewObj.ActionName = "LessonPlanUpdate";
        }
        
        [Test]
        public void TestGetUserIsApplicationAdmin()
        {
            Assert.IsTrue(_testViewObj.UserIsApplicationAdmin);
        }

        [Test]
        public void TestGetUserIsWorkingGroupAdmin()
        {
            Assert.IsTrue(_testViewObj.UserIsWorkingGroupAdmin);
            _currentUserObj = null;
            _testViewObj.CurrentUserObj = _currentUserObj;
            Assert.IsFalse(_testViewObj.UserIsWorkingGroupAdmin);
        }

        [Test]
        public void TestGetScreenTitleCSS()
        {
            Assert.AreEqual("cw-title-sm-master cw-title-margin", _testViewObj.ScreenTitleCSS);
        }

        [Test]
        public void TestGetDoShowNameOrSubject()
        {
            Assert.IsTrue(_testViewObj.DoShowNameOrSubject);
        }
    }
}

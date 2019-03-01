using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CWMasterTeacherDomain.DomainObjects;

namespace CWTesting.Tests.CWMasterTeacherDomain
{
    [TestFixture]
    class ClassMeetingDomainObjBasicTest
    {
        private Guid _expectedId = Guid.NewGuid();
        private ClassMeetingDomainObjBasic _basicObj;
        private String _expectedClassSectionName = "CS 3250";
        private Guid _expectedClassSectionId = Guid.NewGuid();
        private DateTime _expectedDateTime = new DateTime(2018, 2, 15);
        private bool _expectedIsSelected = true;
        private bool _expectedIsReadyToTeach = true;
        private bool _expectedIsCurrent = true;

        [SetUp]
        public void Setup()
        {
            _basicObj = new ClassMeetingDomainObjBasic();
            _basicObj.Id = _expectedId;
            _basicObj.ClassSectionName = _expectedClassSectionName;
            _basicObj.ClassSectionId = _expectedClassSectionId;
            _basicObj.MeetingDate = _expectedDateTime;
            _basicObj.IsSelected = _expectedIsSelected;
            _basicObj.IsReadyToTeach = _expectedIsReadyToTeach;
            _basicObj.IsCurrent = _expectedIsCurrent;
        }

        [Test]
        public void getId()
        {
            Assert.AreEqual(_expectedId, _basicObj.Id);
        }

        [Test]
        public void SetId()
        {
            var expected = Guid.NewGuid();
            _basicObj.Id = expected;
            Assert.AreEqual(expected, _basicObj.Id);
        }

        [Test]
        public void GetClassSectionName()
        {
            Assert.AreEqual(_expectedClassSectionName, _basicObj.ClassSectionName);
        }

        [Test]
        public void SetClassSectionName()
        {
            var expected = "foo";
            _basicObj.ClassSectionName = expected;
            Assert.AreEqual(expected, _basicObj.ClassSectionName);
        }

        [Test]
        public void GetClassSectionId()
        {
            Assert.AreEqual(_expectedClassSectionId, _basicObj.ClassSectionId);
        }

        [Test]
        public void SetClassSectionId()
        {
            var expected = Guid.NewGuid();
            _basicObj.ClassSectionId = expected;
            Assert.AreEqual(expected, _basicObj.ClassSectionId);
        }

        [Test]
        public void GetMeetingDate()
        {
            Assert.AreEqual(_expectedDateTime, _basicObj.MeetingDate);
        }

        [Test]
        public void SetMeetingDate()
        {
            var expected = _expectedDateTime.AddDays(10);
            _basicObj.MeetingDate = expected;
            Assert.AreEqual(expected, _basicObj.MeetingDate);
        }

        [Test]
        public void MeetingDateString()
        {
            String expectedDateTime = "Thu, Feb 15";
            Assert.AreEqual(expectedDateTime, _basicObj.MeetingDateString);
        }

        [Test]
        public void GetIsSelected()
        {
            Assert.AreEqual(_expectedIsSelected, _basicObj.IsSelected);
        }

        [Test]
        public void SetIsSelected()
        {
            var expected = false;
            _basicObj.IsSelected = expected;
            Assert.AreEqual(expected, _basicObj.IsSelected);
        }

        [Test]
        public void GetIsReadyToTeach()
        {
            Assert.AreEqual(_expectedIsReadyToTeach, _basicObj.IsReadyToTeach);
        }

        [Test]
        public void SetIsReadyToTeach()
        {
            var expected = false;
            _basicObj.IsReadyToTeach = expected;
            Assert.AreEqual(expected, _basicObj.IsReadyToTeach);
        }

        [Test]
        public void GetDisplayClass()
        {
            var expected = "cw-list-selected-1 cw-bold";
            Assert.AreEqual(expected, _basicObj.DisplayClass);
        }

        [Test]
        public void GetIsCurrent()
        {
            Assert.AreEqual(_expectedIsCurrent, _basicObj.IsCurrent);
        }

        [Test]
        public void SetIsCurrent()
        {
            var expected = false;
            _basicObj.IsCurrent = expected;
            Assert.AreEqual(expected, _basicObj.IsCurrent);
        }
    }
}

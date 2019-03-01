using System;
using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain;
using System.Web.Mvc;

namespace CWTesting.Tests.CWMasterTeacherDomain
{
    [TestFixture]
    class ClassMeetingDomainObjTest
    {
        private ClassMeetingDomainObjBasic _basicObj;
        private ClassMeetingDomainObj _domainObj;
        private Guid _classSectionId;
        private LessonUseDomainObj _lessonUseDomainObj1;
        private LessonUseDomainObj _lessonUseDomainObj2;

        [SetUp]
        public void setup()
        {
            _basicObj = new ClassMeetingDomainObjBasic();
            _domainObj = new ClassMeetingDomainObj(_basicObj);
            _classSectionId = Guid.NewGuid();

            _lessonUseDomainObj1 = new LessonUseDomainObj();
            _lessonUseDomainObj1.CustomName = _domainObj.Comment + ": NO CLASS";
            _lessonUseDomainObj1.SequenceNumber = 1;
            List<LessonUseDomainObj> testList = new List<LessonUseDomainObj>();
            testList.Add(_lessonUseDomainObj1);

            _lessonUseDomainObj2 = new LessonUseDomainObj();
            _lessonUseDomainObj2.CustomName = _domainObj.Comment + ": NO CLASS";
            _lessonUseDomainObj2.SequenceNumber = 2;
            testList.Add(_lessonUseDomainObj2);

            _domainObj.LessonUseList = testList;
            _domainObj.ClassSectionId = _classSectionId;
            _domainObj.MeetingDate = new DateTime(2016, 10, 10);
            _domainObj.StartTime = new DateTime(2016, 10, 10, 0, 0, 0);
            _domainObj.EndTime = new DateTime(2016, 10, 10, 0, 0, 0);
            _domainObj.MeetingNumber = 2;
            _domainObj.Comment = "Some comment";
            _domainObj.IsNoClass = true;
            _domainObj.IsExamDay = false;
            _domainObj.IsBeginningOfWeek = false;
            _domainObj.IsReadyToTeach = true;
            _domainObj.ClassSectionName = "Some class section name";
        }

        [Test]
        public void DefaultConstructor()
        {
            ClassMeetingDomainObj testObj = new ClassMeetingDomainObj();
            Assert.NotNull(testObj);
        }

        [Test]
        public void GetIdReturnsBasicObjId()
        {
            Assert.AreEqual(_basicObj.Id, _domainObj.Id);
        }

        [Test]
        public void GetClassMeetingIdReturnsId()
        {
            Assert.AreEqual(_domainObj.Id, _domainObj.ClassMeetingId);
        }

        [Test]
        public void GetClassesToGoString()
        {
            _domainObj.ClassCount = 10;
            _domainObj.ClassNumber = 3;
            Assert.AreEqual("7 to go", _domainObj.ClassesToGoString);
        }

        [Test]
        public void GetDisplayNameClass()
        {
            _domainObj.IsBeginningOfWeek = false;
            Assert.AreEqual("", _domainObj.DisplayNameClass);
        }

        [Test]
        public void GetClassNumberDisplayClass()
        {
            _domainObj.IsBeginningOfWeek = false;
            Assert.AreEqual("cw-space-above-bigbigskip", _domainObj.ClassNumberDisplayClass);
        }

        [Test]
        public void GetWeekNumberString()
        {
            _domainObj.IsBeginningOfWeek = true;
            _domainObj.WeekNumber = 13;
            Assert.AreEqual("Week 13", _domainObj.WeekNumberString);
            _domainObj.IsBeginningOfWeek = false;
            Assert.AreEqual("", _domainObj.WeekNumberString);
        }

        [Test]
        public void GetLessonUseSelectList()
        {
            _domainObj.IsNoClass = true;
            SelectList testList = new SelectList(_domainObj.LessonUseList, "Id", "DisplayName");
            Assert.AreEqual(testList.DataValueField, _domainObj.LessonUseSelectList.DataValueField);
            Assert.AreEqual(testList.DataTextField, _domainObj.LessonUseSelectList.DataTextField);
        }

        [Test]
        public void GetClassNumberString()
        {
            _domainObj.ClassNumber = 7;
            Assert.AreEqual("Class 7", _domainObj.ClassNumberString);
        }

        [Test]
        public void GetLessonUseList()
        {
            _domainObj.IsNoClass = true;
            foreach (var x in _domainObj.LessonUseList)
            {
                Assert.AreEqual(Guid.Empty, x.Id);
                Assert.AreEqual(_domainObj.Comment + ": NO CLASS", x.CustomName);
            }

            _domainObj.IsNoClass = false;
            List<LessonUseDomainObj> testList = new List<LessonUseDomainObj>();
            testList.Add(_lessonUseDomainObj1);
            testList.Add(_lessonUseDomainObj2);
            _domainObj.LessonUseList = testList;
            int i = 1;
            foreach (var x in _domainObj.LessonUseList)
            {
                Assert.AreEqual(i, x.SequenceNumber);
                i++;
            }
        }

        [Test]
        public void GetClassSectionId()
        {
            Assert.AreNotEqual(Guid.NewGuid(), _domainObj.ClassSectionId);
            Assert.AreEqual(_basicObj.ClassSectionId, _domainObj.ClassSectionId);
        }

        [Test]
        public void GetMeetingDate()
        {
            Assert.AreEqual(new DateTime(2016, 10, 10, 0, 0, 0), _domainObj.MeetingDate);
        }

        [Test]
        public void GetMeetingDateString()
        {
            string expected = DomainWebUtilities.DateTime_ToLongDateString(new DateTime(2016, 10, 10, 0, 0, 0));
            Assert.AreEqual(expected, _domainObj.MeetingDateString);
        }

        [Test]
        public void GetClassMeetingBorderCSS()
        {
            _domainObj.IsNextClass = true;
            Assert.AreEqual("cw-border-ridge", _domainObj.ClassMeetingBorderCSS);
        }

        [Test]
        public void IsUseSelectedEnabledClass()
        {
            _domainObj.IsLessonUseSelected = true;
            Assert.AreEqual("", _domainObj.IsUseSelectedEnabledClass);
        }

        [Test]
        public void GetStartTime()
        {
            Assert.AreEqual(new DateTime(2016, 10, 10, 0, 0, 0), _domainObj.StartTime);
        }

        [Test]
        public void GetEndTime()
        {
            Assert.AreEqual(new DateTime(2016, 10, 10, 0, 0, 0), _domainObj.EndTime);
        }

        [Test]
        public void GetMeetingNumber()
        {
            Assert.AreEqual(2, _domainObj.MeetingNumber);
        }

        [Test]
        public void GetComment()
        {
            Assert.AreEqual("Some comment", _domainObj.Comment);
        }

        [Test]
        public void GetNoClass()
        {
            _domainObj.IsNoClass = false;
            Assert.AreEqual(false, _domainObj.IsNoClass);
            _domainObj.IsNoClass = true;
            Assert.AreEqual(true, _domainObj.IsNoClass);
        }

        [Test]
        public void GetIsExamDay()
        {
            Assert.AreEqual(false, _domainObj.IsExamDay);
        }

        [Test]
        public void GetIsBeginningOfWeek()
        {
            Assert.AreEqual(false, _domainObj.IsBeginningOfWeek);
        }

        [Test]
        public void GetIsReadyToTeach()
        {
            Assert.AreEqual(true, _domainObj.IsReadyToTeach);
        }

        [Test]
        public void GetClassSectionName()
        {
            Assert.AreEqual("Some class section name", _domainObj.ClassSectionName);
        }

        [Test]
        public void GetDisplayNameNoClassIsTrueHasComment()
        {
            string expected = String.Format("{0:ddd, MMM d, yyyy}", new DateTime(2016, 10, 10))
                + "   (No Class: " + "Some comment" + ")";
            Assert.AreEqual(expected, _domainObj.DisplayName);
        }

        [Test]
        public void GetDisplayNameNoClassIsFalseDoesntHaveComment()
        {
            _domainObj.IsNoClass = false;
            string expected = String.Format("{0:ddd, MMM d, yyyy}", new DateTime(2016, 10, 10));
            Assert.AreEqual(expected, _domainObj.DisplayName);
        }

        [Test]
        public void GetDisplayNameWithTimeNoClassIsTrueHasComment()
        {
            string expected = String.Format("{0:ddd, MMM d, yyyy}", new DateTime(2016, 10, 10))
                + "   (No Class: " + "Some comment" + ")";
            Assert.AreEqual(expected, _domainObj.DisplayNameWithTime);
        }

        [Test]
        public void GetDisplayNameWithTimeNoClassIsFalseDoesntHaveComment()
        {
            _domainObj.IsNoClass = false;
            string expected = String.Format("{0:ddd, MMM d, yyyy}", new DateTime(2016, 10, 10))
                + "...."
                + DomainUtilities.ConvertUtcToLocalTime(new DateTime(2016, 10, 10, 0, 0, 0))
                                 .ToString("hh:mm tt", CultureInfo.InvariantCulture);
            Assert.AreEqual(expected, _domainObj.DisplayNameWithTime);
        }

        [Test]
        public void GetLongDisplayName()
        {
            string expected = "Some class section name" + "........"
                + String.Format("{0:ddd, MMM d, yyyy}", new DateTime(2016, 10, 10))
                + "   (No Class: " + "Some comment" + ")";
            Assert.AreEqual(expected, _domainObj.LongDisplayName);
        }

        [Test]
        public void GetStartTimeLocal()
        {
            var expected = DomainUtilities.ConvertUtcToLocalTime(new DateTime(2016, 10, 10, 0, 0, 0));
            Assert.AreEqual(expected, _domainObj.StartTimeLocal);
        }

        [Test]
        public void GetEndTimeLocal()
        {
            var expected = DomainUtilities.ConvertUtcToLocalTime(new DateTime(2016, 10, 10, 0, 0, 0));
            Assert.AreEqual(expected, _domainObj.EndTimeLocal);
        }
     
        [Test]
        public void GetLessonCount()
        {
            _domainObj.IsNoClass = false;
            Assert.AreEqual(2, _domainObj.LessonCount);
        }

        [Test]
        public void GetUpArrowClass()
        {
            _domainObj.IsLessonUseSelected = true;
            Assert.AreEqual("cw-arrow-up", _domainObj.UpArrowClass);
        }

        [Test]
        public void GetDownArrowClass()
        {
            _domainObj.IsLessonUseSelected = true;
            Assert.AreEqual("cw-arrow-down", _domainObj.DownArrowClass);
        }

        [Test]
        public void GetRightArrowClass()
        {
            _domainObj.IsALessonSelected = true;
            _domainObj.IsNoClass = false;
            Assert.AreEqual("cw-arrow-right", _domainObj.RightArrowClass);
        }

        [Test]
        public void GetLeftArrowClass()
        {
            _domainObj.IsLessonUseSelected = true;
            Assert.AreEqual("cw-arrow-left", _domainObj.LeftArrowClass);
        }

        [Test]
        public void GetUpSmallArrowClass()
        {
            _domainObj.IsLessonUseSelected = true;
            Assert.AreEqual("cw-arrow-up-small", _domainObj.UpSmallArrowClass);
        }

        [Test]
        public void GetDownSmallArrowClass()
        {
            _domainObj.IsLessonUseSelected = true;
            Assert.AreEqual("cw-arrow-down-small", _domainObj.DownSmallArrowClass);
        }

        // Setter tests.
        [Test]
        public void SetEndTimeLocal()
        {
            var expected = new DateTime(2016, 11, 11);
            _domainObj.EndTimeLocal = expected;
            Assert.AreEqual(expected, _domainObj.EndTimeLocal);
        }

        [Test]
        public void SetStartTimeLocal()
        {
            var expected = new DateTime(2016, 12, 01);
            _domainObj.StartTimeLocal = expected;
            Assert.AreEqual(expected, _domainObj.StartTimeLocal);
        }

        [Test]
        public void SetIsReadyToTeach()
        {
            var expected = false;
            _domainObj.IsReadyToTeach = expected;
            Assert.AreEqual(expected, _domainObj.IsReadyToTeach);
        }

        [Test]
        public void SetIsBeginningOfWeek()
        {
            var expected = true;
            _domainObj.IsBeginningOfWeek = expected;
            Assert.AreEqual(expected, _domainObj.IsBeginningOfWeek);
        }

        [Test]
        public void SetIsExamDay()
        {
            var expected = false;
            _domainObj.IsExamDay = expected;
            Assert.AreEqual(expected, _domainObj.IsExamDay);
        }

        [Test]
        public void SetNoClass()
        {
            var expected = false;
            _domainObj.IsNoClass = expected;
            Assert.AreEqual(expected, _domainObj.IsNoClass);
        }

        [Test]
        public void SetComment()
        {
            var expected = "a new comment";
            _domainObj.Comment = expected;
            Assert.AreEqual(expected, _domainObj.Comment);
        }

        [Test]
        public void SetMeetingNumber()
        {
            var expected = 10;
            _domainObj.MeetingNumber = expected;
            Assert.AreEqual(expected, _domainObj.MeetingNumber);
        }

        [Test]
        public void SetEndTime()
        {
            var expected = new DateTime(2017, 01, 31, 06, 40, 25);
            _domainObj.EndTime = expected;
            Assert.AreEqual(expected, _domainObj.EndTime);
        }

        [Test]
        public void SetStartTime()
        {
            var expected = new DateTime(2018, 11, 12, 13, 14, 15);
            _domainObj.StartTime = expected;
            Assert.AreEqual(expected, _domainObj.StartTime);
        }

        [Test]
        public void SetMeetingDate()
        {
            var expected = new DateTime(12, 12, 12, 12, 12, 12);
            _domainObj.MeetingDate = expected;
            Assert.AreEqual(expected, _domainObj.MeetingDate);
        }

        [Test]
        public void SetClassSectionId()
        {
            var expected = Guid.NewGuid();
            _domainObj.ClassSectionId = expected;
            Assert.AreEqual(expected, _domainObj.ClassSectionId);
        }
    }
 }

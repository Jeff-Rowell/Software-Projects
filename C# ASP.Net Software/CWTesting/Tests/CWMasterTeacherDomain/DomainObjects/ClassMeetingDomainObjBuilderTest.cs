using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain;
using CWMasterTeacherDataModel.Interfaces;

namespace CWTesting.Tests.CWMasterTeacherDomain
{
    [TestFixture]
    class ClassMeetingDomainObjBuilderTest
    {

        private ClassMeeting _testClassMeeting0;
        private ClassMeeting _testClassMeeting1;
        private ClassMeeting _testClassMeeting2;
        private ClassMeeting _testClassMeeting3;
        private ClassMeeting _testClassMeeting4;
        private ClassMeeting _testClassMeeting5;
        private ClassMeeting _testClassMeeting6;
        private ClassMeeting _testClassMeeting7;
        private ClassMeeting _testClassMeeting8;
        private ClassMeeting _testClassMeeting9;
        private ClassSection _testClassSection;
        private ClassSection _emptyTestClassSection;
        private Course _testCourse;
        private LessonUse _testLessonUse;

        private ClassMeetingDomainObjBuilder _builder;
         
        private Mock<IClassSectionRepo> _mockClassSectionRepo;
        private Mock<IClassMeetingRepo> _mockClassMeetingRepo;
        private Mock<LessonUseDomainObjBuilder> _testLessonUseDomainObjBuilder;

        private Guid _testUserID;
        private Guid _testTermID;


        [SetUp]
        public void Setup()
        {
            InitializeTestDatabaseObjects();
            InitializeMockRepositories();
            _builder = new ClassMeetingDomainObjBuilder(_mockClassMeetingRepo.Object, 
                _mockClassSectionRepo.Object, _testLessonUseDomainObjBuilder.Object);
        }
        

        [Test] 
        public void testBuild()
        {
            foreach (var classMeeting in _testClassSection.ClassMeetings)
            {
                ClassMeetingDomainObj domainObj = ClassMeetingDomainObjBuilder.Build(classMeeting);
                Assert.NotNull(domainObj);
                assertEquality(domainObj, classMeeting);
            }
            Assert.Throws<NullReferenceException>(delegate { ClassMeetingDomainObjBuilder.Build(null); });
        }

        [Test]
        public void testBuildBasic()
        {
            ClassMeetingDomainObjBasic basicObj;
            foreach(var xClassMeeting in _testClassSection.ClassMeetings)
            {
                basicObj = ClassMeetingDomainObjBuilder.BuildBasic(xClassMeeting);
                assertBasicEquality(basicObj, xClassMeeting);
            }

            Assert.Throws<NullReferenceException>(delegate { ClassMeetingDomainObjBuilder.BuildBasic(null); });
        }

        [Test]
        public void testBuildFromId()
        {
            ClassMeetingDomainObj domainObj;
            foreach (var xClassMeeting in _testClassSection.ClassMeetings)
            {
                domainObj = _builder.BuildFromId(xClassMeeting.Id);
                assertEquality(domainObj, xClassMeeting);
            }

            domainObj = _builder.BuildFromId(Guid.NewGuid());
            Assert.Null(domainObj);
        }

        [Test]
        public void testBuildBasicFromId()
        {
            ClassMeetingDomainObjBasic basicObj;
            foreach (var xClassMeeting in _testClassSection.ClassMeetings)
            {
                basicObj = _builder.BuildBasicFromId(xClassMeeting.Id);
                assertBasicEquality(basicObj, xClassMeeting);
            }

            basicObj = _builder.BuildBasicFromId(Guid.NewGuid());
            Assert.Null(basicObj);
        }

        [Test]
        public void testGetClassMeetingsForClassSection()
        {
            List<ClassMeetingDomainObj> emptyList = _builder.GetClassMeetingsForClassSection(_emptyTestClassSection.Id);
            Assert.IsEmpty(emptyList);

            List<ClassMeetingDomainObj> fullList = _builder.GetClassMeetingsForClassSection(_testClassSection.Id);
            bool found = false;
            foreach (var xClassMeeting in _testClassSection.ClassMeetings)
            {
                found = false;

                foreach (var xClassMeetingDomainObject in fullList)
                {
                    if (xClassMeeting.Id == xClassMeetingDomainObject.Id)
                    {
                        assertEquality(xClassMeetingDomainObject, xClassMeeting);
                        found = true;
                    }
                }
                    
                if (!found)
                {
                    break;
                }
            }
            Assert.IsTrue(found);
        }

        [Test]
        public void testGetClassMeetingBasicsForClassSection()
        {
            List<ClassMeetingDomainObjBasic> emptyList = 
                _builder.GetClassMeetingBasicsForClassSection(_emptyTestClassSection.Id, true);
            Assert.IsEmpty(emptyList);

            List<ClassMeetingDomainObjBasic> fullList = 
                _builder.GetClassMeetingBasicsForClassSection(_testClassSection.Id, true);
            bool found = false;
            foreach (var xClassMeeting in _testClassSection.ClassMeetings)
            {
                found = false;

                foreach (var xBasicObj in fullList)
                {
                    if (xClassMeeting.Id == xBasicObj.Id)
                    {
                        assertBasicEquality(xBasicObj, xClassMeeting);
                        found = true;
                    }
                }

                if (!found)
                {
                    break;
                }
            }
            Assert.IsTrue(found);
        }

        [Test]
        public void testDatesForUserAndTerm()
        {
            List<DateTime> dtList = _builder.DatesForUserAndTerm(_testUserID, _testTermID);
            foreach (var classMeeting in _testClassSection.ClassMeetings)
            {
                Assert.Contains(classMeeting.MeetingDate, dtList);
            }

            dtList = _builder.DatesForUserAndTerm(Guid.NewGuid(), Guid.NewGuid());
            Assert.IsEmpty(dtList);
        }

        

        [Test]
        public void testSevenCurrentDates()
        {
            List<DateTime> dtList = _builder.SevenCurrentDates(_testUserID, _testTermID);
            //3 past, 4 present or future, see loop in InitializeTestDatabaseObjects()
            Assert.Contains(_testClassMeeting2.MeetingDate, dtList);
            Assert.Contains(_testClassMeeting3.MeetingDate, dtList);
            Assert.Contains(_testClassMeeting4.MeetingDate, dtList);
            Assert.Contains(_testClassMeeting5.MeetingDate, dtList);
            Assert.Contains(_testClassMeeting6.MeetingDate, dtList);
            Assert.Contains(_testClassMeeting7.MeetingDate, dtList);
            Assert.Contains(_testClassMeeting8.MeetingDate, dtList);
            //does not contain the rest
            Assert.True(!dtList.Contains(_testClassMeeting0.MeetingDate));
            Assert.True(!dtList.Contains(_testClassMeeting1.MeetingDate));
            Assert.True(!dtList.Contains(_testClassMeeting9.MeetingDate));
        
            dtList = _builder.SevenCurrentDates(Guid.NewGuid(), Guid.NewGuid());
            Assert.IsEmpty(dtList);
        }

        [Test]
        public void testClassMeetingsForUserAndDate()
        {
            List<ClassMeetingDomainObjBasic> basicList = 
                _builder.ClassMeetingsForUserAndDate(_testUserID, _testClassMeeting3.MeetingDate);

            foreach (var xBasicObj in basicList)
            {
                assertBasicEquality(xBasicObj, _testClassMeeting3);
            }

            basicList = _builder.ClassMeetingsForUserAndDate(_testUserID, DateTime.Now.AddDays(30));
            Assert.IsEmpty(basicList);
        }


        private void assertEquality(ClassMeetingDomainObj domainObj, ClassMeeting classMeeting)
        {
            Assert.AreEqual(classMeeting.Id, domainObj.Id);
            Assert.AreEqual(classMeeting.ClassSection.Id, domainObj.ClassSectionId);
            Assert.AreEqual(classMeeting.Id, domainObj.ClassMeetingId);
            Assert.AreEqual(classMeeting.MeetingDate, domainObj.MeetingDate);
            Assert.AreEqual(classMeeting.StartTime, domainObj.StartTime);
            Assert.AreEqual(classMeeting.EndTime, domainObj.EndTime);
            Assert.AreEqual(classMeeting.MeetingNumber, domainObj.MeetingNumber);
            Assert.AreEqual(classMeeting.Comment, domainObj.Comment);
            Assert.AreEqual(classMeeting.NoClass, domainObj.IsNoClass);
            Assert.AreEqual(classMeeting.IsExamDay, domainObj.IsExamDay);
            Assert.AreEqual(classMeeting.IsBeginningOfWeek, domainObj.IsBeginningOfWeek);
            Assert.AreEqual(classMeeting.IsReadyToTeach, domainObj.IsReadyToTeach);
            Assert.AreEqual(classMeeting.ClassSection.Name, domainObj.ClassSectionName);

            if (!domainObj.IsNoClass)//Assert the LessonUses built if they were passed through
            {
                foreach (var xLessonUseDomainObj in domainObj.LessonUseList)
                {
                    foreach (var xLessonUse in classMeeting.LessonUses)
                    {
                        Assert.AreEqual(xLessonUse.Id, xLessonUseDomainObj.Id);
                    }
                }
            }
        }

        public void assertBasicEquality(ClassMeetingDomainObjBasic basicObj, ClassMeeting dbObj)
        {
            Assert.AreEqual(basicObj.Id, dbObj.Id);
            Assert.AreEqual(basicObj.ClassSectionName, dbObj.ClassSection.Name);
            Assert.AreEqual(basicObj.MeetingDate, dbObj.MeetingDate);
            Assert.AreEqual(basicObj.ClassSectionId, dbObj.ClassSectionId);
            Assert.AreEqual(basicObj.IsReadyToTeach, dbObj.IsReadyToTeach);
            Assert.AreEqual(DomainWebUtilities.DateTime_ToLongDateString(dbObj.MeetingDate), basicObj.MeetingDateString);
        }

        private void InitializeTestDatabaseObjects()
        {
            _testUserID = Guid.NewGuid();
            _testTermID = Guid.NewGuid();

            _testCourse = new Course();
            _testCourse.Id = Guid.NewGuid();
            _testCourse.UserId = _testUserID;
            _testCourse.TermId = _testTermID;

            _testClassSection = new ClassSection();
            _testClassSection.Id = Guid.NewGuid();
            _testClassSection.CourseId = _testCourse.Id;
            _testClassSection.Name = "Some name";
            _testClassSection.LastDisplayedClassMeetingId = new Guid();

            _emptyTestClassSection = new ClassSection();
            _emptyTestClassSection.Id = Guid.NewGuid();
            _emptyTestClassSection.CourseId = _testCourse.Id;
            _emptyTestClassSection.Name = "section with no meetings";
            _emptyTestClassSection.LastDisplayedClassMeetingId = null;

            _testClassMeeting0 = new ClassMeeting(); _testClassSection.ClassMeetings.Add(_testClassMeeting0);
            _testClassMeeting1 = new ClassMeeting(); _testClassSection.ClassMeetings.Add(_testClassMeeting1);
            _testClassMeeting2 = new ClassMeeting(); _testClassSection.ClassMeetings.Add(_testClassMeeting2);
            _testClassMeeting3 = new ClassMeeting(); _testClassSection.ClassMeetings.Add(_testClassMeeting3);
            _testClassMeeting4 = new ClassMeeting(); _testClassSection.ClassMeetings.Add(_testClassMeeting4);
            _testClassMeeting5 = new ClassMeeting(); _testClassSection.ClassMeetings.Add(_testClassMeeting5);
            _testClassMeeting6 = new ClassMeeting(); _testClassSection.ClassMeetings.Add(_testClassMeeting6);
            _testClassMeeting7 = new ClassMeeting(); _testClassSection.ClassMeetings.Add(_testClassMeeting7);
            _testClassMeeting8 = new ClassMeeting(); _testClassSection.ClassMeetings.Add(_testClassMeeting8);
            _testClassMeeting9 = new ClassMeeting(); _testClassSection.ClassMeetings.Add(_testClassMeeting9);

            int index = -4;

            /*This loop is used to set properties of Class Meeting dbobjects with all kinds of different values
            //To more thoroughly test every case. Moving datetime values are useful for the SevenCurrentDates test method
            */

            foreach (var xClassMeeting in _testClassSection.ClassMeetings)
            {
                xClassMeeting.ClassSectionId = _testClassSection.Id;
                xClassMeeting.Id = Guid.NewGuid();

                if (!(index == -4))
                {
                    xClassMeeting.MeetingDate = DateTime.Today.AddDays(-1 + (index * 2));
                    xClassMeeting.StartTime = DateTime.Today.AddDays(-1 + (index * 2)).AddHours(8);
                    xClassMeeting.EndTime = DateTime.Today.AddDays(-1 + (index * 2)).AddHours(9).AddMinutes(30);
                }

                xClassMeeting.MeetingNumber = index + 5;
                xClassMeeting.Comment = index % 2 == 0 ? null : "Some comment";
                xClassMeeting.NoClass = index % 2 == 0 ? true : false;
                xClassMeeting.IsExamDay = true;
                xClassMeeting.IsBeginningOfWeek = true;
                xClassMeeting.IsReadyToTeach = true;
                xClassMeeting.NotesForStudents = index % 2 == 0 ? null : "Some Notes";
                xClassMeeting.ClassSection = _testClassSection;

                if (!(index % 2 == 0))
                {
                    _testLessonUse = new LessonUse();
                    _testLessonUse.Id = Guid.NewGuid();
                    _testLessonUse.ClassMeeting = xClassMeeting;
                    _testLessonUse.ClassMeetingId = xClassMeeting.Id;
                    xClassMeeting.LessonUses.Add(_testLessonUse);
                }

                index++;
            }
        }

        private void InitializeMockRepositories()
        {
            _mockClassMeetingRepo = new Mock<IClassMeetingRepo>();
            _mockClassMeetingRepo.Setup(mock => mock.GetById(It.IsAny<Guid>())).Returns(
                (Guid i) =>
                {
                    foreach (var classMeeting in _testClassSection.ClassMeetings)
                    { if (i.Equals(classMeeting.Id)) return classMeeting; }
                    return null;
                }
            );

            _mockClassMeetingRepo.Setup(mock => mock.ClassMeetingsForClassSection(It.IsAny<Guid>())).Returns(
                (Guid i) =>
                {
                    if (i.Equals(_testClassSection.Id))
                    {
                        List<ClassMeeting> list = new List<ClassMeeting>();
                        foreach (var xClassMeeting in _testClassSection.ClassMeetings)
                        {
                            list.Add(xClassMeeting);
                        }
                        return list;
                    }

                    return null;
                }
            );

            _mockClassMeetingRepo.Setup(mock => mock.ClassMeetingsForUserAndDate(It.IsAny<Guid>(), It.IsAny<DateTime>())).Returns(
                (Guid i, DateTime j) =>
                {
                    if (i.Equals(_testUserID))
                    {
                        List<ClassMeeting> list = new List<ClassMeeting>();
                        foreach (var xClassMeeting in _testClassSection.ClassMeetings)
                            if(j.Equals(xClassMeeting.MeetingDate))
                                list.Add(xClassMeeting);
                        return list;
                    }

                    return null;
                }
            );



            _mockClassSectionRepo = new Mock<IClassSectionRepo>();
            _mockClassSectionRepo.Setup(mock => mock.GetById(It.IsAny<Guid>())).Returns(
                (Guid i) =>
                {
                    if (i.Equals(_testClassSection.Id))
                    {
                        return _testClassSection;
                    }
                    return null;
                }
            );


            _mockClassSectionRepo.Setup(mock => mock.ClassSectionsForUserAndTerm(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(
                (Guid i, Guid j) =>
                {
                    if (i.Equals(_testUserID) && j.Equals(_testTermID))
                    {
                        List<ClassSection> list = new List<ClassSection>();
                        list.Add(_testClassSection);
                        return list;
                    }
                    return new List<ClassSection>();
                }
            );

            _testLessonUseDomainObjBuilder = new Mock<LessonUseDomainObjBuilder>(null, null);//never used, just needed for instantiation of builder
        }
    }
}

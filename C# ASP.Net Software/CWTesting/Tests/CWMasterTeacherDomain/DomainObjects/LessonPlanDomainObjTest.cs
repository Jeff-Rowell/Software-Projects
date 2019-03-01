using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CWMasterTeacherDomain;
using Moq;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDataModel;

namespace CWTesting.Tests.CWMasterTeacherDomain
{
    //[TestFixture]
    //class LessonPlanDomainObjectBuilderTest
    //{
    //    private Mock<LessonPlanRepo> _mockRepo;
    //    private LessonPlan _mockLessonPlan;
    //    private Lesson _mockLesson;
    //    private Mock<LessonRepo> _mockLessonRepo;
    //    private LessonPlanDomainObjBuilder _builder;
    //    private DateTime _date1 = new DateTime(2016, 01, 01);
    //    private DateTime _date2 = new DateTime(2016, 01, 02);



    //    //LessonPlanDomainObjBasic _lessonPlanObjBasic;
    //    //LessonPlanDomainObj _lessonPlanObj;
    //    //DateTime _date1;
    //    //DateTime _date2;

    //    [SetUp]
    //    public void setup()
    //    {
    //        _mockLessonPlan = new LessonPlan();
    //        _mockLessonPlan.LessonPlanId = 0;
    //        _mockLessonPlan.Name = "Test Name";
    //        _mockLessonPlan.Text = "Test Text";
    //        _mockLessonPlan.DateModified = _date1;
    //        _mockLessonPlan.ModifiedByUserId = 1;
    //        _mockLessonPlan.DateChoiceConfirmed = _date2;
    //        _mockLessonPlan.LevelOfDetail = 2;
    //        _mockLessonPlan.PredecessorId = 3;
    //        _mockLessonPlan.ModificationNotes = "Test Notes";

    //        _mockRepo = new Mock<LessonPlanRepo>();
    //        _mockRepo.Setup(mock => mock.GetById(It.IsAny<Guid>())).Returns(
    //            (Guid i) =>
    //            {
    //                if (i == 0) return _mockLessonPlan;
    //                else return null;
    //            }
    //        );

            

    //        _mockLessonRepo = new Mock<LessonRepo>();
    //        _mockLessonRepo.Setup(mock => mock.GetById(It.IsAny<Guid>())).Returns(
    //            (Guid i) =>
    //            {
    //                if (i == 0) return _mockLesson;
    //                else return null;
    //            }
    //        );

    //        _builder = new LessonPlanDomainObjBuilder(_mockRepo.Object, _mockLessonRepo.Object);
    //    }

    //    private void assertEquality(LessonPlanDomainObj domainObj)
    //    {
    //        Assert.AreEqual(0, domainObj.Id);
    //        Assert.AreEqual("Test Name", domainObj.Name);
    //        Assert.AreEqual("Test Text", domainObj.Text);
    //        Assert.AreEqual(_date1, domainObj.DateModified);
    //        Assert.AreEqual(1, domainObj.ModifiedByUserId);
    //        Assert.AreEqual(_date2, domainObj.DateChoiceConfirmed);
    //        Assert.AreEqual(2, domainObj.LevelOfDetail);
    //        Assert.AreEqual(3, domainObj.PredecessorId);
    //        Assert.AreEqual("Test Notes", domainObj.ModificationNotes);
    //    }

    //    [Test]
    //    public void testBuildBasic()
    //    {
    //        LessonPlanDomainObjBasic basicObj = _builder.BuildBasic(_mockLessonPlan);
    //        Assert.AreEqual(0, basicObj.Id);
    //        Assert.AreEqual("Test Name", basicObj.Name);
    //    }

    //    [Test]
    //    public void testBuildBasicFromId()
    //    {
    //        LessonPlanDomainObjBasic basicObj = _builder.BuildBasicFromId(0);
    //        Assert.AreEqual(0, basicObj.Id);
    //        Assert.AreEqual("Test Name", basicObj.Name);
    //    }


    //    [Test]
    //    public void testBuild()
    //    {
    //        LessonPlanDomainObj domainObj = _builder.Build(_mockLessonPlan);
    //        Assert.NotNull(domainObj);
    //        assertEquality(domainObj);

    //    }

    //    [Test]
    //    public void testBuildFromId()
    //    {
    //        LessonPlanDomainObj domainObj = _builder.BuildFromId(0);
    //        Assert.NotNull(domainObj);
    //        assertEquality(domainObj);
    //    }

       
    //}
}

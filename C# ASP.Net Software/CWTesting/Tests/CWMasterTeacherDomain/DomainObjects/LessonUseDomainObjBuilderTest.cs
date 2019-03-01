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

namespace CWTesting.Tests.CWMasterTeacherDomain
{
    [TestFixture]
    class LessonUseDomainObjBuilderTest
    {
    //    private Mock<LessonUseRepo> mockRepo;
    //    private LessonUse mockLessonUse;
    //    private LessonUseDomainObjBuilder builder;


    //    [SetUp]
    //    public void setup()
    //    {
    //        mockLessonUse = new LessonUse();
    //        mockLessonUse.LessonUseId = 0;
    //        mockLessonUse.ClassMeetingId = 1;
    //        mockLessonUse.LessonId = 0;
    //        mockLessonUse.SequenceNumber = 12345;
    //        mockLessonUse.CustomName = "Some custom name";
    //        mockLessonUse.CustomText = "Some custom text";
    //        mockLessonUse.CustomTime = 1000;
    //        mockLessonUse.HasCustomTime = false;

    //        mockRepo = new Mock<LessonUseRepo>();
    //        mockRepo.Setup(mock => mock.GetById(It.IsAny<int>())).Returns(
    //            (int i) =>
    //            {
    //                if (i == 0) return mockLessonUse;
    //                else return null;
    //            }
    //        );

    //        builder = new LessonUseDomainObjBuilder(mockRepo.Object);
    //    }


    //    private void assertEquality(LessonUseDomainObj domainObj)
    //    {
    //        Assert.AreEqual(0, domainObj.LessonUseId);
    //        Assert.AreEqual(1, domainObj.ClassMeetingId);
    //        Assert.AreEqual(0, domainObj.LessonId);
    //        Assert.AreEqual(12345, domainObj.SequenceNumber);
    //        Assert.AreEqual("Some custom name", domainObj.CustomName);
    //        Assert.AreEqual("Some custom text", domainObj.CustomText);
    //        Assert.AreEqual(1000, domainObj.CustomTime);
    //        Assert.AreEqual(false, domainObj.HasCustomTime);
    //    }


    //    [Test]
    //    public void testBuildBasic()
    //    {
    //        LessonUseDomainObjBasic basicObj = LessonUseDomainObjBuilder.BuildBasic(mockLessonUse);
    //        Assert.AreEqual(0, basicObj.Id);
    //    }


    //    [Test]
    //    public void testBuildBasicFromId()
    //    {
    //        LessonUseDomainObjBasic basicObj = builder.BuildBasicFromId(0);
    //        Assert.AreEqual(0, basicObj.Id);
    //    }


    //    [Test]
    //    public void testBuild()
    //    {
    //        LessonUseDomainObj domainObj = LessonUseDomainObjBuilder.Build(mockLessonUse);
    //        Assert.NotNull(domainObj);
    //        assertEquality(domainObj);

    //    }

    //    [Test]
    //    public void testBuildFromId()
    //    {
    //        LessonUseDomainObj domainObj = builder.BuildFromId(0);
    //        Assert.NotNull(domainObj);
    //        assertEquality(domainObj);
    //    }


    //    [Test]
    //    public void testBuildWithNullArgumentThrows()
    //    {
    //        Assert.Throws<NullReferenceException>(delegate { LessonUseDomainObjBuilder.Build(null); });
    //    }


    //    [Test]
    //    public void testBuildFromIdWithBadIdThrows()
    //    {
    //        Assert.Throws<NullReferenceException>(delegate { builder.BuildFromId(-1); });
    //    }


    //    [Test]
    //    public void testBuildBasicWithNullArgumentThrows()
    //    {
    //        Assert.Throws<NullReferenceException>(delegate { LessonUseDomainObjBuilder.BuildBasic(null); });
    //    }


    //    [Test]
    //    public void testBuildBasicFromIdWithBadIdThrows()
    //    {
    //        Assert.Throws<NullReferenceException>(delegate { builder.BuildBasicFromId(-1); });
    //    }
    }
}

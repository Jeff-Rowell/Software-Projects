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
    //[TestFixture]
    //class ClassSectionDomainObjBuilderTest
    //{
    //    private Mock<ClassSectionRepo> mockRepo;
    //    private Mock<UserRepo> mockUserRepo;
    //    private ClassSection mockClassSection;
    //    private User mockUser;
    //    private ClassSectionDomainObjBuilder builder;

    //    //TEST_TODO: Restore this test after CUDServices are done.
    //    //[SetUp]
    //    //public void setup()
    //    //{
    //    //    mockClassSection = new ClassSection();
    //    //    mockClassSection.ClassSectionId = 0;
    //    //    mockClassSection.CourseId = 1;
    //    //    mockClassSection.Name = "Some name";
    //    //    mockClassSection.LastDisplayedClassMeetingId = 2;


    //    //    mockRepo = new Mock<ClassSectionRepo>();
    //    //    mockRepo.Setup(mock => mock.GetById(It.IsAny<int>())).Returns(
    //    //        (int i) =>
    //    //        {
    //    //            if (i == 0) return mockClassSection;
    //    //            else return null;
    //    //        }
    //    //    );

    //    //    mockUser = new User();
    //    //    mockUser.FirstName = "Mock";
    //    //    mockUser.LastName = "User";
    //    //    mockUser.UserId = 0;

    //    //    mockUserRepo = new Mock<UserRepo>();
    //    //    mockUserRepo.Setup(mock => mock.GetById(It.IsAny<int>())).Returns(
    //    //        (int i) =>
    //    //        {
    //    //            if (i == 0) return mockUser;
    //    //            else return null;
    //    //        }
    //    //    );


    //    //    builder = new ClassSectionDomainObjBuilder(mockRepo.Object, mockUserRepo.Object);
    //    //}


    //    private void assertEquality(ClassSectionDomainObj domainObj)
    //    {
    //        Assert.AreEqual(0, domainObj.ClassSectionId);
    //        Assert.AreEqual(1, domainObj.CourseId);
    //        Assert.AreEqual("Some name", domainObj.Name);
    //        Assert.AreEqual(2, domainObj.LastDisplayedClassMeetingId);
    //    }


    //    [Test]
    //    public void testBuildBasic()
    //    {
    //        ClassSectionDomainObjBasic basicObj = builder.BuildBasic(mockClassSection);
    //        Assert.AreEqual(0, basicObj.Id);
    //    }


    //    [Test]
    //    public void testBuildBasicFromId()
    //    {
    //        ClassSectionDomainObjBasic basicObj = builder.BuildBasicFromId(0);
    //        Assert.AreEqual(0, basicObj.Id);
    //    }


    //    [Test]
    //    public void testBuild()
    //    {
    //        ClassSectionDomainObj domainObj = builder.Build(mockClassSection);
    //        Assert.NotNull(domainObj);
    //        assertEquality(domainObj);

    //    }

    //    [Test]
    //    public void testBuildFromId()
    //    {
    //        ClassSectionDomainObj domainObj = builder.BuildFromId(0);
    //        Assert.NotNull(domainObj);
    //        assertEquality(domainObj);
    //    }


    //    [Test]
    //    public void testBuildWithNullArgumentThrows()
    //    {
    //        Assert.Throws<NullReferenceException>(delegate { builder.Build(null); });
    //    }


    //    [Test]
    //    public void testBuildFromIdWithBadIdThrows()
    //    {
    //        Assert.Throws<NullReferenceException>(delegate { builder.BuildFromId(-1); });
    //    }


    //    [Test]
    //    public void testBuildBasicWithNullArgumentThrows()
    //    {
    //        Assert.Throws<NullReferenceException>(delegate { builder.BuildBasic(null); });
    //    }


    //    [Test]
    //    public void testBuildBasicFromIdWithBadIdThrows()
    //    {
    //        Assert.Throws<NullReferenceException>(delegate { builder.BuildBasicFromId(-1); });
    //    }
    //}
}

using System;

using NUnit.Framework;
using CWMasterTeacherDomain.DomainObjects;

namespace CWTesting.Tests.CWMasterTeacherDomain

{
    //[TestFixture]
    //class TermDomainObjTest
    //{
    //    private TermDomainObjBasic basicObj;
    //    private TermDomainObj domainObj;

    //    [SetUp]
    //    protected void setup()
    //    {
    //        basicObj = new TermDomainObjBasic(1, "example");
    //        domainObj = new TermDomainObj(basicObj);
    //        domainObj.TermId = 1;
    //        domainObj.InstitutionId = 1;
    //        domainObj.Name = "Some Term name";
    //        domainObj.StartDate = new DateTime(2016, 10, 10, 0, 0, 0);
    //        domainObj.IsCurrent = false;
    //    }

    //    [Test]
    //    public void testGetIdReturnsBasicObjId()
    //    {
    //        Assert.AreEqual(basicObj.Id, domainObj.Id);
    //    }

    //    [Test]
    //    public void testGetTermIdReturnsId()
    //    {
    //        Assert.AreEqual(domainObj.Id, domainObj.TermId);
    //    }

    //    [Test]
    //    public void testGetInstitutionId()
    //    {
    //        Assert.AreEqual(1, domainObj.InstitutionId);
    //    }


    //    [Test]
    //    public void testGetStartDate()
    //    {
    //        Assert.AreEqual(new DateTime(2016, 10, 10, 0, 0, 0), domainObj.StartDate);
    //    }


    //    [Test]
    //    public void testGetIsCurrent()
    //    {
    //        Assert.AreEqual(false, domainObj.IsCurrent);
    //    }


    //    // Setter tests.


    //    [Test]
    //    public void testSetIsCurrent()
    //    {
    //        var expected = false;
    //        domainObj.IsCurrent = expected;
    //        Assert.AreEqual(expected, domainObj.IsCurrent);
    //    }

    //    [Test]
    //    public void testSetIsnstitutionId()
    //    {
    //        var expected = 5;
    //        domainObj.InstitutionId = expected;
    //        Assert.AreEqual(expected, domainObj.InstitutionId);
    //    }

    //    [Test]
    //    public void testSetTermId()
    //    {
    //        var expected = 10;
    //        domainObj.TermId = expected;
    //        Assert.AreEqual(expected, domainObj.TermId);
    //    }


    //    [Test]
    //    public void testSetStartTime()
    //    {
    //        var expected = new DateTime(2018, 11, 12, 13, 14, 15);
    //        domainObj.StartDate = expected;
    //        Assert.AreEqual(expected, domainObj.StartDate);
    //    }
    //}
}
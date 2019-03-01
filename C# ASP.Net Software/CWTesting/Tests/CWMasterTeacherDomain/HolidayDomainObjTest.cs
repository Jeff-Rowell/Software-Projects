using System;
using NUnit.Framework;
using CWMasterTeacherDomain.DomainObjects;

namespace CWTesting.Tests.CWMasterTeacherDomain

{
    //[TestFixture]
    //class HolidayDomainObjTest
    //{
    //    private HolidayDomainObjBasic basicObj;
    //    private HolidayDomainObj domainObj;

    //    [SetUp]
    //    protected void setup()
    //    {
    //        basicObj = new HolidayDomainObjBasic(1, "Some Holiday");
    //        domainObj = new HolidayDomainObj(basicObj);
    //        domainObj.TermId = 1;
    //        domainObj.HolidayId = 1;
    //        domainObj.Name = "Some Holiday";
    //        domainObj.Date = new DateTime(2016, 10, 10, 0, 0, 0);
    //    }

    //    [Test]
    //    public void testGetIdReturnsBasicObjId()
    //    {
    //        Assert.AreEqual(basicObj.Id, domainObj.Id);
    //    }

    //    [Test]
    //    public void testGetHolidayId()
    //    {
    //        Assert.AreEqual(domainObj.Id, domainObj.HolidayId);
    //    }

    //    [Test]
    //    public void testGetTermId()
    //    {
    //        Assert.AreEqual(1, domainObj.TermId);
    //    }

    //    [Test]
    //    public void testGetDate()
    //    {
    //        Assert.AreEqual(new DateTime(2016, 10, 10, 0, 0, 0), domainObj.Date);
    //    }

    //    [Test]
    //    public void testGetName()
    //    {
    //        Assert.AreEqual("Some Holiday", domainObj.Name);
    //    }

    //    [Test]
    //    public void testGetDisplayName()
    //    {
    //        Assert.AreEqual(domainObj.Date.Date.ToString("MM/dd/yyyy") + "__" + domainObj.Name, domainObj.DisplayName);
    //    }

    //    // Setter tests.


    //    [Test]
    //    public void testSetHolidayId()
    //    {
    //        var expected = 5;
    //        domainObj.HolidayId = expected;
    //        Assert.AreEqual(expected, domainObj.HolidayId);
    //    }

    //    [Test]
    //    public void testSetTermId()
    //    {
    //        var expected = 10;
    //        domainObj.TermId = expected;
    //        Assert.AreEqual(expected, domainObj.TermId);
    //    }


    //    [Test]
    //    public void testSetDate()
    //    {
    //        var expected = new DateTime(2018, 11, 12, 13, 14, 15);
    //        domainObj.Date = expected;
    //        Assert.AreEqual(expected, domainObj.Date);
    //    }
    //    [Test]
    //    public void testSetName()
    //    {
    //        string expected = "Some other Holiday";
    //        domainObj.Name = expected;
    //        Assert.AreEqual(expected, domainObj.Name);
    //    }
    //}
}
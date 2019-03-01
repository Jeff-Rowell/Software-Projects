using System;
using NUnit.Framework;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;

namespace CWTesting.Tests.CWMasterTeacherDomain.ViewObjects
{
    [TestFixture]
    class TermViewObjTest
    {
        //    private TermDomainObjBasic termBasic;
        //    private TermDomainObj term;
        //    private TermDomainObj termNull;
        //    private HolidayDomainObjBasic holidayBasic;
        //    private HolidayDomainObj holiday;
        //    private HolidayDomainObj holidayNull;

        //    [SetUp]
        //    public void setup()
        //    {
        //        termBasic = new TermDomainObjBasic(1, "test");
        //        term = new TermDomainObj(termBasic);
        //        term.TermId = 1;
        //        term.InstitutionId = 1;
        //        term.Name = "Term Test";
        //        term.StartDate = new DateTime(2010, 8, 18, 16, 32, 0);
        //        term.EndDate = new DateTime(1999, 8, 18, 16, 32, 0);
        //        term.IsCurrent = true;

        //        holidayBasic = new HolidayDomainObjBasic(1, "test holiday");
        //        holiday = new HolidayDomainObj(holidayBasic);
        //        holiday.HolidayId = 1;
        //        holiday.Name = "Holiday Test";
        //    }

        //    [Test]
        //    public void TermViewObj_TestIfTermAndHolidayAreNotNull()
        //    {
        //        TermAdminViewObj termViewObj = new TermAdminViewObj(term, holiday);

        //        Assert.That(termViewObj.TermName == "Term Test");
        //        Assert.That(termViewObj.TermId == 1);
        //        Assert.That(termViewObj.StartDate == new DateTime(2010, 8, 18, 16, 32, 0));
        //        Assert.That(termViewObj.EndDate == new DateTime(1999, 8, 18, 16, 32, 0));
        //        Assert.That(termViewObj.IsCurrent == true);
        //        Assert.That(termViewObj.HolidayName == "Holiday Test");
        //        Assert.That(termViewObj.HolidayId == 1);
        //    }

        //    [Test]
        //    public void TermViewObj_TestIfTermIsNotNull()
        //    {
        //        TermAdminViewObj termViewObj = new TermAdminViewObj(term, holiday);

        //        Assert.That(termViewObj.TermName == "Term Test");
        //        Assert.That(termViewObj.TermId == 1);
        //        Assert.That(termViewObj.StartDate == new DateTime(2010, 8, 18, 16, 32, 0));
        //        Assert.That(termViewObj.EndDate == new DateTime(1999, 8, 18, 16, 32, 0));
        //        Assert.That(termViewObj.IsCurrent == true);

        //    }

        //    [Test]
        //    public void TermViewObj_TestIfTermIsNull()
        //    {
        //        TermAdminViewObj termViewObj = new TermAdminViewObj(termNull, holiday);

        //        Assert.That(termViewObj.TermId == -1);
        //    }

        //    [Test]
        //    public void TermViewObj_TestIfHolidayIsNotNull()
        //    {
        //        TermAdminViewObj termViewObj = new TermAdminViewObj(term, holiday);

        //        Assert.That(termViewObj.HolidayName == "Holiday Test");
        //        Assert.That(termViewObj.HolidayId == 1);
        //    }

        //    [Test]
        //    public void TermViewObj_TestIfHolidayIsNull()
        //    {
        //        TermAdminViewObj termViewObj = new TermAdminViewObj(term, holidayNull);

        //        Assert.That(termViewObj.HolidayId == -1);
        //    }

        //    [Test]
        //    public void TermViewObj_TestIfTermAndHolidayAreNull()
        //    {
        //        TermAdminViewObj termViewObj = new TermAdminViewObj(termNull, holidayNull);

        //        Assert.That(termViewObj.TermId == -1);
        //        Assert.That(termViewObj.HolidayId == -1);
        //    }
    }
}
using System;
using NUnit.Framework;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDataModel;
using CWMasterTeacher3;
using System.Collections.Generic;

namespace CWTesting.Tests.CWMasterTeacher3
{
    //[TestFixture]
    //class TermModelFactoryTest
    //{

    //    private TermDomainObjBasic termBasic;
    //    private TermDomainObj termObj;
    //    private HolidayDomainObjBasic holidayBasic;
    //    private HolidayDomainObj holidayObj;
    //    private Institution institution;
    //    private TermAdminReturnModel model;
    //    private readonly bool ShowDelete = false;
    //    private TermAdminViewObj viewObj;

    //    [SetUp]
    //    public void setup()
    //    {
    //        institution = new Institution();
    //        institution.Id = Guid.NewGuid();
    //        termBasic = new TermDomainObjBasic(Guid.NewGuid(), "First Term");
    //        termObj = new TermDomainObj(termBasic);
    //        termObj.StartDate = new DateTime(2000, 1, 1);
    //        termObj.EndDate = new DateTime(2000, 5, 24);
    //        termObj.IsCurrent = true;
    //        termObj.Holidays = new List<HolidayDomainObj>(1);
    //        holidayBasic = new HolidayDomainObjBasic(Guid.NewGuid (), "Easter");
    //        holidayObj = new HolidayDomainObj(holidayBasic);
    //        termObj.Holidays.Add(holidayObj);
    //        viewObj = new TermAdminViewObj();
    //        //model = factory.BuildTermViewModel(termObj, holidayObj, ShowDelete, institution);
    //    }

    //    [Test]
    //    public void modelNameAndIdTest()
    //    {
    //        Assert.AreEqual(model.Name, termObj.Name);
    //        Assert.AreEqual(model.SelectedTermId, termObj.Id);
    //        Assert.AreEqual(model.HolidayName, holidayObj.Name);
    //        Assert.AreEqual(model.SelectedHolidayId, holidayObj.Id);
    //        Assert.AreEqual(model.SubmitButtonText, "Save Changes");
    //    }

    //    [Test]
    //    public void modelDatesTest()
    //    {
    //        Assert.AreEqual(model.StartDate, termObj.StartDate);
    //        Assert.AreEqual(model.EndDate, termObj.EndDate);
    //    }

    //    [Test]
    //    public void modelIsCurrentTest()
    //    {
    //        Assert.AreEqual(model.SelectedTermIsCurrent, termObj.IsCurrent);
    //    }

    //    [Test]
    //    public void modelShowDeleteTest()
    //    {
    //        Assert.AreEqual(model.ShowDeleteHoliday, ShowDelete);
    //    }

    //    [Test]
    //    public void modelCreateSubmitText()
    //    {
    //        TermDomainObjBasic createNew = new TermDomainObjBasic(Guid.NewGuid(), "Not a real Term");
    //        TermDomainObj termObj = new TermDomainObj(createNew);
    //        HolidayDomainObjBasic fakeHoliday = new HolidayDomainObjBasic(Guid.NewGuid(), "Not a real Holiday");
    //        HolidayDomainObj holidayObj = new HolidayDomainObj(fakeHoliday);

    //        TermAdminViewObj termViewObj = new TermAdminViewObj();

    //        //model = factory.BuildTermViewModel(termViewObj, ShowDelete, institution);
    //        //Assert.AreEqual(model.SubmitButtonText, "Create New Term");
    //    }
    //}
}

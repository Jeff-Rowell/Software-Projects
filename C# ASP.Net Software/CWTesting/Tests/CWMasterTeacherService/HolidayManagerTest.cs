using System;
using Moq;
using CWMasterTeacherDataModel;
using NUnit.Framework;
using CWMasterTeacherService.RetrieveServices;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDataModel.ObjectBuilders;

namespace CWTesting.Tests.CWMasterTeacherService
{
    class HolidayManagerTest
    {

        //private HolidayManager _manager;
        private HolidayDomainObj _holidayObj;
        private HolidayDomainObjBasic _holdiayBasic;

        [SetUp]
        public void Setup()
        {
            //var mockContext = new Mock<MasterTeacherContext>();
            //var mockRepo = new Mock<HolidayRepo>(mockContext.Object);
            //var mockBuilder = new Mock<HolidayDomainObjBuilder>(mockRepo.Object);
            ////_manager = new HolidayManager(mockRepo.Object);
            //_holdiayBasic = new HolidayDomainObjBasic(1, "St. Patricks's Day");
            //_holidayObj = new HolidayDomainObj(_holdiayBasic);
            //mockBuilder.Setup(x => x.BuildFromId(1)).Returns(_holidayObj);
            //_manager.SetBuilder(mockBuilder.Object);
        }

        [Test]
        public void getHolidayTest()
        {
            int holidayId = 1;
            //HolidayDomainObj foundHoliday = _manager.GetHoliday(holidayId);
            //Assert.AreEqual(foundHoliday, _holidayObj);
            //int nullHolidayId = 0;
            //foundHoliday = _holidayObj = _manager.GetHoliday(nullHolidayId);
            //Assert.IsNull(foundHoliday);
        }
    }
}

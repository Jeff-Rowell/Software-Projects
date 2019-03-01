using NUnit.Framework;
using Moq;
using CWMasterTeacherDataModel;
using CWMasterTeacherService.RetrieveServices;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.ViewObjects;

namespace CWTesting.Tests.CWMasterTeacherService
{
    [TestFixture]
    class TermManagerTest
    {

        private TermAdminService manager;
        private TermDomainObj termObj;
        private TermDomainObj editTermObj;
        private TermDomainObj term40;
        private HolidayDomainObj holiday20;
        private Mock<TermDomainObjBuilder> mockTermBuilder;
        private Mock<HolidayDomainObjBuilder> mockHolidayBuilder;

        [SetUp]
        public void setup()
        {
            var mockContext = new Mock<MasterTeacherContext>();
            var mockRepo = new Mock<TermRepo>(mockContext.Object);
            var mockHolidayRepo = new Mock<HolidayRepo>(mockContext.Object);
            termObj = new TermDomainObj(1, "First Term", new System.DateTime(2000, 1, 1),new System.DateTime(2000, 5, 24), 5);
            editTermObj = new TermDomainObj(1, "Not a real term", new System.DateTime(2000, 1, 1),new System.DateTime(2000, 5, 24), 5);
            term40 = new TermDomainObj(40, "Built Term", new System.DateTime(2000, 1, 1),
                new System.DateTime(2000, 5, 24), 5);
            holiday20 = new HolidayDomainObj(20, 40, "Holiday 20", new System.DateTime(2000, 2, 20));

            mockTermBuilder = new Mock<TermDomainObjBuilder>(mockRepo.Object);
            mockHolidayBuilder = new Mock<HolidayDomainObjBuilder>(mockHolidayRepo.Object);
            mockTermBuilder.Setup(x => x.CreateTerm(It.IsAny<TermDomainObj>())).Returns(termObj);
            mockTermBuilder.Setup(x => x.UpdateTerm(It.IsAny<TermDomainObj>()))
                .Callback((TermDomainObj term) => { return; });
            mockTermBuilder.Setup(x => x.MarkTermAsCurrent(It.IsAny<TermDomainObj>()))
                .Callback((TermDomainObj term) => { return; });
            mockTermBuilder.Setup(x => x.BuildFromId(40)).Returns(term40);
            mockHolidayBuilder.Setup(x => x.BuildFromId(20)).Returns(holiday20);
            mockHolidayBuilder.Setup(x => x.CreateHoliday(It.IsAny<HolidayDomainObj>()))
                .Callback((HolidayDomainObj holiday) => { return; });
            mockHolidayBuilder.Setup(x => x.DeleteHoliday(It.IsAny<int>()))
                .Callback((int holidayId) => { return; });

            manager = new TermAdminService(mockRepo.Object, mockHolidayRepo.Object, mockTermBuilder.Object, mockHolidayBuilder.Object);

        }

        [Test]
        public void createOrEditTermTest()
        {
            int returnId = manager.CreateOrEditTerm(termObj);
            Assert.AreEqual(returnId, termObj.Id);

            int editReturnId = manager.CreateOrEditTerm(editTermObj);
            Assert.AreEqual(editReturnId, editTermObj.Id);
        }

        [Test]
        public void getTermTest()
        {
            int termId = 40;
            TermDomainObj foundTerm = manager.GetTerm(termId);
            Assert.AreEqual(foundTerm, term40);
            TermDomainObj nullTerm = manager.GetTerm(-1);
            Assert.IsNull(nullTerm);
        }

        [Test]
        public void createTermTest()
        {
            manager.CreateTerm(termObj);
            mockTermBuilder.Verify(x => x.CreateTerm(termObj));
        }

        [Test]
        public void editTermTest()
        {
            manager.EditTerm(termObj);
            mockTermBuilder.Verify(x => x.UpdateTerm(termObj));
        }

        [Test]
        public void markTermAsCurrentTest()
        {
            manager.MarkTermAsCurrent(termObj);
            mockTermBuilder.Verify(x => x.MarkTermAsCurrent(termObj));
        }

        [Test]
        public void addHolidayTest()
        {
            manager.AddHoliday(holiday20);
            mockHolidayBuilder.Verify(x => x.CreateHoliday(holiday20));
        }

        [Test]
        public void deleteHolidayTest()
        {
            int fakeHolidayId = 3;
            manager.DeleteHoliday(fakeHolidayId);
            mockHolidayBuilder.Verify(x => x.DeleteHoliday(fakeHolidayId));
        }

        [Test]
        public void getHolidayTest()
        {
            int holidayId = 20;
            HolidayDomainObj foundHoliday = manager.GetHoliday(holidayId);
            Assert.AreEqual(foundHoliday, holiday20);
            HolidayDomainObj nullHoliday = manager.GetHoliday(-1);
            Assert.IsNull(nullHoliday);
        }

        [Test]
        public void buildViewObjectTest()
        {
            TermAdminViewObj termView = manager.BuildViewObj(40, 20);
            Assert.AreEqual(termView.TermId, term40.Id);
            Assert.AreEqual(termView.TermName, term40.Name);
            Assert.AreEqual(termView.StartDate, term40.StartDate);
            Assert.AreEqual(termView.EndDate, term40.EndDate);
            Assert.AreEqual(termView.HolidayId, holiday20.Id);
            Assert.AreEqual(termView.HolidayName, holiday20.Name);
        }
    }
}

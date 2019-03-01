
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CWTesting.Tests.Selenium
{
    [TestFixture]
    class SeleniumTest
    {
        private IWebDriver _driver;
        private CurriculumPage _curriculum;

        [OneTimeSetUp]
        public void Initialize()
        {
            _driver = new ChromeDriver();
            var loginPage = new LoginPage(_driver);
            loginPage.TypeUsername("olive");
            loginPage.TypePassword("TestPass1");
            _curriculum = loginPage.Login();
        }

        [Test]
        public void ExecuteTest()
        {
            LessonComparisonPage lCPage = _curriculum.navigateToLessonComparison();
            lCPage.TypeInEditor("Checking to see if this works");
            lCPage.Save();
            lCPage.ShowDifferences();
            lCPage.SwitchToTeachingNotes();
            lCPage.TypeInEditor("Typing something else");
            lCPage.Save();
            lCPage.ShowDifferences();
        }

        [TearDown]
        public void CleanUp()
        {
            _driver.Close();
        }
    }
}

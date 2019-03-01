using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CWTesting.Tests.Selenium
{
    class CurriculumPage
    {
        private IWebDriver _driver;

        public CurriculumPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public LessonComparisonPage navigateToLessonComparison()
        {
            _driver.FindElement(By.XPath("//div[@class='col-md-2']//button")).Click();
            _driver.FindElement(By.XPath("//div[@class='col-md-2']//ul/li[4]")).Click();
            return new LessonComparisonPage(_driver);
        }
    }
}

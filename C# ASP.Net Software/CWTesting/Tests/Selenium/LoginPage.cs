using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CWTesting.Tests.Selenium
{
    class LoginPage
    {
        private IWebDriver _driver;
        private String _loginURL = "http://localhost:52334//Account//Login?ReturnUrl=%2F";
        private By _uNameBy = By.Id("UserName");
        private By _pWordBy = By.Id("Password");

        public LoginPage(IWebDriver driver)
        {
            this._driver = driver;
            if (!driver.Title.Equals(_loginURL))
            {
                driver.Navigate().GoToUrl(_loginURL);
            }
        }

        public LoginPage TypeUsername(String uName)
        {
            _driver.FindElement(_uNameBy).SendKeys(uName);
            return this;
        }

        public LoginPage TypePassword(String pWord)
        {
            _driver.FindElement(_pWordBy).SendKeys(pWord);
            return this;
        }

        public CurriculumPage Login()
        {
            _driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
            return new CurriculumPage(_driver);
        }
    }
}

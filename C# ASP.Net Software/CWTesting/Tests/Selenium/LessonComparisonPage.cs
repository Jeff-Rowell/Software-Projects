using OpenQA.Selenium;

namespace CWTesting.Tests.Selenium
{
    public class LessonComparisonPage
    {
        private IWebDriver _driver;
        private PageStatus _status;

        private enum PageStatus
        {
            Narrative, TeachingNotes, Documents
        }

        public LessonComparisonPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public LessonComparisonPage ShowDifferences()
        {
            _driver.FindElement(By.XPath("//input[@value='Show Differences']")).Click();
            return this;
        }

        public LessonComparisonPage TypeInEditor(string input)
        {
            IWebElement textBox;
            if (_status == PageStatus.Narrative)
            {
                textBox = _driver.FindElement(By.XPath("//*[@id='editableNarrative_ifr']"));
            }
            else
            {
                textBox = _driver.FindElement(By.XPath("//*[@id='editableLessonPlan_ifr']"));
            }
            _driver.SwitchTo().Frame(textBox);
            var box = _driver.FindElement(By.XPath("//*[@id='tinymce']"));
            box.SendKeys(input);
            _driver.SwitchTo().DefaultContent();
            return this;
        }

        public LessonComparisonPage Save()
        {
            _driver.FindElement(By.XPath("//input[@value='Save Changes']")).Click();
            return this;
        }

        public LessonComparisonPage SwitchToTeachingNotes()
        {
            _driver.FindElement(By.LinkText("Teaching Notes (0)")).Click();
            _status = PageStatus.TeachingNotes;
            return this;
        }

        public LessonComparisonPage SwitchToNarrative()
        {
            _driver.FindElement(By.LinkText("Narrative (0)")).Click();
            _status = PageStatus.Narrative;
            return this;
        }
    }
}

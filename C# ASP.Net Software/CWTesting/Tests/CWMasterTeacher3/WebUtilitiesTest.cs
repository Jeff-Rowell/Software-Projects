using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CWMasterTeacher3;

namespace CWTesting
{
    [TestFixture]
    class WebUtilitiesTest
    {
        private DateTime testDate;

        [SetUp]
        public void SetUp()
        {
            testDate = new DateTime(2015, 10, 8, 13, 20, 0);
        }

        [Test]
        public void DateTime_ToLongDateString_FormatTest()
        {
            string expectedDateFormat = "Thu, Oct 8";
            string longDateFormat = WebUtilities.DateTime_ToLongDateString(testDate);
            Assert.That(expectedDateFormat, Is.EqualTo(longDateFormat));

        }

        [Test]
        public void DateTime_ToDateStringTest_FormatTest()
        {
            string expectedDateFormat = "Oct 8, 2015";
            string toDateString = WebUtilities.DateTime_ToDateString(testDate);
            Assert.That(toDateString, Is.EqualTo(expectedDateFormat));
        }

        [Test]
        public void UpArrowClass_ExpectedValue()
        {
            string expectedWhenTrue = "cw-arrow-up";
            string expectedWhenFalse = "cw-arrow-up-disabled";

            string upArrowClassTrue = WebUtilities.UpArrowClass(true);
            string upArrowClassFalse = WebUtilities.UpArrowClass(false);

            Assert.That(upArrowClassTrue, Is.EqualTo(expectedWhenTrue));
            Assert.That(upArrowClassFalse, Is.EqualTo(expectedWhenFalse));
        }
    }
}

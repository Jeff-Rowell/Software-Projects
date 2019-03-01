using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain;

namespace CWTesting.Tests.CWMasterTeacherDomain.DomainObjects
{
    [Author("Jeremy & Brunno")]
    [TestFixture]
    class LessonPlanDomainObjBasicTest
    {
        LessonPlanDomainObjBasic basicObj;
        LessonPlanDomainObj lessonObj;
        
        [SetUp]
        public void setup()
        {
            basicObj = new LessonPlanDomainObjBasic();

            basicObj.Name = "TestName";
            basicObj.LessonPlanId = 13;
            basicObj.Text = "Today's Objectives";
        }
        [Test]
        public void LessonPlanDominaObject_testSetGetName()
        {
            Assert.That(basicObj.Name == "TestName");
        }
        [Test]
        public void LessonPlanDominaObject_testSetGetId()
        {
            Assert.That(basicObj.Id == 13);
        }
        [Test]
        public void LessonPlanDominaObject_TestSetGetText()
        {
            Assert.AreEqual(basicObj.Text, "Today's Objectives");
        }
    }
}

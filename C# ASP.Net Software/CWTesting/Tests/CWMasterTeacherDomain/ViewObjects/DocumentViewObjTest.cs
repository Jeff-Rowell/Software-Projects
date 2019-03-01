using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;

namespace CWTesting.Tests.CWMasterTeacherDomain
{
    //[TestFixture]
    //class DocumentViewObjTest
    //{
    //    [SetUp]
    //    public void setup() {

    //    }
    //    [Test]
    //    [Author("Tommy Hoang")]
    //    public void DocumentViewObj_TestConstructor() {
    //        DocumentDomainObjBasic docDOobjBasic = new DocumentDomainObjBasic(123, "TestFile", "TestFile", "Note");
    //        DateTime datetime = DateTime.Now;
    //        DocumentDomainObj ddo = new DocumentDomainObj(docDOobjBasic, datetime, true, true, true, false, ".pdf");
    //        DocumentViewObj dVO = new DocumentViewObj(ddo);
    //        Assert.That(dVO.Document, Is.EqualTo(ddo));

    //    }

    //    [Test]
    //    [Author("Tommy Hoang")]
    //    public void DocumentViewObj_TestFullyParameterizedConstructor() {
    //        int documentUseID = 123;
    //        DocumentUseDomainObjBasic docUseDomainObjBasic = new DocumentUseDomainObjBasic(documentUseID);
    //        int parentLessonID = 456;
    //        DocumentDomainObjBasic docDomainObjBasic = new DocumentDomainObjBasic(123, "TestFile", "TestFile", "Note");
    //        DateTime datetime = DateTime.Now;
    //        DocumentDomainObj docDomainObj = new DocumentDomainObj(docDomainObjBasic, datetime, false, false, true, true, ".pdf");
    //        DocumentUseDomainObj docUseDomainObj = new DocumentUseDomainObj(docUseDomainObjBasic, docDomainObj, parentLessonID,
    //            true, true, true, true, true, "Document Name");
    //        int possibleParentID = 456;
    //        DocumentViewObj docVObj= new DocumentViewObj(docUseDomainObj,possibleParentID);
    //        Assert.That(docVObj.Document.IsReferenceInLesson,Is.EqualTo(docUseDomainObj.IsReference));
    //        Assert.That(docVObj.Document.IsUsedInLesson, Is.EqualTo(true));
    //    }


    //    //TEST_TODO: I killed this test because of an error due to changes in DocumentDomainObjBasic.  It needs to be fixed. (Dollard)
    //    //[Test]
    //    //[Author("Tommy Hoang")]
    //    //public void DisplayNameExtended_TEST1stCase() {
    //    //    DocumentDomainObjBasic docDomainObjBasic = new DocumentDomainObjBasic(123, "TestFile", "TestFile", "Note");
    //    //    DateTime datetime = DateTime.Now;
    //    //    DocumentDomainObj docDomainObj = new DocumentDomainObj(docDomainObjBasic, datetime, true, true, true, true, ".pdf");
    //    //    DocumentViewObj docVObj = new DocumentViewObj(docDomainObj);
    //    //    Assert.That(docVObj.DisplayNameExtended, Is.EqualTo("*   " +
    //    //        docDomainObj.BasicObj.DisplayName));
    //    //}

    //    //TEST_TODO: I killed this test because of an error due to changes in DocumentDomainObjBasic.  It needs to be fixed. (Dollard)
    //    //[Test]
    //    //[Author("Tommy Hoang")]
    //    //public void DisplayNameExtended_TEST2ndCase() {
    //    //    DocumentDomainObjBasic docDomainObjBasic = new DocumentDomainObjBasic(123, "TestFile", "TestFile", "Note");
    //    //    DateTime datetime = DateTime.Now;
    //    //    DocumentDomainObj docDomainObj2 = new DocumentDomainObj(docDomainObjBasic, datetime, true, false, true, true, ".pdf");
    //    //    DocumentViewObj docVObj2 = new DocumentViewObj(docDomainObj2);
    //    //    Assert.That(docVObj2.DisplayNameExtended, Is.EqualTo("*** " +
    //    //            docDomainObj2.BasicObj.DisplayName));
    //    //}
    //    //TEST_TODO: I killed this test because of an error due to changes in DocumentDomainObjBasic.  It needs to be fixed. (Dollard)
    //    //[Test]
    //    //[Author("Tommy Hoang")]
    //    //public void DisplayNameExtended_TEST3rdCase() {
    //    //    DocumentDomainObjBasic docDomainObjBasic = new DocumentDomainObjBasic(123, "TestFile", "TestFile", "Note");
    //    //    DateTime datetime = DateTime.Now;
    //    //    DocumentDomainObj docDomainObj3 = new DocumentDomainObj(docDomainObjBasic, datetime, false, false, true, true, ".pdf");
    //    //    DocumentViewObj docVObj3 = new DocumentViewObj(docDomainObj3);
    //    //    Assert.That(docVObj3.DisplayNameExtended, Is.EqualTo("    " +
    //    //        docDomainObj3.BasicObj.DisplayName));
    //    //}
    //}
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDomain.DomainObjects;

namespace CWTesting.Tests.CWMasterTeacherDomain.ViewObjects
{
    [TestFixture]
    class DocumentUseViewObjTest
    {
        //private DocumentUseViewObj testDocumentUseViewObj;
        //private DocumentDomainObjBasic testDocumentBasic;
        //private DocumentDomainObj testDocument;
        //private DocumentUseDomainObjBasic testDocumentUseBasic;
        //private DocumentUseDomainObj testDocumentUse;
        //private static int testDocumentId = 1;
        //private static string testName = "testName";

        //[TestCase(false, 1, 2, "cw-document ")]
        //[TestCase(true,  1, 2, "cw-document-outforedit ")]
        //[TestCase(false, 1, 1, "cw-document cw-list-selected-2")]
        //[TestCase(true,  1, 1, "cw-document-outforedit cw-list-selected-2")]
        //[Author("Jason Gould")]
        //public void GetDisplayClass_ShouldConstructTheDisplayClassCorrectly(bool isOutForEdit, int actualDocumentUseId, int selectedDocumentUseId, string expectedResult)
        //{
        //    testDocumentBasic = new DocumentDomainObjBasic(testDocumentId, testName, "testDisplayName", "testModificationNotes");
        //    testDocument = new DocumentDomainObj(testDocumentBasic, null, true, true, true, true, ".txt");
        //    testDocumentUseBasic = new DocumentUseDomainObjBasic(actualDocumentUseId);
        //    testDocumentUse = new DocumentUseDomainObj(testDocumentUseBasic, testDocument, 1, true, true, false, true, isOutForEdit, "Document Name");
        //    testDocumentUseViewObj = new DocumentUseViewObj(testDocumentUse, selectedDocumentUseId);

        //    Assert.That(testDocumentUseViewObj.DisplayClass, Is.EqualTo(expectedResult));
        //}

        //[TestCase(false, "testDisplayName......(testModificationNotes)")]
        //[TestCase(true,  "testDisplayName......(OUT FOR EDIT!!!)")]
        //[Author("Jason Gould")]
        //public void DisplayName_ShouldConstructTheDisplayNameCorrectly(bool isOutForEdit, string expectedResult)
        //{
        //    testDocumentBasic = new DocumentDomainObjBasic(testDocumentId, testName, "testDisplayName", "testModificationNotes");
        //    testDocument = new DocumentDomainObj(testDocumentBasic, null, true, true, true, true, ".txt");
        //    testDocumentUseBasic = new DocumentUseDomainObjBasic(1);
        //    testDocumentUse = new DocumentUseDomainObj(testDocumentUseBasic, testDocument, 1, true, true, false, true, isOutForEdit, "Document Name");
        //    testDocumentUseViewObj = new DocumentUseViewObj(testDocumentUse, 1);

        //    Assert.That(testDocumentUseViewObj.DisplayName, Is.EqualTo(expectedResult));
        //}
    }
}

using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWTesting.Tests.CWMasterTeacherDomain
{
    [TestFixture]
    public class DocumentsTabViewObjTest
    {
        //private static DocumentUseDomainObjBasic testDocumentUseBasic = new DocumentUseDomainObjBasic(1);
        //private static DocumentDomainObjBasic testDocumentBasic = new DocumentDomainObjBasic(2, "testName", "testDisplayName", "testModificationNotes");
        //private static DocumentDomainObj testDocument = new DocumentDomainObj(testDocumentBasic, null, true, true, false, true, ".txt");
        //private static DocumentDomainObj testDocumentIsPdf = new DocumentDomainObj(testDocumentBasic, null, true, true, true, false, ".txt");
        //private static DocumentUseDomainObj testReferenceDocumentUse = new DocumentUseDomainObj(testDocumentUseBasic, testDocument, 1, true, true, false, true, true, "Document Name");
        //private static DocumentUseDomainObj testInstructorDocumentUse = new DocumentUseDomainObj(testDocumentUseBasic, testDocumentIsPdf, 1, true, false, true, true, false, "Document Name");
        //private static DocumentUseDomainObj testTeachingDocumentUse = new DocumentUseDomainObj(testDocumentUseBasic, testDocument, 1, true, false, false, true, false, "Document Name");
        //private static DocumentUseViewObj testReferenceDocUseViewObj = new DocumentUseViewObj(testReferenceDocumentUse, 1);
        //private static DocumentUseViewObj testInstructorDocUseViewObj = new DocumentUseViewObj(testInstructorDocumentUse, 1);
        //private static DocumentUseViewObj testTeachingDocUseViewObj = new DocumentUseViewObj(testTeachingDocumentUse, 1);
        //private static List<DocumentUseViewObj> testAllDocUsesList = new List<DocumentUseViewObj> { testReferenceDocUseViewObj, testInstructorDocUseViewObj, testTeachingDocUseViewObj };
        //private static DocumentViewObj testDocumentViewObj = new DocumentViewObj(testDocument);

        //private static object[] allDocUsesLists =
        //{
        //    testAllDocUsesList,
        //    new List<DocumentUseViewObj> { }
        //};

        //private static object[] instructorDocUseViewObjLists =
        //{
        //     new List<DocumentUseViewObj> { testInstructorDocUseViewObj },
        //     new List<DocumentUseViewObj> { }
        //};

        //[Test]
        //[Sequential]
        //[Author("Joey Idler")]
        //public void DocumentsTabViewObj_CorrectlyInitInstructorOnlyDocumentUsesAndGroupDocStorageButtonText(
        //    [ValueSource("allDocUsesLists")]List<DocumentUseViewObj> allDocUses,
        //    [ValueSource("instructorDocUseViewObjLists")]List<DocumentUseViewObj> expected)
        //{
        //    DocumentsTabViewObj viewObj = new DocumentsTabViewObj(allDocUses, null, null, null, 0);

        //    Assert.That(viewObj.InstructorOnlyDocumentUses, Is.EqualTo(expected));
        //}

        //private static object[] referenceDocUseViewObjLists =
        //{
        //    new List<DocumentUseViewObj> { testReferenceDocUseViewObj },
        //    new List<DocumentUseViewObj> { }
        //};

        //[Test]
        //[Sequential]
        //[Author("Joey Idler")]
        //public void DocumentsTabViewObj_CorrectlyInitReferenceDocumentUses(
        //    [ValueSource("allDocUsesLists")]List<DocumentUseViewObj> allDocUses,
        //    [ValueSource("referenceDocUseViewObjLists")]List<DocumentUseViewObj> expected)
        //{
        //    DocumentsTabViewObj viewObj = new DocumentsTabViewObj(allDocUses, null, null, null, 0);

        //    Assert.That(viewObj.ReferenceDocumentUses, Is.EqualTo(expected));
        //}

        //private static object[] teachingDocUseViewObjLists =
        //{
        //    new List<DocumentUseViewObj> { testTeachingDocUseViewObj },
        //    new List<DocumentUseViewObj> { }
        //};

        //[Test]
        //[Sequential]
        //[Author("Joey Idler")]
        //public void DocumentsTabViewObj_CorrectlyInitTeachingDocumentUses(
        //    [ValueSource("allDocUsesLists")]List<DocumentUseViewObj> allDocUses,
        //    [ValueSource("teachingDocUseViewObjLists")]List<DocumentUseViewObj> expected)
        //{
        //    DocumentsTabViewObj viewObj = new DocumentsTabViewObj(allDocUses, null, null, null, 0);

        //    Assert.That(viewObj.TeachingDocumentUses, Is.EqualTo(expected));
        //}

        //private static object[] allDocsLists =
        //{
        //    new List<DocumentViewObj> { testDocumentViewObj, testDocumentViewObj },
        //    null
        //};

        //private static object[] allDocumentsLists =
        //{
        //    new List<DocumentViewObj> { testDocumentViewObj },
        //    null
        //};

        //private static string[] groupDocStorageButtonText =
        //{
        //    "Hide Group Document Storage",
        //    "Show Group Document Storage"
        //};

        //[Test]
        //[Sequential]
        //[Author("Joey Idler")]
        //public void DocumentsTabViewObj_CorrectlyInitAllDocuments(
        //    [ValueSource("allDocsLists")]List<DocumentViewObj> allDocs, [ValueSource("allDocumentsLists")]List<DocumentViewObj> expectedDocViewObjList,
        //    [ValueSource("groupDocStorageButtonText")]string expectedGroupDocStorageButtonText)
        //{
        //    DocumentsTabViewObj viewObj = new DocumentsTabViewObj(testAllDocUsesList, null, null, allDocs, 1);

        //    Assert.That(viewObj.AllDocuments, Is.EqualTo(expectedDocViewObjList));
        //    Assert.That(viewObj.GroupDocStorageButtonText, Is.EqualTo(expectedGroupDocStorageButtonText));
        //}

        ////private static string[] displayName =
        ////{
        ////    testDocumentBasic.DisplayName,
        ////    testDocumentBasic.DisplayName,
        ////    null
        ////};

        //private static DocumentUseDomainObj[] selectedDocumentUseForNames =
        //{
        //    testReferenceDocumentUse,
        //    null,
        //    null
        //};

        //private static DocumentDomainObj[] selectedDocument =
        //{
        //    null,
        //    testDocument,
        //    null
        //};

        //[Test]
        //[Sequential]
        //[Author("Joey Idler")]
        //public void SelectedDocumentUseDisplayName_CorrectDocumentUseDisplayNameReturned(
        //    [ValueSource("selectedDocumentUseForNames")]DocumentUseDomainObj selectedDocumentUse,
        //    [ValueSource("selectedDocument")]DocumentDomainObj selectedDocument, [ValueSource("displayName")]string expected)
        //{
        //    DocumentsTabViewObj viewObj = new DocumentsTabViewObj(testAllDocUsesList, selectedDocumentUse, selectedDocument, null, 0);

        //    Assert.That(viewObj.SelectedDocumentUseDisplayName, Is.EqualTo(expected));
        //}

        //private static string[] names =
        //{
        //    testDocumentBasic.Name,
        //    null,
        //    null
        //};

        //[Test]
        //[Sequential]
        //[Author("Joey Idler")]
        //public void SelectedDocumentUseName_CorrectDocumentUseNameReturned(
        //    [ValueSource("selectedDocumentUseForNames")]DocumentUseDomainObj selectedDocumentUse,
        //    [ValueSource("names")]string expected)
        //{
        //    DocumentsTabViewObj viewObj = new DocumentsTabViewObj(testAllDocUsesList, selectedDocumentUse, null, null, 0);

        //    Assert.That(viewObj.SelectedDocumentName, Is.EqualTo(expected));
        //}

        //private static DocumentUseDomainObj[] selectedDocumentUse =
        //{
        //    testReferenceDocumentUse,
        //    testInstructorDocumentUse,
        //    null
        //};

        //[Test]
        //[Sequential]
        //[Author("Joey Idler")]
        //public void ShowUploadPdfCopy_ShouldSelectedDocumentUseShowUploadPdfCopy(
        //    [ValueSource("selectedDocumentUse")]DocumentUseDomainObj selectedDocumentUse,
        //    [Values(true, false, false)]bool expected)
        //{
        //    DocumentsTabViewObj viewObj = new DocumentsTabViewObj(testAllDocUsesList, selectedDocumentUse, null, null, 0);

        //    Assert.That(viewObj.ShowUploadPdfCopy, Is.EqualTo(expected));
        //}

        //[Test]
        //[Sequential]
        //[Author("Joey Idler")]
        //public void SelectedDocUseHasPdfCopy_CorrectlyDetermineIfSelectedDocumentUseHasPdfCopy(
        //    [ValueSource("selectedDocumentUse")]DocumentUseDomainObj selectedDocumentUse,
        //    [Values(true, false, false)]bool expected)
        //{
        //    DocumentsTabViewObj viewObj = new DocumentsTabViewObj(testAllDocUsesList, selectedDocumentUse, null, null, 0);

        //    Assert.That(viewObj.SelectedDocUseHasPdfCopy, Is.EqualTo(expected));
        //}

        //[Test]
        //[Sequential]
        //[Author("Joey Idler")]
        //public void DownloadDocumentLabel_ReturnCorrectLabelForSelectedDocumentUse(
        //    [ValueSource("selectedDocumentUse")]DocumentUseDomainObj selectedDocumentUse,
        //    [Values("Download ..txt", "Download ..txt", null)]string expected)
        //{
        //    DocumentsTabViewObj viewObj = new DocumentsTabViewObj(testAllDocUsesList, selectedDocumentUse, null, null, 0);

        //    Assert.That(viewObj.DownloadDocumentLabel, Is.EqualTo(expected));
        //}

        //[Test]
        //[Sequential]
        //[Author("Joey Idler")]
        //public void DownloadPdfLabel_ReturnCorrectLabelForSelectedDocumentUse(
        //    [ValueSource("selectedDocumentUse")]DocumentUseDomainObj selectedDocumentUse,
        //    [Values("Download .pdf", "Download .pdf", null)]string expected)
        //{
        //    DocumentsTabViewObj viewObj = new DocumentsTabViewObj(testAllDocUsesList, selectedDocumentUse, null, null, 0);

        //    Assert.That(viewObj.DownloadPdfLabel, Is.EqualTo(expected));
        //}

        //[Test]
        //[Sequential]
        //[Author("Joey Idler")]
        //public void DownloadDocumentToEditLabel_ReturnCorrectLabelForSelectedDocumentUse(
        //    [ValueSource("selectedDocumentUse")]DocumentUseDomainObj selectedDocumentUse,
        //    [Values("Download ..txt to Edit", null, null)]string expected)
        //{
        //    DocumentsTabViewObj viewObj = new DocumentsTabViewObj(testAllDocUsesList, selectedDocumentUse, null, null, 0);

        //    Assert.That(viewObj.DownloadDocumentToEditLabel, Is.EqualTo(expected));
        //}

        //[Test]
        //[Sequential]
        //[Author("Joey Idler")]
        //public void DownloadPdfToSaveLabel_ReturnCorrectLabelForSelectedDocumentUse(
        //    [ValueSource("selectedDocumentUse")]DocumentUseDomainObj selectedDocumentUse,
        //    [Values("Download .pdf to save", "Download .pdf to save", null)]string expected)
        //{
        //    DocumentsTabViewObj viewObj = new DocumentsTabViewObj(testAllDocUsesList, selectedDocumentUse, null, null, 0);

        //    Assert.That(viewObj.DownloadPdfToSaveLabel, Is.EqualTo(expected));
        //}

        //[Test]
        //[Sequential]
        //[Author("Joey Idler")]
        //public void SelectedDocumentUseIsReference_CorrectlyReturnIfSelectedDocumentUseIsReference(
        //    [ValueSource("selectedDocumentUse")]DocumentUseDomainObj selectedDocumentUse,
        //    [Values(true, false, false)]bool expected)
        //{
        //    DocumentsTabViewObj viewObj = new DocumentsTabViewObj(testAllDocUsesList, selectedDocumentUse, null, null, 0);

        //    Assert.That(viewObj.SelectedDocumentUseIsReference, Is.EqualTo(expected));
        //}

        //[Test]
        //[Sequential]
        //[Author("Joey Idler")]
        //public void SelectedDocumentUseIsInstructorOnly_CorrectlyReturnIfSelectedDocumentUseIsInstructorOnly(
        //    [ValueSource("selectedDocumentUse")]DocumentUseDomainObj selectedDocumentUse,
        //    [Values(false, true, false)]bool expected)
        //{
        //    DocumentsTabViewObj viewObj = new DocumentsTabViewObj(testAllDocUsesList, selectedDocumentUse, null, null, 0);

        //    Assert.That(viewObj.SelectedDocumentUseIsInstructorOnly, Is.EqualTo(expected));
        //}

        //[Test]
        //[Sequential]
        //[Author("Joey Idler")]
        //public void SelectedDocumentUseModificationNotes_ReturnCorrectModificationNotesForSelectedDocumentUse(
        //    [ValueSource("selectedDocumentUse")]DocumentUseDomainObj selectedDocumentUse,
        //    [Values("testModificationNotes", "testModificationNotes", null)]string expected)
        //{
        //    DocumentsTabViewObj viewObj = new DocumentsTabViewObj(testAllDocUsesList, selectedDocumentUse, null, null, 0);

        //    Assert.That(viewObj.SelectedDocumentUseModificationNotes, Is.EqualTo(expected));
        //}

        //[Test]
        //[Sequential]
        //[Author("Joey Idler")]
        //public void SelectedDocUseIsPdf_CorrectlyReturnIfSelectedDocumentUseIsPdf(
        //    [ValueSource("selectedDocumentUse")]DocumentUseDomainObj selectedDocumentUse,
        //    [Values(false, true, false)]bool expected)
        //{
        //    DocumentsTabViewObj viewObj = new DocumentsTabViewObj(testAllDocUsesList, selectedDocumentUse, null, null, 0);

        //    Assert.That(viewObj.SelectedDocUseIsPdf, Is.EqualTo(expected));
        //}

        //[Test]
        //[Sequential]
        //[Author("Joey Idler")]
        //public void SelectedDocUseIsOutForEdit_CorrectlyReturnIfSelectedDocumentUseIsOutForEdit(
        //    [ValueSource("selectedDocumentUse")]DocumentUseDomainObj selectedDocumentUse,
        //    [Values(true, false, false)]bool expected)
        //{
        //    DocumentsTabViewObj viewObj = new DocumentsTabViewObj(testAllDocUsesList, selectedDocumentUse, null, null, 0);

        //    Assert.That(viewObj.SelectedDocUseIsOutForEdit, Is.EqualTo(expected));
        //}
    }
}
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherService.ViewObjectBuilder;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDataModel;

namespace CWTesting.Tests.CWMasterTeacherService
{
    //[TestFixture]
    //class DocumentsTabServiceTest
    //{
    //    private static DocumentUseDomainObjBasic documentUseBasic = new DocumentUseDomainObjBasic(1);
    //    private static DocumentDomainObjBasic documentBasic = new DocumentDomainObjBasic(2, "testName", "testDisplayName", "testModificationNotes");
    //    private static DocumentDomainObj testDocument = new DocumentDomainObj(documentBasic, null, true, true, true, true, ".txt");
    //    private static DocumentUseDomainObj testDocumentUse = new DocumentUseDomainObj(documentUseBasic, testDocument, 1, true, true, false, true, false, "Document Name");
    //    private static DocumentDomainObj testActiveDocument = new DocumentDomainObj(documentBasic, null, true, true, true, true, ".txt");
    //    private static DocumentDomainObj testArchivedDocument = new DocumentDomainObj(documentBasic, null, false, false, false, false, ".txt");
    //    private static DocumentUseDomainObj testActiveDocumentUse = new DocumentUseDomainObj(documentUseBasic, testActiveDocument, 1, true, true, false, true, false, "Document Name");
    //    private static DocumentUseDomainObj testArchivedDocumentUse = new DocumentUseDomainObj(documentUseBasic, testArchivedDocument, 2, false, true, true, false, false, "Document Name");
    //    private DocumentsTabViewObjBuilder testService;

    //    [OneTimeSetUp]
    //    public void Setup()
    //    {
    //        Mock<IDocumentUseDomainObjBuilder> mockDocumentUseBuilderFactory = new Mock<IDocumentUseDomainObjBuilder>();
    //        mockDocumentUseBuilderFactory.Setup(x => x.AllDocumentUsesThatShareMasterLesson(It.IsAny<int>())).Returns(new List<DocumentUseDomainObj>() { testArchivedDocumentUse });
    //        mockDocumentUseBuilderFactory.Setup(x => x.ActiveDocumentUsesThatShareMasterLesson(It.IsAny<int>())).Returns(new List<DocumentUseDomainObj>() { testActiveDocumentUse });
    //        mockDocumentUseBuilderFactory.Setup(x => x.ActiveDocumentUsesForLesson(It.IsAny<int>())).Returns(new List<DocumentUseDomainObj>() { testDocumentUse });
    //        mockDocumentUseBuilderFactory.Setup(x => x.BuildFromId(It.IsAny<int>())).Returns(testDocumentUse);
    //        IDocumentUseDomainObjBuilder mockDocumentUseBuilder = mockDocumentUseBuilderFactory.Object;

    //        Mock<IDomainObjBuilder<DocumentDomainObj, DocumentDomainObjBasic, Document>> mockDocumentBuilderFactory = 
    //            new Mock<IDomainObjBuilder<DocumentDomainObj, DocumentDomainObjBasic, Document>>();
    //        mockDocumentBuilderFactory.Setup(x => x.BuildFromId(It.IsAny<int>())).Returns(testDocument);
    //        IDomainObjBuilder<DocumentDomainObj, DocumentDomainObjBasic, Document> mockDocumentBuilder = mockDocumentBuilderFactory.Object;

    //        testService = new DocumentsTabViewObjBuilder(mockDocumentUseBuilder, mockDocumentBuilder);
    //    }

    //    private static DocumentUseDomainObj[] SelectedDocumentUses =
    //    {
    //        testDocumentUse,
    //        null,
    //        null
    //    };
        
        
    //    //[Test]
    //    //[Sequential]
    //    //[Author("Jason Gould")]
    //    //public void Retrieve_CorrectlyInitializesDocumentUseOnlyWhenProvided( 
    //    //    [Values(1, 0, -1)] int selectedDocumentUseId, 
    //    //    [ValueSource("SelectedDocumentUses")] DocumentUseDomainObj expectedResult)
    //    //{            
    //    //    DocumentsTabViewObj viewObj = testService.Retrieve(1, selectedDocumentUseId, -1, false, 1, false);
            
    //    //    Assert.That(viewObj.SelectedDocumentUse, Is.EqualTo(expectedResult), "SelectedDocumentUse value was set incorrectly");
    //    //    Assert.That(viewObj.SelectedDocument, Is.EqualTo(null));
    //    //}

    //    private static DocumentDomainObj[] SelectedDocumentIds =
    //    {
    //        testDocument,
    //        null,
    //        null,
    //        null
    //    };

    //    //[Sequential]
    //    //[Test]
    //    //[Author("Jason Gould")]
    //    //public void Retrieve_CorrectlyInitializesDocumentOnlyWhenProvided(
    //    //    [Values(1, 0, -1, 1)] int selectedDocumentId,
    //    //    [Values(-1, -1, -1, 1)] int selectedDocumentUseId,
    //    //    [ValueSource("SelectedDocumentIds")] DocumentDomainObj expectedResult)
    //    //{
    //    //    DocumentsTabViewObj viewObj = testService.Retrieve(1, selectedDocumentUseId, selectedDocumentId, false, 1, false);

    //    //    Assert.That(viewObj.SelectedDocument, Is.EqualTo(expectedResult), "SelectedDocument value was set incorrectly");
    //    //}

    //    private static DocumentDomainObj [] GroupDocuments =
    //    {
    //        testArchivedDocument,
    //        null
    //    };

    //    //[Sequential]
    //    //[Test]
    //    //[Author("Jason Gould")]
    //    //public void Retrieve_OnlyRetrievesGroupDocumentsWhenAsked(
    //    //    [Values(true, false)] bool showGroupDocStorage,
    //    //    [ValueSource("GroupDocuments")] DocumentDomainObj expectedResult)
    //    //{
    //    //    DocumentsTabViewObj viewObj = testService.Retrieve(1, 1, -1, true, 1, showGroupDocStorage);
    //    //    List<DocumentViewObj> retrievedDocuments = viewObj.AllDocuments;

    //    //    if (retrievedDocuments == null)
    //    //    {
    //    //        Assert.That(retrievedDocuments, Is.EqualTo(expectedResult));
    //    //    } else
    //    //    {
    //    //        Assert.That(retrievedDocuments.First().Document, Is.EqualTo(expectedResult));
    //    //    }
    //    //}

    //    private static DocumentDomainObj[] ActiveAndArchivedDocuments =
    //    {
    //        testArchivedDocument,
    //        testActiveDocument
    //    };

    //    //[Sequential]
    //    //[Test]
    //    //[Author("Jason Gould")]
    //    //public void Retrieve_ProperlyRetrievesAllOrActiveOnlyGroupDocuments(
    //    //   [Values(true, false)] bool includeArchived,
    //    //   [ValueSource("ActiveAndArchivedDocuments")] DocumentDomainObj expectedResult)
    //    //{
    //    //    DocumentsTabViewObj viewObj = testService.Retrieve(1, 1, -1, includeArchived, 1, true);

    //    //    Assert.That(viewObj.AllDocuments.First<DocumentViewObj>().Document, Is.EqualTo(expectedResult));
    //    //}

    //    //[TestCase(1, 1)]
    //    //[TestCase(0, 0)]
    //    //[TestCase(-1, 0)]
    //    //[Author("Jason Gould")]
    //    //public void Retrieve_ProperlyRetrievesActiveDocumentUses(int lessonId, int expectedResult)
    //    //{
    //    //    DocumentsTabViewObj viewObj = testService.Retrieve(lessonId, 1, -1, false, 1, false);

    //    //    Assert.That(viewObj.ReferenceDocumentUses.Count, Is.EqualTo(expectedResult));
    //    //}
    //}
}

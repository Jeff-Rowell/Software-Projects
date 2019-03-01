using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDomain.DomainObjects;
using System.Web.Mvc;

namespace CWTesting.Tests.CWMasterTeacherView
{
    [TestFixture]
    class CourseCreateViewObjTest
    {
        // Standard Parameters to CourseCreateViewObj
        private int courseId = 1;
        private int institutionId = 1;
        private int userId = 1;
        private int termId = 1;
        private String message = "Test message";
        private bool showDeleteCourse = true;
        private bool showNewWorkingGroup = true;
        private bool showDeleteWorkingGroup = true;
        private bool showAllCourses = true;
        private bool isCourseDeletable = true;
        private bool showRenameWorkingGroup = true;
        private String narrativeMessage = "Test narrativeMessage";
        private List<WorkingGroupDomainObjBasic> workingGroupList = new List<WorkingGroupDomainObjBasic>();
        private List<UserDomainObjBasic> userList = new List<UserDomainObjBasic>();
        private List<TermDomainObj> termList = new List<TermDomainObj>();
        private List<CourseDomainObjBasic> courseList = new List<CourseDomainObjBasic>();
        private String courseName = "test courseName";
        private String institutionName = "test institutionName";



        //[Test]
        //[Author("Ivan Gajic", "Nick Beier")]
        //public void CourseCreateViewObject_Constructor_Test()
        //{
        //    CourseCreateAdminViewObj testViewObj = new CourseCreateAdminViewObj(courseId, institutionId, userId, termId,
        //                                                              message, showDeleteCourse, showNewWorkingGroup, showDeleteWorkingGroup, showAllCourses,
        //                                                              isCourseDeletable, showRenameWorkingGroup, narrativeMessage,
        //                                                              workingGroupList, userList, termList, courseList,
        //                                                              courseName, institutionName);

        //    Assert.That(testViewObj, Is.Not.Null);
        //    CheckStandardPassthroughs(testViewObj);

        //    SelectList institutionSelectList = new SelectList(workingGroupList, "Id", "Name");
        //    SelectList userSelectList = new SelectList(userList, "Id", "FullName");
        //    SelectList termSelectList = new SelectList(termList, "Id", "Name");
        //    SelectList courseSelectList = new SelectList(courseList, "Id", "DisplayName");

        //    Assert.That(testViewObj.WorkingGroupSelectList, Is.EqualTo(institutionSelectList));
        //    Assert.That(testViewObj.UserSelectList, Is.EqualTo(userSelectList));
        //    Assert.That(testViewObj.TermSelectList, Is.EqualTo(termSelectList));
        //    Assert.That(testViewObj.CourseSelectList, Is.EqualTo(courseSelectList));
            
        //}

        private void CheckStandardPassthroughs(CourseCreateAdminViewObj testViewObj)
        {
            //Passthroughs
            Assert.That(testViewObj.CourseId, Is.EqualTo(1));
            Assert.That(testViewObj.WorkingGroupId, Is.EqualTo(1));
            Assert.That(testViewObj.UserId, Is.EqualTo(1));
            Assert.That(testViewObj.TermId, Is.EqualTo(1));
            Assert.That(testViewObj.Message, Is.EqualTo(message));
            Assert.That(testViewObj.ShowDeleteCourse, Is.EqualTo(showDeleteCourse));
            Assert.That(testViewObj.ShowNewWorkingGroup, Is.EqualTo(showNewWorkingGroup));
            Assert.That(testViewObj.ShowDeleteWorkingGroup, Is.EqualTo(showDeleteWorkingGroup));
            Assert.That(testViewObj.ShowAllCourses, Is.EqualTo(showAllCourses));
            Assert.That(testViewObj.IsCourseDeletable, Is.EqualTo(isCourseDeletable));
            Assert.That(testViewObj.ShowRenameWorkingGroup, Is.EqualTo(showRenameWorkingGroup));
            Assert.That(testViewObj.NarrativeMessage, Is.EqualTo(narrativeMessage));
            Assert.That(testViewObj.CourseName, Is.EqualTo(courseName));
            Assert.That(testViewObj.WorkingGroupName, Is.EqualTo(institutionName));
        }

        //[Test]
        //[Author("Nick Beier")]
        //public void CourseCreateViewObject_nonEmptyCourseListTest()
        //{

        //    DateTime startDateTerm1 = new DateTime(2015, 1, 1);
        //    DateTime endDateTerm1 = new DateTime(2015, 5, 1);
        //    DateTime startDateTerm2 = new DateTime(2015, 1, 2);
        //    DateTime endDateTerm2 = new DateTime(2015, 5, 2);

        //    CourseDomainObj course1 = new CourseDomainObj();
        //    CourseDomainObjBasic course1Basic = new CourseDomainObjBasic(courseId: 0,
        //                                                                   name: "Course1",
        //                                                                   isMaster: false,
        //                                                                   termName: "Term1",
        //                                                                   userDisplayName: "UserDisplayName",
        //                                                                   termId: 0,
        //                                                                   hasActiveMessages: false,
        //                                                                   hasImportantMessages: false,
        //                                                                   hasActiveStoredMessages: false,
        //                                                                   hasNewMessages: false,
        //                                                                   hasOutForEditDocuments: false,
        //                                                                   hasStoredMessages: false,
        //                                                                   termEndDate: new DateTime(2016, 01, 01));
        //    course1Basic.Name = "Course1";
        //    course1.CourseDomainObjBasic = course1Basic;

        //    CourseDomainObj course2 = new CourseDomainObj();
        //    CourseDomainObjBasic course2Basic = new CourseDomainObjBasic(courseId: 0,
        //                                                                   name: "Course2",
        //                                                                   isMaster: false,
        //                                                                   termName: "Term2",
        //                                                                   userDisplayName: "UserDisplayName2",
        //                                                                   termId: 0,
        //                                                                   hasActiveMessages: false,
        //                                                                   hasImportantMessages: false,
        //                                                                   hasActiveStoredMessages: false,
        //                                                                   hasNewMessages: false,
        //                                                                   hasOutForEditDocuments: false,
        //                                                                   hasStoredMessages: false,
        //                                                                   termEndDate: new DateTime(2016, 01, 01));
        //    course2Basic.Name = "Course2";
        //    course2.CourseDomainObjBasic = course2Basic;

        //    List<CourseDomainObjBasic> notEmptyCourseList = new List<CourseDomainObjBasic>();
        //    notEmptyCourseList.Add(course2Basic);
        //    notEmptyCourseList.Add(course1Basic);

        //    CourseCreateAdminViewObj testViewObj = new CourseCreateAdminViewObj(courseId, institutionId, userId, termId,
        //                                                              message, showDeleteCourse, showNewWorkingGroup, showDeleteWorkingGroup, showAllCourses,
        //                                                              isCourseDeletable, showRenameWorkingGroup, narrativeMessage,
        //                                                              workingGroupList, userList, termList, notEmptyCourseList,
        //                                                              courseName, institutionName);

        //    Assert.That(testViewObj, Is.Not.Null);

        //    SelectList courseSelectList = testViewObj.CourseSelectList;
        //    Assert.That(courseSelectList, Is.Not.Null);

        //    int count = 0;
        //    foreach (var item in courseSelectList.Items)
        //    {
        //        count++;
        //    }
        //    Assert.That(count, Is.EqualTo(2));
        //}
    }
}

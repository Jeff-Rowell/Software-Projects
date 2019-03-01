using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.CUDRouters;
using CWMasterTeacherService.DomainObjectBuilders;
using CWMasterTeacherService.ViewObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWMT.Controllers
{
    public class StudentHomeController : BaseStudentController
    {
        private StudentHomeViewObjBuilder _studentHomeViewObjBuilder;
        private StudentHomeCUDRouter _studentHomeCUDRouter;

        public StudentHomeController(UserDomainObjBuilder userObjBuilder, StudentUserDomainObjBuilder studentUserObjBuilder,
                        StudentHomeCUDRouter studentHomeCUDRouter, StudentHomeViewObjBuilder studentHomeViewObjBuilder)
                        :base(userObjBuilder, studentUserObjBuilder)
        {
            _studentHomeCUDRouter = studentHomeCUDRouter;
            _studentHomeViewObjBuilder = studentHomeViewObjBuilder;
        }
        // GET: StudentHome
        public ActionResult Index(Guid? workingGroupId = null, Guid? termId = null, 
                                    Guid? instructorUserId = null, Guid? classSectionId = null, 
                                    bool doShowRequestCourseAccess = false, string accessRequestMessage = "",
                                    bool doShowRequestGroupAccess = false, Guid? selectedInstructorId = null,
                                    string courseRequestMessage = "", Guid? displayedClassSectionId = null,
                                    Guid? selectedClassMeetingId = null, bool showAllDates = false)
        {
            StudentHomeViewObj viewObj = _studentHomeViewObjBuilder.Retrieve(studentUserObj: CurrentStudentUserObj,
                                                                                instructorUserId: instructorUserId == null ? Guid.Empty : instructorUserId.Value,
                                                                                classSectionId: classSectionId == null ? Guid.Empty : classSectionId.Value,
                                                                                doShowRequestCourseAccess: doShowRequestCourseAccess,
                                                                                accessRequestMessage: accessRequestMessage,
                                                                                doShowRequestGroupAccess: doShowRequestGroupAccess,
                                                                                courseRequestMessage: courseRequestMessage,
                                                                                displayedClassSectionId: displayedClassSectionId == null ? Guid.Empty : displayedClassSectionId.Value,
                                                                                selectedClassMeetingId: selectedClassMeetingId == null ? Guid.Empty : selectedClassMeetingId.Value,
                                                                                doShowAllDates: showAllDates );
            return View(viewObj);
        }

        public ActionResult RequestGroupAccess(string accessCode)
        {
            string message = _studentHomeCUDRouter.RequestGroupAccessReturnMessage(CurrentStudentUserId, accessCode);

            return RedirectToAction("Index", new { accessRequestMessage = message });
        }

        public ActionResult RequestCourseAccess(Guid classSectionId)
        {
            string message = _studentHomeCUDRouter.RequestCourseAccessReturnMessage(CurrentStudentUserId, classSectionId);
            return RedirectToAction("Index", new { courseRequestMessage = message });
        }

    }
}
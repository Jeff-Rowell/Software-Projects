using CWMasterTeacher3;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.ViewObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWMT.Controllers
{
    [Authorize]
    public class CompareSelectController : BaseController
    {
        private CompareSelectViewObjBuilder _builder;
        private UserDomainObjBuilder _userObjBuilder;

        public CompareSelectController(UserDomainObjBuilder userObjBuilder, CompareSelectViewObjBuilder builder) : base(userObjBuilder)
        {
            _builder = builder;
            _userObjBuilder = userObjBuilder;
        }
        


        public ActionResult Index(Guid referenceEditableLessonId, Guid? courseId = null, Guid? comparisonLessonId = null,
                                    Guid? selectedMetaCourseId = null, Guid? selectedLessonId = null,
                                    bool isDisplayModeNarrative = false, 
                                    bool isDisplayModeLessonPlan = false, bool isDisplayModeDocuments = false, 
                                    bool doLockEditableLesson = false, int comparisonModeInt = -1)
        {
            ModelState.Clear();
            CompareSelectViewObj viewObj = _builder.Retrieve(referenceEditableLessonId: referenceEditableLessonId == null ? Guid.Empty : referenceEditableLessonId,
                                                             courseId: courseId == null ? Guid.Empty: courseId.Value,
                                                             selectedMetaCourseId: selectedMetaCourseId == null ? Guid.Empty : selectedMetaCourseId.Value,
                                                             selectedLessonId: selectedLessonId == null ? Guid.Empty : selectedLessonId.Value,
                                                             currentUserObj: CurrentUserObj,
                                                             isDisplayModeNarrative: isDisplayModeNarrative,
                                                             isDisplayModeLessonPlan: isDisplayModeLessonPlan,
                                                             isDisplayModeDocuments: isDisplayModeDocuments,
                                                             doLockEditableLesson: doLockEditableLesson,
                                                             comparisonModeInt: comparisonModeInt);

            return View(viewObj);
        }

    }
}
using CWMasterTeacherDataModel;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.CUDRouters;
using CWMasterTeacherService.CUDServices;
using CWMasterTeacherService.ViewObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWMasterTeacherDataModel.ObjectBuilders;

namespace CWMasterTeacher3
{
    [Authorize]
    public class ClassSectionAdminController : BaseController
    {
        private UserDomainObjBuilder _userObjBuilder;
        private ClassSectionCUDService _classSectionCUDService;
        private ClassMeetingCUDService _classMeetingCUDService;
        private TermCUDService _termCUDService;
        private CourseCUDService _courseCUDService;
        private ClassSectionAdminViewObjBuilder _viewObjBuilder;
        private ClassSectionAdminCUDRouter _classSectionAdminCUDRouter;

        public ClassSectionAdminController(UserDomainObjBuilder userObjBuilder, 
                                            ClassSectionCUDService classSectionCUDService, 
                                            ClassMeetingCUDService classMeetingCUDService,
                                            TermCUDService termCUDService, 
                                            CourseCUDService courseCUDService, 
                                            ClassSectionAdminViewObjBuilder viewObjBuilder,
                                            ClassSectionAdminCUDRouter classSectionAdminCUDRouter)
            : base(userObjBuilder)
        {
            _userObjBuilder = userObjBuilder;
            _classSectionCUDService = classSectionCUDService;
            _classMeetingCUDService = classMeetingCUDService;
            _termCUDService = termCUDService;
            _courseCUDService = courseCUDService;
            _viewObjBuilder = viewObjBuilder;
            _classSectionAdminCUDRouter = classSectionAdminCUDRouter;
        }

        public ActionResult Index(Guid? selectedClassSectionId = null, 
                                Guid? selectedUserId = null, Guid?
                                selectedTermId = null, 
                                Guid? selectedCourseId = null,
                                bool addSingleClassMeeting = false)
        {
            try
            {

                ClassSectionAdminViewObj viewObj = _viewObjBuilder.RetrieveClassSectionViewObj(currentUserId: CurrentUserId, 
                                                                                        selectedUserId: selectedUserId ?? Guid.Empty, 
                                                                                        selectedTermId: selectedTermId ?? Guid.Empty, 
                                                                                        selectedCourseId: selectedCourseId ?? Guid.Empty, 
                                                                                        selectedClassSectionId: selectedClassSectionId ?? Guid.Empty, 
                                                                                        addSingleClassMeeting: addSingleClassMeeting);


                return View(viewObj);
            }
            catch (Exception e)
            {
                WebLogger.Log.ClassSectionIndexFailed(e);
                throw;
            }

        }

        public ActionResult CreateClassSection(ClassSectionAdminReturnModel model)
        {
            try
            {
                Guid classSectionId = _classSectionAdminCUDRouter.CreateClassSectionReturnId(courseId: model.SelectedCourseId,
                                                                                                classSectionName: model.SelectedClassSectionName,
                                                                                                startTimeLocalString: model.StartTimeLocal,
                                                                                                endTimeLocalString: model.EndTimeLocal,
                                                                                                meetsSunday: model.MeetsSunday,
                                                                                                meetsMonday: model.MeetsMonday,
                                                                                                meetsTuesday: model.MeetsTuesday,
                                                                                                meetsWednesday: model.MeetsWednesday,
                                                                                                meetsThursday: model.MeetsThursday,
                                                                                                meetsFriday: model.MeetsFriday,
                                                                                                meetsSaturday: model.MeetsSaturday);

                if (classSectionId != Guid.Empty)
                {

                    return RedirectToAction("Index", "ClassMeetingAdmin", new { selectedClassSectionId = classSectionId });
                }
                else
                {
                    return RedirectToAction("Index", new { selectedClassSectionId = model.SelectedClassSectionId });
                }
            }
            catch (Exception e)
            {
                WebLogger.Log.CreateClassSectionFailed(e);
                throw;
            }


        }

    }
}

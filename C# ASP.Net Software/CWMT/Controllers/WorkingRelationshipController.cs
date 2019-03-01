using CWMasterTeacherDataModel;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.CUDRouters;
using CWMasterTeacherService.ViewObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWMasterTeacherDataModel.ObjectBuilders;

namespace CWMasterTeacher3.Controllers
{
    [Authorize]
    public class WorkingRelationshipController : BaseController
    {
        private WorkingRelationshipRepo _workRelRepo;
        private WorkingRelationshipViewObjBuilder _viewObjBuilder;
        private WorkingRelationshipCUDRouter _workingRelationshipCUDRouter;

        public WorkingRelationshipController(UserDomainObjBuilder userObjBuilder, 
                                            WorkingRelationshipRepo workRelRepo,
                                            WorkingRelationshipViewObjBuilder viewObjBuilder,
                                            WorkingRelationshipCUDRouter workingRelationshipCUDRouter)
            : base(userObjBuilder)
        {
            _workRelRepo = workRelRepo;
            _viewObjBuilder = viewObjBuilder;
            _workingRelationshipCUDRouter = workingRelationshipCUDRouter;
        }

        // GET: WorkingRelationship
        public ActionResult Index(Guid? courseId = null, Guid? selectedWorkRelId = null)
        {
            WorkingRelationshipViewObj viewObj = _viewObjBuilder.RetrieveWorkingRelService(courseId ?? Guid.Empty, 
                                                                                            CurrentUserId, 
                                                                                            selectedWorkRelId?? Guid.Empty);
            //WorkingRelationshipReturnModel model = _factory.BuildWorkingRelationshipViewModel(viewObj);
            //model.UserIsEditor = CurrentUser.IsNarrativeEditor;
            //model.UserIsAdmin = CurrentUser.IsAdmin;
            return View(viewObj);
        }

        public ActionResult UpdateWorkingRelationship(Guid workRelId, bool followNarrative, int sequenceNumber, Guid courseId)
        {
            _workingRelationshipCUDRouter.UpdateWorkingRelationship(workRelId, followNarrative, sequenceNumber);

            return RedirectToAction("Index", new { courseId = courseId });
        }
    }
}
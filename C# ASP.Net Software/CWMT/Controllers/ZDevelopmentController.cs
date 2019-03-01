using CWMasterTeacherDataModel;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherService.ViewObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWMasterTeacherDataModel.ObjectBuilders;

namespace CWMasterTeacher3
{
    public class ZDevelopmentController : BaseController
    {
        private ZDevelopmentRepo _zDevelopmentRepo;
        private zDevelopmentViewObjBuilder _viewObjBuilder;

        public ZDevelopmentController(UserDomainObjBuilder userObjBuilder, ZDevelopmentRepo zDevelopmentRepo, zDevelopmentViewObjBuilder viewObjBuilder): base(userObjBuilder)
        {
            _zDevelopmentRepo = zDevelopmentRepo;
            _viewObjBuilder = viewObjBuilder;
        }

        // GET: ZTestJunk
        public ActionResult Index(string message = "")
        {
            zDevelopmentViewObj viewObj = _viewObjBuilder.BuildZDevelopmentViewModel(message);
            
            return View(viewObj);
        }

        public ActionResult RepairOriginalIds()
        {
            //_zDevelopmentRepo.RepairAllOriginalCourseIds();
            string message = "OriginalCourseIds have been repaired.";
            return RedirectToAction("Index", new { message = message });
        }

        public ActionResult ResetAllNarratives()
        {
            //_zDevelopmentRepo.ResetAllNarratives();
            string message = "Narratives have been reset.";
            return RedirectToAction("Index", new { message = message });
        }

        public ActionResult TestLink()
        {
            string message = "At least the test link works.";
            return RedirectToAction("Index", new { message = message });
        }

    }
}
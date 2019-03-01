using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CWMasterTeacherDomain
{
    public class LessonPlanViewObj
    {
        public LessonPlanViewObj (LessonPlanDomainObj lessonPlanObj)
        {
            LessonPlanObj = lessonPlanObj;
        }

        public LessonPlanDomainObj LessonPlanObj { get;}
        public LessonDomainObjBasic LessonObjBasic { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string DisplayText
        {
            get { return "<h4>" + LessonPlanObj.Name + "</h4>" + LessonPlanObj.Text; ; }
        } 

        public Guid LessonPlanId
        {
            get { return LessonPlanObj == null ? Guid.Empty : LessonPlanObj.Id; }
        }
        public bool IsLessonMaster
        {
            get { return LessonPlanObj == null ? false : LessonPlanObj.IsMasterEdit; }
        }

        public Guid SelectedLessonId { get; set; }
        public string DateLabelText
        {
            get
            {
                string IndvOrMstr = LessonPlanObj.IsIndvNotMaster ? "(Indiv)" : "(Master)";
                //string dateString =  LessonPlanObj.DateModified.ToString("m d yyyy, h:m:s.fffff");
                string dateString = LessonPlanObj.DateModified.Date.ToString("d");
                return dateString + " " + IndvOrMstr;
            }
        }
        public string LessonPlanEditLinkText
        {
            get
            {
                string editString = IsLessonMaster ? "Edit_Master_" : "Edit_My_";
                return editString  + DomainWebUtilities.LessonPlanTypeName;
            }
        }

        public bool ShowComparisonLessonLink
        {
            get { return LessonObjBasic == null ? false: LessonObjBasic.LessonPlanHasNewChangesInGroup 
                                                    && LessonObjBasic.DoShowLessonPlanNotifications 
                                                    && HasBasicEditRights; }
        }

        public string ComparisonLessonLinkText
        {
            get { return DomainWebUtilities.ComparisonLessonLinkText; }
        }

        public bool HasBasicEditRights { get; set; }
        public bool HasMasterEditRights { get; set; }
    }//End Class
}

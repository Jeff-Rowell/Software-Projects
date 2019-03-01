using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    public class NarrativeViewObjBuilder
    {
        private LessonDomainObjBuilder _lessonObjBuilder;
        private NarrativeDomainObjBuilder _narrativeObjBuilder;


        public NarrativeViewObjBuilder (LessonDomainObjBuilder lessonObjBuilder, NarrativeDomainObjBuilder narrativeObjBuilder)
        {
            _lessonObjBuilder = lessonObjBuilder;
            _narrativeObjBuilder = narrativeObjBuilder;
        }

        public NarrativeViewObj RetrieveNarrativeViewObj(Guid lessonId, Guid currentUserId, bool hasBasicEditRights, 
                                                            bool hasMasterEditRights, bool doShowNarrativeNotifications)
        {
            NarrativeViewObj viewObj = new NarrativeViewObj();
            
            if(lessonId != Guid.Empty)
            {
                LessonDomainObjBasic lessonObj = _lessonObjBuilder.BuildBasicFromId(lessonId);
                viewObj.LessonObj = lessonObj;
                viewObj.SelectedLessonId = lessonId;
                viewObj.NarrativeObj = lessonObj == null ? null : _narrativeObjBuilder.BuildFromId(lessonObj.NarrativeId);
                viewObj.HasBasicEditRights = hasBasicEditRights;
                viewObj.HasMasterEditRights = hasMasterEditRights;
            }

            return viewObj;
        }

    }
}

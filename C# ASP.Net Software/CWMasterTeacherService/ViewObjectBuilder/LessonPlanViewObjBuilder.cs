using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDomain;
using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.ObjectBuilders;

namespace CWMasterTeacherService
{
    public class LessonPlanViewObjBuilder
    {
        private LessonPlanDomainObjBuilder _lessonPlanObjBuilder;
        private LessonDomainObjBuilder _lessonObjBuilder;

        public LessonPlanViewObjBuilder (LessonPlanDomainObjBuilder lessonPlanObjBuilder, LessonDomainObjBuilder lessonObjBuilder)
        {
            _lessonPlanObjBuilder = lessonPlanObjBuilder;
            _lessonObjBuilder = lessonObjBuilder;
        }


        public LessonPlanViewObj Retrieve(Guid selectedLessonId, bool isMasterEdit, bool hasBasicEditRights, 
                                            bool hasMasterEditRights, bool doShowLessonPlanNotifications)
        {
            LessonPlanDomainObj lessonPlanObj = new LessonPlanDomainObj();
            if(selectedLessonId != Guid.Empty)
            {
                lessonPlanObj = _lessonPlanObjBuilder.BuildFromLessonId(selectedLessonId);
                lessonPlanObj.IsMasterEdit = isMasterEdit;
            }

            return FinishRetrieve(lessonPlanObj, selectedLessonId, hasBasicEditRights, hasMasterEditRights, doShowLessonPlanNotifications  );
        } 


        //Eventually we will have a RetrieveSpecificLessonPlan and I want this code to be reusable.
        private LessonPlanViewObj FinishRetrieve(LessonPlanDomainObj lessonPlanObj, Guid selectedLessonId, bool hasBasicEditRights, 
                                                    bool hasMasterEditRights, bool doShowLessonPlanNotifications)
        {
            LessonPlanViewObj viewObj = new LessonPlanViewObj(lessonPlanObj );
            viewObj.SelectedLessonId = selectedLessonId;
            viewObj.HasBasicEditRights = hasBasicEditRights;
            viewObj.HasMasterEditRights = hasMasterEditRights;
            viewObj.LessonObjBasic = _lessonObjBuilder.BuildBasicFromId(selectedLessonId);

            return viewObj;

        }
    }
}

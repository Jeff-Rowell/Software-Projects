using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain
{
    public class LessonPlanDomainObj
    {
        public LessonPlanDomainObj() { }

        public LessonPlanDomainObj(LessonPlanDomainObjBasic basicLessonPlan, string text, DateTime dateModified,
                                Guid? modifiedByUserId, bool isIndivNotMaster, 
                                string modificationNotes)
        {
            BasicLessonPlan = basicLessonPlan;
            Text = text;
            DateModified = dateModified;
            ModifiedByUserId = modifiedByUserId;
            ModificationNotes = modificationNotes;
            IsIndvNotMaster = isIndivNotMaster;
        }

        public LessonPlanDomainObjBasic BasicLessonPlan { get; set; }
        public string Text { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime GroupMostRecentModDate { get; set; }
        public Guid? ModifiedByUserId { get; set; }

        public string ModificationNotes { get; set; }
        public bool IsIndvNotMaster { get; set; }  
        public bool IsMasterEdit { get; set; }
        public DateTime DateCreated { get; set; }

        public Guid Id
        {
            get { return BasicLessonPlan == null ? Guid.Empty : BasicLessonPlan.Id; }
        }
        public string Name
        {
            get { return BasicLessonPlan == null ? "" : BasicLessonPlan.Name; }
        }
    }
}

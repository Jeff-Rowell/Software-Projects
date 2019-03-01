using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.DomainObjects
{
    public class NarrativeDomainObj
    {

        private NarrativeDomainObjBasic _basicObj;

        public NarrativeDomainObj  (NarrativeDomainObjBasic basic)
        {
            _basicObj = basic;
        }

        public Guid Id { get { return _basicObj.Id; } }

        public bool IsMaster { get; set; }

        public string Text { get; set; }

        public Guid ModifiedByUserId { get; set; }

        public DateTime DateModified { get; set; }

        public bool DoSuggestRemovingComment { get; set; }

        public Guid SelectedLessonId { get; set; }

        public string ComparisonLessonLinkText
        {
            get { return DomainWebUtilities.ComparisonLessonLinkText; }
        }

        public string FullNarrativeText { get; set; }

    }

}

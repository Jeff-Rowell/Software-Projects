using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherService.CUDServices;
using CWMasterTeacherDomain;

namespace CWMasterTeacherDataModel.ObjectBuilders
{
   public class NarrativeDomainObjBuilder
    {
        private NarrativeRepo _narrativeRepo;
        private NarrativeCUDService _narrativeCUDService;
        private LessonRepo _lessonRepo;

        public NarrativeDomainObjBuilder(NarrativeRepo repo, LessonRepo lessonRepo)
        {
            _narrativeRepo = repo;
            _lessonRepo = lessonRepo;
        }

        public NarrativeDomainObjBuilder()
        {        }

        public static NarrativeDomainObj Build(Narrative dbObject) {
            NarrativeDomainObjBasic basic = BuildBasic(dbObject);
            NarrativeDomainObj domainObj = new NarrativeDomainObj(basic);
            domainObj.Text = dbObject.Text;
            domainObj.ModifiedByUserId = dbObject.ModifiedByUserId;
            domainObj.DateModified = dbObject.DateModified;

            domainObj.DoSuggestRemovingComment = dbObject.DoSuggestRemovingComment;
            return domainObj;
        }

        public static NarrativeDomainObj BuildEmpty()
        {
            NarrativeDomainObjBasic basic = BuildBasicEmpty();
            NarrativeDomainObj domainObj = new NarrativeDomainObj(basic);
            domainObj.Text = "";
            domainObj.ModifiedByUserId = Guid.Empty;
            domainObj.DateModified = new DateTime();
            domainObj.DoSuggestRemovingComment = false;
            return domainObj;
        }

        public NarrativeDomainObj BuildFromId(Guid id)
        {
            Narrative narrativeDb = _narrativeRepo.GetById(id);
            return narrativeDb == null ? null : Build(narrativeDb);
        }

        public static NarrativeDomainObjBasic BuildBasic(Narrative db)
        {
            NarrativeDomainObjBasic _basic = new NarrativeDomainObjBasic();
            _basic.Id = db.Id;
            return _basic;
        }

        public static NarrativeDomainObjBasic BuildBasicEmpty()
        {
            NarrativeDomainObjBasic _basic = new NarrativeDomainObjBasic();
            _basic.Id = Guid.Empty;
            return _basic;
        }

        public NarrativeDomainObjBasic BuildBasicFromId(Guid id)
        {
            Narrative narrative = _narrativeRepo.GetById(id);
            return narrative == null ? null : BuildBasic(narrative);
        }

        private static string GetFullNarrative(Lesson lesson)
        {
            User user = lesson.Course.User;
            Narrative masterNarrative = lesson.Course.IsMaster ? lesson.Narrative : lesson.MasterLesson.Narrative;

            string fullText = "";

            if (masterNarrative != null)
            {
                string sharedHeading = "<h5>Master " + DomainWebUtilities.NarrativeTypeName + "____" + masterNarrative.DateModified.Date.ToString("d") + "</h5>";
                fullText = fullText + sharedHeading + masterNarrative.Text;
            }

            List<Narrative> commentNarratives = masterNarrative.CommentNarratives.OrderByDescending(x => x.DateModified).ToList();
            foreach (var xNarrative in commentNarratives)
            {
                if (xNarrative != null && xNarrative.Text.Length > 3)
                {
                    string xHeading = "<h5>" + xNarrative.User.DisplayName + "'s " + DomainWebUtilities.NarrativeCommentTypeName + "____" + xNarrative.DateModified.Date.ToString("d") + "</h5>";
                    fullText = fullText + xHeading + xNarrative.Text;
                }
            }

            return fullText;
        }

    }
}

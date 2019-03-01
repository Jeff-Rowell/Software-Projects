using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class NarrativeViewObj
    {
        public LessonDomainObjBasic LessonObj { get; set; }
        public NarrativeDomainObj NarrativeObj { get; set; }

        public bool HasBasicEditRights { get; set; }
        public bool HasMasterEditRights { get; set; }

        //public UserDomainObj CurrentUserObj { get; set; }

        private string IndivOrMasterString
        {
            get { return (LessonObj != null && LessonObj.IsMaster) ? "Master" : ""; }
        }

        private string ActionString 
        {
            get
            {
                if(LessonObj != null)
                {
                    if (LessonObj.IsMaster)
                    {
                        return "Edit_";
                    }
                    else
                    {
                        return "Add_Or_Edit_";
                    }
                }
                else
                {
                    return "";
                }
            }
        }

        private string NarrativeTypeNameString
        {
            get
            {
                if (LessonObj != null)
                {
                    if (LessonObj.IsMaster)
                    {
                        return DomainWebUtilities.NarrativeTypeName ;
                    }
                    else
                    {
                        return DomainWebUtilities.NarrativeCommentTypeName;
                    }
                }
                else
                {
                    return "";
                }
            }
        }


        public string NarrativeEditLinkText
        {
            get { return ActionString + IndivOrMasterString + "_" + NarrativeTypeNameString; }
        }


        public Guid EditorNarrativeId
        {
            get { return LessonObj.NarrativeId; }
        }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Text
        {
            get { return LessonObj == null ? null : LessonObj.FullNarrative; }
        }

        public Guid SelectedLessonId { get; set; }

        public bool ShowComparisonLessonLink
        {
            get { return LessonObj == null ? false : LessonObj.NarrativeHasNewChangesInGroup  
                                                    && LessonObj.DoShowNarrativeNotifications
                                                    && HasBasicEditRights; }
        }

        public string ComparisonLessonLinkText
        {
            get { return "Narrative has been modified"; }
        }

        public Guid NarrativeId
        {
            get { return NarrativeObj == null ? Guid.Empty : NarrativeObj.Id; }
        }

    }
}

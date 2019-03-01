using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.DomainObjects
{
    public class LessonUseDomainObj
    {
        private LessonUseDomainObjBasic _lessonUseDomainObjBasic;

        /// <summary>
        /// Construct a new LessonUseDomainObj with the given basic object.
        /// </summary>
        /// <param name="basic"></param>
        public LessonUseDomainObj(LessonUseDomainObjBasic basic)
        {
            _lessonUseDomainObjBasic = basic;
        }

        /// <summary>
        /// This paramaterless constructor is used in ClassMeetingDomainObject to create lessonUses for NoClass days.
        /// </summary>
        public LessonUseDomainObj()
        {
            LessonUseDomainObjBasic basic = new LessonUseDomainObjBasic();
            _lessonUseDomainObjBasic = basic;
        }

        //this might not be needed?
        /// <summary>
        /// Unique identifier of the lesson use domain object.
        /// </summary>
        public Guid LessonUseId { get; set; }

        /// <summary>
        /// Unique identifier of the class meeting this lesson use is associated to.
        /// </summary>
        public Guid ClassMeetingId { get; set; }

        /// <summary>
        /// The unique identifier of the lesson associated with this lesson use domain object.
        /// </summary>
        public Guid LessonId { get; set; }

        /// <summary>
        /// Sequence number.
        /// </summary>
        public int? SequenceNumber { get; set; }

        /// <summary>
        /// The custom name of this lesson use domain object.
        /// </summary>
        public string CustomName { get; set; }

        /// <summary>
        /// Custom text of the lesson use.
        /// </summary>
        public string CustomText { get; set; }

        public bool HasCustomText
        {
            get { return CustomText != null && CustomText.Length > 1; }
        }

        /// <summary>
        /// Text of the associated LessonPlan Object.
        /// </summary>
        public string LessonPlanText { get; set; }

        /// <summary>
        /// Lesson use custom time.
        /// </summary>
        public int? CustomTime { get; set; }

        /// <summary>
        /// Does this lesson use domain object have a custom time?
        /// </summary>
        public bool HasCustomTime { get; set; }

        public string Text
        {
            get
            {
                if (CustomText != null && CustomText.Length > 1)
                {
                    return CustomText;
                }
                else
                {
                    return LessonPlanText;
                }
            }
        }


        /// <summary>
        /// The unique identifier of this lesson use domain object.
        /// </summary>
        public Guid Id
        {
            get { return _lessonUseDomainObjBasic.Id; }
            set { _lessonUseDomainObjBasic.Id = value; }
        }

        /// <summary>
        /// The name of this lesson use domain object.
        /// </summary>
        public string Name
        {
            get
            {
                if(CustomName !=null && CustomName.Length > 1)
                {
                    return CustomName;
                }
                else
                {
                    return _lessonUseDomainObjBasic == null ? "??Name??" : _lessonUseDomainObjBasic.Name;
                }
            }
            set { _lessonUseDomainObjBasic.Name = value; }
        }

        /// <summary>
        /// The name to be displayed for this lesson use domain object.
        /// </summary>
        public string DisplayName
        {
            get
            {
                return DisplayNameConstructor(true);
            }
        }

        /// <summary>
        /// The short version of the display name.
        /// </summary>
        public string ShortDisplayName
        {
            get
            {
                return DisplayNameConstructor(false);
            }
        }

        /// <summary>
        /// Logic to construct a DisplayName or ShortDisplayName.
        /// </summary>
        /// <param name="isLongNotShort">
        ///     Determines if a normal or short display name should be constructed.
        /// </param>
        /// <returns>
        ///     string - The constructed display name.
        /// </returns>
        private string DisplayNameConstructor(bool isLongNotShort)
        {
            if ((CustomText != null && CustomText.Length < 3) && LessonId != Guid.Empty )
            {
                return Name;
            }
            else if (CustomName != null && CustomName.Length > 0)
            {
                if (isLongNotShort)
                {
                    return "*" + CustomName;
                }
                else
                {
                    return CustomName;
                }
            }
            else //This indicates that something is wrong
            {
                return "*No Custom Name Found*";
            }
        }

        //TODO everything else

    }
}

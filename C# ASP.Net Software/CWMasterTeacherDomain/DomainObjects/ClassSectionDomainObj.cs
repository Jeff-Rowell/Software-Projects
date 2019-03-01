using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;


namespace CWMasterTeacherDomain.DomainObjects
{

    public class ClassSectionDomainObj
    {
        /// <summary>
        /// The basic object, useful for lists.
        /// </summary>
        private ClassSectionDomainObjBasic _basicObj;


        /// <summary>
        /// Construct a new ClassSectionDomainObj with the given basic object.
        /// </summary>
        /// <param name="basicObj">
        ///     Basic object containing minimum amount of properties for
        ///     a domain object.
        /// </param>
        public ClassSectionDomainObj(ClassSectionDomainObjBasic basicObj)
        {
            _basicObj = basicObj;
        }

        /// <summary>
        /// Unique identifier of this domain object.
        /// </summary>
        public Guid Id
        {
            get
            {
                return _basicObj.Id;
            }
        }

        /// <summary>
        /// Class section identifier, same as Id.
        /// </summary>
        public Guid ClassSectionId { get; set; }

        /// <summary>
        /// Identifier of the course associated with this class section.
        /// </summary>
        public Guid CourseId
        {
            get { return _basicObj.CourseId; }
        }
    
        /// <summary>
        /// Name of the class section.
        /// </summary>
        public string Name
        {
            get { return _basicObj.Name; }
            set { _basicObj.Name = value; }
        }

        /// <summary>
        /// The identifier of the last displayed class meeting.
        /// </summary>
        public Guid? LastDisplayedClassMeetingId { get; set; }

        /// <summary>
        /// List of class meetings for this class section.
        /// </summary>
        public List<ClassMeetingDomainObj> ClassMeetingList { get; set; }

        public string TermName { get; set; }

        public String DisplayName
        {
            get { return _basicObj.DisplayName; }
        }

        public int AwaitingApprovalCount { get; set; }

        public Guid MirrorTargetClassSectionId
        {
            get { return _basicObj == null ? Guid.Empty : _basicObj.MirrorTargetClassSectionId.GetValueOrDefault(); }
        }

        public bool DoAllowEditing
        {
            get { return MirrorTargetClassSectionId == Guid.Empty; }
        }



    }//End Class
}
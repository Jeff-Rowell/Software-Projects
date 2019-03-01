using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface IClassMeetingDomainObjBuilder : IDomainObjBuilder<ClassMeetingDomainObj,
                                                                       ClassMeetingDomainObjBasic,
                                                                       ClassMeeting>
    {
        /// <summary>
        /// Interface to be implemented by classes wishing to provide services that build and return ClassMeetingDomainObjBasic.
        /// </summary>
        List<ClassMeetingDomainObj> GetClassMeetingsForClassSection(Guid selectedClassSectionId);
        List<DateTime> SevenCurrentDates(Guid userId, Guid termId);
        List<DateTime> DatesForUserAndTerm(Guid userId, Guid termId);
        List<ClassMeetingDomainObjBasic> ClassMeetingsForUserAndDate(Guid userId, DateTime date);
    }
}

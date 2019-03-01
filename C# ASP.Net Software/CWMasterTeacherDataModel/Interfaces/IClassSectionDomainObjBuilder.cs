using CWMasterTeacherDomain.DomainObjects;
using System.Collections.Generic;
using System;

namespace CWMasterTeacherDataModel.Interfaces
{
    /// <summary>
    /// Interface to be implemented by classes wishing to provide services that build and return ClassSectionDomainObj.
    /// </summary>
    public interface IClassSectionDomainObjBuilder: IDomainObjBuilder<ClassSectionDomainObj,
                                                                      ClassSectionDomainObjBasic,
                                                                      ClassSection>
    {
        List<ClassSectionDomainObjBasic> GetClassSectionsForCourseList(Guid selectedCourseId);
        List<ClassSectionDomainObjBasic> ClassSectionsForUser(Guid userId);
        List<ClassSectionDomainObjBasic> ClassSectionsForUserAndCurrentTerm(Guid userId);
        List<ClassSectionDomainObjBasic> ReferenceSectionsForClassSection(Guid classSectionId);
        List<ClassSectionDomainObjBasic> PossibleReferenceClassSections(Guid classSectionId, int takeThisMany);
        void SetLastDisplayedClassMeetingId(Guid classMeetingId, Guid classSectionId);
        Guid GetLastDisplayedClassMeetingId(Guid currentUserId);
    }
}

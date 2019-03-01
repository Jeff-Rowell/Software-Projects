using System.Collections.Generic;
using System.Linq;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherService.CUDServices;
using System;

namespace CWMasterTeacherDataModel.ObjectBuilders
{
    public class ClassSectionDomainObjBuilder : IClassSectionDomainObjBuilder
    {
        private ClassSectionRepo _classSectionRepo;
        private UserRepo _userRepo;
        private ClassSectionCUDService _classSectionCUDService;
        private ClassMeetingDomainObjBuilder _classMeetingDomainObjBuilder;
        public Guid id;

        /// <summary>
        /// Construct a ClassSectionDomainObjBuilder with the given ClassSectionRepo.
        /// </summary>
        /// <param name="repo">
        ///     ClassSectionRepo used by the builder to retrieve ClassSections.
        /// </param>
        public ClassSectionDomainObjBuilder(ClassSectionRepo repo, UserRepo userRepo, 
                                            ClassSectionCUDService classSectionCUDService, 
                                            ClassMeetingDomainObjBuilder classMeetingDomainObjBuilder)
        {
            _classSectionRepo = repo;
            _userRepo = userRepo;
            _classSectionCUDService = classSectionCUDService;
            _classMeetingDomainObjBuilder = classMeetingDomainObjBuilder;
        }



        //public ClassSectionDomainObjBuilder()
        //{        }


        /// <summary>
        /// Build a ClassSectionDomainObj from the given ClassSection DbObject.
        /// </summary>
        /// <param name="dbObject">
        ///     ClassSection DbObject.
        /// </param>
        /// <returns>
        ///     ClassSectionDomainObj
        /// </returns>
        public static ClassSectionDomainObj Build(ClassSection dbObject)
        {
            if(dbObject != null)
            {
                ClassSectionDomainObjBasic basicObj = BuildBasic(dbObject);
                ClassSectionDomainObj domainObj = new ClassSectionDomainObj(basicObj);

                domainObj.ClassSectionId = dbObject.Id;
                domainObj.Name = dbObject.Name;
                domainObj.LastDisplayedClassMeetingId = dbObject.LastDisplayedClassMeetingId;
                domainObj.TermName = dbObject.Course.Term.Name;
                domainObj.ClassMeetingList = GetClassMeetingList(dbObject);
                domainObj.ClassSectionId = dbObject.Id;
                domainObj.AwaitingApprovalCount = dbObject.ClassSectionStudents.Where(x => !x.HasBeenApproved && !x.HasBeenDenied).Count();

                return domainObj;
            }
            else
            {
                return null;
            }

        }

        private static List <ClassMeetingDomainObj> GetClassMeetingList (ClassSection dbObject)
        {
            if(dbObject.MirrorTargetClassSectionId == null)
            {
                return dbObject.ClassMeetings.Select(xMeeting => ClassMeetingDomainObjBuilder.Build(xMeeting)).ToList();
            }
            else
            {
                if(dbObject.MirrorTargetClassSection != null)
                {
                    return dbObject.MirrorTargetClassSection.ClassMeetings.Select(xMeeting => ClassMeetingDomainObjBuilder.Build(xMeeting)).ToList();
                }
                else //This one should never happen
                {
                    return dbObject.ClassMeetings.Select(xMeeting => ClassMeetingDomainObjBuilder.Build(xMeeting)).ToList();
                }
            }
        }



        /// <summary>
        /// Build a ClassSectionDomainObj from the ClassSection with the given Id.
        /// </summary>
        /// <param name="id">
        ///     ClassSection identifier.
        /// </param>
        /// <returns>
        ///     ClassSectionDomainObj
        /// </returns>
        public ClassSectionDomainObj BuildFromId(Guid id)
        {
            return id == Guid.Empty ? null : Build(_classSectionRepo.GetById(id));
        }


        /// <summary>
        /// Build a ClassSectionDomainObjBasic from the given ClassSection DbObject.
        /// </summary>
        /// <param name="dbObject">
        ///     ClassSection database object.
        /// </param>
        /// <returns>
        ///     ClassSectionDomainObjBasic
        /// </returns>
        public static ClassSectionDomainObjBasic BuildBasic(ClassSection dbObject)
        {
            if(dbObject == null)
            {
                return null;
            }
            ClassSectionDomainObjBasic basicObj = new ClassSectionDomainObjBasic();
            basicObj.Id = dbObject.Id;
            basicObj.Name = dbObject.Name;
            basicObj.CourseUserDisplayName = dbObject.Course.User.DisplayName;
            basicObj.CourseId = dbObject.CourseId;
            basicObj.MirrorTargetClassSectionId = dbObject.MirrorTargetClassSectionId;
            return basicObj;
        }


        /// <summary>
        /// Build a ClassSectionDomainObjBasic from the ClassSection with the given Id.
        /// </summary>
        /// <param name="id">
        ///     ClassSection identifier.
        /// </param>
        /// <returns>
        ///     ClassSectionDomainObjBasic
        /// </returns>
        public ClassSectionDomainObjBasic BuildBasicFromId(Guid id)
        {
            return id == Guid.Empty ? null : BuildBasic(_classSectionRepo.GetById(id));
        }

        /// <summary>
        /// Generates of list of ClassSectionDomainObjBasic associated with a given course Id.
        /// </summary>
        /// <param name="selectedCourseId"></param>
        /// <returns></returns>
        public List<ClassSectionDomainObjBasic> GetClassSectionsForCourseList(Guid selectedCourseId)
        {
            IEnumerable<ClassSection> classSectionList = _classSectionRepo.ClassSectionsForCourse(selectedCourseId);
            return classSectionList == null ? new List<ClassSectionDomainObjBasic>() : classSectionList.Select(x => BuildBasic(x)).ToList();
        }

        public List<ClassSectionDomainObjBasic> ClassSectionsForUser(Guid userId)
        {
            IEnumerable<ClassSection> classSectionList = _classSectionRepo.ClassSectionsForUser(userId);
            return classSectionList == null ? new List<ClassSectionDomainObjBasic>() : classSectionList.Select(x => BuildBasic(x)).ToList();
        }

        public List<ClassSectionDomainObjBasic> ClassSectionsForUserAndCurrentTerm(Guid userId)
        {
            IEnumerable<ClassSection> classSectionList = _classSectionRepo.ClassSectionsForUserAndCurrentTerm(userId);
            return classSectionList == null ? new List<ClassSectionDomainObjBasic>() : classSectionList.Select(x => BuildBasic(x)).ToList();
        }

        public List <ClassSectionDomainObjBasic> ReferenceSectionsForClassSection(Guid classSectionId)
        {
            List<ClassSection> classSectionList = _classSectionRepo.ReferenceSectionsForClassSection(classSectionId);
            return classSectionList == null ? new List<ClassSectionDomainObjBasic>() : classSectionList.Select(x => BuildBasic(x)).ToList();
        }

        public List<ClassSectionDomainObjBasic> PossibleReferenceClassSections(Guid classSectionId, int takeThisMany)
        {
            List<ClassSection> classSectionList = _classSectionRepo.PossibleReferenceClassSections(classSectionId, takeThisMany);
            return classSectionList == null ? new List<ClassSectionDomainObjBasic>() : classSectionList.Select(x => BuildBasic(x)).ToList();
        }

        public void SetLastDisplayedClassMeetingId (Guid classMeetingId, Guid classSectionId)
        {
            _classSectionCUDService.SetLastDisplayedClassMeetingId(classMeetingId, classSectionId);
        }

        public Guid GetLastDisplayedClassMeetingId(Guid currentUserId)
        {
            User user = _userRepo.GetById(currentUserId);
            return _classSectionRepo.GetLastDisplayedClassMeetingId(user);
        }






    }//End Class
}

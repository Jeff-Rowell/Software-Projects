using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel.ObjectBuilders
{


    /// <summary>
    /// A class for building WorkingReleationship domain objects.
    /// </summary>
    public class WorkingRelationshipDomainObjBuilder : IWorkingRelationshipDomainObjBuilder
    {
        private readonly WorkingRelationshipRepo _workingRelRepo;

        /// <summary>
        /// Provides services that build and return WorkingRelDomainObjs.
        /// </summary>
        /// <param name="courseRepo"></param>
        public WorkingRelationshipDomainObjBuilder(WorkingRelationshipRepo workingRelRepo)
        {
            _workingRelRepo = workingRelRepo;

        }

        /// <summary>
        /// A method to build a course domain object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WorkingRelationshipDomainObj BuildFromId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException($"Invalid id {id}. Id must be greater than 0.");
            }

            WorkingRelationship workingRel = _workingRelRepo.GetById(id);
            return workingRel == null ? null : Build(workingRel);

        }

        /// <summary>
        /// A method to build a basic course domain object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WorkingRelationshipDomainObjBasic BuildBasicFromId(Guid id)
        {
            WorkingRelationship workingRel = _workingRelRepo.GetById(id);

            return workingRel == null ? null : BuildBasic(workingRel);
        }

        /// <summary>
        /// A methos to build a couse
        /// </summary>
        /// <param name="workingRel"></param>
        /// <returns>a course domain object</returns>
        public static WorkingRelationshipDomainObj Build(WorkingRelationship dbObj)
        {
            WorkingRelationshipDomainObj domainObj = new WorkingRelationshipDomainObj();

            domainObj.BasicObj = BuildBasic(dbObj);
            domainObj.User = UserDomainObjBuilder.Build(dbObj.User);
            domainObj.FollowUserNarrative = dbObj.FollowUserNarratives;
            domainObj.UserNarrativeSequenceNumber = dbObj.UserNarrativeSequenceNumber;
            domainObj.TargetCourse = CourseDomainObjBuilder.Build(dbObj.Course);
            domainObj.TargetCourseDisplayName = dbObj.Name;
            
            return domainObj;
        }

        /// <summary>
        /// A static method to construct a basic object.
        /// </summary>
        /// <param name="course"></param>
        /// <returns>a basic course domain instance</returns>
        public static WorkingRelationshipDomainObjBasic BuildBasic(WorkingRelationship dbObj)
        {
            WorkingRelationshipDomainObjBasic basicObj = new WorkingRelationshipDomainObjBasic();
            basicObj.Name = dbObj.Name;
            basicObj.Id = dbObj.Id;

            return basicObj;
        }

    }
}
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDataModel.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;

namespace CWMasterTeacherDataModel.ObjectBuilders
{
    /// <summary>
    /// WorkingGroupDomainObjBuilder builds DomainObjects and BasicDomainObjects
    /// by implementing 4 methods: Build, BuildBasic, BuildFromId, and BuildBasicFromId
    /// </summary>
    public class WorkingGroupDomainObjBuilder : IWorkingGroupDomainObjBuilder
    {
        private WorkingGroupRepo _workingGroupRepo;

        /// <summary>
        /// Constructs a WorkingGroupDomainObjBuilder, requires an initialized InstutitionRepo
        /// </summary>
        /// <param name="workingGroupRepo"></param>
        public WorkingGroupDomainObjBuilder(WorkingGroupRepo workingGroupRepo)
        {
            _workingGroupRepo = workingGroupRepo;
        }
        /// <summary>
        /// Build creates WorkingGroupDomainObj from an Instution DbObject
        /// </summary>
        /// <param name="workingGroup"></param>
        /// <returns>WorkingGroupDomainObj</returns>
        public static WorkingGroupDomainObj Build(WorkingGroup workingGroup)
        {
            WorkingGroupDomainObj domainObject = new WorkingGroupDomainObj();
            WorkingGroupDomainObjBasic domainObjectBasic = new WorkingGroupDomainObjBasic();
            domainObjectBasic.WorkingGroupId = workingGroup.Id;
            domainObjectBasic.Name = workingGroup.Name;
            domainObject.WorkingGroupBasicObj = domainObjectBasic;
            domainObject.StudentAccessCode = workingGroup.StudentAccessCode;
            domainObject.InstructorAccessCode = workingGroup.InstructorAccessCode;

            return domainObject;
        }
        
        /// <summary>
        /// BuildFromId generates a WorkingGroupDomainObj from an id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>WorkingGroupDomainObj</returns>
        public WorkingGroupDomainObj BuildFromId(Guid id)
        {
            if(id == Guid.Empty)
            {
                return null;
            }
            else
            {
                WorkingGroup workingGroup = _workingGroupRepo.GetById(id);
                return workingGroup == null ? null : Build(workingGroup);
            }
            
        }

        /// <summary>
        /// BuildBasic generates a WorkingGroupDomainObjBasic from an WorkingGroup DbObject.
        /// </summary>
        /// <param name="workingGroup"></param>
        /// <returns>WorkingGroupDomainObjBasic</returns>
        public static WorkingGroupDomainObjBasic BuildBasic(WorkingGroup workingGroup)
        {
            WorkingGroupDomainObjBasic basicObject = new WorkingGroupDomainObjBasic();
            basicObject.WorkingGroupId = workingGroup.Id;
            basicObject.Name = workingGroup.Name;
            return basicObject;
        }

        /// <summary>
        /// BuildBasicFromId generates a WorkingGroupDomainObjBasic from an id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>WorkingGroupDomainObjBasic</returns>
        public WorkingGroupDomainObjBasic BuildBasicFromId(Guid id)
        {
            return BuildBasic(_workingGroupRepo.GetById(id));
        }

        /// <summary>
        /// Generates a list of basic WorkingGroup domain objects from a list of WorkingGroup
        /// DBobjects. This is necessary here to keep db depenedencies out of the domain object.
        /// </summary>
        /// <param name="workingGroupList"></param>
        /// <returns></returns>
        public List<WorkingGroupDomainObjBasic> AllWorkingGroupsBasicList()
        {
            List<WorkingGroup> workingGroupList = _workingGroupRepo.GetWorkingGroups().ToList();
            List<WorkingGroupDomainObjBasic> workingGroupObjList = workingGroupList.Select(x => BuildBasic(x)).ToList();
            return workingGroupObjList;
        }


        /// <summary>
        /// I know this looks nuts but it's useful in building the WorkingGroupAdmin UI
        /// </summary>
        /// <returns></returns>
        public List<WorkingGroupDomainObjBasic> SingleWorkingGroupBasicList(Guid workingGroupId)
        {
            List<WorkingGroupDomainObjBasic> workingGroupObjList = new List<WorkingGroupDomainObjBasic>();
            WorkingGroup workingGroup = _workingGroupRepo.GetById(workingGroupId);
            workingGroupObjList.Add(BuildBasic(workingGroup));
            return workingGroupObjList;
        }

        public Guid WorkingGroupIdForInstructorAccessCode(string accessCode)
        {
            WorkingGroup workingGroup = _workingGroupRepo.WorkingGroupForInstructorAccessCode(accessCode);
            return workingGroup == null ? Guid.Empty : workingGroup.Id;
        }

        public Guid WorkingGroupIdForStudentAccessCode(string accessCode)
        {
            WorkingGroup workingGroup = _workingGroupRepo.WorkingGroupForStudentAccessCode(accessCode);
            return workingGroup == null ? Guid.Empty : workingGroup.Id;
        }

        public List<WorkingGroupDomainObjBasic> WorkingGroupsForStudentUser(Guid studentUserId)
        {
            return _workingGroupRepo.WorkingGroupsForStudentUser(studentUserId).Select(x => BuildBasic(x)).ToList();
        }
    }
}

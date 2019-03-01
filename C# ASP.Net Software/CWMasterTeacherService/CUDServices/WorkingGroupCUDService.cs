using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class WorkingGroupCUDService
    {
        private WorkingGroupRepo _workingGroupRepo;
        private WorkingGroupPreferenceRepo _workingGroupPreferenceRepo;

        public WorkingGroupCUDService(WorkingGroupRepo workingGroupRepo, WorkingGroupPreferenceRepo workingGroupPreferenceRepo)
        {
            _workingGroupRepo = workingGroupRepo;
            _workingGroupPreferenceRepo = workingGroupPreferenceRepo;
        }

        public string AddOrUpdateWorkingGroup(Guid workingGroupId, string name, string studentAccessCode, string instructorAccessCode)
        {
            WorkingGroup referenceWorkingGroup = _workingGroupRepo.GetById(workingGroupId);
            string referenceStudentCode = "";
            string referenceInstructorCode = "";
            if(referenceWorkingGroup != null)
            {
                referenceStudentCode = referenceWorkingGroup.StudentAccessCode;
                referenceInstructorCode = referenceWorkingGroup.InstructorAccessCode;
            }
            if(studentAccessCode == instructorAccessCode)
            {
                return "Student Access Code and Instructor Access Code have to be different.";
            }
            if(_workingGroupRepo.AccessCodeIsBeingUsed(studentAccessCode) && studentAccessCode != referenceStudentCode)
            {
                return "Student Access Code '" + studentAccessCode + "' is already being used.";
            }
            else if (_workingGroupRepo.AccessCodeIsBeingUsed(instructorAccessCode) && instructorAccessCode != referenceInstructorCode)
            {
                return "Instructor Access Code '" + instructorAccessCode + "' is already being used.";
            }
            else
            {
                WorkingGroup workingGroup = null;
                if (workingGroupId == Guid.Empty)
                {
                    workingGroup = CreateWorkingGroup(name: name, studentAccessCode: studentAccessCode, instructorAccessCode: instructorAccessCode);
                    return workingGroup != null ? "Working Group successfully created." : "Problem creating Working Group.";
                }
                else
                {
                    workingGroup = UpdateWorkingGroup(workingGroupId: workingGroupId, newName: name, studentAccessCode: studentAccessCode, instructorAccessCode: instructorAccessCode);
                    return workingGroup != null ? "Working Group successfully updated." : "Problem creating Working Group.";
                }
            }
        }

        public WorkingGroup CreateWorkingGroup(string name, string studentAccessCode, string instructorAccessCode)
        {
            WorkingGroupPreference groupPref = _workingGroupPreferenceRepo.CreateWorkingGroupPreference(name + "_Preferences");
            WorkingGroup workingGroup = new WorkingGroup();
            workingGroup.WorkingGroupPreferenceId = groupPref.Id;
            workingGroup = SetWorkingGroupValues(workingGroup, name, studentAccessCode, instructorAccessCode);
            _workingGroupRepo.Insert(workingGroup);
            return workingGroup;
        }

        private WorkingGroup SetWorkingGroupValues(WorkingGroup workingGroup, string name, string studentAccessCode, string instructorAccessCode)
        {
            workingGroup.Name = name;
            workingGroup.StudentAccessCode = studentAccessCode;
            workingGroup.InstructorAccessCode = instructorAccessCode;
            return workingGroup;
        }

        public WorkingGroup  UpdateWorkingGroup(Guid workingGroupId, string newName, string studentAccessCode, string instructorAccessCode)
        {
            WorkingGroup workingGroup = _workingGroupRepo.GetById(workingGroupId);
            workingGroup = SetWorkingGroupValues(workingGroup, newName, studentAccessCode, instructorAccessCode);
            _workingGroupRepo.Update(workingGroup);
            return workingGroup;
        }

        public string DeleteWorkingGroupReturnMessage(Guid workingGroupId)
        {
            WorkingGroup workingGroup = _workingGroupRepo.GetById(workingGroupId);
            if (workingGroup != null)
            {
                if (WorkingGroupHasDependencies(workingGroup))
                {
                    return "WorkingGroup cannot be deleted because it has attached Users, Terms, or Courses, MetaCourses, TemplateSets, or WorkingGroupStudents.";
                }
                else
                {
                    _workingGroupRepo.Delete(workingGroup.Id);
                    return "WorkingGroup has been deleted.";
                }
            }
            else
            {
                return "WorkingGroup with that Id does not exist.";
            }
        }

        private bool WorkingGroupHasDependencies(WorkingGroup workingGroup)
        {
            return workingGroup.Users.Count > 0 || 
                        workingGroup.Terms.Count > 0 ||
                        workingGroup.Courses.Count > 0 ||
                        workingGroup.MetaCourses.Count > 0 ||
                        workingGroup.TemplateSets.Count > 0 ||
                        workingGroup.WorkingGroupStudents.Count > 0;
        }





    }
}

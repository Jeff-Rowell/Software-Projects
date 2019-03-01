using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class WorkingRelationshipCUDService
    {
        private LessonCUDService _lessonCUDService;
        WorkingRelationshipRepo _workingRelationshipRepo;

        public WorkingRelationshipCUDService(WorkingRelationshipRepo workingRelationshipRepo, LessonCUDService lessonCUDService)
        {
            _lessonCUDService = lessonCUDService;
            _workingRelationshipRepo = workingRelationshipRepo;
        }

        public WorkingRelationship CreateWorkingRelationship(Course course, Guid targetUserId, Guid targetCourseId, int userNarrativeSequenceNumber, bool followUserNarratives,
                                                            string name)
        {
            WorkingRelationship workRel = new WorkingRelationship();
            workRel.CourseId = course.Id;
            workRel.UserId = targetUserId;
            workRel.UserNarrativeSequenceNumber = userNarrativeSequenceNumber;
            workRel.FollowUserNarratives = followUserNarratives;
            workRel.Name = name;

            _workingRelationshipRepo.Insert(workRel);
            return workRel;
        }

        public void RenumberWorkingRelationshipList(List<WorkingRelationship> workRelList)
        {
            int index = 1;
            foreach (var xWorkRel in workRelList)
            {
                xWorkRel.UserNarrativeSequenceNumber = index;
                _workingRelationshipRepo.Update(xWorkRel);
                index = index + 1;
            }
        }

        public WorkingRelationship RenameWorkingRelationship(WorkingRelationship workRel, string newName)
        {
            workRel.Name = newName;
            _workingRelationshipRepo.Update(workRel);
            return workRel;
        }

        public WorkingRelationship UpdateWorkingRelationshipValues(Guid workRelId, bool followNarratives, int sequenceNumber)
        {
            WorkingRelationship workRel = _workingRelationshipRepo.GetById(workRelId);
            if (workRel.UserNarrativeSequenceNumber != sequenceNumber)
            {
                List<WorkingRelationship> workRelList = _workingRelationshipRepo.GetWorkRelsForCourse(workRel.CourseId);
                foreach (var xWorkRel in workRelList)
                {
                    if (xWorkRel.UserNarrativeSequenceNumber >= sequenceNumber && xWorkRel.Id != workRel.Id)
                    {
                        xWorkRel.UserNarrativeSequenceNumber = xWorkRel.UserNarrativeSequenceNumber + 1;
                        _workingRelationshipRepo.Update(xWorkRel);
                    }
                }
                workRel.UserNarrativeSequenceNumber = sequenceNumber;
                workRel.FollowUserNarratives = followNarratives;
                _workingRelationshipRepo.Update(workRel);

                RenumberWorkRels(workRelList);
            }
            else
            {
                workRel.FollowUserNarratives = followNarratives;
                _workingRelationshipRepo.Update(workRel);
            }

            return workRel;
        }



        private void RenumberWorkRels(List<WorkingRelationship> workRelList)
        {
            workRelList = workRelList.OrderBy(x => x.UserNarrativeSequenceNumber).ToList();
            int index = 1;
            foreach (var xWorkRel in workRelList)
            {
                xWorkRel.UserNarrativeSequenceNumber = index;
                _workingRelationshipRepo.Update(xWorkRel);
                index = index + 1;
            }

        }


    }// End Class
}

using CWMasterTeacherDataModel;
using CWMasterTeacherDomain;
using CWMasterTeacherService.CUDServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDRouters
{
    public class ClassMeetingAdminCUDRouter
    {
        ClassMeetingCUDService _classMeetingCUDService;
        private ClassSectionCUDService _classSectionCUDService;

        public ClassMeetingAdminCUDRouter(ClassMeetingCUDService classMeetingCUDService, ClassSectionCUDService classSectionCUDService )
        {
            _classMeetingCUDService = classMeetingCUDService;
            _classSectionCUDService = classSectionCUDService;
        }

        public void CreateClassMeeting(Guid classSectionId, DateTime meetingDate, string startTimeLocalString, string endTimeLocalString)
        {
            DateTime startTimeLocal = DomainWebUtilities.TimeString_ToDateTime(startTimeLocalString);
            DateTime endTimeLocal = DomainWebUtilities.TimeString_ToDateTime(endTimeLocalString);

            _classMeetingCUDService.CreateClassMeeting(classSectionId, meetingDate, startTimeLocal, endTimeLocal, false);
        }

        public void DeleteClassMeeting(Guid classMeetingId)
        {
            _classMeetingCUDService.DeleteClassMeeting(classMeetingId);
        }

        public void CreateClassMeeting(Guid classSectionId, DateTime meetingDate, DateTime startTimeLocal, DateTime endTimeLocal, bool isFirstDayOfWeek)
        {
            _classMeetingCUDService.CreateClassMeeting(classSectionId, meetingDate, startTimeLocal, endTimeLocal, isFirstDayOfWeek);
        }

        public void DeleteClassSection (Guid classSectionId)
        {
            _classSectionCUDService.DeleteClassSection(classSectionId);
        }

        public void MarkClassMeetingAsNoClass(Guid selectedClassMeetingId, bool selectedClassMeetingHasNoClass, string noClassComment)
        {
            if (!selectedClassMeetingHasNoClass || noClassComment.Length > 3)
            {
                if (noClassComment.Length <= 3)
                {
                    noClassComment = "NA";
                }
                _classMeetingCUDService.SetNoClass(selectedClassMeetingId, selectedClassMeetingHasNoClass, noClassComment);

            }
        }

        public void RenameClassSection(Guid classSectionId, string classSectionName)
        {
            _classSectionCUDService.RenameClassSection(classSectionId, classSectionName);
        }

        public void CreateMirrorClassSection(Guid mirrorCourseId, Guid targetClassSectionId)
        {
            _classSectionCUDService.CreateMirrorClassSection(mirrorCourseId, targetClassSectionId);
        }
    }
}

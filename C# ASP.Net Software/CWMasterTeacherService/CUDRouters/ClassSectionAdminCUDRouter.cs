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
    public class ClassSectionAdminCUDRouter
    {
        private ClassSectionCUDService _classSectionCUDService;
        private ClassMeetingCUDService _classMeetingCUDService;
        private TermCUDService _termCUDService;
        private CourseCUDService _courseCUDService;

        public ClassSectionAdminCUDRouter(ClassSectionCUDService classSectionCUDService,
                                            ClassMeetingCUDService classMeetingCUDService,
                                            TermCUDService termCUDService,
                                            CourseCUDService courseCUDService)
        {
            _classSectionCUDService = classSectionCUDService;
            _classMeetingCUDService = classMeetingCUDService;
            _termCUDService = termCUDService;
            _courseCUDService = courseCUDService;
        }

        public Guid CreateClassSectionReturnId(Guid courseId, string classSectionName, string startTimeLocalString, string endTimeLocalString,
                                        bool meetsSunday, bool meetsMonday,
                                        bool meetsTuesday, bool meetsWednesday, bool meetsThursday, bool meetsFriday, bool meetsSaturday)
        {
            if (meetsSunday || meetsMonday || meetsTuesday || meetsWednesday || meetsThursday || meetsFriday || meetsSaturday)
            {
                ClassSection classSection = _classSectionCUDService.CreateClassSection(courseId, classSectionName);

                DateTime startTimeLocal = DomainWebUtilities.TimeString_ToDateTime(startTimeLocalString);
                DateTime endTimeLocal = DomainWebUtilities.TimeString_ToDateTime(endTimeLocalString);


                _classMeetingCUDService.CreateClassMeetingsForClassSection(classSection: classSection, 
                                                                            startTimeLocal: startTimeLocal, 
                                                                            endTimeLocal: endTimeLocal, 
                                                                            meetsSunday: meetsSunday, 
                                                                            meetsMonday: meetsMonday, 
                                                                            meetsTuesday: meetsTuesday, 
                                                                            meetsWednesday: meetsWednesday, 
                                                                            meetsThursday: meetsThursday, 
                                                                            meetsFriday: meetsFriday, 
                                                                            meetsSaturday: meetsSaturday);
                return classSection.Id;
            }
            else
            {
                return Guid.Empty;
            }
        }

    }//End Class
}

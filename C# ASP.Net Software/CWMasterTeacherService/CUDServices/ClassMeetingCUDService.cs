using CWMasterTeacherDataModel;
using CWMasterTeacherDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class ClassMeetingCUDService
    {
        private ClassSectionRepo _classSectionRepo;
        private HolidayRepo _holidayRepo;
        private ClassMeetingRepo _classMeetingRepo;
        private CourseRepo _courseRepo;


        public ClassMeetingCUDService(ClassMeetingRepo classMeetingRepo, ClassSectionRepo classSectionRepo, HolidayRepo holidayRepo, CourseRepo courseRepo)
        {
            _classSectionRepo = classSectionRepo;
            _holidayRepo = holidayRepo;
            _classMeetingRepo = classMeetingRepo;
            _courseRepo = courseRepo;
        }


        public void CreateClassMeetingsForClassSection(ClassSection classSection, DateTime startTimeLocal, DateTime endTimeLocal, 
                                        bool meetsSunday, bool meetsMonday, bool meetsTuesday, bool meetsWednesday, bool meetsThursday,
                                        bool meetsFriday, bool meetsSaturday)
        {
            //We have to call the repo and not just rely on Entity Framework because the Class Section may be brand new and won't be carrying more than the Id
            Course course = _courseRepo.GetById(classSection.CourseId);
            Term term = course.Term;
            DateTime startDate = term.StartDate;
            DateTime endDate = term.EndDate;


            DayOfWeek firstDayOfWeek = FirstDayOfWeek(meetsSunday, meetsMonday, meetsTuesday, meetsWednesday, meetsThursday, meetsFriday, meetsSaturday);
            for (DateTime xDate = startDate; xDate <= endDate; xDate = xDate.AddDays(1.0))
            {
                if (xDate.DayOfWeek == DayOfWeek.Sunday && meetsSunday ||
                    xDate.DayOfWeek == DayOfWeek.Monday && meetsMonday ||
                    xDate.DayOfWeek == DayOfWeek.Tuesday && meetsTuesday ||
                    xDate.DayOfWeek == DayOfWeek.Wednesday && meetsWednesday ||
                    xDate.DayOfWeek == DayOfWeek.Thursday && meetsThursday ||
                    xDate.DayOfWeek == DayOfWeek.Friday && meetsFriday ||
                    xDate.DayOfWeek == DayOfWeek.Saturday && meetsSaturday)
                {
                    DateTime fullStartTimeLocal = new DateTime(xDate.Year, xDate.Month, xDate.Day, startTimeLocal.Hour, startTimeLocal.Minute, startTimeLocal.Second);
                    DateTime fullEndTime = new DateTime(xDate.Year, xDate.Month, xDate.Day, endTimeLocal.Hour, endTimeLocal.Minute, endTimeLocal.Second);
                    bool isFirstDayOfWeek = xDate.DayOfWeek == firstDayOfWeek;
                    CreateClassMeeting(classSection.Id, xDate, fullStartTimeLocal, fullEndTime, isFirstDayOfWeek);
                }
            }

            MarkHolidays(classSection);
            ReNumberMeetings(classSection.Id);

        }
        /// <summary>
        /// Gets the first day of the week that a class meets, given the booleans for the days that it meets.  Used in labeling weeks in calendar
        /// </summary>
        private DayOfWeek FirstDayOfWeek(bool meetsSunday, bool meetsMonday, bool meetsTuesday, bool meetsWednesday, bool meetsThursday,
                                                bool meetsFriday, bool meetsSaturday)
        {
            if (meetsSunday)
            {
                return DayOfWeek.Sunday;
            }
            else if (meetsMonday)
            {
                return DayOfWeek.Monday;
            }
            else if (meetsTuesday)
            {
                return DayOfWeek.Tuesday;
            }
            else if (meetsWednesday)
            {
                return DayOfWeek.Wednesday;
            }
            else if (meetsThursday)
            {
                return DayOfWeek.Thursday;
            }
            else if (meetsFriday)
            {
                return DayOfWeek.Friday;
            }
            else
            {
                return DayOfWeek.Saturday;
            }
        }

        public ClassMeeting CreateClassMeeting(Guid classSectionId, DateTime meetingDate, DateTime startTimeLocal, DateTime endTimeLocal, bool isFirstDayOfWeek)
        {
            ClassMeeting classMeeting = new ClassMeeting();
            classMeeting.ClassSectionId = classSectionId;

            classMeeting.StartTime = DomainUtilities.ConvertLocalTimeToUtc(startTimeLocal);
            classMeeting.EndTime = DomainUtilities.ConvertLocalTimeToUtc(endTimeLocal);

            classMeeting.MeetingDate = meetingDate;
            classMeeting.IsBeginningOfWeek = isFirstDayOfWeek;
            classMeeting.Comment = "NA";

            _classMeetingRepo.Insert(classMeeting);

            return classMeeting;
        }

        /// <summary>
        /// Renumbers meetings to cover up any gaps in the sequence numbers to to moving things around, etc.
        /// </summary>
        public void ReNumberMeetings(Guid classSectionId)
        {
            List<ClassMeeting> meetingList = _classMeetingRepo.ClassMeetingsForClassSection(classSectionId).ToList();

            int meetingNumber = 1;
            foreach (var xMeeting in meetingList)
            {
                if (!xMeeting.NoClass && !xMeeting.IsExamDay)
                {
                    xMeeting.MeetingNumber = meetingNumber;
                    meetingNumber = meetingNumber + 1;
                    _classMeetingRepo.Update(xMeeting);
                }
                else
                {
                    xMeeting.MeetingNumber = -1;
                    _classMeetingRepo.Update(xMeeting);
                }

            }
        }

        /// <summary>
        /// Goes through the class meeetings for a class section and marks the dates that were set as holidays in the Term object.
        /// </summary>
        public void MarkHolidays(ClassSection classSection)
        {
            Guid termId = classSection.Course.TermId;
            List<Holiday> holidayList = _holidayRepo.HolidaysForTerm(termId).ToList();
            foreach (var xMeeting in classSection.ClassMeetings)
            {
                foreach (var xHoliday in holidayList)
                {
                    if (xMeeting.MeetingDate.Date == xHoliday.Date.Date)
                    {
                        SetNoClassForMeeting(xMeeting, true, xHoliday.Name);
                    }
                }
            }
        }

        /// <summary>
        /// Sets the NoClass boolean for a ClassMeeting. "SetNoClass" calls this but just needs an Id
        /// </summary>
        private void SetNoClassForMeeting(ClassMeeting classMeeting, bool hasNoClass, string comment = "")
        {
            classMeeting.NoClass = hasNoClass;
            classMeeting.Comment = comment;
            _classMeetingRepo.Update(classMeeting);
        }

        /// <summary>
        /// Calls SetNoClassForMeeting but takes in an Id instead of a ClassMeetingObject
        /// </summary>
        public void SetNoClass(Guid classMeetingId, bool hasNoClass, string comment = "")
        {
            ClassMeeting classMeeting = _classMeetingRepo.GetById(classMeetingId);
            SetNoClassForMeeting(classMeeting, hasNoClass, comment);
        }

        /// <summary>
        /// Sets the IsReadyToTeach boolean which shows up as a check box indicating that ClassMeeting is ready to teach.
        /// </summary>
        /// <param name="classMeetingId"></param>
        /// <param name="isReadyToTeach"></param>
        public void SetClassMeetingIsReady(Guid classMeetingId, bool isReadyToTeach)
        {
            if (classMeetingId != Guid.Empty)
            {
                ClassMeeting classMeeting = _classMeetingRepo.GetById(classMeetingId);
                classMeeting.IsReadyToTeach = isReadyToTeach;
                _classMeetingRepo.Update(classMeeting);
            }
        }

        public void DeleteClassMeeting(Guid classMeetingId)
        {
            _classMeetingRepo.Delete(classMeetingId);
        }

        public void SaveNotesForStudents(string notesForStudents, Guid selectedClassMeetingId)
        {
            ClassMeeting classMeeting = _classMeetingRepo.GetById(selectedClassMeetingId);
            if(classMeeting != null)
            {
                classMeeting.NotesForStudents = notesForStudents;
                _classMeetingRepo.Update(classMeeting);
            }
        }



    }//End Class
}

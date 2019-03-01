using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class LessonUseCUDService
    {
        private LessonUseRepo _lessonUseRepo;
        private ClassMeetingRepo _classMeetingRepo;
        private ReleasedDocumentRepo _releasedDocumentRepo;

        public LessonUseCUDService(LessonUseRepo lessonUseRepo, ClassMeetingRepo classMeetingRepo,
                                    ReleasedDocumentRepo releasedDocumentRepo)
        {
            _lessonUseRepo = lessonUseRepo;
            _classMeetingRepo = classMeetingRepo;
            _releasedDocumentRepo = releasedDocumentRepo;
        }

        public Guid CreateLessonUseReturnId(Guid lessonId, Guid classMeetingId)
        {
            return CreateLessonUse(lessonId, classMeetingId).Id;
        }

        public LessonUse CreateLessonUse(Guid lessonId, Guid classMeetingId, bool doPutAtStartOfList = false)
        {
            ClassMeeting classMeeting = _classMeetingRepo.GetById(classMeetingId);

            if (!classMeeting.NoClass)
            {
                LessonUse lessonUse = new LessonUse();
                lessonUse.LessonId = lessonId;
                lessonUse.ClassMeetingId = classMeetingId;
                lessonUse.SequenceNumber = doPutAtStartOfList ? ClassMeetingMinimumSequenceNum(classMeeting) - 1 : classMeeting.LessonUses.Count + 1;
                lessonUse.CustomName = "";
                lessonUse.CustomText = "";

                _lessonUseRepo.Insert(lessonUse);

                return lessonUse;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Creates a LessonUse that does not point to a Lesson object.  This supports the creation of "custom lessons" that are used once, then discarded.
        /// </summary>

        public LessonUse CreateIndependentLessonUse(Guid classMeetingId, int sequenceNumber)
        {
            ClassMeeting classMeeting = _classMeetingRepo.GetById(classMeetingId);

            if (classMeeting != null && !classMeeting.NoClass)
            {
                LessonUse lessonUse = new LessonUse();
                lessonUse.LessonId = null;
                lessonUse.ClassMeetingId = classMeetingId;
                if (sequenceNumber > 0)
                {
                    foreach (var xLessonUse in classMeeting.LessonUses)
                    {
                        if (xLessonUse.SequenceNumber >= sequenceNumber)
                        {
                            xLessonUse.SequenceNumber = xLessonUse.SequenceNumber + 1;
                            _lessonUseRepo.Update(xLessonUse);
                        }
                    }
                    lessonUse.SequenceNumber = sequenceNumber;
                }
                else
                {
                    lessonUse.SequenceNumber = classMeeting.LessonUses.Count + 1;
                }

                lessonUse.CustomName = "";
                lessonUse.CustomText = "";

                _lessonUseRepo.Insert(lessonUse);

                return lessonUse;
            }
            else
            {
                return null;
            }
        }

        public void DeleteLessonUse(Guid lessonUseId)
        {
            LessonUse lessonUse = _lessonUseRepo.GetById(lessonUseId);
            Guid classMeetingId = lessonUse.ClassMeetingId;
            DeleteLessonUse(lessonUseId, classMeetingId);
        }

        public void DeleteLessonUse(Guid lessonUseId, Guid classMeetingId)
        {

            _lessonUseRepo.Delete(lessonUseId);
            ReNumberLessonUses(classMeetingId);
        }

        /// <summary>
        /// Moves selected LessonUse to the next ClassMeeting after the one it is currently scheduled for.
        /// </summary>
        public void MoveLessonUseForward(Guid lessonUseId, bool doPutAtStartOfList = false)
        {
            LessonUse lessonUse = _lessonUseRepo.GetById(lessonUseId);
            ClassMeeting meeting = lessonUse.ClassMeeting;
            ClassMeeting nextMeeting = GetNextMeetingThatMeets(meeting);

            if (nextMeeting != null && !nextMeeting.NoClass)
            {
                lessonUse.ClassMeetingId = nextMeeting.Id;
                lessonUse.SequenceNumber = doPutAtStartOfList ? ClassMeetingMinimumSequenceNum(nextMeeting) - 1 : nextMeeting.LessonUses.Count + 1;

                _lessonUseRepo.Update(lessonUse);

                ReNumberLessonUses(meeting.Id);
            }
        }

        private int ClassMeetingMinimumSequenceNum(ClassMeeting classMeeting)
        {
            if(classMeeting.LessonUses.Count > 0)
            {
                return classMeeting.LessonUses.Min(x => x.SequenceNumber.GetValueOrDefault());
            }
            else
            {
                return 0;
            }
        }

        public void CopyLessonUseForward(Guid lessonUseId)
        {
            LessonUse lessonUse = _lessonUseRepo.GetById(lessonUseId);
            ClassMeeting meeting = lessonUse.ClassMeeting;
            ClassMeeting nextMeeting = GetNextMeetingThatMeets(meeting);
            if(lessonUse.LessonId != null)
            {
                CreateLessonUse(lessonId: lessonUse.LessonId.Value, classMeetingId:  nextMeeting.Id, doPutAtStartOfList: true);
            }
        }

        private ClassMeeting GetNextMeetingThatMeets(ClassMeeting meeting)
        {
            ClassMeeting nextMeeting = _classMeetingRepo.NextClassMeetingAfterThisOne(meeting);

            while (nextMeeting != null && nextMeeting.NoClass)
            {
                nextMeeting = _classMeetingRepo.NextClassMeetingAfterThisOne(nextMeeting);
            }
            return nextMeeting;
        }

        /// <summary>
        /// Moves a LessonUse to the previous class meeting from the one for which it is currently scheduled
        /// </summary>
        public void MoveLessonUseBack(Guid lessonUseId)
        {
            LessonUse lessonUse = _lessonUseRepo.GetById(lessonUseId);
            ClassMeeting meeting = lessonUse.ClassMeeting;

            ClassMeeting lastMeeting = _classMeetingRepo.LastClassMeetingBeforeThisOne(meeting);

            while (lastMeeting != null && lastMeeting.NoClass)
            {
                lastMeeting = _classMeetingRepo.LastClassMeetingBeforeThisOne(lastMeeting);
            }
            if (lastMeeting != null && !lastMeeting.NoClass)
            {
                lessonUse.ClassMeetingId = lastMeeting.Id;
                lessonUse.SequenceNumber = lastMeeting.LessonUses.Count + 1;

                _lessonUseRepo.Update(lessonUse);
                ReNumberLessonUses(meeting.Id);
            }

        }

        /// <summary>
        /// Raises LessonUse sequence number and adjusts other lessons accordingly in order to move it down in the list.
        /// </summary>
        public void RaiseLessonUseSequenceNumber(Guid lessonUseId)
        {
            LessonUse lessonUse = _lessonUseRepo.GetById(lessonUseId);
            int sequenceNumber = lessonUse.SequenceNumber.Value;
            LessonUse lessonUseAbove = _lessonUseRepo.LessonUseAboveGivenSequenceNumber(lessonUse.ClassMeeting, sequenceNumber);

            if (lessonUseAbove != null)
            {
                lessonUse.SequenceNumber = sequenceNumber + 1;
                lessonUseAbove.SequenceNumber = sequenceNumber;
                _lessonUseRepo.Update(lessonUse);
                _lessonUseRepo.Update(lessonUseAbove);
            }
        }

        /// <summary>
        /// Lowers LessonUse sequence number and adjusts other lessons accordingly in order to move it up in the list.
        /// </summary>
        public void LowerLessonUseSequenceNumber(Guid lessonUseId)
        {
            LessonUse lessonUse = _lessonUseRepo.GetById(lessonUseId);
            int sequenceNumber = lessonUse.SequenceNumber.Value;
            LessonUse lessonUseBelow = _lessonUseRepo.LessonUseBelowGivenSequenceNumber(lessonUse.ClassMeeting, sequenceNumber);
            if (lessonUseBelow != null)
            {
                lessonUse.SequenceNumber = sequenceNumber - 1;
                lessonUseBelow.SequenceNumber = sequenceNumber;
                _lessonUseRepo.Update(lessonUse);
                _lessonUseRepo.Update(lessonUseBelow);
            }
        }

        /// <summary>
        /// Renumbers LessonUses in order to remove any gaps in the numbering that may have resulted from deletions or re-ordering.
        /// </summary>
        public void ReNumberLessonUses(Guid classMeetingId)
        {
            int index = 1;
            foreach (LessonUse xUse in _lessonUseRepo.LessonUsesForClassMeeting(classMeetingId))
            {
                xUse.SequenceNumber = index;
                index = index + 1;
                _lessonUseRepo.Update(xUse);
            }
        }

        public Guid CustomLessonPlanUpdateReturnId(Guid lessonUseId, Guid selectedClassMeetingId, int lessonUseSequenceNumber,
                                            string customName, string customText)
        {
            LessonUse lessonUse;
            if (lessonUseId != Guid.Empty)
            {
                lessonUse = _lessonUseRepo.GetById(lessonUseId);
            }
            else
            {
                lessonUse = CreateIndependentLessonUse(selectedClassMeetingId, lessonUseSequenceNumber);
            }

            lessonUse.CustomName = customName;
            lessonUse.CustomText = customText;

            _lessonUseRepo.Update(lessonUse);

            return lessonUse.Id;
        }



    }//End Class
}

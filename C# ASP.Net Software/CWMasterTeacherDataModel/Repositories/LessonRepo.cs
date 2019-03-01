using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class LessonRepo: BaseRepo<Lesson>, ILessonRepo
    {
        private NarrativeRepo _narrativeRepo;
        private DocumentUseRepo _documentUseRepo;
        private LessonPlanRepo _lessonPlanRepo;
        private MessageUseRepo _messageUseRepo;
        private TermRepo _termRepo;

        public LessonRepo(MasterTeacherContext context, NarrativeRepo narrativeRepo, 
                                DocumentUseRepo documentUseRepo, LessonPlanRepo lessonPlanRepo,
                                MessageUseRepo messageUseRepo, TermRepo termRepo)
            : base(context)
        {
            _narrativeRepo = narrativeRepo;
            _documentUseRepo = documentUseRepo;
            _lessonPlanRepo = lessonPlanRepo;
            _messageUseRepo = messageUseRepo;
            _termRepo = termRepo;
        }


        /// <summary>
        /// Active lessons for course.  Ordered by SequenceNumber
        /// </summary>
        public IEnumerable<Lesson> LessonsForCourse(Guid courseId)
        {
            var listQuery = from x in _context.Lessons
                                             where x.CourseId == courseId  && x.IsActive 
                                             orderby x.SequenceNumber 
                                             select x;
            return listQuery.ToList();                                      
        }

        /// <summary>
        /// Active Lessons for ContainerLesson.  Ordered by SequenceNumber
        /// </summary>
        public IEnumerable<Lesson> LessonsForContainerLesson(Guid containerLessonId)
        {
            var listQuery = from x in _context.Lessons
                            where x.ContainerLessonId == containerLessonId && x.IsActive
                            orderby x.SequenceNumber 
                            select x;
            return listQuery.ToList();
        }

        /// <summary>
        /// Active Lessons for Container Course.  Ordered by SequenceNumber.
        /// </summary>
        public IEnumerable<Lesson> LessonsForContainerCourse(Guid containerCourseId)
        {
            var listQuery = from x in _context.Lessons
                            where x.CourseId == containerCourseId && x.ContainerLessonId== null && x.IsActive
                            orderby x.SequenceNumber 
                            select x;
            return listQuery.ToList();
        }


        public IEnumerable<Lesson> LessonsThatShareMaster(Guid masterLessonId)
        {
            var listQuery = from x in _context.Lessons
                            where (x.MasterLessonId == masterLessonId || x.Id == masterLessonId) && x.IsActive
                            select x;
            return listQuery.ToList();
        }

        /// <summary>
        /// Used in buiding CurriculumViewModel and setting the value for returning home after toggling to master lesson.
        /// Finds all lessons for Master and User, orders by DateModified descending, then returns FirstOrDefault.
        /// </summary>
        public Lesson LessonForMasterLessonAndUser(Guid masterLessonId, Guid userId)
        {
            var query = from x in _context.Lessons
                        where x.MasterLessonId == masterLessonId && x.Course.UserId == userId && x.IsActive
                        orderby x.DateTimeCreated descending
                        select x;
            return query.FirstOrDefault();
        }

        public Lesson LessonForMetaLessonAndCourse(Guid metaLessonId, Guid courseId)
        {
            var query = from x in _context.Lessons
                        where x.MetaLessonId == metaLessonId && x.Course.Id == courseId && x.IsActive
                        orderby x.DateTimeCreated descending
                        select x;
            return query.FirstOrDefault();
        }

        public IEnumerable<Lesson> ActiveLessonsForCourse(Guid courseId)
        {
            var listQuery = from x in _context.Lessons
                            where x.CourseId == courseId && x.IsActive
                            select x;
            return listQuery.ToList();
        }

        /// <summary>
        /// Lessons that share container, which could be a Lesson or a Course, with the given lesson.
        /// </summary>
        /// <param name="lesson"></param>
        /// <returns></returns>
        public IEnumerable<Lesson> LessonsThatShareContainer(Lesson lesson)
        {
            if (lesson.ContainerLessonId != null)
            {
                return LessonsForContainerLesson(lesson.ContainerLessonId.Value);
            }
            else  //Which means the container is a course
            {
                return LessonsForContainerCourse(lesson.CourseId);
            }
        }

        
        /// <summary>
        /// Gets the Individual Narrative for a Lesson.  Will return null if Lesson is master.
        /// </summary>
        public Narrative GetIndividualNarrative(Guid lessonId)
        {
            Lesson lesson = GetById(lessonId);
            if (lesson.Course.IsMaster)  //Which means this is a master
            {
                return null;
            }
            else
            {
                return NarrativeForLesson(lessonId);
            }
        }


        /// <summary>
        /// Returns narrative for master lesson of given lesson, which may be itself.
        /// </summary>
        public Narrative GetMasterNarrative(Guid lessonId)
        {
            Lesson lesson = GetById(lessonId);
            if (lesson.Course.IsMaster) 
            {
                return NarrativeForLesson (lesson.Id);
            }
            else if (lesson.MasterLesson != null)
            {
                return NarrativeForLesson(lesson.MasterLesson.Id);
            }
            else
            {
                return null;
            }
        }

 

      

        /// <summary>
        /// Lessons that have a sequence number strictly greater than given Lesson
        /// </summary>
        public Lesson LessonAboveGivenLesson(Lesson givenLesson)
        {
            List<Lesson> lessonList = LessonsThatShareContainer(givenLesson).ToList();
            var lessonQuery = from x in lessonList
                           where  x.SequenceNumber > givenLesson.SequenceNumber 
                           orderby x.SequenceNumber
                           select x;
            return lessonQuery.FirstOrDefault();
        }

        /// <summary>
        /// Lessons that have a sequence number strictly less than given Lesson
        /// </summary>
        public Lesson LessonBelowGivenLesson(Lesson givenLesson)
        {
            List<Lesson> lessonList = LessonsThatShareContainer(givenLesson).ToList();
            var lessonQuery = from x in lessonList
                              where x.SequenceNumber < givenLesson.SequenceNumber
                              orderby x.SequenceNumber descending 
                              select x;
            return lessonQuery.FirstOrDefault();
        }

        private int MaxSequenceNumber(List<Lesson> lessonList)
        {
            int maxInt = 0;
            foreach (var xLesson in lessonList)
            {
                if (xLesson.SequenceNumber!= null && xLesson.SequenceNumber  > maxInt)
                {
                    maxInt = xLesson.SequenceNumber.Value ;
                }
            }
            return maxInt;
        }


        public string GetLessonPlanTextForLessonById(Guid lessonId)
        {
            LessonPlan lessonPlan = GetLessonPlanForLessonById(lessonId);
            if(lessonPlan != null)
            {
                return lessonPlan.Text;
            }
            else
            {
                return "";
            }
        }

        public LessonPlan GetLessonPlanForLessonById(Guid lessonId)
        {
            Lesson lesson = GetById(lessonId);
            return lesson == null ? null : lesson.LessonPlan;
        }

        /// <summary>
        /// Returns the Narrative from the most recent LessonNarrative created for this Lesson.
        /// This was updated on June 12, 2017
        /// </summary>
        public Narrative NarrativeForLesson(Guid lessonId)
        {
            Lesson lesson = GetById(lessonId);

            if (lesson.NarrativeId != null && lesson.NarrativeId != Guid.Empty)
            {
                return lesson.Narrative;
            }
            else
            {
                return null;
            }
        }

        public void SetLessonPlanForLesson(Guid lessonId, Guid lessonPlanId)
        {
            Lesson lesson = GetById(lessonId);
            lesson.LessonPlanId = lessonPlanId;
            Update(lesson);
        }

        public List<Lesson>LessonsThatShareMetaLesson(Guid metaLessonId)
        {
            var metaQuery = from x in _context.Lessons
                            where x.MetaLessonId == metaLessonId
                            select x;
            return metaQuery.ToList();
        }

        public void SetLessonPlanDateChoiceConfirmed(Lesson lesson, DateTime date)
        {
            lesson.LessonPlanDateChoiceConfirmed = date;
            Update(lesson);
        }



    }//end class
}//end namespace

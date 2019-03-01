using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel.Interfaces
{
    public interface ILessonRepo : IRepository<Lesson>
    {
        IEnumerable<Lesson> ActiveLessonsForCourse(Guid courseId);
        IEnumerable<Lesson> LessonsForCourse(Guid courseId);
       IEnumerable<Lesson> LessonsForContainerLesson(Guid containerLessonId);
       IEnumerable<Lesson> LessonsForContainerCourse(Guid containerCourseId);
       IEnumerable<Lesson> LessonsThatShareMaster(Guid masterLessonId);
       Lesson LessonForMasterLessonAndUser(Guid masterLessonId, Guid userId);
       Lesson LessonForMetaLessonAndCourse(Guid metaLessonId, Guid courseId);
       IEnumerable<Lesson> LessonsThatShareContainer(Lesson lesson);
       Narrative GetIndividualNarrative(Guid lessonId);
       Narrative GetMasterNarrative(Guid lessonId);
       Lesson LessonAboveGivenLesson(Lesson givenLesson);
       Lesson LessonBelowGivenLesson(Lesson givenLesson);
       string GetLessonPlanTextForLessonById(Guid lessonId);
       LessonPlan GetLessonPlanForLessonById(Guid lessonId);
       Narrative NarrativeForLesson(Guid lessonId);
       void SetLessonPlanForLesson(Guid lessonId, Guid lessonPlanId);
       List<Lesson> LessonsThatShareMetaLesson(Guid metaLessonId);
       void SetLessonPlanDateChoiceConfirmed(Lesson lesson, DateTime date);
    }
}

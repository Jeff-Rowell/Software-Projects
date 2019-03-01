using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDataModel.Interfaces;

namespace CWMasterTeacherService.CUDServices
{
    public class LessonCUDService
    {
        private MasterTeacherContext _context;
        private INarrativeRepo _narrativeRepo;
        private IDocumentUseRepo _documentUseRepo;
        private ILessonPlanRepo _lessonPlanRepo;
        private IStashedLessonPlanRepo _stashedLessonPlanRepo;
        private IStashedNarrativeRepo _stashedNarrativeRepo;
        private IMessageUseRepo _messageUseRepo;
        private ITermRepo _termRepo;
        private IMetaLessonRepo _metaLessonRepo;
        private ICourseRepo _courseRepo;
        private ILessonRepo _lessonRepo;
        private DocumentUseCUDService _documentUseCUDService;
        private StashedLessonPlanCUDService _lessonPlanLessonCUDService;
        private MessageUseCUDService _messageUseCUDService;
        private IMessageUseDomainObjBuilder _messageUseObjBuilder;
        private SharedCUDService _sharedCUDService;
        private LessonPlanCUDService _lessonPlanCUDService;
        private NarrativeCUDService _narrativeCUDService;
        private ILessonDomainObjBuilder _lessonDomainObjBuilder;

        public LessonCUDService(MasterTeacherContext context, ILessonRepo lessonRepo, INarrativeRepo narrativeRepo,
                                IDocumentUseRepo documentUseRepo, ILessonPlanRepo lessonPlanRepo, IStashedLessonPlanRepo stashedLessonPlanRepo,
                                IMessageUseRepo messageUseRepo, ITermRepo termRepo, IMetaLessonRepo metaLessonRepo, ICourseRepo courseRepo,
                                DocumentUseCUDService documentUseCUDService, IStashedNarrativeRepo stashedNarrativeRepo,
                                StashedLessonPlanCUDService lessonPlanLessonCUDService, MessageUseCUDService messageUseCUDService,
                                LessonPlanCUDService lessonPlanCUDService, NarrativeCUDService narrativeCUDService,
                                IMessageUseDomainObjBuilder messageUseObjBuilder, SharedCUDService sharedCUDService,
                                ILessonDomainObjBuilder lessonDomainObjBuilder)
        {
            _context = context;
            _narrativeRepo = narrativeRepo;
            _documentUseRepo = documentUseRepo;
            _lessonPlanRepo = lessonPlanRepo;
            _stashedLessonPlanRepo = stashedLessonPlanRepo;
            _stashedNarrativeRepo = stashedNarrativeRepo;
            _messageUseRepo = messageUseRepo;
            _termRepo = termRepo;
            _lessonRepo = lessonRepo;
            _metaLessonRepo = metaLessonRepo;
            _courseRepo = courseRepo;
            _documentUseCUDService = documentUseCUDService;
            _messageUseCUDService = messageUseCUDService;
            _lessonPlanLessonCUDService = lessonPlanLessonCUDService;
            _messageUseObjBuilder = messageUseObjBuilder;
            _sharedCUDService = sharedCUDService;
            _lessonPlanCUDService = lessonPlanCUDService;
            _narrativeCUDService = narrativeCUDService;
            _lessonDomainObjBuilder = lessonDomainObjBuilder;
        }

        /// <summary>
        /// We can call this from either Create or Update.  Values in given lesson will be updated to paramets passed in.  
        /// Ids will only be updated if != Guid.Empty.  
        /// </summary>
        private Lesson SetLessonValues(Lesson lesson, string name, Guid containerLessonId, Guid courseId, Guid masterLessonId,
                        Guid predecessorLessonId, int sequenceNumber, bool isActive, bool isFolder, Guid metaLessonId, bool isHidden,
                        Guid lessonPlanId, Guid narrativeId, DateTime dateModified)
        {
            lesson.Name = name;
            lesson.MetaLessonId = metaLessonId;
            lesson.LessonPlanId = lessonPlanId;
            lesson.NarrativeId = narrativeId;

            lesson.CourseId = courseId;

            if (containerLessonId != Guid.Empty)
            {
                lesson.ContainerLessonId = containerLessonId;
            }
            else
            {
                lesson.ContainerLessonId = null;
            }

            if (courseId != Guid.Empty)
            {
                lesson.CourseId = courseId;
            }
            if (masterLessonId != Guid.Empty)
            {
                lesson.MasterLessonId = masterLessonId;
            }
            if (metaLessonId != Guid.Empty)
            {
                lesson.MetaLessonId = metaLessonId;
            }
            if (predecessorLessonId != Guid.Empty)
            {
                lesson.PredecessorLessonId = predecessorLessonId;
            }
            if (sequenceNumber > 0)
            {
                lesson.SequenceNumber = sequenceNumber;
            }
            lesson.IsActive = isActive;
            lesson.IsFolder = isFolder;
            lesson.IsHidden = isHidden;

            lesson.DateTimeCreated = DateTime.Now;
            lesson.DateTimeDocumentsChoiceConfirmed = DateTime.Now;

            return lesson;
        }

        private Lesson SetLessonDates(Lesson lesson, DateTime dateTimeDocumentsChoiceConfirmed,
                                        DateTime referenceDateTimeDocChoiceConfirmed, DateTime groupMostRecentDocModDate,
                                        DateTime dateDocumentsModified, DateTime lessonPlanDateChoiceConfirmed,
                                        DateTime lessonPlanReferenceDateChoiceConfirmed, DateTime lessonPlanGroupMostRecentModDate,
                                        DateTime narrativeDateChoiceConfirmed, DateTime narrativeReferenceDateChoiceConfirmed,
                                        DateTime narrativeGroupMostRecentModDate)
        {
            lesson.DateTimeDocumentsChoiceConfirmed = dateTimeDocumentsChoiceConfirmed;
            lesson.ReferenceDateTimeDocChoiceConfirmed = referenceDateTimeDocChoiceConfirmed;
            lesson.DateDocumentsModified = dateDocumentsModified;
            lesson.LessonPlanDateChoiceConfirmed = lessonPlanDateChoiceConfirmed;
            lesson.LessonPlanReferenceDateChoiceConfirmed = lessonPlanReferenceDateChoiceConfirmed;
            lesson.NarrativeDateChoiceConfirmed = narrativeDateChoiceConfirmed;
            lesson.NarrativeReferenceDateChoiceConfirmed = narrativeReferenceDateChoiceConfirmed;

            return lesson;
        }

        /// <summary>
        /// Creates new Lesson and renumbers sequence numbers for those that share the container.
        /// </summary>
        public Lesson CreateLesson(string name, Guid containerLessonId, Guid courseId, Guid masterLessonId,
                        Guid predecessorLessonId, int sequenceNumber, bool isFolder, Guid currentUserId, bool isHidden,
                        DateTime dateModified)
        {
            Course course = _courseRepo.GetById(courseId);

            Lesson masterLesson = null;
            if (masterLessonId == Guid.Empty && !course.IsMaster)
            {
                masterLesson = CreateHiddenMasterLesson(name: name, containerLessonId: containerLessonId, nonMasterCourse: course,
                                        sequenceNumber: sequenceNumber, currentUserId: currentUserId);
                masterLessonId = masterLesson.Id;
            }

            using (var lessonCreateTransaction = _context.Database.BeginTransaction())
            {
                Lesson lesson = new Lesson();

                try
                {
                    MetaLesson metaLesson = null;
                    if(masterLesson != null)
                    {
                        metaLesson = masterLesson.MetaLesson;
                    }
                    else
                    {
                        metaLesson = new MetaLesson();
                        metaLesson.MetaCourseId = course.MetaCourse.Id;
                        metaLesson.Name = name;
                        _metaLessonRepo.Insert(metaLesson);
                    }


                    Narrative narrative = _narrativeCUDService.CreateNarrativeFromCourseId(courseId);

                    LessonPlan lessonPlan = _lessonPlanCUDService.CreateNewLessonPlan(name, currentUserId, course.IsMaster);

                    if (containerLessonId != Guid.Empty)
                    {
                        RaiseSeveralLessonSeqenceNumbersForContainerLesson(containerLessonId, sequenceNumber, Guid.Empty);
                    }//end if
                    else //in which case parent is a course
                    {
                        RaiseSeveralLessonSeqenceNumbersForContainerCourse(containerLessonId, sequenceNumber, Guid.Empty);
                    }//end else

                    lesson = SetLessonValues(lesson: lesson,
                                            name: name,
                                            containerLessonId: containerLessonId,
                                            courseId: courseId,
                                            masterLessonId: masterLessonId,
                                            predecessorLessonId: predecessorLessonId,
                                            sequenceNumber: sequenceNumber,
                                            isActive: true,
                                            isFolder: isFolder,
                                            metaLessonId: metaLesson.Id,
                                            isHidden: isHidden,
                                            lessonPlanId: lessonPlan.Id,
                                            narrativeId: narrative.Id,
                                            dateModified: dateModified);

                    _lessonRepo.Insert(lesson);

                    lesson = SetLessonDates(lesson: lesson,
                                            dateTimeDocumentsChoiceConfirmed: DateTime.Now,
                                            referenceDateTimeDocChoiceConfirmed: new DateTime(),
                                            groupMostRecentDocModDate: new DateTime(),
                                            dateDocumentsModified: new DateTime(),
                                            lessonPlanDateChoiceConfirmed: DateTime.Now,
                                            lessonPlanReferenceDateChoiceConfirmed: new DateTime(),
                                            lessonPlanGroupMostRecentModDate: new DateTime(),
                                            narrativeDateChoiceConfirmed: DateTime.Now,
                                            narrativeReferenceDateChoiceConfirmed: new DateTime(),
                                            narrativeGroupMostRecentModDate: new DateTime());


                    if (lesson.ContainerLessonId != null)
                    {
                        RenumberLessonsForContainerLesson(lesson.ContainerLessonId.Value, false);
                    }//end if
                    else if (lesson.CourseId != null)
                    {
                        RenumberLessonsForContainerCourse(lesson.CourseId, false);
                    }//end else

                    lessonCreateTransaction.Commit();


                }//end try

                catch (Exception e)
                {
                    lessonCreateTransaction.Rollback();
                    throw;
                }//end catch 

                return lesson;
            }//end using
        }//end method

        /// <summary>
        /// Takes sequence numbers greater than or equal to target and raises them by one.
        /// </summary>
        private void RaiseSeveralLessonSeqenceNumbersForContainerLesson(Guid containerLessonId, int targetSequenceNumber, Guid insertLessonId)
        {
            List<Lesson> lessonList = _lessonRepo.LessonsForContainerLesson(containerLessonId).ToList();
            RaiseSeveralLessonSeqenceNumbersPart2(lessonList, targetSequenceNumber, insertLessonId);
        }

        /// <summary>
        /// Takes sequence number greater than or equal to target and raises them by one.
        /// </summary>
        private void RaiseSeveralLessonSeqenceNumbersForContainerCourse(Guid containerCourseId, int targetSequenceNumber, Guid insertLessonId)
        {
            List<Lesson> lessonList = _lessonRepo.LessonsForContainerCourse(containerCourseId).ToList();
            RaiseSeveralLessonSeqenceNumbersPart2(lessonList, targetSequenceNumber, insertLessonId);
        }

        private void RaiseSeveralLessonSeqenceNumbersPart2(List<Lesson> lessonList, int targetSequenceNumber, Guid insertLessonId)
        {
            foreach (Lesson xLesson in lessonList)
            {
                if (xLesson.SequenceNumber >= targetSequenceNumber && xLesson.Id != insertLessonId)
                {
                    xLesson.SequenceNumber = xLesson.SequenceNumber + 1;
                    _lessonRepo.Update(xLesson);
                }
            }
        }

        /// <summary>
        /// Takes all the lessons sorted by sequence number and renumbers them to remove any gaps.
        /// </summary>
        /// <param name="doRenumberChildren">Do you want to renumber the lessons further down in the tree?</param>
        private void RenumberLessonsForContainerLesson(Guid containerLessonId, bool doRenumberChildren)
        {
            List<Lesson> lessonList = _lessonRepo.LessonsForContainerLesson(containerLessonId).ToList();
            RenumberLessonsPart2(lessonList);
            if (doRenumberChildren && lessonList.Count > 0)
            {
                foreach (var xLesson in lessonList)
                {
                    RenumberLessonsForContainerLesson(xLesson.Id, true);
                }
            }
        }

        /// <summary>
        /// takes all the lessons sorted by sequence number and renumbers them to remove any gaps.
        /// </summary>
        /// <param name="doRenumberChildren">Do you want to renumber the lessons further down in the tree?</param>
        private void RenumberLessonsForContainerCourse(Guid courseId, bool doRenumberChildren)
        {
            List<Lesson> lessonList = _lessonRepo.LessonsForContainerCourse(courseId).ToList();
            RenumberLessonsPart2(lessonList);

            if (doRenumberChildren && lessonList.Count > 0)
            {
                foreach (var xLesson in lessonList)
                {
                    RenumberLessonsForContainerLesson(xLesson.Id, true);
                }
            }
        }

        /// <summary>
        /// Renumbers all lessons in a course to remove any gaps in sequence numbers.
        /// Can be called by users from Lesson Manager.
        /// </summary>
        /// <param name="courseId"></param>
        public void RenumberAllLessonsInCourse(Guid courseId)
        {
            RenumberLessonsForContainerCourse(courseId, true);
        }

        private void RenumberLessonsPart2(List<Lesson> lessonList)
        {
            int index = 1;
            foreach (Lesson xLesson in lessonList)
            {
                xLesson.SequenceNumber = index;
                _lessonRepo.Update(xLesson);
                index = index + 1;
            }
        }




        /// <summary>
        /// Updates lesson values.  Will renumber sequence numbers for lessons that share container.
        /// </summary>
        public Lesson UpdateLesson(Guid lessonId, string name, Guid containerLessonId, Guid courseId, Guid masterLessonId,
                        Guid predecessorLessonId, int sequenceNumber, bool isFolder, bool isHidden, DateTime dateModified)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);


            if (containerLessonId != Guid.Empty)
            {
                RaiseSeveralLessonSeqenceNumbersForContainerLesson(containerLessonId, sequenceNumber, Guid.Empty);
            }
            else //in which case parent is a course
            {
                RaiseSeveralLessonSeqenceNumbersForContainerCourse(courseId, sequenceNumber, Guid.Empty);
            }

            lesson = SetLessonValues(lesson: lesson,
                                     name: name,
                                     containerLessonId: containerLessonId,
                                     courseId: courseId,
                                     masterLessonId: masterLessonId,
                                    predecessorLessonId: predecessorLessonId,
                                    sequenceNumber: sequenceNumber,
                                    isActive: true,
                                    isFolder: isFolder,
                                    metaLessonId: lesson.MetaLessonId,
                                    isHidden: isHidden,
                                    lessonPlanId: lesson.LessonPlanId,
                                    narrativeId: lesson.NarrativeId,
                                    dateModified: dateModified);

            _lessonRepo.Update(lesson);
            if (lesson.ContainerLessonId != null)
            {
                RenumberLessonsForContainerLesson(lesson.ContainerLessonId.Value, false);
            }
            else if (lesson.CourseId != Guid.Empty)
            {
                RenumberLessonsForContainerCourse(lesson.CourseId, false);
            }

            return lesson;
        }

        public Guid ImportLesson_ReturnId(Guid fromLessonId, Guid containerLessonId, Guid toCourseLessonId)
        {
            Lesson newLesson = new Lesson();
            Lesson fromLesson = _lessonRepo.GetById(fromLessonId);
            Lesson toCourseLesson = _lessonRepo.GetById(toCourseLessonId);
            Course toCourse = toCourseLesson.Course;
            Course masterCourse = toCourse.MasterCourseId == null ? null : toCourse.MasterCourse;

            newLesson.CourseId = toCourse.Id;
            newLesson.ContainerLessonId = containerLessonId;

            Narrative narrative = _narrativeCUDService.CopyNarrative(fromLesson.Narrative);
            newLesson.NarrativeId = narrative.Id;

            newLesson = SetMasterValuesForImport(fromLesson, masterCourse, toCourse, newLesson, containerLessonId);
            newLesson = CopyBasicValues(fromLesson: fromLesson, 
                                        newLesson: newLesson,
                                        isFromAnotherUser: true);

            newLesson = SetMetaLessonAndLessonPlanForImport(newLesson, fromLesson, toCourse);
            newLesson.DateCreated = DateTime.Now;

            _lessonRepo.Insert(newLesson);

            return newLesson.Id;

        }

        private Lesson SetMetaLessonAndLessonPlanForImport(Lesson newLesson, Lesson fromLesson, Course toCourse)
        {
            if (fromLesson.Course.MetaCourseId != toCourse.MetaCourseId)
            {
                MetaLesson newMetaLesson = CopyMetaLessonToNewCourse(fromLesson.MetaLesson, toCourse);
                fromLesson.MetaLessonId = newMetaLesson.Id;

                LessonPlan lessonPlan = new LessonPlan();
                _lessonPlanRepo.Insert(lessonPlan);

                newLesson.LessonPlanId = lessonPlan.Id;
                if (newLesson.MasterLessonId != null) //Which means this is not a master
                {
                    Lesson masterLesson = _lessonRepo.GetById(newLesson.MasterLessonId.Value);
                    masterLesson.LessonPlanId = lessonPlan.Id;
                }
            }
            else
            {
                newLesson.MetaLessonId = fromLesson.MetaLessonId;
                newLesson.LessonPlanId = fromLesson.LessonPlanId;
            }

            return newLesson;
        }

        /// <summary>
        /// Copies a lesson to a new course.  
        /// </summary>
        /// <param name="masterCourse">Which may be different from where the lesson is coming.</param>
        public Lesson CopyLesson(Lesson fromLesson, Course masterCourse, Guid toCourseId, Guid? containerLessonId)
        {
            Lesson newLesson = new Lesson();
            using (var lessonCopyTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    bool isFromMasterToIndividual = IsFromMasterToIndividual(fromLesson, masterCourse);

                    if(!(isFromMasterToIndividual && fromLesson.IsHidden))//We don't copy hidden lesson from master to individual
                    {
                        newLesson.CourseId = toCourseId;
                        newLesson.ContainerLessonId = containerLessonId;

                        newLesson = SetMasterValues(fromLesson, masterCourse, toCourseId, newLesson);

                        newLesson = CopyBasicValues(fromLesson: fromLesson, 
                                                    newLesson: newLesson, 
                                                    isFromAnotherUser: isFromMasterToIndividual);

                        newLesson.MetaLessonId = fromLesson.MetaLessonId;
                        newLesson.LessonPlanId = fromLesson.LessonPlanId;

                        _lessonRepo.Insert(newLesson);

                        if (!isFromMasterToIndividual)
                        {
                            CopyMessages(fromLesson, newLesson);

                            SetMessageBooleans(newLesson);
                        }

                        _documentUseCUDService.CopyAllDocumentUses_FromLessonToLesson(fromLesson.Id, newLesson.Id);
                        SetLessonDocumentsBoolean(newLesson);
                    }

                    lessonCopyTransaction.Commit();
                }
                catch (Exception e)
                {
                    lessonCopyTransaction.Rollback();
                    throw;
                }
            }


            //Now iterate back over everything to pick up the children
            foreach (Lesson xLesson in fromLesson.ContainerLessonChildren)
            {
                CopyLesson(xLesson, masterCourse, toCourseId, newLesson.Id);
            }

            return newLesson;
        }

        private static bool IsFromMasterToIndividual(Lesson fromLesson, Course masterCourse)
        {
            return masterCourse != null && fromLesson.CourseId == masterCourse.Id;
        }

        private void SetMessageBooleans(Lesson newLesson)
        {
            if (newLesson.Course.Term == null) //Since we may have just created this lesson, it might not be able to reach this far
            {
                newLesson.Course.Term = _termRepo.GetById(newLesson.Course.TermId);
            }

            _sharedCUDService.SetLessonMessageBooleans(newLesson);
        }

        private void CopyMessages(Lesson fromLesson, Lesson newLesson)
        {
            //Copy Message Uses
            List<MessageUseDomainObj> allMessageUseObjs = _messageUseObjBuilder.GetAllMessageUseObjsForLesson(fromLesson.Id);
            foreach (var xMessageUseObj in allMessageUseObjs)
            {
                if (xMessageUseObj.IsThisOrChildrenNotArchived)
                {
                    _messageUseCUDService.Create(xMessageUseObj.MessageId, newLesson.Id, xMessageUseObj.IsStored, xMessageUseObj.IsNew);
                }
            }
        }

        private static Lesson CopyBasicValues(Lesson fromLesson, Lesson newLesson, bool isFromAnotherUser)
        {
            newLesson.PredecessorLessonId = fromLesson.Id;
            newLesson.Name = fromLesson.Name;
            newLesson.IsActive = true;
            newLesson.EstimatedTimeMin = fromLesson.EstimatedTimeMin;
            newLesson.DateTimeCreated = DateTime.Now;
            newLesson.SequenceNumber = fromLesson.SequenceNumber;
            newLesson.IsFolder = fromLesson.IsFolder;
            newLesson.ChangeNotes = fromLesson.ChangeNotes;
            newLesson.IsHidden = fromLesson.IsHidden;

            newLesson.DateTimeDocumentsChoiceConfirmed = isFromAnotherUser ? DateTime.Now: fromLesson.DateTimeDocumentsChoiceConfirmed;
            newLesson.ReferenceDateTimeDocChoiceConfirmed = isFromAnotherUser ? new DateTime() : fromLesson.ReferenceDateTimeDocChoiceConfirmed;
            newLesson.DateDocumentsModified = fromLesson.DateDocumentsModified;
            newLesson.LessonPlanReferenceDateChoiceConfirmed = isFromAnotherUser ? new DateTime() : fromLesson.LessonPlanReferenceDateChoiceConfirmed;
            newLesson.LessonPlanDateChoiceConfirmed = isFromAnotherUser ? DateTime.Now : fromLesson.LessonPlanDateChoiceConfirmed;
            newLesson.NarrativeReferenceDateChoiceConfirmed = isFromAnotherUser ? new DateTime() : fromLesson.NarrativeReferenceDateChoiceConfirmed;
            newLesson.NarrativeDateChoiceConfirmed = isFromAnotherUser ? DateTime.Now : fromLesson.NarrativeDateChoiceConfirmed;

            return newLesson;
        }

        private Lesson SetMasterValues(Lesson fromLesson, Course masterCourse, Guid toCourseId, Lesson newLesson)
        {
            if (masterCourse == null)//Which means this is going to be a master
            {
                newLesson.MasterLessonId = null;
                newLesson.NarrativeId = fromLesson.NarrativeId;
            }
            else if (IsFromMasterToIndividual(fromLesson, masterCourse))
            {
                newLesson.MasterLessonId = fromLesson.Id;
                Narrative newNarrative = _narrativeCUDService.CreateCommentNarrative(toCourseId, fromLesson.NarrativeId);
                newLesson.NarrativeId = newNarrative.Id;
            }
            else if (fromLesson.Course.MasterCourseId == masterCourse.Id)//Which means we're copying within a semester.
            {
                newLesson.MasterLessonId = fromLesson.MasterLessonId;
                newLesson.NarrativeId = fromLesson.NarrativeId;
            }
            else //Which means this is individual to individual and to a new term
            {
                var lessonQuery = from x in masterCourse.Lessons
                                  where x.MetaLessonId == fromLesson.MetaLessonId
                                  select x;
                Lesson masterLesson = lessonQuery.FirstOrDefault();
                if (masterLesson != null)
                {
                    newLesson.MasterLessonId = masterLesson.Id;
                }
                newLesson.NarrativeId = fromLesson.NarrativeId;
            }
            return newLesson;
        }

        private Lesson SetMasterValuesForImport(Lesson fromLesson, Course masterCourse, Course toCourse, Lesson newLesson,
                                                Guid containerLessonId)
        {
            if (masterCourse == null)//Which means this is going to be a master
            {
                newLesson.MasterLessonId = null;
            }
            else //Which means this is individual to individual
            {
                var lessonQuery = from x in masterCourse.Lessons
                                  where x.MetaLessonId == fromLesson.MetaLessonId
                                  select x;
                Lesson masterLesson = lessonQuery.FirstOrDefault();
                if (masterLesson != null)
                {
                    newLesson.MasterLessonId = masterLesson.Id;
                }
                else
                {
                    Lesson hiddenMasterLesson = CreateHiddenMasterLesson(fromLesson.Name, containerLessonId, toCourse, 0, toCourse.UserId);
                    newLesson.MasterLessonId = hiddenMasterLesson.Id;

                }
            }
            return newLesson;
        }

        /// <summary>
        /// Raises a Lesson SequenceNumber by one and adjusts others accordingly.  Used to move a lesson up one in Lesson Manager.
        /// </summary>
        public void RaiseLessonSequenceNumberByOne(Guid lessonId)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            if (lesson.SequenceNumber != null)
            {
                int sequenceNumber = lesson.SequenceNumber.Value;

                Lesson lessonAbove = _lessonRepo.LessonAboveGivenLesson(lesson);

                if (lessonAbove != null)
                {
                    lesson.SequenceNumber = sequenceNumber + 1;
                    lessonAbove.SequenceNumber = sequenceNumber;
                    _lessonRepo.Update(lesson);
                    _lessonRepo.Update(lessonAbove);
                }
                if (lesson.ContainerLessonId != null)
                {
                    RenumberLessonsForContainerLesson(lesson.ContainerLessonId.Value, false);
                }
                else if (lesson.CourseId != Guid.Empty)
                {
                    RenumberLessonsForContainerCourse(lesson.CourseId, false);
                }
            }
        }

        /// <summary>
        /// Lowers a lesson SequenceNumber by one and adjusts others accordingly.  Used to move a lesson down one in Lesson Manager.
        /// </summary>
        public void LowerLessonSequenceNumberByOne(Guid lessonId)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            if (lesson.SequenceNumber != null)
            {
                int sequenceNumber = lesson.SequenceNumber.Value;

                Lesson lessonBelow = _lessonRepo.LessonBelowGivenLesson(lesson);

                if (lessonBelow != null)
                {
                    lesson.SequenceNumber = sequenceNumber - 1;
                    lessonBelow.SequenceNumber = sequenceNumber;
                    _lessonRepo.Update(lesson);
                    _lessonRepo.Update(lessonBelow);
                }
                if (lesson.ContainerLessonId != null)
                {
                    RenumberLessonsForContainerLesson(lesson.ContainerLessonId.Value, false);
                }
                else if (lesson.CourseId != Guid.Empty)
                {
                    RenumberLessonsForContainerCourse(lesson.CourseId, false);
                }
            }
        }
        public void DeleteLessonById(Guid lessonId)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            DeleteLesson(lesson);
        }

        /// <summary>
        /// This assumes that CheckIfLessonIsDeletable has been run so there are no LessonUses or children
        /// This will delete any LessonPlans or Narratives.  MessageUses and DocumentUses will be deleted but could leave
        /// orphan messages or documents.  This had to be done this way because of circular reference issues.
        /// </summary>
        /// <param name="lesson"></param>
        public void DeleteLesson(Lesson lesson)
        {
            _stashedLessonPlanRepo.DeleteMultiple(lesson.StashedLessonPlans.ToList());
            _stashedNarrativeRepo.DeleteMultiple(lesson.StashedNarratives.ToList());
            _documentUseRepo.DeleteMultiple(lesson.DocumentUses.ToList());
            _messageUseRepo.DeleteMultiple(lesson.MessageUses.ToList());

            List<Lesson> containerChildren = lesson.ContainerLessonChildren.ToList();

            List<Guid> idList = new List<Guid>();
            foreach (var xLesson in containerChildren)
            {
                idList.Add(xLesson.Id);
            }
            foreach (Guid xId in idList)
            {
                DeleteLessonById(xId);
            }

            Guid lessonId = lesson.Id;
            _lessonRepo.Delete(lesson.Id);
            _metaLessonRepo.RemoveOrphanMetaLessons();
        }

        /// <summary>
        /// Sets the boolean that controls the OutForEdit icon
        /// </summary>
        public void SetLessonDocumentsBoolean(Lesson lesson)
        {
            List<DocumentUse> docUseList = _documentUseRepo.ActiveDocumentUsesForLesson(lesson.Id).ToList();
            lesson.HasOutForEditDocuments = false;
            foreach (var x in docUseList)
            {
                if (x.IsOutForEdit)
                {
                    lesson.HasOutForEditDocuments = true;
                }
            }
            _lessonRepo.Update(lesson);
        }

        //TODO: Remove this after we redo Optional to refer to tags.
        /// <summary>
        /// Togggles the IsOptional boolean
        /// </summary>
        public void ToggleLessonIsOptional(Guid lessonId)
        {
            //Lesson lesson = _lessonRepo.GetById(lessonId);
            //lesson.IsOptional = !lesson.IsOptional;
            //_lessonRepo.Update(lesson);
        }

        public Tuple<string, Guid> DeleteLessonReturnMessageAndId(Guid selectedLessonId)
        {
            Guid lessonId = Guid.Empty;
            string deletionMessage = "";
            Lesson lesson = _lessonRepo.GetById(selectedLessonId);
            if (lesson != null)
            {
                bool isLessonDeletable = CheckIfLessonIsDeletable(lesson);
                if (!isLessonDeletable)
                {
                    deletionMessage = "Lesson is not deletable due to dependencies.";
                    lessonId = selectedLessonId;
                }
                else
                {
                    DeleteLessonById(selectedLessonId); //Mirrors will be updated by this call
                    deletionMessage = "Lesson has been deleted.";
                }
            }
            return Tuple.Create(deletionMessage, lessonId);
        }

        public bool CheckIfLessonIsDeletable(Lesson lesson)
        {
            if (lesson == null)
            {
                return true;
            }
            else
            {
                return lesson.MasterLessonChildren.Count == 0 &&
                    lesson.PredecessorLessonChildren.Count == 0 &&
                    lesson.LessonUses.Count == 0 ;
            }
        }

        public Lesson CreateHiddenMasterLesson(string name, Guid containerLessonId, Course nonMasterCourse,
                int sequenceNumber, Guid currentUserId)
        {
            if (nonMasterCourse.IsMaster)
            {
                return null;
            }

            Guid masterContainerId = Guid.Empty;
            Guid masterCourseId = nonMasterCourse.MasterCourseId == null ? Guid.Empty : nonMasterCourse.MasterCourseId.Value;

            Lesson containerLesson = _lessonRepo.GetById(containerLessonId);
            if (containerLesson != null)
            {
                masterContainerId = containerLesson.MasterLessonId == null ? Guid.Empty : containerLesson.MasterLessonId.Value;
            }


            return CreateLesson(name: name, containerLessonId: masterContainerId, courseId: masterCourseId,
                                masterLessonId: Guid.Empty, predecessorLessonId: Guid.Empty, sequenceNumber: sequenceNumber,
                                isFolder: false, currentUserId: currentUserId, isHidden: true, dateModified: DateTime.Now);
        }

        public void UpdateDocumentsChoiceConfirmedDateToComparisonLesson(Guid editableLessonId, Guid comparisonLessonId)
        {
            Lesson comparisonLesson = _lessonRepo.GetById(comparisonLessonId);
            DateTime date = comparisonLesson.DateDocumentsModified == null ? new DateTime() : comparisonLesson.DateDocumentsModified.Value;
            UpdateDocumentsChoiceConfirmedDate(editableLessonId, date.AddMilliseconds(3));
        }
        public void UpdateDocumentsChoiceConfirmedDate(Guid editableLessonId, DateTime date)
        {
            Lesson lesson = _lessonRepo.GetById(editableLessonId);
            lesson.DateTimeDocumentsChoiceConfirmed = date;
            _lessonRepo.Update(lesson);
        }

        public void SetDateDocChoiceConfirmedBackInTime(Guid editableLessonId, int numberOfDaysBack)
        {
            Lesson editableLesson = _lessonRepo.GetById(editableLessonId);
            editableLesson.ReferenceDateTimeDocChoiceConfirmed = editableLesson.DateTimeDocumentsChoiceConfirmed;
            DateTime newDate = editableLesson.DateTimeDocumentsChoiceConfirmed == null ? new DateTime() : 
                                            editableLesson.DateTimeDocumentsChoiceConfirmed.Value.Subtract(TimeSpan.FromDays(numberOfDaysBack));
            editableLesson.DateTimeDocumentsChoiceConfirmed = newDate;

            _lessonRepo.Update(editableLesson);
        }

        public void SetIsHidden(Guid lessonId, bool isHidden)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            if(lesson != null)
            {
                lesson.IsHidden = isHidden;
                _lessonRepo.Update(lesson);
            }

        }

        public void ReturnToReferenceDateDocumentChoiceConfirmed(Guid editableLessonId)
        {
            Lesson editableLesson = _lessonRepo.GetById(editableLessonId);
            if (editableLesson.ReferenceDateTimeDocChoiceConfirmed != null)
            {
                editableLesson.DateTimeDocumentsChoiceConfirmed = editableLesson.ReferenceDateTimeDocChoiceConfirmed.Value;
                _lessonRepo.Update(editableLesson);
            }
        }

        private MetaLesson CopyMetaLessonToNewCourse(MetaLesson fromMetaLesson, Course toCourse)
        {
            MetaLesson newMetaLesson = new MetaLesson();
            newMetaLesson.Name = fromMetaLesson.Name + "_in_" + toCourse.MetaCourse.Name;
            newMetaLesson.MetaCourseId = toCourse.MetaCourseId;
            _metaLessonRepo.Insert(newMetaLesson);
            return newMetaLesson;
        }



       

        /// <summary>
        /// If you are creating a new DocumentUse, this will throw an exception.  Use the 
        /// </summary>
        /// <param name="lessonId"></param>
        public void UpdateDateDocumentsModified(Guid lessonId, DateTime modDate)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            if (lesson != null)
            {
                lesson.DateDocumentsModified = modDate;
                _lessonRepo.Update(lesson);
            }
        }



        //private bool doLessonsMatch(Lesson refLesson, Lesson compLesson)
        //{
        //    return refLesson.PredecessorLessonId == compLesson.PredecessorLessonId &&
        //            refLesson.Name == compLesson.Name &&
        //            refLesson.IsActive == compLesson.IsActive &&
        //            refLesson.EstimatedTimeMin == compLesson.EstimatedTimeMin &&
        //            refLesson.DateTimeDocumentsChoiceConfirmed == compLesson.DateTimeDocumentsChoiceConfirmed &&
        //            refLesson.SequenceNumber == compLesson.SequenceNumber &&
        //            refLesson.IsFolder == compLesson.IsFolder &&
        //            refLesson.ChangeNotes == compLesson.ChangeNotes &&
        //            (refLesson.ContainerLesson == null || refLesson.ContainerLesson.MetaLessonId == compLesson.ContainerLesson.MetaLessonId) &&
        //            refLesson.LessonPlanId == compLesson.LessonPlanId &&
        //            refLesson.NarrativeId == compLesson.NarrativeId &&
        //            refLesson.IsFolder == compLesson.IsFolder &&
        //            refLesson.IsArchived == compLesson.IsArchived &&
        //            refLesson.IsHidden == compLesson.IsHidden;
        //}

        public void ToggleIsCollapsed(Guid lessonId)
        {
            Lesson lesson = _lessonRepo.GetById(lessonId);
            lesson.IsCollapsed = !lesson.IsCollapsed;
            _lessonRepo.Update(lesson);
        }

        public void UpdateNarrativeDateChoiceConfirmed(Guid editableLessonId, Guid comparisonLessonId, bool doSuggestRemoveComment)
        {
            //We have to do this here with two dB calls because MVC was losing the milliseconds.
            Lesson comparisonLesson = _lessonRepo.GetById(comparisonLessonId);
            DateTime date = comparisonLesson.Narrative.DateModified;
            SetNarrativeDateChoiceConfirmed(editableLessonId, date, doSuggestRemoveComment);
        }

        public void SetNarrativeDateChoiceConfirmed(Guid editableLessonId, DateTime date, bool doSuggestRemoveComment)
        {
            Lesson editableLesson = _lessonRepo.GetById(editableLessonId);
            editableLesson.NarrativeDateChoiceConfirmed = date.AddMilliseconds(1);
            editableLesson.Narrative.DoSuggestRemovingComment = doSuggestRemoveComment;
            _lessonRepo.Update(editableLesson);
        }

        public void UpdateNarrativeDateChoiceConfirmedToGroupDate(Guid lessonId)
        {
            LessonDomainObjBasic lessonObj = _lessonDomainObjBuilder.BuildBasicFromId(lessonId);
            Lesson lesson = _lessonRepo.GetById(lessonId);
            if (lessonObj != null && lessonObj.NarrativeGroupMostRecentModDate != null)
            {
                lesson.NarrativeDateChoiceConfirmed = lessonObj.NarrativeGroupMostRecentModDate;
                _lessonRepo.Update(lesson);
            }

        }

        public void SetNarrativeDateChoiceConfirmedBackInTime(Guid editableLessonId, int numberOfDaysBack)
        {
            Lesson editableLesson = _lessonRepo.GetById(editableLessonId);
            editableLesson.NarrativeReferenceDateChoiceConfirmed = editableLesson.NarrativeDateChoiceConfirmed;
            DateTime newDate = editableLesson.NarrativeDateChoiceConfirmed.GetValueOrDefault().Subtract(TimeSpan.FromDays(numberOfDaysBack));
            editableLesson.NarrativeDateChoiceConfirmed = newDate;

            _lessonRepo.Update(editableLesson);
        }

        public void ReturnToReferenceNarrativeChoiceConfirmedDate(Guid editableLessonId)
        {
            Lesson editableLesson = _lessonRepo.GetById(editableLessonId);
            if (editableLesson.NarrativeReferenceDateChoiceConfirmed  != null)
            {
                editableLesson.NarrativeDateChoiceConfirmed = editableLesson.NarrativeReferenceDateChoiceConfirmed.Value;
                _lessonRepo.Update(editableLesson);
            }
        }

        public void UpdateLessonPlanDateChoiceConfirmed(Guid editableLessonId, Guid editableLessonPlanId, Guid comparisonLessonPlanId, bool doSuggestUseMaster)
        {
            //We have to do this here with two dB calls because MVC was losing the milliseconds.
            LessonPlan comparisonLessonPlan = _lessonPlanRepo.GetById(comparisonLessonPlanId);
            DateTime date = comparisonLessonPlan.DateModified;
            SetLessonPlanDateChoiceConfirmed(editableLessonId, editableLessonPlanId, date, doSuggestUseMaster);
        }

        public void SetLessonPlanDateChoiceConfirmed(Guid editableLessonId, Guid editableLessonPlanId, DateTime date, bool doSuggestUseMaster)
        {
            Lesson editableLesson = _lessonRepo.GetById(editableLessonId);
            editableLesson.LessonPlanDateChoiceConfirmed = date.AddMilliseconds(3);
            UpdateSuggestUsingMasterLessonPlan(editableLessonPlanId, doSuggestUseMaster);
            _lessonRepo.Update(editableLesson);
        }

        public void UpdateSuggestUsingMasterLessonPlan(Guid editableLessonPlanId, bool doSuggestUseMaster)
        {
            LessonPlan editableLessonPlan = _lessonPlanRepo.GetById(editableLessonPlanId);
            editableLessonPlan.DoSuggestUsingMaster = doSuggestUseMaster;
            _lessonPlanRepo.Update(editableLessonPlan);
        }

        public void SetLessonPlanDateChoiceConfirmedBackInTime(Guid editableLessonId, int numberOfDaysBack)
        {
            Lesson editableLesson = _lessonRepo.GetById(editableLessonId);
            editableLesson.LessonPlanReferenceDateChoiceConfirmed = editableLesson.LessonPlanDateChoiceConfirmed;
            DateTime newDate = editableLesson.LessonPlanDateChoiceConfirmed.GetValueOrDefault().Subtract(TimeSpan.FromDays(numberOfDaysBack));
            editableLesson.LessonPlanDateChoiceConfirmed = newDate;

            _lessonRepo.Update(editableLesson);
        }

        public void ReturnToReferenceChoiceConfirmedDate(Guid editableLessonId)
        {
            Lesson editableLesson = _lessonRepo.GetById(editableLessonId);
            if (editableLesson.LessonPlanReferenceDateChoiceConfirmed != null)
            {
                editableLesson.LessonPlanDateChoiceConfirmed = editableLesson.LessonPlanReferenceDateChoiceConfirmed.GetValueOrDefault();
                _lessonRepo.Update(editableLesson);
            }

        }
    }//End Class
}

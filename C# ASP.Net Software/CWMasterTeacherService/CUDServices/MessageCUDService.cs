using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class MessageCUDService
    {
        private LessonRepo _lessonRepo;
        private MessageUseRepo _messageUseRepo;
        private CourseRepo _courseRepo;
        private CourseCUDService _courseCUDService;
        private MessageRepo _messageRepo;
        private LessonCUDService _lessonCUDService;
        private MessageUseCUDService _messageUseCUDService;
        private SharedCUDService _sharedCUDService;


        public MessageCUDService (MessageRepo messageRepo, LessonRepo lessonRepo, MessageUseRepo messageUseRepo, CourseRepo courseRepo,
                        CourseCUDService courseCUDService, LessonCUDService lessonCUDService, MessageUseCUDService messageUseCUDService,
                        SharedCUDService sharedCUDService)
        {
            _lessonRepo = lessonRepo;
            _messageUseRepo = messageUseRepo;
            _courseRepo = courseRepo;
            _courseCUDService = courseCUDService;
            _messageRepo = messageRepo;
            _lessonCUDService = lessonCUDService;
            _messageUseCUDService = messageUseCUDService;
            _sharedCUDService = sharedCUDService;
        }

        public Message Create(Guid authorId, Guid? threadParentId, string subject, string text)
        {
            Message message = new Message();
            message.AuthorId = authorId;
            message.ThreadParentId = threadParentId;
            message.Subject = subject;
            message.Text = text;
            message.TimeStamp = DateTime.Now;

            _messageRepo.Insert(message);
            return message;

        }

        /// <summary>
        /// Creates a Message object and creates MessageUse objects to link it to recipients.
        /// </summary>
        public void PostMessage(Guid lessonId, Guid parentMessageId, Guid authorId, string subject, string text, bool isToSelf, bool isToStorage)
        {
            Guid? threadParentId = null;

            if (parentMessageId != Guid.Empty)
            {
                threadParentId = parentMessageId;
            }
            else
            {
                threadParentId = null;
            }
            Message newMessage = Create(authorId, threadParentId, subject, text);

            if (isToSelf)
            {
                _messageUseCUDService.Create(newMessage.Id, lessonId, isToStorage, false);
            }
            else if (parentMessageId != Guid.Empty) //This means it's a reply
            {
                List<MessageUse> messageUseList = _messageUseRepo.GetMessageUsesForMessage(parentMessageId).ToList();
                foreach (var xMessageUse in messageUseList)
                {
                    _messageUseCUDService.Create(newMessage.Id, xMessageUse.LessonId, false, xMessageUse.LessonId != lessonId);
                    _sharedCUDService.UpdateMessageBooleansForLessonAndCourse(xMessageUse.Lesson);
                }
                SetShowArchivedInMessageUses(newMessage);

            }
            else
            {
                List<Lesson> lessonList = _sharedCUDService.LessonsThatShareMaster(lessonId, false).ToList();
                foreach (var xLesson in lessonList)
                {
                    if (!xLesson.Course.IsMaster)
                    {
                        _messageUseCUDService.Create(newMessage.Id, xLesson.Id, false, xLesson.Id != lessonId);
                        _sharedCUDService.UpdateMessageBooleansForLessonAndCourse(xLesson);
                    }
                }
            }
            _sharedCUDService.UpdateMessageBooleansForLessonAndCourseById(lessonId);
        }//End PostMessage

        /// <summary>
        /// This sets the ShowArchived boolean on message uses in order to show MessageUses that have been archive, then replied to.
        /// For example, A sends B and C a message, B archives it, then C replies.  This override will show the archived message
        /// for B so B can see the reply and the whole thread.
        /// </summary>
        /// <param name="message"></param>
        private void SetShowArchivedInMessageUses(Message message)
        {
            List<MessageUse> messageUseList = _messageUseRepo.GetMessageUsesForMessage(message.Id).ToList();
            foreach (var xMessageUse in messageUseList)
            {
                xMessageUse.ShowArchived = true;
            }

            if (message.ThreadParentId != null)
            {
                Message parentMessage = _messageRepo.GetById(message.ThreadParentId.Value);
                if (parentMessage != null)
                {
                    SetShowArchivedInMessageUses(parentMessage);
                }
            }
        }


        public void UpdateIsStored(Guid messageUseId, bool isStored)
        {
            MessageUse messageUse = _messageUseRepo.GetById(messageUseId);
            messageUse.IsStored = isStored;

            DateTime refTime = messageUse.Lesson.Course.Term.StartDate;
            refTime = refTime.AddHours(1);
            messageUse.StorageReferenceTime = refTime;

            _messageUseRepo.Update(messageUse);
            _sharedCUDService.UpdateMessageBooleansForLessonAndCourseById(messageUse.LessonId);
        }

        /// <summary>
        ///  This had to go here and not in MessageUseRepo, in order to avoid some circular reference issues in Ninject.
        /// </summary>
        public void UpdateIsNew(Guid messageUseId, bool isNew)
        {
            MessageUse messageUse = _messageUseRepo.GetById(messageUseId);
            messageUse.IsNew = isNew;
            _messageUseRepo.Update(messageUse);
            _sharedCUDService.UpdateMessageBooleansForLessonAndCourseById(messageUse.LessonId);
        }

        public void UpdateIsImportant(Guid messageUseId, bool isImportant)
        {
            MessageUse messageUse = _messageUseRepo.GetById(messageUseId);
            messageUse.IsImportant = isImportant;

            _messageUseRepo.Update(messageUse);
            _sharedCUDService.UpdateMessageBooleansForLessonAndCourseById(messageUse.LessonId);
        }

        public void UpdateIsArchived(Guid messageUseId, bool isArchived)
        {
            MessageUse messageUse = _messageUseRepo.GetById(messageUseId);
            messageUse.IsArchived = isArchived;
            messageUse.ShowArchived = false;  //If messageUse is not archived, this is irrelevant.  If you're archiving it we assume you don't want to see it.

            _messageUseRepo.Update(messageUse);
            _sharedCUDService.UpdateMessageBooleansForLessonAndCourseById(messageUse.LessonId);
        }

        public void UpdateIsForEndOfSemRev(Guid messageUseId, bool isForEndOfSemRev)
        {
            MessageUse messageUse = _messageUseRepo.GetById(messageUseId);
            messageUse.IsForEndOfSemRev = isForEndOfSemRev;

            _messageUseRepo.Update(messageUse);
            _sharedCUDService.UpdateMessageBooleansForLessonAndCourseById(messageUse.LessonId);
        }

       




    }// End Class
}

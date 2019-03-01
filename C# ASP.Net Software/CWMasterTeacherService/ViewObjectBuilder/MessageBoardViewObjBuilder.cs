using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDomain.DomainObjects;
using System.Collections.Generic;
using System.Linq;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherService.CUDServices;
using System;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    /// <summary>
    /// Provides retrieval services to get an instance of a MessageBoardViewObj containing the desired data.
    /// </summary>
    public class MessageBoardViewObjBuilder
    {
        private IMessageUseDomainObjBuilder _messageUseDomainObjBuilder;
        private IMessageDomainObjBuilder _messageDomainObjBuilder;
        private LessonCUDService _lessonCUDService;
        private SharedCUDService _sharedCUDService;

        /// <summary>
        ///  Creates a MessageBoardService that uses the supplied builders and service when acquiring data.
        /// </summary>
        /// <param name="messageUseDomainObjBuilder">An IMessageUseDomainObjBuilder that is used when acquiring message use data.</param>
        /// <param name="messageDomainObjBuilder">An IMessageDomainObjBuilder that is used when acquiring message data.</param>
        public MessageBoardViewObjBuilder(IMessageUseDomainObjBuilder messageUseDomainObjBuilder, IMessageDomainObjBuilder messageDomainObjBuilder,
                                    LessonCUDService lessonCUDService, SharedCUDService sharedCUDService)
        {
            _messageUseDomainObjBuilder = messageUseDomainObjBuilder;
            _messageDomainObjBuilder = messageDomainObjBuilder;
            _lessonCUDService = lessonCUDService;
            _sharedCUDService = sharedCUDService;
        }

        /// <summary>
        ///  Retrieves a MessageBoardViewObj built using the given arguments.
        /// </summary>
        /// <param name="selectedLessonId">The Id of the selected lesson.</param>
        /// <param name="selectedMessageUseId">The Id of the selected message use. Values less than 1 are evaluated as no selected message use.</param> 
        /// <returns>A MessageBoardViewObj</returns>
        public MessageBoardViewObj Retrieve(Guid selectedLessonId, Guid selectedMessageUseId, Guid currentUserId, bool showArchivedMessages)
        {
            MessageBoardViewObj viewObj = new MessageBoardViewObj();
            MessageUseDomainObj selectedMessageUseObj = null;
            if (selectedMessageUseId != Guid.Empty)
            {
                selectedMessageUseObj = _messageUseDomainObjBuilder.BuildFromId(selectedMessageUseId);
                _messageDomainObjBuilder.UpdateIsNew(selectedMessageUseId, false);
                _sharedCUDService.UpdateMessageBooleansForLessonAndCourseById(selectedMessageUseObj.LessonId);
            }

            viewObj.SelectedMessageUseObj = selectedMessageUseObj;
            viewObj.SelectedLessonId = selectedLessonId;
            viewObj.DisplayMessageUseId = selectedMessageUseId;
            viewObj.ShowArchived = showArchivedMessages;

            viewObj.LiveMessages  = new List<MessageUseDomainObj>();
            viewObj.StoredMessages = new List<MessageUseDomainObj>();
            viewObj.ArchivedMessages = new List<MessageUseDomainObj>();

            List<MessageUseDomainObj> parentMessageUseObjList = _messageUseDomainObjBuilder.GetParentMessageUsesList(selectedLessonId);

            foreach (var x in parentMessageUseObjList )
            {
                x.SelectedMessageUseId = selectedMessageUseId;

                if (x.IsThisOrChildrenLive)
                {
                    viewObj.LiveMessages.Add(x);
                }
                else if (x.IsThisOrChildrenStoredButNotArchived)
                {
                    viewObj.StoredMessages.Add(x);
                }
                else
                {
                    viewObj.ArchivedMessages.Add(x);
                }
            }


            return viewObj;
        }

       

    }
}

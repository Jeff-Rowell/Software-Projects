//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CWMasterTeacherDataModel
//{
//    public static class RepoFactory
//    {
//        private static ClassMeetingRepo _classMeetingRepo;
//        private static ClassSectionRepo _classSectionRepo;
//        private static CourseRepo _courseRepo;
//        private static DocumentLinkRepo _documentLinkRepo;
//        private static DocumentRepo _documentRepo;
//        private static DocumentTypeRepo _documentTypeRepo;
//        private static DocumentTypeSetRepo _documentTypeSetRepo;
//        private static DocumentUseRepo _documentUseRepo;
//        private static HolidayRepo _holidayRepo;
//        private static InstitutionRepo _institutionRepo;
//        private static LessonPlanLinkRepo _lessonPlanLinkRepo;
//        private static LessonPlanRepo _lessonPlanRepo;
//        private static LessonRepo _lessonRepo;
//        private static LessonUseRepo _lessonUseRepo;
//        private static LevelRepo _levelRepo;
//        private static LevelSetRepo _levelSetRepo;
//        private static MessageRepo _messageRepo;
//        private static MessageUseRepo _messageUseRepo;
//        private static NarrativeRepo _narrativeRepo;
//        private static ReferenceCalendarRepo _referenceCalendarRepo;
//        private static TermRepo _termRepo;
//        private static UserRepo _userRepo;


//        public static ClassMeetingRepo GetClassMeetingRepo
//        {
//            get
//            {
//                if (_classMeetingRepo == null)
//                {
//                    _classMeetingRepo = new ClassMeetingRepo();
//                }
//                return _classMeetingRepo;
//            }
//        }

//        public static ClassSectionRepo GetClassSectionRepo
//        {
//            get
//            {
//                if (_classSectionRepo == null)
//                {
//                    _classSectionRepo = new ClassSectionRepo();
//                }
//                return _classSectionRepo;
//            }
//        }

//        public static CourseRepo GetCourseRepo
//        {
//            get
//            {
//                if (_courseRepo == null)
//                {
//                    _courseRepo = new CourseRepo();
//                }
//                return _courseRepo;
//            }
//        }

//        public static DocumentLinkRepo GetDocumentLinkRepo
//        {
//            get
//            {
//                if (_documentLinkRepo == null)
//                {
//                    _documentLinkRepo = new DocumentLinkRepo();
//                }
//                return _documentLinkRepo;
//            }
//        }

//        public static DocumentRepo GetDocumentRepo
//        {
//            get
//            {
//                if (_documentRepo == null)
//                {
//                    _documentRepo = new DocumentRepo();
//                }
//                return _documentRepo;
//            }
//        }

//        public static DocumentTypeRepo GetDocumentTypeRepo
//        {
//            get
//            {
//                if (_documentTypeRepo == null)
//                {
//                    _documentTypeRepo = new DocumentTypeRepo();
//                }
//                return _documentTypeRepo;
//            }
//        }

//        public static DocumentTypeSetRepo GetDocumentTypeSetRepo
//        {
//            get
//            {
//                if (_documentTypeSetRepo == null)
//                {
//                    _documentTypeSetRepo = new DocumentTypeSetRepo();
//                }
//                return _documentTypeSetRepo;
//            }
//        }

//        public static DocumentUseRepo GetDocumentUseRepo
//        {
//            get
//            {
//                if (_documentUseRepo == null)
//                {
//                    _documentUseRepo = new DocumentUseRepo();
//                }
//                return _documentUseRepo;
//            }
//        }

//        public static HolidayRepo GetHolidayRepo
//        {
//            get
//            {
//                if (_holidayRepo == null)
//                {
//                    _holidayRepo = new HolidayRepo();
//                }
//                return _holidayRepo;
//            }
//        }

//        public static InstitutionRepo GetInstitutionRepo
//        {
//            get
//            {
//                if (_institutionRepo == null)
//                {
//                    _institutionRepo = new InstitutionRepo();
//                }
//                return _institutionRepo;
//            }
//        }

//        public static LessonPlanLinkRepo GetLessonPlanLinkRepo
//        {
//            get
//            {
//                if (_lessonPlanLinkRepo == null)
//                {
//                    _lessonPlanLinkRepo = new LessonPlanLinkRepo();
//                }
//                return _lessonPlanLinkRepo;
//            }
//        }

//        public static LessonPlanRepo GetLessonPlanRepo
//        {
//            get
//            {
//                if (_lessonPlanRepo == null)
//                {
//                    _lessonPlanRepo = new LessonPlanRepo();
//                }
//                return _lessonPlanRepo;
//            }
//        }

//        public static LessonRepo GetLessonRepo
//        {
//            get
//            {
//                if (_lessonRepo == null)
//                {
//                    _lessonRepo = new LessonRepo();
//                }
//                return _lessonRepo;
//            }
//        }

//        public static LessonUseRepo GetLessonUseRepo
//        {
//            get
//            {
//                if (_lessonUseRepo == null)
//                {
//                    _lessonUseRepo = new LessonUseRepo();
//                }
//                return _lessonUseRepo;
//            }
//        }

//        public static LevelRepo GetLevelRepo
//        {
//            get
//            {
//                if (_levelRepo == null)
//                {
//                    _levelRepo = new LevelRepo();
//                }
//                return _levelRepo;
//            }
//        }

//        public static LevelSetRepo GetLevelSetRepo
//        {
//            get
//            {
//                if (_levelSetRepo == null)
//                {
//                    _levelSetRepo = new LevelSetRepo();
//                }
//                return _levelSetRepo;
//            }
//        }

//        public static MessageRepo GetMessageRepo
//        {
//            get
//            {
//                if (_messageRepo == null)
//                {
//                    _messageRepo = new MessageRepo();
//                }
//                return _messageRepo;
//            }
//        }

//        public static MessageUseRepo GetMessageUseRepo
//        {
//            get
//            {
//                if (_messageUseRepo == null)
//                {
//                    _messageUseRepo = new MessageUseRepo();
//                }
//                return _messageUseRepo;
//            }
//        }

//        public static NarrativeRepo GetNarrativeRepo
//        {
//            get
//            {
//                if (_narrativeRepo == null)
//                {
//                    _narrativeRepo = new NarrativeRepo();
//                }
//                return _narrativeRepo;
//            }
//        }

//        public static ReferenceCalendarRepo GetReferenceCalendarRepo
//        {
//            get
//            {
//                if (_referenceCalendarRepo == null)
//                {
//                    _referenceCalendarRepo = new ReferenceCalendarRepo();
//                }
//                return _referenceCalendarRepo;
//            }
//        }

//        public static TermRepo GetTermRepo
//        {
//            get
//            {
//                if (_termRepo == null)
//                {
//                    _termRepo = new TermRepo();
//                }
//                return _termRepo;
//            }
//        }

//        public static UserRepo GetUserRepo
//        {
//            get
//            {
//                if (_userRepo == null)
//                {
//                    _userRepo = new UserRepo();
//                }
//                return _userRepo;
//            }
//        }





//    }
//}

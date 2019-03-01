using CWMasterTeacherDataModel;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDomain.DomainObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWTesting.Tests.CWMasterTeacherService.CUDServices
{
    public class MocksForCUDs
    {

        internal Mock<ILessonRepo> mockLessonRepo { get; private set; }
        internal Mock<IUserRepo> mockUserRepo { get; private set; }
        internal Mock<ICourseRepo> mockCourseRepo { get; private set; }
        internal Mock<IMetaCourseRepo> mockMetaCourseRepo { get; private set; }
        internal Mock<ICoursePreferenceRepo> mockCoursePreferenceRepo { get; private set; }
        internal Mock<IWorkingGroupRepo> mockWorkingGroupRepo { get; private set; }
        internal Mock<IUserPreferenceRepo> mockUserPreferenceRepo { get; private set; }
        internal Mock<IDocumentUseRepo> mockDocumentUseRepo { get; private set; }
        internal Mock<IStashedLessonPlanRepo> mockStashedLessonPlanRepo { get; private set; }
        internal Mock<IStashedNarrativeRepo> mockStashedNarrativeRepo { get; private set; }
        internal Mock<IMessageUseRepo> mockMessageUseRepo { get; private set; }
        internal Mock<IMetaLessonRepo> mockMetaLessonRepo { get; private set; }
        internal Mock<ITermRepo> mockTermRepo { get; private set; }
        internal Mock<IMessageUseDomainObjBuilder> mockMessageUseDomainObjBuilder { get; private set; }
        internal Mock<ILessonPlanRepo> mockLessonPlanRepo { get; private set; }
        internal Mock<INarrativeRepo> mockNarrativeRepo { get; private set; }
        internal Mock<ILessonDomainObjBuilder> mockLessonDomainObjBuilder { get; private set; }

        internal List<Course> repoCourseList { get; private set; }
        internal List<MetaCourse> repoMetaCourseList { get; private set; }
        internal List<CoursePreference> repoCoursePreferenceList { get; private set; }
        internal List<Lesson> repoLessonList { get; private set; }
        internal List<User> repoUserList { get; private set; }
        internal List<DocumentUse> repoDocumentUseList { get; private set; }
        internal List<StashedLessonPlan> repoStashedLessonPlanList { get; private set; }
        internal List<StashedNarrative> repoStashedNarrativeList { get; private set; }
        internal List<MessageUse> repoMessageUseList { get; private set; }
        internal List<MetaLesson> repoMetaLessonList { get; private set; }
        internal List<Term> repoTermList { get; private set; }
        internal List<LessonPlan> repoLessonPlanList { get; private set; }
        internal List<Narrative> repoNarrativeList { get; private set; }

        Exception e;

        internal MocksForCUDs()
        {
            InitializeMockRepositoriesAndLists();
            InitializeMockCourseRepo();
            InitializeMockLessonRepo();
            InitializeMockCoursePreferenceRepo();
            InitializeMockMetaCourseRepo();
            InitializeMockUserRepo();
            InitializeMockStashedLessonPlanRepo();
            InitializeMockStashedNarrativeRepo();
            InitializeMockDocumentUseRepo();
            InitializeMockMessageUseRepo();
            InitializeMockMessageUseDomainObjBuilder();
            InitializeMockTermRepo();
            InitializeMockLessonPlanRepo();
            InitializeMockNarrativeRepo();
            InitializeMockLessonDomainObjBuilder();
        }

        internal void InitializeMockRepositoriesAndLists()
        {
            repoCourseList = new List<Course>();
            repoMetaCourseList = new List<MetaCourse>();
            repoCoursePreferenceList = new List<CoursePreference>();
            repoLessonList = new List<Lesson>();
            repoUserList = new List<User>();
            repoDocumentUseList = new List<DocumentUse>();
            repoStashedLessonPlanList = new List<StashedLessonPlan>();
            repoStashedNarrativeList = new List<StashedNarrative>();
            repoMessageUseList = new List<MessageUse>();
            repoMetaLessonList = new List<MetaLesson>();
            repoTermList = new List<Term>();
            repoLessonPlanList = new List<LessonPlan>();
            repoNarrativeList = new List<Narrative>();

            mockLessonRepo = new Mock<ILessonRepo>();
            mockUserRepo = new Mock<IUserRepo>();
            mockCourseRepo = new Mock<ICourseRepo>();
            mockMetaCourseRepo = new Mock<IMetaCourseRepo>();
            mockCoursePreferenceRepo = new Mock<ICoursePreferenceRepo>();
            mockWorkingGroupRepo = new Mock<IWorkingGroupRepo>();
            mockUserPreferenceRepo = new Mock<IUserPreferenceRepo>();
            mockDocumentUseRepo = new Mock<IDocumentUseRepo>();
            mockStashedLessonPlanRepo = new Mock<IStashedLessonPlanRepo>();
            mockStashedNarrativeRepo = new Mock<IStashedNarrativeRepo>();
            mockMessageUseRepo = new Mock<IMessageUseRepo>();
            mockMetaLessonRepo = new Mock<IMetaLessonRepo>();
            mockTermRepo = new Mock<ITermRepo>();
            mockMessageUseDomainObjBuilder = new Mock<IMessageUseDomainObjBuilder>();
            mockLessonPlanRepo = new Mock<ILessonPlanRepo>();
            mockNarrativeRepo = new Mock<INarrativeRepo>();
            mockLessonDomainObjBuilder = new Mock<ILessonDomainObjBuilder>();
        }


        internal void InitializeMockCourseRepo() {
            mockCourseRepo.Setup(mock => mock.GetById(It.IsAny<Guid>())).Returns((Guid i) =>
            {
                return repoCourseList.Find(x => x.Id == i);
            });

            mockCourseRepo.Setup(mock => mock.Insert(It.IsAny<Course>())).Callback((Course i) =>
            {
                if (i.Id == Guid.Empty)
                {
                    i.Id = Guid.NewGuid();
                }

                repoCourseList.Add(i);

            });

            mockCourseRepo.Setup(mock => mock.Delete(It.IsAny<Guid>())).Callback((Guid i) =>
            {
                repoCourseList.Remove(repoCourseList.Find(x => x.Id == i));
            });

            mockLessonRepo.Setup(mock => mock.Delete(It.IsAny<Guid>())).Callback((Guid i) =>
            {
                repoLessonList.Remove(repoLessonList.Find(x => x.Id == i));
            });

            mockCourseRepo.Setup(mock => mock.Update(It.IsAny<Course>())).Callback((Course i) =>
            {
                if (!repoCourseList.Remove(repoCourseList.Find(x => x.Id == i.Id))) throw e;
                repoCourseList.Add(i);
            });

            mockCourseRepo.Setup(mock => mock.CoursesForTermAndUser(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(
                (Guid i, Guid j) =>
                {
                    return repoCourseList.Where(x => x.TermId == i && x.UserId == j && x.MasterCourseId != null)
                    .OrderBy(x => x.Name).ToList();
                });

            mockCourseRepo.Setup(mock => mock.MasterCoursesForTermAndMetaCourse(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(
                (Guid i, Guid j) =>
                {
                    return repoCourseList.Where(x => x.TermId == i && x.MetaCourseId == j && x.MasterCourseId == null)
                    .OrderByDescending(x => x.Term.StartDate).ThenBy(x => x.Name).ToList();
                });

        }

        internal void InitializeMockLessonRepo()
        {
            mockLessonRepo.Setup(mock => mock.Insert(It.IsAny<Lesson>())).Callback(
            (Lesson i) =>
            {
                if (i.Id == Guid.Empty)
                {
                    i.Id = Guid.NewGuid();
                }

                repoLessonList.Add(i);
            }
        );

            mockLessonRepo.Setup(mock => mock.Update(It.IsAny<Lesson>())).Callback((Lesson i) =>
            {
                if (!repoLessonList.Remove(repoLessonList.Find(x => x.Id == i.Id))) throw e;
                repoLessonList.Add(i);
            });

            mockLessonRepo.Setup(mock => mock.GetById(It.IsAny<Guid>())).Returns((Guid i) =>
            {
                return repoLessonList.Find(x => x.Id == i);

            });

            mockLessonRepo.Setup(mock => mock.ActiveLessonsForCourse(It.IsAny<Guid>())).Returns((Guid i) =>
            {
                return repoLessonList.Where(x => x.CourseId == i && x.IsActive).ToList();
            });

            mockLessonRepo.Setup(mock => mock.LessonsForContainerLesson(It.IsAny<Guid>())).Returns((Guid i) =>
            {
                return repoLessonList.Where(x => x.ContainerLessonId.Equals(i) && x.IsActive).OrderBy(x => x.SequenceNumber).ToList();
            });

            mockLessonRepo.Setup(mock => mock.LessonsForCourse(It.IsAny<Guid>())).Returns((Guid i) =>
            {
                return repoLessonList.Where(x => x.CourseId == i && x.IsActive).OrderBy(x => x.SequenceNumber).ToList();
            });
            
            mockLessonRepo.Setup(mock => mock.LessonAboveGivenLesson(It.IsAny<Lesson>())).Returns((Lesson i) =>
            {
                List<Lesson> lessonList = mockLessonRepo.Object.LessonsThatShareContainer(i).ToList();
                return lessonList.Where(x => x.SequenceNumber > i.SequenceNumber).OrderBy(x => x.SequenceNumber).FirstOrDefault();
            });

            mockLessonRepo.Setup(mock => mock.LessonBelowGivenLesson(It.IsAny<Lesson>())).Returns((Lesson i) =>
            {
                List<Lesson> lessonList = mockLessonRepo.Object.LessonsThatShareContainer(i).ToList();
                return lessonList.Where(x => x.SequenceNumber < i.SequenceNumber).OrderByDescending(x => x.SequenceNumber).FirstOrDefault();
            });

            mockLessonRepo.Setup(mock => mock.LessonsThatShareContainer(It.IsAny<Lesson>())).Returns((Lesson i) =>
            {
                if (i.ContainerLessonId != null)
                {
                    return mockLessonRepo.Object.LessonsForContainerLesson(i.ContainerLessonId.Value);
                }
                else  //Which means the container is a course
                {
                    return mockLessonRepo.Object.LessonsForContainerCourse(i.CourseId);
                }
            });


        }

        internal void InitializeMockCoursePreferenceRepo()
        {
            mockLessonRepo.Setup(mock => mock.LessonsForContainerCourse(It.IsAny<Guid>())).Returns((Guid containerCourseId) =>
            {
                return repoLessonList.Where(x => x.CourseId == containerCourseId && x.ContainerLessonId == null && x.IsActive).OrderBy(x => x.SequenceNumber).ToList();
            });

            mockCoursePreferenceRepo.Setup(mock => mock.Insert(It.IsAny<CoursePreference>())).Callback((CoursePreference i) =>
            {
                if (i.Id == Guid.Empty)
                {
                    i.Id = Guid.NewGuid();
                }

                repoCoursePreferenceList.Add(i);
            });
        }

        internal void InitializeMockMetaCourseRepo()
        {
            mockMetaCourseRepo.Setup(mock => mock.Insert(It.IsAny<MetaCourse>())).Callback((MetaCourse i) =>
            {
                if (i.Id == Guid.Empty)
                {
                    i.Id = Guid.NewGuid();
                }

                repoMetaCourseList.Add(i);
            });
        }

        internal void InitializeMockUserRepo()
        {
            mockUserRepo.Setup(mock => mock.GetById(It.IsAny<Guid>())).Returns((Guid i) =>
            {
                return repoUserList.Find(x => x.Id == i);
            });

            mockUserRepo.Setup(mock => mock.Update(It.IsAny<User>())).Callback((User i) =>
            {
                if (!repoUserList.Remove(repoUserList.Find(x => x.Id == i.Id))) throw e;
                repoUserList.Add(i);
            });

            mockUserRepo.Setup(mock => mock.Insert(It.IsAny<User>())).Callback((User i) =>
            {
                if (i.Id == Guid.Empty)
                {
                    i.Id = Guid.NewGuid();
                }

                repoUserList.Add(i);
            });

        }

        internal void InitializeMockStashedLessonPlanRepo()
        {
            mockStashedLessonPlanRepo.Setup(mock => mock.DeleteMultiple(It.IsAny<List<StashedLessonPlan>>())).Callback((List<StashedLessonPlan> list) =>
            {
                foreach (var x in list)
                {
                    repoStashedLessonPlanList.Remove(x);
                }
            });
        }

        internal void InitializeMockStashedNarrativeRepo()
        {
            mockStashedNarrativeRepo.Setup(mock => mock.DeleteMultiple(It.IsAny<List<StashedNarrative>>())).Callback((List<StashedNarrative> list) =>
            {
                foreach (var x in list)
                {
                    repoStashedNarrativeList.Remove(x);
                }
            });
        }

        internal void InitializeMockDocumentUseRepo()
        {
            mockDocumentUseRepo.Setup(mock => mock.DeleteMultiple(It.IsAny<List<DocumentUse>>())).Callback((List<DocumentUse> list) =>
            {
                foreach (var x in list)
                {
                    repoDocumentUseList.Remove(x);
                }
            });

            mockDocumentUseRepo.Setup(mock => mock.ActiveDocumentUsesForLesson(It.IsAny<Guid>())).Returns((Guid i) =>
            {
                return repoDocumentUseList.Where(x => x.LessonId == i && x.IsActive).OrderBy(x => x.Document.Name).ToList();
            });

            mockDocumentUseRepo.Setup(mock => mock.Insert(It.IsAny<DocumentUse>())).Callback((DocumentUse i) =>
            {
                if (i.Id == Guid.Empty)
                {
                    i.Id = Guid.NewGuid();
                }

                repoDocumentUseList.Add(i);
            });
        }

        internal void InitializeMockMessageUseRepo()
        {
            mockMessageUseRepo.Setup(mock => mock.DeleteMultiple(It.IsAny<List<MessageUse>>())).Callback((List<MessageUse> list) =>
            {
                foreach (var x in list)
                {
                    repoMessageUseList.Remove(x);
                }
            });

            mockMessageUseRepo.Setup(mock => mock.Insert(It.IsAny<MessageUse>())).Callback((MessageUse i) =>
            {
                if (i.Id == Guid.Empty)
                {
                    i.Id = Guid.NewGuid();
                }

                repoMessageUseList.Add(i);
            });

            mockMessageUseRepo.Setup(mock => mock.GetById(It.IsAny<Guid>())).Returns((Guid i) =>
            {
                return repoMessageUseList.Find(x => x.Id == i);
            });

            mockMessageUseRepo.Setup(mock => mock.GetAllActiveMessageUsesForLesson(It.IsAny<Guid>())).Returns((Guid i) =>
            {
                return repoMessageUseList.Where(x => x.Id == i).OrderByDescending(x => x.Message.TimeStamp).ToList();
            });

            mockMessageUseRepo.Setup(mock => mock.GetAllMessageUsesForLesson(It.IsAny<Guid>())).Returns((Guid i) =>
            {
                return repoMessageUseList.Where(x => x.LessonId == i).OrderByDescending(x => x.Message.TimeStamp).ToList();
            });

            mockMessageUseRepo.Setup(mock => mock.GetChildMessageUsesForMessageUse(It.IsAny<MessageUse>())).Returns((MessageUse i) =>
            {
                return repoMessageUseList.Where(x => x.LessonId == i.LessonId && x.Message.ThreadParentId == i.Message.Id).OrderByDescending(x => x.Message.TimeStamp).ToList();
            });

            mockMessageUseRepo.Setup(mock => mock.GetChildMessageUsesForMessageUse(It.IsAny<MessageUse>())).Returns((MessageUse i) =>
            {
                return repoMessageUseList.Where(x => x.LessonId == i.LessonId && x.Message.ThreadParentId == i.Message.Id).OrderByDescending(x => x.Message.TimeStamp).ToList();
            });
        }

        internal void InitializeMockMessageUseDomainObjBuilder()
        {
            mockMessageUseDomainObjBuilder.Setup(mock => mock.GetAllMessageUseObjsForLesson(It.IsAny<Guid>())).Returns((Guid i) =>
            {
                IEnumerable<MessageUse> allMessageUsesList = mockMessageUseRepo.Object.GetAllMessageUsesForLesson(i);
                allMessageUsesList.Select(x => mockMessageUseDomainObjBuilder.Object.BuildCompleteFromDbObject(x)).ToList();
                return null;
            });

            mockMessageUseDomainObjBuilder.Setup(mock => mock.BuildCompleteFromDbObject(It.IsAny<MessageUse>())).Returns((MessageUse i) =>
            {
                if (i != null)
                {
                    MessageUseDomainObj messageUseObj = MessageUseDomainObjBuilder.Build(i);
                    messageUseObj.ChildMessageUseObjList = mockMessageUseDomainObjBuilder.Object.GetChildMessageUseObjsForMessageUseList(i);
                    foreach (var xObj in messageUseObj.ChildMessageUseObjList)
                    {
                        mockMessageUseDomainObjBuilder.Object.GetChildMessageUseObjsForMessageUseListFromId(xObj.Id);
                    }
                    return messageUseObj;
                }
                else
                {
                    return null;
                }
            });

            mockMessageUseDomainObjBuilder.Setup(mock => mock.GetChildMessageUseObjsForMessageUseList(It.IsAny<MessageUse>())).Returns((MessageUse i) =>
            {
                IEnumerable<MessageUse> childMessageUsesList = mockMessageUseRepo.Object.GetChildMessageUsesForMessageUse(i);
                IEnumerable<MessageUseDomainObj> childMessageUseObjList = childMessageUsesList.Select(x => mockMessageUseDomainObjBuilder.Object.BuildCompleteFromDbObject(x));
                return childMessageUseObjList.ToList();
            });

            mockMessageUseDomainObjBuilder.Setup(mock => mock.GetChildMessageUseObjsForMessageUseListFromId(It.IsAny<Guid>())).Returns((Guid i) =>
            {
                MessageUse messageUse = mockMessageUseRepo.Object.GetById(i);
                IEnumerable<MessageUse> childMessageUsesList = mockMessageUseRepo.Object.GetChildMessageUsesForMessageUse(messageUse);
                IEnumerable<MessageUseDomainObj> childMessageUseObjList = childMessageUsesList.Select(x => mockMessageUseDomainObjBuilder.Object.BuildCompleteFromDbObject(x));
                return childMessageUseObjList.ToList();
            });
        }

        internal void InitializeMockTermRepo()
        {
            mockTermRepo.Setup(mock => mock.Insert(It.IsAny<Term>())).Callback((Term i) =>
            {
                if (i.Id == Guid.Empty)
                {
                    i.Id = Guid.NewGuid();
                }

                repoTermList.Add(i);
            });

            mockTermRepo.Setup(mock => mock.GetById(It.IsAny<Guid>())).Returns((Guid i) =>
            {
                return repoTermList.Find(x => x.Id == i);
            });
        }

        internal void InitializeMockLessonPlanRepo()
        {
            mockLessonPlanRepo.Setup(mock => mock.GetById(It.IsAny<Guid>())).Returns((Guid i) =>
            {
                return repoLessonPlanList.Find(x => x.Id == i);
            });

            mockLessonPlanRepo.Setup(mock => mock.Insert(It.IsAny<LessonPlan>())).Callback((LessonPlan i) =>
            {
                if (i.Id == Guid.Empty)
                {
                    i.Id = Guid.NewGuid();
                }
                repoLessonPlanList.Add(i);
            });

            mockLessonPlanRepo.Setup(mock => mock.Update(It.IsAny<LessonPlan>())).Callback((LessonPlan i) =>
            {
                if (!repoLessonPlanList.Remove(repoLessonPlanList.Find(x => x.Id == i.Id))) throw e;
                repoLessonPlanList.Add(i);
            });
        }

        internal void InitializeMockNarrativeRepo()
        {
            mockNarrativeRepo.Setup(mock => mock.Insert(It.IsAny<Narrative>())).Callback((Narrative i) =>
            {
                if (i.Id == Guid.Empty)
                {
                    i.Id = Guid.NewGuid();
                }
                repoNarrativeList.Add(i);
            });
        }

        internal void InitializeMockLessonDomainObjBuilder()
        {
            mockLessonDomainObjBuilder.Setup(mock => mock.BuildBasicFromId(It.IsAny<Guid>())).Returns(
                (Guid i) =>
                {
                    foreach (var x in repoLessonList)
                        if (i.Equals(x.Id)) return LessonDomainObjBuilder.BuildBasic(x);
                    return null;
                });
        }


        public void ClearLists()
        {
            repoCourseList.Clear();
            repoMetaCourseList.Clear();
            repoCoursePreferenceList.Clear();
            repoLessonList.Clear();
            repoUserList.Clear();
            repoDocumentUseList.Clear();
            repoStashedLessonPlanList.Clear();
            repoStashedNarrativeList.Clear();
            repoMessageUseList.Clear();
            repoMetaLessonList.Clear();
            repoTermList.Clear();
            repoLessonPlanList.Clear();
            repoNarrativeList.Clear();
        }
    }
}

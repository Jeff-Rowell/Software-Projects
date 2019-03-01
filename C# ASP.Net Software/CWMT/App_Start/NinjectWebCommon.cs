[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CWMasterTeacher3.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(CWMasterTeacher3.App_Start.NinjectWebCommon), "Stop")]

namespace CWMasterTeacher3.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using CWMasterTeacherDataModel;
    using CWMasterTeacherDataModel.ObjectBuilders;
    using CWMasterTeacherDataModel.Interfaces;
    using CWMasterTeacherDomain.DomainObjects;
    using CWMasterTeacherService.ViewObjectBuilder;
    using CWMasterTeacherService;
    using CWMasterTeacherService.CUDServices;
    using CWMasterTeacherService.CUDRouters;
    using CWMasterTeacherService.DomainObjectBuilders;
    using CWMT.Services;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ClassMeetingRepo>().To<ClassMeetingRepo>().InRequestScope();
            kernel.Bind<IClassMeetingRepo>().To<ClassMeetingRepo>().InRequestScope();
            kernel.Bind<ClassSectionRepo>().To<ClassSectionRepo>().InRequestScope();
            kernel.Bind<IClassSectionRepo>().To<ClassSectionRepo>().InRequestScope();
            kernel.Bind<ICourseRepo>().To<CourseRepo>().InRequestScope();
            kernel.Bind<CourseRepo>().To<CourseRepo>().InRequestScope();
            kernel.Bind<DocumentLinkRepo>().To<DocumentLinkRepo>().InRequestScope();
            kernel.Bind<DocumentRepo>().To<DocumentRepo>().InRequestScope();
            kernel.Bind<DocumentUseRepo>().To<DocumentUseRepo>().InRequestScope();
            kernel.Bind<IDocumentUseRepo>().To<DocumentUseRepo>().InRequestScope();
            kernel.Bind<HolidayRepo>().To<HolidayRepo>().InRequestScope();
            kernel.Bind<WorkingGroupRepo>().To<WorkingGroupRepo>().InRequestScope();
            kernel.Bind<IWorkingGroupRepo>().To<WorkingGroupRepo>().InRequestScope();
            kernel.Bind<LessonPlanLinkRepo>().To<LessonPlanLinkRepo>().InRequestScope();
            kernel.Bind<LessonPlanRepo>().To<LessonPlanRepo>().InRequestScope();
            kernel.Bind<ILessonPlanRepo>().To<LessonPlanRepo>().InRequestScope();
            kernel.Bind<LessonRepo>().To<LessonRepo>().InRequestScope();
            kernel.Bind<ILessonRepo>().To<LessonRepo>().InRequestScope();
            kernel.Bind<LessonUseRepo>().To<LessonUseRepo>().InRequestScope();
            kernel.Bind<MessageRepo>().To<MessageRepo>().InRequestScope();
            kernel.Bind<MessageUseRepo>().To<MessageUseRepo>().InRequestScope();
            kernel.Bind<IMessageUseRepo>().To<MessageUseRepo>().InRequestScope();
            kernel.Bind<NarrativeRepo>().To<NarrativeRepo>().InRequestScope();
            kernel.Bind<INarrativeRepo>().To<NarrativeRepo>().InRequestScope();
            kernel.Bind<ReferenceCalendarRepo>().To<ReferenceCalendarRepo>().InRequestScope();
            kernel.Bind<TermRepo>().To<TermRepo>().InRequestScope();
            kernel.Bind<ITermRepo>().To<TermRepo>().InRequestScope();
            kernel.Bind<UserRepo>().To<UserRepo>().InRequestScope();
            kernel.Bind<IUserRepo>().To<UserRepo>().InRequestScope();
            kernel.Bind<StashedLessonPlanRepo>().To<StashedLessonPlanRepo>().InRequestScope();
            kernel.Bind<IStashedLessonPlanRepo>().To<StashedLessonPlanRepo>().InRequestScope();
            kernel.Bind<StashedNarrativeRepo>().To<StashedNarrativeRepo>().InRequestScope();
            kernel.Bind<IStashedNarrativeRepo>().To<StashedNarrativeRepo>().InRequestScope();
            kernel.Bind<ZDevelopmentRepo>().To<ZDevelopmentRepo>().InRequestScope();
            kernel.Bind<CoursePreferenceRepo>().To<CoursePreferenceRepo>().InRequestScope();
            kernel.Bind<ICoursePreferenceRepo>().To<CoursePreferenceRepo>().InRequestScope();
            kernel.Bind<UserPreferenceRepo>().To<UserPreferenceRepo>().InRequestScope();
            kernel.Bind<IUserPreferenceRepo>().To<UserPreferenceRepo>().InRequestScope();
            kernel.Bind<WorkingGroupPreferenceRepo >().To<WorkingGroupPreferenceRepo>().InRequestScope();
            kernel.Bind<MetaCourseRepo>().To<MetaCourseRepo>().InRequestScope();
            kernel.Bind<IMetaCourseRepo>().To<MetaCourseRepo>().InRequestScope();
            kernel.Bind<MetaLessonRepo>().To<MetaLessonRepo>().InRequestScope();
            kernel.Bind<IMetaLessonRepo>().To<MetaLessonRepo>().InRequestScope();
            kernel.Bind<ClassSectionStudentRepo>().To<ClassSectionStudentRepo>().InRequestScope();
            kernel.Bind<ReleasedDocumentRepo>().To<ReleasedDocumentRepo>().InRequestScope();
            kernel.Bind<StudentUserRepo>().To<StudentUserRepo>().InRequestScope();



            kernel.Bind<MasterTeacherContext>().To<MasterTeacherContext>().InRequestScope();

            kernel.Bind<IDocumentUseDomainObjBuilder>().To<DocumentUseDomainObjBuilder>().InRequestScope();
            kernel.Bind<IDomainObjBuilder<DocumentDomainObj, DocumentDomainObjBasic, Document>>().To<DocumentDomainObjBuilder>().InRequestScope();
            kernel.Bind<IMessageUseDomainObjBuilder>().To<MessageUseDomainObjBuilder>().InRequestScope();
            kernel.Bind<IMessageDomainObjBuilder>().To<MessageDomainObjBuilder>().InRequestScope();
            kernel.Bind<IUserDomainObjBuilder>().To<UserDomainObjBuilder>().InRequestScope();
            kernel.Bind<ITermDomainObjBuilder>().To<TermDomainObjBuilder>().InRequestScope();
            kernel.Bind<ICourseDomainObjBuilder>().To<CourseDomainObjBuilder>().InRequestScope();
            kernel.Bind<IWorkingGroupDomainObjBuilder>().To<WorkingGroupDomainObjBuilder>().InRequestScope();
            kernel.Bind<IClassSectionDomainObjBuilder>().To<ClassSectionDomainObjBuilder>().InRequestScope();
            kernel.Bind<IClassMeetingDomainObjBuilder>().To<ClassMeetingDomainObjBuilder>().InRequestScope();
            kernel.Bind<ILessonUseDomainObjBuilder>().To<LessonUseDomainObjBuilder>().InRequestScope();
            kernel.Bind<ILessonPlanDomainObjBuilder>().To<LessonPlanDomainObjBuilder>().InRequestScope();
            kernel.Bind<IDocumentDomainObjBuilder>().To<DocumentDomainObjBuilder>().InRequestScope();
            kernel.Bind<TermDomainObjBuilder>().To<TermDomainObjBuilder>().InRequestScope();
            kernel.Bind<IDomainObjBuilder<TermDomainObj, TermDomainObjBasic, Term>>().To<TermDomainObjBuilder>().InRequestScope();
            kernel.Bind<HolidayDomainObjBuilder>().To<HolidayDomainObjBuilder>().InRequestScope();
            kernel.Bind<IDomainObjBuilder<HolidayDomainObj, HolidayDomainObjBasic, Holiday>>().To<HolidayDomainObjBuilder>().InRequestScope();
            kernel.Bind<ILessonDomainObjBuilder>().To<LessonDomainObjBuilder>().InRequestScope();
            kernel.Bind<ILessonForComparisonDomainObjBuilder>().To<LessonForComparisonDomainObjBuilder>().InRequestScope();
            kernel.Bind<StashedLessonPlanDomainObjBuilder>().To<StashedLessonPlanDomainObjBuilder>().InRequestScope();
            kernel.Bind<NarrativeDomainObjBuilder>().To<NarrativeDomainObjBuilder>().InRequestScope();
            kernel.Bind<MetaCourseDomainObjBuilder>().To<MetaCourseDomainObjBuilder>().InRequestScope();
            kernel.Bind<StudentUserDomainObjBuilder>().To<StudentUserDomainObjBuilder>().InRequestScope();
            kernel.Bind<ClassSectionStudentDomainObjBuilder>().To<ClassSectionStudentDomainObjBuilder>().InRequestScope();
            kernel.Bind<ReleasedDocumentDomainObjBuilder>().To<ReleasedDocumentDomainObjBuilder>().InRequestScope();


            kernel.Bind<DocumentsTabViewObjBuilder>().To<DocumentsTabViewObjBuilder>().InRequestScope();
            kernel.Bind<MessageBoardViewObjBuilder>().To<MessageBoardViewObjBuilder>().InRequestScope();
            kernel.Bind<CourseCopyAdminViewObjBuilder>().To<CourseCopyAdminViewObjBuilder>().InRequestScope();
            kernel.Bind<CourseCreateAdminViewObjBuilder>().To<CourseCreateAdminViewObjBuilder>().InRequestScope();
            kernel.Bind<CurriculumViewObjBuilder>().To<CurriculumViewObjBuilder>().InRequestScope();
            kernel.Bind<LessonPlanViewObjBuilder>().To<LessonPlanViewObjBuilder>().InRequestScope();
            kernel.Bind<DocumentStorageService>().To<DocumentStorageService>().InRequestScope();
            kernel.Bind<LessonManagerViewObjBuilder>().To<LessonManagerViewObjBuilder>().InRequestScope();
            kernel.Bind<NarrativeViewObjBuilder>().To<NarrativeViewObjBuilder>().InRequestScope();
            kernel.Bind<UserProfileViewObjBuilder>().To<UserProfileViewObjBuilder>().InRequestScope();
            kernel.Bind<zDevelopmentViewObjBuilder>().To<zDevelopmentViewObjBuilder>().InRequestScope();
            kernel.Bind<LessonCompareViewObjBuilder>().To<LessonCompareViewObjBuilder>().InRequestScope();
            kernel.Bind<CompareSelectViewObjBuilder>().To<CompareSelectViewObjBuilder>().InRequestScope();
            kernel.Bind<EmptyEditableViewObjBuilder>().To<EmptyEditableViewObjBuilder>().InRequestScope();
            kernel.Bind<StudentHomeViewObjBuilder>().To<StudentHomeViewObjBuilder>().InRequestScope();
            kernel.Bind<WorkingGroupAdminViewObjBuilder>().To<WorkingGroupAdminViewObjBuilder>().InRequestScope();
            kernel.Bind<AdminResetPasswordViewObjBuilder>().To<AdminResetPasswordViewObjBuilder>().InRequestScope();


            kernel.Bind<ClassMeetingCUDService>().To<ClassMeetingCUDService>().InRequestScope();
            kernel.Bind<ClassSectionCUDService>().To<ClassSectionCUDService>().InRequestScope();
            kernel.Bind<CourseCUDService>().To<CourseCUDService>().InRequestScope();
            kernel.Bind<DocumentCUDService>().To<DocumentCUDService>().InRequestScope();
            kernel.Bind<DocumentUseCUDService>().To<DocumentUseCUDService>().InRequestScope();
            kernel.Bind<HolidayCUDService>().To<HolidayCUDService>().InRequestScope();
            kernel.Bind<WorkingGroupCUDService>().To<WorkingGroupCUDService>().InRequestScope();
            kernel.Bind<LessonPlanCUDService>().To<LessonPlanCUDService>().InRequestScope();
            kernel.Bind<LessonCUDService>().To<LessonCUDService>().InRequestScope();
            kernel.Bind<LessonUseCUDService>().To<LessonUseCUDService>().InRequestScope();
            kernel.Bind<MessageCUDService>().To<MessageCUDService>().InRequestScope();
            kernel.Bind<MessageUseCUDService>().To<MessageUseCUDService>().InRequestScope();
            kernel.Bind<NarrativeCUDService>().To<NarrativeCUDService>().InRequestScope();
            kernel.Bind<ReferenceCalendarCUDService>().To<ReferenceCalendarCUDService>().InRequestScope();
            kernel.Bind<TermCUDService>().To<TermCUDService>().InRequestScope();
            kernel.Bind<UserCUDService>().To<UserCUDService>().InRequestScope();
            kernel.Bind<StashedLessonPlanCUDService>().To<StashedLessonPlanCUDService>().InRequestScope();
            kernel.Bind<SharedCUDService>().To<SharedCUDService>().InRequestScope();
            kernel.Bind<StudentUserCUDService>().To<StudentUserCUDService>().InRequestScope();
            kernel.Bind<ReleasedDocumentCUDService>().To<ReleasedDocumentCUDService>().InRequestScope();
            kernel.Bind<CoursePreferenceCUDService>().To<CoursePreferenceCUDService>().InRequestScope();




            kernel.Bind<TermAdminViewObjBuilder>().To<TermAdminViewObjBuilder>().InRequestScope();


            kernel.Bind<ClassMeetingAdminCUDRouter>().To<ClassMeetingAdminCUDRouter>().InRequestScope();
            kernel.Bind<ClassroomCUDRouter>().To<ClassroomCUDRouter>().InRequestScope();
            kernel.Bind<ClassSectionAdminCUDRouter>().To<ClassSectionAdminCUDRouter>().InRequestScope();
            kernel.Bind<CourseCopyAdminCUDRouter>().To<CourseCopyAdminCUDRouter>().InRequestScope();
            kernel.Bind<CourseCreateAdminCUDRouter>().To<CourseCreateAdminCUDRouter>().InRequestScope();
            kernel.Bind<CurriculumCUDRouter>().To<CurriculumCUDRouter>().InRequestScope();
            kernel.Bind<DailyPlanningCUDRouter>().To<DailyPlanningCUDRouter>().InRequestScope();
            kernel.Bind<DocumentsTabCUDRouter>().To<DocumentsTabCUDRouter>().InRequestScope();
            kernel.Bind<LessonManagerCUDRouter>().To<LessonManagerCUDRouter>().InRequestScope();
            kernel.Bind<LessonPlanTabCUDRouter>().To<LessonPlanTabCUDRouter>().InRequestScope();
            kernel.Bind<MessageBoardTabCUDRouter>().To<MessageBoardTabCUDRouter>().InRequestScope();
            kernel.Bind<NarrativeTabCUDRouter>().To<NarrativeTabCUDRouter>().InRequestScope();
            kernel.Bind<TermAdminCUDRouter>().To<TermAdminCUDRouter>().InRequestScope();
            kernel.Bind<TextEditorCUDRouter>().To<TextEditorCUDRouter>().InRequestScope();
            kernel.Bind<UserAdminCUDRouter>().To<UserAdminCUDRouter>().InRequestScope();
            kernel.Bind<WorkingGroupAdminCUDRouter>().To<WorkingGroupAdminCUDRouter>().InRequestScope();
            kernel.Bind<CoursePreferenceAdminCUDRouter>().To<CoursePreferenceAdminCUDRouter>().InRequestScope();

            kernel.Bind<UserManagementService>().To<UserManagementService>().InRequestScope();








        }        
    }
}

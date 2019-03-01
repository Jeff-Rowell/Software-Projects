using CWMasterTeacherDomain;
using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDataModel.ObjectBuilders;
using CWMasterTeacherDataModel.Interfaces;
using System.Diagnostics;

namespace CWMasterTeacherService
{
    public class CurriculumViewObjBuilder
    {
        private ILessonDomainObjBuilder _lessonObjBuilder;
        private ICourseDomainObjBuilder _courseObjBuilder;
        private IUserDomainObjBuilder _userObjBuilder;
        private ITermDomainObjBuilder _termObjBuilder;

        public CurriculumViewObjBuilder (ILessonDomainObjBuilder lessonDomainObjBuilder, 
                                ICourseDomainObjBuilder courseDomainObjBuilder, IUserDomainObjBuilder userDomainObjBuilder,
                                ITermDomainObjBuilder termDomainObjBuilder)
        {
            Debug.Assert(lessonDomainObjBuilder != null);
            Debug.Assert(courseDomainObjBuilder != null);
            Debug.Assert(userDomainObjBuilder != null);
            Debug.Assert(termDomainObjBuilder != null);

            _lessonObjBuilder = lessonDomainObjBuilder;
            _courseObjBuilder = courseDomainObjBuilder;
            _userObjBuilder = userDomainObjBuilder;
            _termObjBuilder = termDomainObjBuilder;   
        }

        public CurriculumViewObjBuilder() { }

        public CurriculumViewObj RetrieveCurriculumViewObj(string currentUserName, Guid courseId, Guid lessonId,
                                                          bool showMasters, bool showAll, Guid displayMessageUseId, bool showArchivedMessages,
                                                        Guid selectedDocumentUseId, Guid selectedDocumentId, bool showUploadDialog,
                                                        bool showArchivedDocs, int showThisManyDocs, bool isPdfCopyUpload, string fileValidationMessage,
                                                        bool showGroupDocStorage)
        {

            //First we get our user
            UserDomainObj userObj = _userObjBuilder.BuildFromUserName(currentUserName);
            //Then create our ViewObj
            CurriculumViewObj viewObj = new CurriculumViewObj();

            //First we set our values for courseObj and lessonObj
            Tuple<CourseDomainObj,LessonDomainObjBasic> courseTuple = GetCourseObj_LessonObj(courseId, lessonId, userObj);
            CourseDomainObj courseObj = courseTuple.Item1 ;
            LessonDomainObjBasic lessonObj = courseTuple.Item2;

            //Now set the values in the vieObj.
            viewObj.CourseObj = courseObj == null ? null : courseObj;
            viewObj.LessonObj = lessonObj == null ? null : lessonObj;
            viewObj.UserObj = userObj;

            if (courseObj != null )
            {
                courseObj.SelectedLessonId = lessonObj == null ? Guid.Empty : lessonObj.Id;
                viewObj.CourseObj.CourseDomainObjBasic.SelectedCourseId = courseId; //We need this to set CSS booleans.
            }

            //Now we set the individual values in the View Object
            if (lessonObj != null && lessonObj.IsMaster)
            {
                viewObj.ReturnLessonId = _lessonObjBuilder.ReturnLessonIdForMasterLesson(lessonObj.Id, userObj.Id);
            }

            viewObj.CurrentTermObj = _termObjBuilder.CurrentTerm() == null ? null : _termObjBuilder.CurrentTerm();
            viewObj.CourseObjList = _courseObjBuilder.GetDisplayCourseListForUser(userObj.Id) == null ? null : _courseObjBuilder.GetDisplayCourseListForUser(userObj.Id);

            //Now the pass-throughs
            viewObj.DisplayMessageUseId = displayMessageUseId;
            viewObj.ShowArchivedMessages = showArchivedMessages;
            viewObj.SelectedDocumentUseId = selectedDocumentUseId;
            viewObj.SelectedDocumentId = selectedDocumentId;
            viewObj.ShowUploadDialog = showUploadDialog;
            viewObj.ShowArchivedDocs = showArchivedDocs;
            viewObj.ShowThisManyDocs = showThisManyDocs;
            viewObj.IsPdfCopyUpload = isPdfCopyUpload;
            viewObj.FileValidationMessage = fileValidationMessage;
            viewObj.ShowGroupDocStorage = showGroupDocStorage;


            return viewObj;
        }

        private Tuple<CourseDomainObj, LessonDomainObjBasic> GetCourseObj_LessonObj(Guid courseId, Guid lessonId, UserDomainObj userObj)
        {
            CourseDomainObj courseObj = null;
            LessonDomainObjBasic lessonObj = null;
            //Then we set user, course, and lesson from the Ids
            if (lessonId != Guid.Empty)
            {
                lessonObj = _lessonObjBuilder.BuildBasicFromId(lessonId);
            }
            else if (courseId != Guid.Empty)
            {
                courseObj = _courseObjBuilder.BuildFromId(courseId);
            }
            else if (userObj.LastDisplayedCourseId != Guid.Empty)//If we didn't come in with a course or lesson, set to user's last displayed course
            {
                courseObj = _courseObjBuilder.BuildFromId(userObj.LastDisplayedCourseId);
            }


            if (lessonId == Guid.Empty && courseObj != null)//If we didn't get passed a selected lesson, let's try to set it to the last displayed lesson for the course
            {
                if (courseObj.LastDisplayedLessonId != Guid.Empty)
                {
                    lessonId = courseObj.LastDisplayedLessonId;
                    lessonObj = _lessonObjBuilder.BuildBasicFromId(lessonId);
                }
            }

            if (lessonObj != null)
            {
                if (courseObj == null)
                {
                    courseObj = _courseObjBuilder.BuildFromId(lessonObj.CourseId);
                }
                if(courseObj.LastDisplayedLessonId != lessonObj.Id)
                {
                    _courseObjBuilder.SetLastDisplayedLessonId(lessonObj.Id);
                }
            }
            else if (courseObj != null)
            {
                _userObjBuilder.SetLastDisplayedCourseId(userObj.Id, courseObj.Id);
            }

            if (userObj != null && courseObj != null && userObj.LastDisplayedCourseId != courseObj.Id)
            {

                _userObjBuilder.SetLastDisplayedCourseId(userObj.Id, courseObj.Id);

            }

            return new Tuple<CourseDomainObj, LessonDomainObjBasic>(courseObj, lessonObj);
        }

        public Guid GetToggleMasterEditReturnLessonId(bool isMasterEdit, Guid selectedLessonId, Guid returnLessonId)
        {
            if (isMasterEdit && selectedLessonId != Guid.Empty)
            {
                LessonDomainObj lesson = _lessonObjBuilder.BuildFromId(selectedLessonId);
                Guid lessonId = Guid.Empty;
                if (lesson.MasterLessonId != Guid.Empty)
                {
                    lessonId = lesson.MasterLessonId;
                }
                else
                {
                    lessonId = selectedLessonId;
                }
                return lessonId;
            }
            else if (returnLessonId != Guid.Empty)
            {
                return returnLessonId;
            }
            else
            {
                return selectedLessonId;
            }
        }


        




    }//End Class
}

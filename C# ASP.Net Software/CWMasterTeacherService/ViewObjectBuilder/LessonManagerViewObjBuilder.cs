using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDomain.ViewObjects;
using CWMasterTeacherDomain.DomainObjects;
using CWMasterTeacherDataModel.Interfaces;
using CWMasterTeacherDomain;
using System.Web.Mvc;

namespace CWMasterTeacherService.ViewObjectBuilder
{
    public class LessonManagerViewObjBuilder
    {
        ICourseDomainObjBuilder _courseObjBuilder;
        ILessonDomainObjBuilder _lessonObjBuilder;
        IUserDomainObjBuilder _userObjBuilder;

        public LessonManagerViewObjBuilder(ICourseDomainObjBuilder courseObjBuilder, ILessonDomainObjBuilder lessonObjBuilder,
                                    IUserDomainObjBuilder userObjBuilder)
        {
            _courseObjBuilder = courseObjBuilder;
            _lessonObjBuilder = lessonObjBuilder;
            _userObjBuilder = userObjBuilder;
        }

        public LessonManagerViewObj RetrieveLessonManagerViewObject(Guid courseId, Guid selectedLessonId, Guid parentId,
                                            string lessonName, bool isCreate,
                                            bool isEdit, bool isArchive, bool isDelete, bool showSelectLocation,
                                            bool parentIsCourse, bool isMove, Guid currentUserId, bool showConfirmDelete,
                                            string deletionMessage)
        {
            LessonManagerViewObj viewObj = new LessonManagerViewObj();
            viewObj.CurrentUserObj = _userObjBuilder.BuildFromId(currentUserId);

            if (selectedLessonId != Guid.Empty)
            {
                viewObj.LessonObj = _lessonObjBuilder.BuildFromId(selectedLessonId);

                if (courseId == Guid.Empty)
                {
                    if (viewObj.LessonObj.CourseId != Guid.Empty)
                    {
                        courseId = viewObj.LessonObj.CourseId;
                    }
                }

                if (isCreate)
                {
                    if (parentId == Guid.Empty)
                    {
                        parentId = viewObj.LessonObj.Id;
                    }
                }
                else if (parentId == Guid.Empty && viewObj.LessonObj != null)
                {
                    if (viewObj.LessonObj.ContainerLessonId != null)
                    {
                        parentId = viewObj.LessonObj.ContainerLessonId.Value;
                    }
                    else
                    {
                        parentIsCourse = true;
                    }
                }

                if (parentId != Guid.Empty)
                {
                    viewObj.ParentLessonObj = _lessonObjBuilder.BuildFromId(parentId);
                }

                if (!isCreate)//So this is an edit
                {
                   viewObj.LessonName = viewObj.LessonObj.Name;
                }
            }


            if (courseId != Guid.Empty)
            {
                CourseDomainObj courseObj = _courseObjBuilder.BuildFromId(courseId);
                if( courseObj!= null)
                {
                    viewObj.CourseObj = courseObj;
                    viewObj.CourseObj.IsForLessonManager = true;
                    viewObj.CourseObj.SelectedLessonId = selectedLessonId;
                }
            }


            if (parentId != Guid.Empty && !parentIsCourse) //Which means parent will be a lesson
            {
                LessonDomainObj parent = _lessonObjBuilder.BuildFromId(parentId);
                int siblingCount = _lessonObjBuilder.SiblingCountForLesson(parentId);
                viewObj.SequenceNumberSelectList = Enumerable.Range(1, siblingCount + 1).Select(x => new SelectListItem { Text = x.ToString() });
                viewObj.ParentName = parent.DisplayName;
            }
            else if (courseId != Guid.Empty)
            {
                int siblingCount = viewObj.CourseObj.ContainerChildLessons.Count();
                viewObj.SequenceNumberSelectList = Enumerable.Range(1, siblingCount + 1).Select(x => new SelectListItem { Text = x.ToString() });
                viewObj.ParentName = viewObj.CourseObj.DisplayName;
                viewObj.ParentIsCourse = true;
                parentId = Guid.Empty;
            }

            viewObj.SelectedLessonId = selectedLessonId;

            if(lessonName.Length > 3)
            {
                viewObj.LessonName = lessonName;
            }
            
            viewObj.ParentId = parentId;
            //viewObj.LessonLevelId = lessonLevelId;
            viewObj.ShowSelectLocation = showSelectLocation;

            viewObj.IsCreate = isCreate;
            viewObj.IsEdit = isEdit;
            viewObj.IsMove = isMove;
            viewObj.IsArchive = isArchive;
            viewObj.IsDelete = isDelete;
            viewObj.ShowConfirmDelete = showConfirmDelete;
            viewObj.DeletionMessage = deletionMessage;




            return viewObj;
        }



    }
}

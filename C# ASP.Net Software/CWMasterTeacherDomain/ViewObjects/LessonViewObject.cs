using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.ViewObjects
{
    class LessonViewObject
    {
        private LessonDomainObj _lessonObj;

        public LessonViewObject(LessonDomainObj lessonObj)
        {
            _lessonObj = lessonObj;
        }

        public Guid LessonId
        {
            get
            {
                return _lessonObj.Id;
            }
        }



    }
}

using CWMasterTeacherDataModel;
using CWMasterTeacherDomain.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.DomainObjectBuilders
{
    public class MetaCourseDomainObjBuilder
    {
        private MetaCourseRepo _metaCourseRepo;

        public MetaCourseDomainObjBuilder(MetaCourseRepo metaCourseRepo)
        {
            _metaCourseRepo = metaCourseRepo;
        }

        public static MetaCourseDomainObjBasic BuildBasic(MetaCourse metaCourse)
        {
            MetaCourseDomainObjBasic basicObj = new MetaCourseDomainObjBasic();
            basicObj.Id = metaCourse.Id;
            basicObj.Name = metaCourse.Name;
            return basicObj;
        }

        public MetaCourseDomainObjBasic BuildBasicFromId(Guid metaCourseId)
        {
            MetaCourse metaCourse = _metaCourseRepo.GetById(metaCourseId);
            return metaCourse == null ? null : BuildBasic(metaCourse);
        }


    }
}

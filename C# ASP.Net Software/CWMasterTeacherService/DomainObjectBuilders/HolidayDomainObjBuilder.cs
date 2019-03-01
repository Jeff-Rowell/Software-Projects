using System;
using CWMasterTeacherDomain.DomainObjects;

namespace CWMasterTeacherDataModel.ObjectBuilders
{
    /// <summary>
    /// Builds Holiday Domain Objects out of DB Objects
    /// </summary>
    public class HolidayDomainObjBuilder : Interfaces.IDomainObjBuilder<HolidayDomainObj, HolidayDomainObjBasic, Holiday>
    {      
        public HolidayRepo _holidayRepo;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public HolidayDomainObjBuilder()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="holidayRepo">A repository containing the Holiday Data</param>
        public HolidayDomainObjBuilder(HolidayRepo holidayRepo)
        {
            _holidayRepo = holidayRepo;
        }

        /// <summary>
        /// Builds a Holiday Domain Object out of an equiavalent DBObject
        /// </summary>
        /// <param name="dbObj">Holiday Database Object</param>
        /// <returns></returns>
        public static HolidayDomainObj Build(IEntity dbObj)
        {
            Holiday holiday = (Holiday) dbObj;
            HolidayDomainObjBasic holidayBasic = BuildBasic(dbObj);
            HolidayDomainObj holidayDomainObj = new HolidayDomainObj(holidayBasic);

            holidayDomainObj.Id = holiday.Id;
            holidayDomainObj.TermId = holiday.TermId;
            holidayDomainObj.Name = holiday.Name;
            holidayDomainObj.Date = holiday.Date;


            return holidayDomainObj;
        }

        /// <summary>
        /// Builds a Holiday Domain Object by id
        /// </summary>
        /// <param name="id">The id of the Holiday</param>
        /// <returns></returns>
        public virtual HolidayDomainObj BuildFromId(Guid id)
        {
            Holiday holiday = _holidayRepo.GetById(id);

            return Build(holiday);
        }

        /// <summary>
        /// Builds a Basic Holiday Domain Object out of a Holiday db Object
        /// </summary>
        /// <param name="dbObj">Holiday db Object</param>
        /// <returns></returns>
        public static HolidayDomainObjBasic BuildBasic(IEntity dbObj)
        {
            Holiday holiday = (Holiday) dbObj;
            Guid id = holiday.Id;
            string name = holiday.Name;
            
            HolidayDomainObjBasic holidayDomainObjBasic = new HolidayDomainObjBasic(id, name);

            return holidayDomainObjBasic;
        }

        /// <summary>
        /// Builds a Basic Holiday Object out of it's id
        /// </summary>
        /// <param name="id">ID of the Holiday Object</param>
        /// <returns></returns>
        public virtual HolidayDomainObjBasic BuildBasicFromId(Guid id)
        {
            Holiday holiday = _holidayRepo.GetById(id);
            return BuildBasic(holiday);
        }


    }//End Class
}


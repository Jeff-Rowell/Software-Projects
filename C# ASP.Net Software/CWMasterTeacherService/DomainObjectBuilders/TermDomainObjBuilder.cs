using System;
using CWMasterTeacherDomain.DomainObjects;
using System.Collections.Generic;
using System.Linq;
using CWMasterTeacherDataModel.Interfaces;

namespace CWMasterTeacherDataModel.ObjectBuilders
{
    /// <summary>
    /// Builds Term Domain Objects out of DB Objects
    /// </summary>
    public class TermDomainObjBuilder : ITermDomainObjBuilder
    { 
        public TermRepo _termRepo;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TermDomainObjBuilder()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="termRepo">A repository containing the Term Data</param>
        public TermDomainObjBuilder(TermRepo termRepo)
        {
            _termRepo = termRepo;
        }

        /// <summary>
        /// Builds a Term Domain Object out of an equiavalent DBObject
        /// </summary>
        /// <param name="dbObj">Term Database Object</param>
        /// <returns></returns>
        public static TermDomainObj Build(IEntity dbObj)
        {
            Term term = (Term)dbObj;
            TermDomainObjBasic termBasic = BuildBasic(dbObj);
            TermDomainObj termDomainObj = new TermDomainObj(termBasic);
            termDomainObj.Courses = null;
            termDomainObj.EndDate = term.EndDate;
            List<Holiday> Holidays = term.Holidays.ToList();
            List<HolidayDomainObj> holidaysDomain = new List<HolidayDomainObj>();

            foreach (Holiday holiday in Holidays)
            {
                HolidayDomainObj holidayDomain = HolidayDomainObjBuilder.Build(holiday);

                holidaysDomain.Add(holidayDomain);
            }

            termDomainObj.Holidays = holidaysDomain;
            termDomainObj.Id = term.Id;
            termDomainObj.WorkingGroupId = term.WorkingGroup.Id;
            termDomainObj.IsCurrent = term.IsCurrent;
            termDomainObj.Name = term.Name;
            termDomainObj.StartDate = term.StartDate;
            termDomainObj.TermId = term.Id;

            return termDomainObj;
        }

        /// <summary>
        /// Builds a Term Domain Object by id
        /// </summary>
        /// <param name="id">The id of the Term</param>
        /// <returns></returns>
        public TermDomainObj BuildFromId(Guid id)
        {
            Term term = _termRepo.GetById(id);

            return Build(term);
        }

        

        /// <summary>
        /// Builds a Basic Term Domain Object out of a Term db Object
        /// </summary>
        /// <param name="dbObj">Term db Object</param>
        /// <returns></returns>
        public static TermDomainObjBasic BuildBasic(IEntity dbObj)
        {
            Term term = (Term)dbObj;
            Guid id = term.Id;
            string name = term.Name;
            TermDomainObjBasic termDomainObjBasic = new TermDomainObjBasic(id, name);

            return termDomainObjBasic;
        }

        public static TermDomainObj BuildWithCourseList(Term term, List<CourseDomainObjBasic> courseList)
        {
            TermDomainObj termObj = Build(term);
            termObj.CourseList = courseList;
            return termObj;
        }

        public static TermDomainObj BuildWithUserList(Term term, List<UserDomainObjBasic> userList)
        {
            TermDomainObj termObj = Build(term);
            termObj.UserList = userList;
            return termObj;
        }

        /// <summary>
        /// Builds a Basic Term Object out of it's id
        /// </summary>
        /// <param name="id">ID of the Term Object</param>
        /// <returns></returns>
        public virtual TermDomainObjBasic BuildBasicFromId(Guid id)
        {
            Term term = _termRepo.GetById(id);

            return BuildBasic(term);
        }

        /// <summary>
        /// Builds a list of terms that are associated with the provided workingGroup id.
        /// </summary>
        /// <param name="workingGroupId">The id of the workingGroup for which all terms will be built.</param>
        /// <returns>A list of TermDomainObj that are associated with the workingGroup id.</returns>
        public List<TermDomainObj> GetTermsForWorkingGroupList(Guid workingGroupId)
        {
            IEnumerable<Term> termsForWorkingGroupList = _termRepo.TermsForWorkingGroup(workingGroupId);
            return termsForWorkingGroupList == null ? new List<TermDomainObj>() : termsForWorkingGroupList.Select(x => Build(x)).ToList();
        }

        public TermDomainObj CurrentTerm()
        {
            Term term = _termRepo.CurrentTerm;
            return term == null ? null : Build(term);
        }

        public List<TermDomainObjBasic> TermsForWorkingGroup(Guid currentWorkingGroupId)
        {
            List<Term> termList = _termRepo.TermsForWorkingGroup(currentWorkingGroupId).ToList();
            return termList == null ? new List<TermDomainObjBasic>() : termList.Select(x => BuildBasic(x)).ToList();
        }

        public List<TermDomainObjBasic> TermsThatHaveNotEndedYet()
        {
            List<Term> termList = _termRepo.TermsThatHaveNotEndedYet();
            return termList == null ? new List<TermDomainObjBasic>() : termList.Select(x => BuildBasic(x)).ToList();
        }


    }//End class
}
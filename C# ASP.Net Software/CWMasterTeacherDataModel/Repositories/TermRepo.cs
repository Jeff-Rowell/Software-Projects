using CWMasterTeacherDataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class TermRepo: BaseRepo<Term>, ITermRepo
    {
        public TermRepo(MasterTeacherContext context)
            : base(context)
        { }

        public IEnumerable<Term> TermsForWorkingGroup(Guid workingGroupId)
        {
            var listQuery = from x in _context.Terms
                                             where x.WorkingGroupId == workingGroupId
                                             orderby x.StartDate descending 
                                             select x;
            return listQuery.ToList();                                      
        }

        public Term CurrentTerm
        {
            get
            {
                var termQuery = from x in _context.Terms
                                where x.IsCurrent 
                                orderby x.EndDate descending
                                select x;

                var termList = termQuery.ToList();
                if (termList.Count > 0)
                {
                    return termList.First();
                }
                else
                {
                    var termQuery2 = from x in _context.Terms
                                     orderby x.EndDate descending
                                     select x;

                    return termQuery2.FirstOrDefault();
                }
            }
        }

        public IEnumerable<Term> AllCurrentTermsForWorkingGroup(Guid workingGroupId)
        {
            var listQuery = from x in _context.Terms
                            where x.WorkingGroupId == workingGroupId && x.IsCurrent 
                            orderby x.StartDate descending
                            select x;
            return listQuery.ToList(); 
        }

        public Term CreateTerm(Guid workingGroupId, string name, DateTime startDate, DateTime endDate)
        {
            Term term = new Term();
            term.WorkingGroupId = workingGroupId;
            term.Name = name;
            term.StartDate = startDate;
            term.EndDate = endDate;
            term.IsCurrent = false;

            Insert(term);

            return term;
        }

        public List<Term> TermsThatHaveNotEndedYet()
        {
            var query = from x in _context.Terms
                        where x.EndDate > DateTime.Now
                        select x;
            return query.ToList();
        }

        
    }//End class
}

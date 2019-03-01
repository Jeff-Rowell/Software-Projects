using CWMasterTeacherDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherService.CUDServices
{
    public class TermCUDService
    {
        TermRepo _termRepo;

        public TermCUDService (TermRepo termRepo)
        {
            _termRepo = termRepo;
        }

        public Guid CreateTermReturnId(Guid workingGroupId, string name, DateTime startDate, DateTime endDate)
        {
            Term term = new Term();
            term.WorkingGroupId = workingGroupId;
            term.Name = name;
            term.StartDate = startDate;
            term.EndDate = endDate;
            term.IsCurrent = false;

            _termRepo.Insert(term);

            return term.Id;
        }


        public Guid UpdateTermValuesReturnId(Guid termId, string name, DateTime startDate, DateTime endDate)
        {
            Term term = _termRepo.GetById(termId);
            term.Name = name;
            term.StartDate = startDate;
            term.EndDate = endDate;
            term.IsCurrent = false;

            _termRepo.Update(term);

            return term.Id;
        }

        public void MarkTermAsCurrent(Guid termId)
        {
            Term currentTerm = _termRepo.GetById(termId);
            List<Term> termList = _termRepo.AllCurrentTermsForWorkingGroup(currentTerm.WorkingGroupId).ToList();

            foreach (var xTerm in termList)
            {
                xTerm.IsCurrent = false;
                _termRepo.Update(xTerm);
            }

            currentTerm.IsCurrent = true;
            _termRepo.Update(currentTerm);

        }
    }//End Class
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWMasterTeacherDataModel.Interfaces;
using Microsoft.AspNet.Identity;
using WebMatrix.WebData;

namespace CWMasterTeacherDataModel
{
    public class UserRepo: BaseRepo<User>, IUserRepo
    {
        private WorkingGroupRepo _workingGroupRepo;

        public UserRepo(MasterTeacherContext context, WorkingGroupRepo workingGroupRepo)
            : base(context)
        {
            _workingGroupRepo = workingGroupRepo;
        }

        public IEnumerable<User> ApprovedUsersForWorkingGroup(Guid workingGroupId)
        {
            var listQuery = from x in _context.Users
                                             where x.WorkingGroupId == workingGroupId && x.HasAdminApproval 
                                             orderby x.LastName
                                             select x;
            return listQuery.ToList();                                      
        }

        public IEnumerable<User> AllUsersForWorkingGroup(Guid workingGroupId)
        {
            var listQuery = from x in _context.Users
                            where x.WorkingGroupId == workingGroupId
                            orderby x.LastName
                            select x;
            return listQuery.ToList();
        }

        public User UserByUserName(string userName)
        {
            var listQuery = from x in _context.Users
                            where x.UserName == userName
                            select x;
            return listQuery.FirstOrDefault();
        }

       

    }
}

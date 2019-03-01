using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class StudentUser: IEntity 
    {
        public StudentUser()
        {
            ClassSectionStudents = new HashSet<ClassSectionStudent>();
            WorkingGroupStudents = new HashSet<WorkingGroupStudent>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual ICollection<ClassSectionStudent> ClassSectionStudents { get; set; }

        public virtual ICollection<WorkingGroupStudent> WorkingGroupStudents { get; set; }

    }
}

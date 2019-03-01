using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDataModel
{
    public class CoursePreference :IEntity
    {
        /// <summary>
        /// This is the class that will hold preferences that need to be displayed at the course level.
        /// Such as whether to display notifications about changes.
        /// </summary>
        public CoursePreference()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool DoShowNarrativeNotifications { get; set; }

        public bool DoShowLessonPlanNotifications { get; set; }

        public bool DoShowDocumentNotifications { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        //Anything added here needs to also be added to CoursePreference Create and CoursePreference Copy.
    }
}

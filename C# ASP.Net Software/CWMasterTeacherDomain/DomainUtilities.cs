using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CWMasterTeacherDomain
{
    public class DomainUtilities
    {
        private static int _narrativeStashMaxSize = 3;
        private static int _lessonPlanStashMaxSize = 3;




        /// <summary>
        /// Converts the given time from local time to UTC time, returning a new DateTime.
        /// </summary>
        /// <param name="dateTime">
        ///     DateTime to be converted.
        /// </param>
        /// <returns>
        ///     DateTime the given time in UTC time.
        /// </returns>
        public static DateTime ConvertLocalTimeToUtc(DateTime dateTime)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTimeToUtc(dateTime, timeZoneInfo);
        }

        /// <summary>
        /// Converts the given time from UTC time to local time, returning a new DateTime.
        /// </summary>
        /// <param name="dateTime">
        ///     DateTime to be converted.
        /// </param>
        /// <returns>
        ///     DateTime the given time in local time.
        /// </returns>
        public static DateTime ConvertUtcToLocalTime(DateTime dateTime)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZoneInfo);
        }


        /// <summary>
        /// This is used to test time dependent functionality.  We can come here and make it be a different time.
        /// </summary>
        public static DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        public static int NarrativeStashMaxSize
        {
            get { return _narrativeStashMaxSize; }
        }

        public static int LessonPlanStashMaxSize
        {
            get { return _lessonPlanStashMaxSize; }
        }

        public class LessonForComparisonDomainObjComparer : IEqualityComparer<LessonForComparisonDomainObj>
        {
            public bool Equals(LessonForComparisonDomainObj obj1, LessonForComparisonDomainObj obj2)
            {
                return obj1.Id == obj2.Id;
            }

            public int GetHashCode(LessonForComparisonDomainObj obj)
            {
                Guid objId = obj.Id;
                return objId.GetHashCode();

            }
        }

        public class DomainObjComparer : IEqualityComparer<IDomainObj>
        {
            public bool Equals(IDomainObj obj1, IDomainObj obj2)
            {
                return obj1.Id == obj2.Id;
            }

            public int GetHashCode(IDomainObj obj)
            {
                Guid objId = obj.Id;
                return objId.GetHashCode();

            }
        }

        public enum ComparisonMode
        {
            None = 0,
            ShowComparisonLessons = 1,
            ViewArchive = 2,
            ImportContent = 3,
            ImportLesson = 4,
            UpdateIndivFromMaster = 5,
            UpdateMasterFromIndiv = 6
        }

        public static int ComparisonModeGetIntFromEnum(ComparisonMode mode)
        {
            return (int)mode;
        }

        public static ComparisonMode ComparisonModeGetEnumFromInt(int value)
        {
            if(Enum.IsDefined(typeof(ComparisonMode), value))
            {
                return (ComparisonMode)value;
            }
            else
            {
                return ComparisonMode.None;
            }
        }

        public static bool CompModeIsShowComparisonLessons(int compModeInt)
        {
            return (int)ComparisonMode.ShowComparisonLessons == compModeInt;
        }
        public static bool CompModeIsViewArchive(int compModeInt)
        {
            return (int)ComparisonMode.ViewArchive  == compModeInt;
        }
        public static bool CompModeIsImportContent(int compModeInt)
        {
            return (int)ComparisonMode.ImportContent == compModeInt;
        }
        public static bool CompModeIsImportLesson(int compModeInt)
        {
            return (int)ComparisonMode.ImportLesson == compModeInt;
        }
        public static bool CompModeIsUpdateFromMaster(int compModeInt)
        {
            return (int)ComparisonMode.UpdateIndivFromMaster == compModeInt;
        }
        public static bool CompModeIsUpdateFromGroup(int compModeInt)
        {
            return (int)ComparisonMode.UpdateMasterFromIndiv == compModeInt;
        }

        public static bool Date1_IsGreater(DateTime date1, DateTime date2)
        {
            TimeSpan diff = TimeSpan.FromSeconds(2);
            return date1 > new DateTime(1000, 01, 01) && date1.Subtract(date2) > diff;
        }

    }
}

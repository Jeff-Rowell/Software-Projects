using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWMasterTeacherDomain.ViewObjects
{
    public class DateViewObj
    {
        private DateTime _date;
        private bool _isSelected;

        public DateViewObj(DateTime date)
        {
            _date = date;
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }



        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }


        public string LongDateString
        {
            get { return DomainWebUtilities.DateTime_ToLongDateString(_date); }

        }


        public string DateString
        {
            get { return DomainWebUtilities.DateTime_ToDateString(_date); }
        }

        public string DisplayClass
        {
            get
            {
                return DomainWebUtilities.ListSelectedClass(IsSelected, true);
            }
        }
    }
}

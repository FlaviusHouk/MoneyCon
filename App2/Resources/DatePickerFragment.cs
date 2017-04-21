using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App2.Resources
{
    public class DatePickerFragment : DialogFragment,
                                    DatePickerDialog.IOnDateSetListener
    {
        public static readonly string TAG = "Вибір дати";
        Action<DateTime> _dateSelectedHandler = delegate { };
        public static DatePickerFragment NewInstanse(Action<DateTime> returningDate)
        {
            DatePickerFragment _frag = new DatePickerFragment();
            _frag._dateSelectedHandler = returningDate;
            return _frag;
        }
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime now = DateTime.Now;
            DatePickerDialog dial = new DatePickerDialog(Activity, this, now.Year, now.Month - 1, now.Day);
            return dial;
        }
        public void OnDateSet(DatePicker obj, int year, int month, int day)
        {
            DateTime chosen = new DateTime(year, month + 1, day);
            _dateSelectedHandler(chosen);
        }
    }
}
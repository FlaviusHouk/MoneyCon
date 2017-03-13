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
    [Activity(Label = "LookForPeriodRequest")]
    public class LookForPeriodRequest : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FindForPeriodRequest);
            Button ReqBut = (Button)FindViewById(Resource.Id.FindForPerBut);
            ReqBut.Click += GetRequested;
        }

        private void GetRequested(object e, EventArgs args)
        {
            Intent intent = new Intent(this, typeof(LookForPeriodRequested));
            EditText InputField = (EditText)FindViewById(Resource.Id.YearForPeriod_1);
            intent.PutExtra("BYear", InputField.Text);
            InputField = (EditText)FindViewById(Resource.Id.MonthForPeriod_1);
            intent.PutExtra("BMonth", InputField.Text);
            InputField = (EditText)FindViewById(Resource.Id.DayForPeriod_1);
            intent.PutExtra("BDay", InputField.Text);
            InputField = (EditText)(EditText)FindViewById(Resource.Id.YearForPeriod_2);
            intent.PutExtra("EYear", InputField.Text);
            InputField = (EditText)FindViewById(Resource.Id.MonthForPeriod_2);
            intent.PutExtra("EMonth", InputField.Text);
            InputField = (EditText)FindViewById(Resource.Id.DayForPeriod_2);
            intent.PutExtra("EDay", InputField.Text);
            StartActivity(intent);
        }
    }
}
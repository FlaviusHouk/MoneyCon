using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Graphics;
using Android.Widget;

namespace App2.Resources
{
    [Activity(Label = "LookForPeriodRequest")]
    public class LookForPeriodRequest : Activity
    {
        Typeface boldFont;
        Typeface mediumFont;
        DateTime start, end;
        DatePickerFragment startPicker, endPicker;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            boldFont = boldFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Bold.otf");
            mediumFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Medium.otf");
            startPicker = DatePickerFragment.NewInstanse(SetStartData);
            endPicker = DatePickerFragment.NewInstanse(SetEndDeta);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FindForPeriodRequest);
            Button ReqBut = (Button)FindViewById(Resource.Id.FindForPerBut);
            ReqBut.Click += GetRequested;
            FindViewById<TextView>(Resource.Id.TextViev1_LookForDay).Typeface = boldFont;
            FindViewById<TextView>(Resource.Id.LookForPeriod_SubHeader1).Typeface = mediumFont;
            FindViewById<TextView>(Resource.Id.LookForPeriod_SubHeader2).Typeface = mediumFont;
            FindViewById<Button>(Resource.Id.FirstDataBut).Click += ShowStartPicker;
            FindViewById<Button>(Resource.Id.SecondDataBut).Click += ShowEndPicker;
        }

        private void ShowStartPicker(object sender, EventArgs e)
        {
            startPicker.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void ShowEndPicker(object sender, EventArgs e)
        {
            endPicker.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void SetStartData(DateTime date)
        {
            start = date;
            TextView field = FindViewById<TextView>(Resource.Id.FindForPeriod_StartDate);
            field.Text = start.ToShortDateString();
            field.Typeface = mediumFont;
        }

        private void SetEndDeta(DateTime date)
        {
            end = date;
            TextView field = FindViewById<TextView>(Resource.Id.FindForPeriod_EndDate);
            field.Text = end.ToShortDateString();
            field.Typeface = mediumFont;
        }

        private void GetRequested(object e, EventArgs args)
        {
            Intent intent = new Intent(this, typeof(LookForPeriodRequested));
            intent.PutExtra("BYear", start.Year.ToString());
            intent.PutExtra("BMonth", (start.Month + 1).ToString());
            intent.PutExtra("BDay", start.Day.ToString());
            intent.PutExtra("EYear", end.Year.ToString());
            intent.PutExtra("EMonth", (end.Month + 1).ToString());
            intent.PutExtra("EDay", end.Day.ToString());
            StartActivity(intent);
        }
    }
}
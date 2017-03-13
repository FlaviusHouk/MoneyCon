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
    [Activity(Label = "FindForDayRequest")]
    public class FindForDayRequest : Activity
    {
        DateTime date;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DayAct);
            Button ReqBut = (Button)FindViewById(Resource.Id.FindForDayBut);
            ReqBut.Click += GiveRequested;
        }

        private void GiveRequested(object e, EventArgs args)
        {
            EditText Year = (EditText)FindViewById(Resource.Id.YearForDay);
            EditText Month = (EditText)FindViewById(Resource.Id.MonthForDay);
            EditText Day = (EditText)FindViewById(Resource.Id.DayForDay);
            try
            {
                date = new DateTime(int.Parse(Year.Text), int.Parse(Month.Text), int.Parse(Day.Text));
            }
            catch
            {
                AlertDialog.Builder errMsg = new AlertDialog.Builder(this);
                errMsg.SetTitle("Ой...");
                errMsg.SetMessage("Ви помилилися з введеням дати. Будь-ласка перевірте дані й спробуйте знову");
                errMsg.SetPositiveButton("OK", (senderAlert, ar) => { });
                errMsg.SetCancelable(true);
                errMsg.Create().Show();
                return;
            }
            Intent intent = new Intent(this, typeof(FindForDayRequested));
            intent.PutExtra("Year", Year.Text);
            intent.PutExtra("Month", Month.Text);
            intent.PutExtra("Day", Day.Text);
            StartActivity(intent);
        }
    }
}
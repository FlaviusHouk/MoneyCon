using System;
using System.Collections.Generic;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App2.Resources
{
    [Activity(Label = "LookForPeriodRequested")]
    public class LookForPeriodRequested : Activity
    {
        private DateTime BDate;
        private DateTime EDate;
        Typeface boldFont;
        Typeface mediumFont;
        Typeface lightFont;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            boldFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Bold.otf");
            mediumFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Medium.otf");
            lightFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Light.otf");
            base.OnCreate(savedInstanceState);
            string Year = Intent.GetStringExtra("BYear");
            string Month = Intent.GetStringExtra("BMonth");
            string Day = Intent.GetStringExtra("BDay");
            SetContentView(Resource.Layout.FindForPeriodRequested);
            BDate = new DateTime(int.Parse(Year), int.Parse(Month) - 1, int.Parse(Day));       
            Year = Intent.GetStringExtra("EYear");
            Month = Intent.GetStringExtra("EMonth");
            Day = Intent.GetStringExtra("EDay");
            EDate = new DateTime(int.Parse(Year), int.Parse(Month), int.Parse(Day));
            Action<string, string> LookForDay_out = DrawRows;
            TextView Header = (TextView)FindViewById(Resource.Id.TextViev1_LookForPer_Req);
            Header.Text = "Витрати за період з " + BDate.ToShortDateString() + " по " + EDate.ToShortDateString();
            Header.Typeface = boldFont;
            TextView sumView = (TextView)FindViewById(Resource.Id.TextViev1_LookForPer_Sum);
            sumView.Typeface = lightFont;
            sumView.Text = "Витрати за цей період становлять: " + DataBase.PerSum(BDate,EDate,LookForDay_out).ToString() + " грн";
        }

        private void DrawRows(string description, string price)
        {
            TableLayout Info = (TableLayout)FindViewById(Resource.Id.Table_Per);
            LayoutInflater inflator = LayoutInflater.From(this);
            if (Info.GetChildAt(0) != null)
            {
                LayoutInflater sep = LayoutInflater.From(this);
                TableRow rowSep = (TableRow)sep.Inflate(Resource.Layout.separator, null);
                Info.AddView(rowSep);
            }
            TableRow row = (TableRow)inflator.Inflate(Resource.Layout.TwoTextTemplate, null);
            TextView FirstCol = (TextView)row.FindViewById(Resource.Id.descCol);
            TextView SecondCol = (TextView)row.FindViewById(Resource.Id.priceCol);
            FirstCol.Text = description;
            SecondCol.Text = price;
            FirstCol.Typeface = lightFont;
            SecondCol.Typeface = lightFont;
            if ((Info.ChildCount) % 4 == 0)
            {
                Android.Graphics.Drawables.ColorDrawable back = new Android.Graphics.Drawables.ColorDrawable(Color.White);
                back.Alpha = 128;
                row.Background = back;
            }
            else if ((Info.ChildCount) % 4 == 2)
            {
                Android.Graphics.Drawables.ColorDrawable back = new Android.Graphics.Drawables.ColorDrawable(Color.LightGray);
                back.Alpha = 128;
                row.Background = back;
            }
            Info.AddView(row);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
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
    [Activity(Label = "FindForDayRequested")]
    public class LookTagRequested : Activity
    {
        Typeface boldFont;
        Typeface mediumFont;
        Typeface lightFont;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            boldFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Bold.otf");
            mediumFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Medium.otf");
            lightFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Light.otf");
            base.OnCreate(savedInstanceState);
            string tag = Intent.GetStringExtra("Tag");
            SetContentView(Resource.Layout.LookTagRet);
            TextView header = (TextView)FindViewById(Resource.Id.TextViev1_LookForTag_Req);
            header.Text = "Витрати за тегом " + tag;
            header.Typeface = boldFont;
            DataBase.LookFor(tag, DrawRows);
            TextView SumView = (TextView)FindViewById(Resource.Id.TextViev1_LookForDay_Sum);
            //SumView.Text = "Витрати за цей день становлять: " + DataBase.DaySum(requestedTime).ToString() + " грн";
        }

        private void DrawRows(string date, string description, string price)
        {
            TableLayout Info = (TableLayout)FindViewById(Resource.Id.Table_Day);
            if (Info.GetChildAt(0) != null)
            {
                LayoutInflater sep = LayoutInflater.From(this);
                TableRow rowSep = (TableRow)sep.Inflate(Resource.Layout.separator, null);
                Info.AddView(rowSep);
            }
            LayoutInflater inflator = LayoutInflater.From(this);
            TableRow row = (TableRow)inflator.Inflate(Resource.Layout.ThreeTextTemplate, null);
            TextView ZeroCol = row.FindViewById<TextView>(Resource.Id.yearCol);
            TextView FirstCol = (TextView)row.FindViewById(Resource.Id.descCol);
            TextView SecondCol = (TextView)row.FindViewById(Resource.Id.priceCol);
            ZeroCol.Text = date;
            ZeroCol.Gravity = GravityFlags.CenterHorizontal;
            ZeroCol.Typeface = lightFont;
            FirstCol.Text = price;
            FirstCol.Typeface = lightFont;
            FirstCol.Gravity = GravityFlags.CenterHorizontal;
            SecondCol.Text = description;
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
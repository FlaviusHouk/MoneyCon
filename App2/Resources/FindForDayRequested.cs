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
    [Activity(Label = "FindForDayRequested")]
    public class FindForDayRequested : Activity
    {
        private DateTime requestedTime;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            string Year = Intent.GetStringExtra("Year");
            int Month = int.Parse(Intent.GetStringExtra("Month"));
            int Day = int.Parse(Intent.GetStringExtra("Day"));
            requestedTime = new DateTime(int.Parse(Year), Month, Day);
            DataBase.outputMeth1 LookForDay_out = DrawRows;
            SetContentView(Resource.Layout.DayActRet);
            TextView Header = (TextView)FindViewById(Resource.Id.TextViev1_LookForDay_Req);
            Header.Text = "������� �� " + requestedTime.ToShortDateString();
            DataBase.LookFor(requestedTime, LookForDay_out);
            TextView SumView = (TextView)FindViewById(Resource.Id.TextViev1_LookForDay_Sum);
            SumView.Text = "������� �� ��� ���� ����������: " + DataBase.DaySum(requestedTime).ToString() + " ���";
        }

        private void DrawRows(string description, string price)
        {
            TableLayout Info = (TableLayout)FindViewById(Resource.Id.Table_Day);
            //Info.SetBackgroundResource(Resource.Drawable.rectangle);
            //TableRow row = new TableRow(this);
            //TableRow.LayoutParams rowPar = new TableRow.LayoutParams(TableRow.LayoutParams.WrapContent);
            //row.LayoutParameters = rowPar;
            //TextView col1 = new TextView(this);
            //col1.Text = description;
            //TextView col2 = new TextView(this);
            //col2.Text = price;
            //row.AddView(col1);
            //row.AddView(col2);
            //Info.AddView(row);
            LayoutInflater inflator = LayoutInflater.From(this);
            TableRow row = (TableRow)inflator.Inflate(Resource.Layout.RowTemplate, null);
            TextView FirstCol = (TextView)row.FindViewById(Resource.Id.descCol);
            TextView SecondCol = (TextView)row.FindViewById(Resource.Id.priceCol);
            FirstCol.Text = description;
            FirstCol.Gravity = GravityFlags.CenterHorizontal;
            SecondCol.Text = price;
            Info.AddView(row);
        }
    }
}
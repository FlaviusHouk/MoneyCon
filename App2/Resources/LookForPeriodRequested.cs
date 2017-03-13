using System;
using System.Collections.Generic;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            string Year = Intent.GetStringExtra("BYear");
            string Month = Intent.GetStringExtra("BMonth");
            string Day = Intent.GetStringExtra("BDay");
            SetContentView(Resource.Layout.FindForPeriodRequested);
            try
            {
                BDate = new DateTime(int.Parse(Year), int.Parse(Month), int.Parse(Day));
            }
            catch
            {
                AlertDialog.Builder errMsg = new AlertDialog.Builder(this);
                errMsg.SetTitle("Ой...");
                errMsg.SetMessage("Ви помилилися з введеням дати. Будь-ласка перевірте дані й спробуйте знову");
                errMsg.SetPositiveButton("OK", (senderAlert, ar) => { OnBackPressed(); });
                errMsg.SetCancelable(true);
                errMsg.Create().Show();
                return;
            }
            Year = Intent.GetStringExtra("EYear");
            Month = Intent.GetStringExtra("EMonth");
            Day = Intent.GetStringExtra("EDay");
            try
            {
                EDate = new DateTime(int.Parse(Year), int.Parse(Month), int.Parse(Day));
            }
            catch
            {
                AlertDialog.Builder errMsg = new AlertDialog.Builder(this);
                errMsg.SetTitle("Ой...");
                errMsg.SetMessage("Ви помилилися з введеням дати. Будь-ласка перевірте дані й спробуйте знову");
                errMsg.SetPositiveButton("OK", (senderAlert, ar) => { OnBackPressed(); });
                errMsg.SetCancelable(true);
                errMsg.Create().Show();
                return;
            }
            DataBase.outputMeth1 LookForDay_out = DrawRows;
            
            TextView Header = (TextView)FindViewById(Resource.Id.TextViev1_LookForPer_Req);
            Header.Text = "Витрати за період з " + BDate.ToShortDateString() + " по " + EDate.ToShortDateString();
            TextView SumView = (TextView)FindViewById(Resource.Id.TextViev1_LookForPer_Sum);
            SumView.Text = "Витрати за цей період становлять: " + DataBase.PerSum(BDate,EDate,LookForDay_out).ToString() + " грн";
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
            TableRow row = (TableRow)inflator.Inflate(Resource.Layout.RowTemplate, null);
            TextView FirstCol = (TextView)row.FindViewById(Resource.Id.descCol);
            TextView SecondCol = (TextView)row.FindViewById(Resource.Id.priceCol);
            FirstCol.Text = description;
            SecondCol.Text = price;
            Info.AddView(row);
        }
    }
}
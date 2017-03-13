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
    [Activity(Label = "AddRecAct")]
    public class AddRecAct : Activity
    {
        private string actName;
        private double price;
        private DateTime curTime = DateTime.Now;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddRecLay);
            DefaultDateValue();
            Button AddBut = (Button)FindViewById(Resource.Id.Add);
            AddBut.Click += AddRecHand;
        }

        protected void AddRecHand(object e, EventArgs args)
        {
            EditText name = (EditText)FindViewById(Resource.Id.ActName);
            actName = name.Text;
            EditText priceField = (EditText)FindViewById(Resource.Id.ActPrice);
            price = double.Parse(priceField.Text);
            EditText Year = (EditText)FindViewById(Resource.Id.ActYear);
            EditText Month = (EditText)FindViewById(Resource.Id.ActMonth);
            EditText Day = (EditText)FindViewById(Resource.Id.ActDay);
            DateTime date;
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
            DataBase.AddRec(date.ToShortDateString(), price.ToString(), actName);
            AlertDialog.Builder dial = new AlertDialog.Builder(this);
            dial.SetTitle("Додавання витрати");
            dial.SetMessage("Видаток " + actName + " зроблений " + date.ToShortDateString() + " додано.");
            dial.SetPositiveButton("OK", (senderAlert, ar) => 
            {
                priceField.Text = String.Empty;
                name.Text = String.Empty;
            });
            dial.SetCancelable(true);
            dial.Create().Show();
        }

       

        private void DefaultDateValue()
        {
            EditText Year = (EditText)FindViewById(Resource.Id.ActYear);
            EditText Month = (EditText)FindViewById(Resource.Id.ActMonth);
            EditText Day = (EditText)FindViewById(Resource.Id.ActDay);
            int year = curTime.Year;
            Year.Text = year.ToString();
            Month.Text = curTime.Month.ToString();
            Day.Text = curTime.Day.ToString();
        }
    }
}
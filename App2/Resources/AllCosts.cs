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
    [Activity(Label = "AllCosts")]
    public class AllCosts : Activity
    {
        Bundle savedInstanse;
        TableLayout Info;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            savedInstanse = savedInstanceState;
            SetContentView(Resource.Layout.AllCosts);
            TextView Header = (TextView)FindViewById(Resource.Id.TextViev1_All);
            Header.Text = "Всі видатки";
            Info = (TableLayout)FindViewById(Resource.Id.Table_All);
            DataBase.ReadRec(DrawRows);
            FirstRowDecoration();
        }

        private void FirstRowDecoration()
        {
            Info.GetChildAt(0).FindViewById<TextView>(Resource.Id.yearCol).Gravity = GravityFlags.Center;
            Info.GetChildAt(0).FindViewById<TextView>(Resource.Id.priceCol).Gravity = GravityFlags.Center;
            Info.GetChildAt(0).FindViewById<TextView>(Resource.Id.descCol).Gravity = GravityFlags.Center;
            Info.GetChildAt(0).FindViewById<TextView>(Resource.Id.yearCol).SetPadding(0, 0, 0, 0);
            Info.GetChildAt(0).FindViewById<TextView>(Resource.Id.descCol).SetPadding(0, 0, 0, 0);
        }

        private void DrawRows(string date, string price, string description)
        {
            LayoutInflater inflator = LayoutInflater.From(this);
            if (Info.GetChildAt(0) != null)
            {
                LayoutInflater sep = LayoutInflater.From(this);
                TableRow rowSep = (TableRow)sep.Inflate(Resource.Layout.separator, null);
                Info.AddView(rowSep);
            }
            TableRow row = (TableRow)inflator.Inflate(Resource.Layout.RowTemplate2, null);
            TextView LastCol = (TextView)row.FindViewById(Resource.Id.descCol);
            TextView PreLastCol = (TextView)row.FindViewById(Resource.Id.priceCol);
            TextView FirstCol = (TextView)row.FindViewById(Resource.Id.yearCol);
            LastCol.Text = description;
            PreLastCol.Text = price;
            FirstCol.Text = date;
            row.Clickable = true;
            row.LongClick += DeleteRowHandler;
            Info.AddView(row);
        }

        private void DeleteRowHandler(object sender, View.LongClickEventArgs e)
        {
            AlertDialog.Builder deleteDial = new AlertDialog.Builder(this);
            TextView date = (TextView)((TableRow)sender).GetChildAt(0);
            TextView descr = (TextView)((TableRow)sender).GetChildAt(2);
            TextView price = (TextView)((TableRow)sender).GetChildAt(4);
            deleteDial.SetTitle("Ви дійсно хочете видалити запис " + descr.Text + "?");
            deleteDial.SetPositiveButton("Так", (innSender, innArr) => 
            {
                DateTime tempo = DateTime.Parse(date.Text);
                DataBase.Delete(tempo.ToShortDateString(), price.Text, descr.Text);
                this.OnCreate(savedInstanse);
            });
            deleteDial.SetNegativeButton("Ні", (innSender, innArr) => { });
            deleteDial.Create().Show();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Calligraphy;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace App2.Resources
{
    [Activity(Label = "AllCosts")]
    public class AllCosts : Activity
    {
        Bundle savedInstanse;
        TableLayout Info;
        Typeface boldFont;
        Typeface mediumFont;
        Typeface lightFont;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            boldFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Bold.otf");
            mediumFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Medium.otf");
            lightFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Light.otf");
            base.OnCreate(savedInstanceState);
            savedInstanse = savedInstanceState;
            SetContentView(Resource.Layout.AllCosts);
            TextView Header = (TextView)FindViewById(Resource.Id.TextViev1_All);
            Header.Text = "�� �������";
            Header.Typeface = boldFont;
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
            TableRow row = (TableRow)inflator.Inflate(Resource.Layout.ThreeTextTemplate, null);
            TextView LastCol = (TextView)row.FindViewById(Resource.Id.descCol);
            TextView PreLastCol = (TextView)row.FindViewById(Resource.Id.priceCol);
            TextView FirstCol = (TextView)row.FindViewById(Resource.Id.yearCol);
            LastCol.Text = description;
            LastCol.Typeface = lightFont;
            PreLastCol.Text = price;
            PreLastCol.Typeface = lightFont;
            FirstCol.Text = date;
            FirstCol.Typeface = lightFont;
            row.Clickable = true;
            row.LongClick += DeleteRowHandler;
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

        private void DeleteRowHandler(object sender, View.LongClickEventArgs e)
        {
            AlertDialog.Builder deleteDial = new AlertDialog.Builder(this);
            TextView date = (TextView)((TableRow)sender).GetChildAt(0);
            TextView descr = (TextView)((TableRow)sender).GetChildAt(2);
            TextView price = (TextView)((TableRow)sender).GetChildAt(4);
            deleteDial.SetTitle("�� ����� ������ �������� ����� " + descr.Text + "?");
            deleteDial.SetPositiveButton("���", (innSender, innArr) => 
            {
                DateTime tempo = DateTime.Parse(date.Text);
                DataBase.Delete(tempo.ToShortDateString(), price.Text, descr.Text);
                this.OnCreate(savedInstanse);
            });
            deleteDial.SetNegativeButton("ͳ", (innSender, innArr) => { });
            deleteDial.Create().Show();
        }
    }
}
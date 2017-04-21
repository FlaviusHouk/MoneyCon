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
    [Activity(Label = "TemplateScreen")]
    public class TemplateScreen : Activity
    {
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
            SetContentView(Resource.Layout.TemplateScreen);
            TextView header = FindViewById<TextView>(Resource.Id.Temlpates_Header);
            header.Typeface = boldFont;
            header.Text = "Шаблони";
            Info = FindViewById<TableLayout>(Resource.Id.Templates_All);
            DataBase.ReadTemplates(DrawRows);
        }
        private void DrawRows(string price, string desc, string tag)
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
            LastCol.Text = desc;
            LastCol.Typeface = lightFont;
            PreLastCol.Text = price;
            PreLastCol.Typeface = lightFont;
            FirstCol.Text = tag;
            FirstCol.Typeface = lightFont;
            row.Clickable = true;
            row.Click += AddTemplate;
            Info.AddView(row);
        }

        private void AddTemplate(object sender, EventArgs e)
        {
            TextView date = (TextView)((TableRow)sender).GetChildAt(0);
            TextView descr = (TextView)((TableRow)sender).GetChildAt(2);
            TextView price = (TextView)((TableRow)sender).GetChildAt(4);
            DataBase.AddRec(DateTime.Now.ToShortDateString(), price.Text, descr.Text, date.Text);
            StringBuilder msg = new StringBuilder("Шаблон ");
            AlertDialog.Builder dial = new AlertDialog.Builder(this);
            dial.SetMessage(msg.AppendFormat("{0} було додано", descr.Text).ToString());
            dial.SetTitle("Додавання");
            dial.SetPositiveButton("Повернутися", (innerSender, innerE) => { OnBackPressed(); });
            dial.SetNegativeButton("Лишитися", (innerSender, innerE) => { });
            dial.Create().Show();
        }
    }
}
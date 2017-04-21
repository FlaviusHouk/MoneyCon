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
    [Activity(Label = "TagEdit")]
    public class TagEdit : Activity
    {
        TableLayout Tags;
        Bundle savedInstanceState;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.savedInstanceState = savedInstanceState;
            SetContentView(Resource.Layout.TagEdit);
            Tags = FindViewById<TableLayout>(Resource.Id.Table_Tag);
            DataBase.ReadTags(DrawRows);
        }

        private void DrawRows(string tag)
        {
            LayoutInflater inflator = LayoutInflater.From(this);
            if (Tags.GetChildAt(0) != null)
            {
                LayoutInflater sep = LayoutInflater.From(this);
                TableRow rowSep = (TableRow)sep.Inflate(Resource.Layout.separator, null);
                rowSep.SetGravity(GravityFlags.CenterHorizontal);
                Tags.AddView(rowSep);
            }
            TableRow row = (TableRow)inflator.Inflate(Resource.Layout.OneTextTempate, null);
            row.SetGravity(GravityFlags.CenterHorizontal);
            TextView tagCol = row.FindViewById<TextView>(Resource.Id.descCol);
            tagCol.Text = tag;
            tagCol.SetTextSize(Android.Util.ComplexUnitType.Pt, 16);
            row.Clickable = true;
            row.LongClick += TagHand;
            Tags.AddView(row);
        }

        private void TagHand(object sender, EventArgs e)
        {
            AlertDialog.Builder dial = new AlertDialog.Builder(this);
            TextView tag = (TextView)((TableRow)sender).GetChildAt(0);
            dial.SetPositiveButton("Змінити", (InnerSender, InnerE) => 
            {
                
            });
            dial.SetNeutralButton("Відмінити", (InnerSender, InnerE) => { });
            dial.SetNegativeButton("Видалити", (InnerSender, InnerE) => 
            {
                DataBase.DeleteTag(tag.Text);
                OnCreate(savedInstanceState);
            });
            dial.SetTitle("Що ви хочете зробити з тегом \"" + tag.Text + "\"?");
            dial.Create().Show();          
        }
    }
}
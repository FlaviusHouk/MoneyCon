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
using Android.Graphics;

namespace App2.Resources
{
    [Activity(Label = "AddTagAct")]
    public class AddTagAct : Activity
    {
        Typeface boldFont;
        Typeface mediumFont;
        Typeface lightFont;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            boldFont = Typeface.CreateFromAsset(Assets, "Fonts/DaxlinePro-ExtraBold.otf");
            mediumFont = Typeface.CreateFromAsset(Assets, "Fonts/DaxlinePro-Medium.otf");
            lightFont = Typeface.CreateFromAsset(Assets, "Fonts/DaxlinePro-Light.otf");
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddTagLayout);
            FindViewById<TextView>(Resource.Id.AddTag_Header).Typeface = boldFont;
            FindViewById<TextView>(Resource.Id.TagName).Typeface = lightFont;
            Button addTag = FindViewById<Button>(Resource.Id.AddTagBut);
            addTag.Click += AddTag;
        }

        public void AddTag(object sender, EventArgs e)
        {
            DataBase.AddTag(FindViewById<EditText>(Resource.Id.TagName).Text);
            
            StartActivity(typeof(AddRecAct));
        }
    }
}
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
    [Activity(Label = "FindForDayRequest")]
    public class LookTagRequest : Activity
    {
        Typeface boldFont;
        Typeface mediumFont;
        Typeface lightFont;
        private List<TagSpinnerWrapper> tags;
        NormalSpinner comboBox;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            boldFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Bold.otf");
            mediumFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Medium.otf");
            lightFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Light.otf");
            tags = new List<TagSpinnerWrapper>();
            DataBase.ReadTags(BuildTagCol);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LookTagAct);
            comboBox = new NormalSpinner(this);
            ArrayAdapter<TagSpinnerWrapper> someAdapter = new ArrayAdapter<TagSpinnerWrapper>(this, Resource.Layout.ComboBoxElement, tags);
            someAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            comboBox.Adapter = someAdapter;
            LinearLayout SpinnerLay = FindViewById<LinearLayout>(Resource.Id.PossibleTag);
            SpinnerLay.AddView(comboBox, new ViewGroup.LayoutParams(-1, -1));
            Button ReqBut = (Button)FindViewById(Resource.Id.LookTagBut);
            ReqBut.Click += GiveRequested;
            FindViewById<TextView>(Resource.Id.TextViev1_LookForTag).Typeface = boldFont;
            FindViewById<TextView>(Resource.Id.TextViev1_LookForTag).TextSize = 24;
        }

        private void GiveRequested(object e, EventArgs args)
        {
            //EditText tag = (EditText)FindViewById(Resource.Id.PossibleTag);
            Intent intent = new Intent(this, typeof(LookTagRequested));
            intent.PutExtra("Tag", ((TagSpinnerWrapper)comboBox.SelectedItem).ToString());
            StartActivity(intent);
        }

        private void BuildTagCol(string tag)
        {
            TagSpinnerWrapper text = new TagSpinnerWrapper(this, mediumFont);
            text.Text = tag;
            tags.Add(text);
        }
    }
}
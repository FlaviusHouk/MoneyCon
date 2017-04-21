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
    public class Fonts
    {
        public Fonts(Android.Content.Res.AssetManager Assets)
        {
            boldFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Bold.otf");
            mediumFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Medium.otf");
            lightFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Light.otf");
        }
        Typeface boldFont;
        Typeface mediumFont;
        Typeface lightFont;
        public Typeface BoldFont
        {
            get { return boldFont; }
        }
        public Typeface MediumFont
        {
            get { return mediumFont; }
        }
        public Typeface LightFont
        {
            get { return LightFont; }
        }
    }
    public class NormalSpinner : Spinner
    {
        public NormalSpinner(Context context) : base(context)
        { }
        public NormalSpinner(Context context, Android.Util.IAttributeSet attr) : base(context, attr)
        { }
        public NormalSpinner(Context context, Android.Util.IAttributeSet attr, int defStyle) : base(context, attr, defStyle)
        { }
        public override void SetSelection(int position, bool animate)
        {
            bool selection = position == SelectedItemPosition;
            base.SetSelection(position, animate);
            if (selection)
            {
                OnItemSelectedListener.OnItemSelected(this, SelectedView, position, SelectedItemId);
            }
        }
        public override void SetSelection(int position)
        {
            bool selection = position == SelectedItemPosition;
            base.SetSelection(position);
            if (selection)
            {
                OnItemSelectedListener.OnItemSelected(this, SelectedView, position, SelectedItemId);
            }
        }
    }
}
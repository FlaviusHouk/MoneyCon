using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace App2
{
    [Activity(Label = "Проект")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var metrics = Resources.DisplayMetrics;
            SetContentView(Resource.Layout.Start);
            LinearLayout startButLay = FindViewById<LinearLayout>(Resource.Id.StartButLay);
            LinearLayout.LayoutParams SBLPar = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            SBLPar.TopMargin = (int)(metrics.HeightPixels * 0.75);
            startButLay.LayoutParameters = SBLPar;
            Button button = FindViewById<Button>(Resource.Id.button1);
            button.Click += DrawMainWindow;
        }
        private int ConvertPixelsToDp(float pixelValue)
        {
            var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
            return dp;
        }
        private int ConvertDPToPixels(float DPValue)
        {
            var dp = (int)((DPValue) * Resources.DisplayMetrics.Density);
            return dp;
        }

        private void DrawMainWindow(object e, EventArgs args)
        {
            //base.OnBackPressed();
            StartActivity(typeof(Resources.MenuActivity));
        }
    }
}


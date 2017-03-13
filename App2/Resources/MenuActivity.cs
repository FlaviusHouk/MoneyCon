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
    [Activity(Label = "MoneyCon", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class MenuActivity : Activity
    {
        static int count = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            DataBase.Open();
            count++;
            base.OnCreate(savedInstanceState);
            try
            {
                if (DataBase.FirstCheck() != 1)
                {
                    StartActivity(typeof(MainActivity));
                }
            }
            catch (Exception e)
            {
                DataBase.FirstStart();
                StartActivity(typeof(MainActivity));
            }
            if (DataBase.IsPassworded)
            {
                StartActivity(typeof(PasswordScreen));
            }
            SetContentView(Resource.Layout.MainWindow);
            Button AddRecButton = (Button)FindViewById(Resource.Id.MainBut1);
            AddRecButton.Click += AddRecButtonHandler;
            Button FindForDayBut = (Button)FindViewById(Resource.Id.MainBut3);
            FindForDayBut.Click += FindForDayHandler;
            Button FindForPeriodBut = (Button)FindViewById(Resource.Id.MainBut4);
            FindForPeriodBut.Click += FindForPeriodHandler;
            Button AllCostsBut = (Button)FindViewById(Resource.Id.MainBut2);
            AllCostsBut.Click += AllCostsHand;
            Button QuitBut = (Button)FindViewById(Resource.Id.MainBut6);
            QuitBut.Click += QuitMeth;
            Button Prefs = FindViewById<Button>(Resource.Id.MainBut5);
            Prefs.Click += PrefsHandler;
        }

        private void AddRecButtonHandler(object e, EventArgs args)
        {
            StartActivity(typeof(Resources.AddRecAct));
        }

        private void FindForDayHandler(object e, EventArgs args)
        {
            StartActivity(typeof(Resources.FindForDayRequest));
        }

        private void FindForPeriodHandler(object e, EventArgs args)
        {
            StartActivity(typeof(Resources.LookForPeriodRequest));
        }

        private void AllCostsHand(object e, EventArgs args)
        {
            StartActivity(typeof(Resources.AllCosts));
        }

        private void PrefsHandler(object e, EventArgs args)
        {
            StartActivity(typeof(Resources.Prefs));
        }

        private void QuitMeth(object e, EventArgs args)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            dialog.SetMessage("Ви дійсно хочете вийти?");
            dialog.SetPositiveButton("Так", (sender, ar) => { DataBase.Close(); FinishAffinity(); });
            dialog.SetNegativeButton("Ні", (sender, ar) => { });
            dialog.Create().Show();
        }  
    }
}
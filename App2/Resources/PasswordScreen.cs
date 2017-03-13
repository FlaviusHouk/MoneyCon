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
    [Activity(Label = "Activity1")]
    public class PasswordScreen : Activity
    {
        Button but;
        EditText text;
        static bool passed = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PasswordChecker);
            text = FindViewById<EditText>(Resource.Id.passField);
            but = FindViewById<Button>(Resource.Id.Check);
            but.Click += Check;
        }
        private void Check(object sender, EventArgs e)
        {
            passed = false;
            if (DataBase.CheckPass(text.Text))
            {
                passed = true;
                OnBackPressed();
            }
            else
            {
                AlertDialog.Builder dial = new AlertDialog.Builder(this);
                dial.SetMessage("Пароль введено не правильно");
                dial.Create().Show();
            }
        }
        public override void OnBackPressed()
        {
            if (passed)
            {
                base.OnBackPressed();
            }
            else
            {
                AlertDialog.Builder dial = new AlertDialog.Builder(this);
                dial.SetMessage("Ви хочете вийти?");
                dial.SetPositiveButton("Так", (sender, e) => { System.Environment.Exit(0); });
                dial.Create().Show();
            }
        }
    }
}
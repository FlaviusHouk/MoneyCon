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
    [Activity(Label = "Prefs")]
    public class Prefs : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Prefs);
            Button export = FindViewById<Button>(Resource.Id.expButt);
            export.Click += Export;
            Button import = FindViewById<Button>(Resource.Id.impButt);
            import.Click += Import;
            Button SetPass = FindViewById<Button>(Resource.Id.passButt);
            SetPass.Click += SetPassword;
            Button encoding = FindViewById<Button>(Resource.Id.enc);
            encoding.Click += EncryptHandler;
            Button decoding = FindViewById<Button>(Resource.Id.dec);
            decoding.Click += DecryptHanler;
        }

        private void DecryptHanler(object sender, EventArgs e)
        {
            DataBase.DecryptAll();
            AlertDialog.Builder dial = new AlertDialog.Builder(this);
            dial.SetMessage("Базу розшифровано");
            dial.Create().Show();
        }

        private void EncryptHandler(object sender, EventArgs e)
        {
            DataBase.EncryptAll();
            AlertDialog.Builder dial = new AlertDialog.Builder(this);
            dial.SetMessage("Базу зашифровано");
            dial.Create().Show();
        }

        private void Export(object sender, EventArgs e)
        {
            StartActivity(typeof(Resources.FileExplorerExp));
        }

        private void Import(object sender, EventArgs e)
        {
            StartActivity(typeof(Resources.FileExplorerImp));
        }

        private void SetPassword(object sender, EventArgs e)
        {
            StartActivity(typeof(Resources.SetPassAct));
        }
    }
}
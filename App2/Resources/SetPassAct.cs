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
    [Activity(Label = "SetPassAct")]
    public class SetPassAct : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SetPassLay);
            Button setPassButt = FindViewById<Button>(Resource.Id.SetPass);
            setPassButt.Click += SetPassword;
        }
        private void SetPassword(object sender, EventArgs e)
        {
            EditText FirstPass = FindViewById<EditText>(Resource.Id.SetPassField1);
            EditText SecondPass = FindViewById<EditText>(Resource.Id.SetPassField2);
            if (FirstPass.Text != SecondPass.Text)
            {
                AlertDialog.Builder dial = new AlertDialog.Builder(this);
                dial.SetTitle("Помилка");
                dial.SetMessage("Введені паролі не співпадають спробуйте знову");
                dial.SetPositiveButton("Добре", (inSender, inE) =>
                {
                    FirstPass.Text = String.Empty;
                    SecondPass.Text = String.Empty;
                });
                dial.Create().Show();
            }
            else
            {
                DataBase.SetKey(FirstPass.Text);
                AlertDialog.Builder dial = new AlertDialog.Builder(this);
                dial.SetTitle("Контроль видатків");
                dial.SetMessage("Пароль встановлено");
                dial.SetPositiveButton("Повернутися", (inSender, inE) =>
                {
                    OnBackPressed();
                });
                dial.Create().Show();
            }
        }
    }
}
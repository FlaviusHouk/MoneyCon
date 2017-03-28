/* Цей файл — частина MoneyCon.

   Moneycon - вільна програма: ви можете повторно її розповсюджувати та/або
   змінювати її на умовах Стандартної суспільної ліцензії GNU в тому вигляді,
   в якому вона була опублікована Фондом вільного програмного забезпечення;
   або третьої версії ліцензії, або (зігдно з вашим вибором) будь-якої наступної
   версії.

   Moneycon розповсюджується з надією, що вона буде корисною,
   але БЕЗ БУДЬ-ЯКИХ ГАРАНТІЙ; навіть без неявної гарантії ТОВАРНОГО ВИГЛЯДУ
   або ПРИДАТНОСТІ ДЛЯ КОНКРЕТНИХ ЦІЛЕЙ. Детальніше див. в Стандартній
   суспільній ліцензії GNU.

   Ви повинні були отримати копію Стандартної суспільної ліцензії GNU
   разом з цією програмою. Якщо це не так, див.
   <http://www.gnu.org/licenses/>.*/

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
                dial.SetTitle("Ïîìèëêà");
                dial.SetMessage("Ââåäåí³ ïàðîë³ íå ñï³âïàäàþòü ñïðîáóéòå çíîâó");
                dial.SetPositiveButton("Äîáðå", (inSender, inE) =>
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
                dial.SetTitle("Êîíòðîëü âèäàòê³â");
                dial.SetMessage("Ïàðîëü âñòàíîâëåíî");
                dial.SetPositiveButton("Ïîâåðíóòèñÿ", (inSender, inE) =>
                {
                    OnBackPressed();
                });
                dial.Create().Show();
            }
        }
    }
}

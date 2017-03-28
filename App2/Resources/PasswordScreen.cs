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
                dial.SetMessage("Ïàðîëü ââåäåíî íå ïðàâèëüíî");
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
                dial.SetMessage("Âè õî÷åòå âèéòè?");
                dial.SetPositiveButton("Òàê", (sender, e) => { System.Environment.Exit(0); });
                dial.Create().Show();
            }
        }
    }
}

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
    [Activity(Label = "LookForPeriodRequest")]
    public class LookForPeriodRequest : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FindForPeriodRequest);
            Button ReqBut = (Button)FindViewById(Resource.Id.FindForPerBut);
            ReqBut.Click += GetRequested;
        }

        private void GetRequested(object e, EventArgs args)
        {
            Intent intent = new Intent(this, typeof(LookForPeriodRequested));
            EditText InputField = (EditText)FindViewById(Resource.Id.YearForPeriod_1);
            intent.PutExtra("BYear", InputField.Text);
            InputField = (EditText)FindViewById(Resource.Id.MonthForPeriod_1);
            intent.PutExtra("BMonth", InputField.Text);
            InputField = (EditText)FindViewById(Resource.Id.DayForPeriod_1);
            intent.PutExtra("BDay", InputField.Text);
            InputField = (EditText)(EditText)FindViewById(Resource.Id.YearForPeriod_2);
            intent.PutExtra("EYear", InputField.Text);
            InputField = (EditText)FindViewById(Resource.Id.MonthForPeriod_2);
            intent.PutExtra("EMonth", InputField.Text);
            InputField = (EditText)FindViewById(Resource.Id.DayForPeriod_2);
            intent.PutExtra("EDay", InputField.Text);
            StartActivity(intent);
        }
    }
}

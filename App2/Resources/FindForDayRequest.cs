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
    [Activity(Label = "FindForDayRequest")]
    public class FindForDayRequest : Activity
    {
        DateTime date;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DayAct);
            Button ReqBut = (Button)FindViewById(Resource.Id.FindForDayBut);
            ReqBut.Click += GiveRequested;
        }

        private void GiveRequested(object e, EventArgs args)
        {
            EditText Year = (EditText)FindViewById(Resource.Id.YearForDay);
            EditText Month = (EditText)FindViewById(Resource.Id.MonthForDay);
            EditText Day = (EditText)FindViewById(Resource.Id.DayForDay);
            try
            {
                date = new DateTime(int.Parse(Year.Text), int.Parse(Month.Text), int.Parse(Day.Text));
            }
            catch
            {
                AlertDialog.Builder errMsg = new AlertDialog.Builder(this);
                errMsg.SetTitle("Îé...");
                errMsg.SetMessage("Âè ïîìèëèëèñÿ ç ââåäåíÿì äàòè. Áóäü-ëàñêà ïåðåâ³ðòå äàí³ é ñïðîáóéòå çíîâó");
                errMsg.SetPositiveButton("OK", (senderAlert, ar) => { });
                errMsg.SetCancelable(true);
                errMsg.Create().Show();
                return;
            }
            Intent intent = new Intent(this, typeof(FindForDayRequested));
            intent.PutExtra("Year", Year.Text);
            intent.PutExtra("Month", Month.Text);
            intent.PutExtra("Day", Day.Text);
            StartActivity(intent);
        }
    }
}

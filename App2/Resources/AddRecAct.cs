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
    [Activity(Label = "AddRecAct")]
    public class AddRecAct : Activity
    {
        private string actName;
        private double price;
        private DateTime curTime = DateTime.Now;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddRecLay);
            DefaultDateValue();
            Button AddBut = (Button)FindViewById(Resource.Id.Add);
            AddBut.Click += AddRecHand;
        }

        protected void AddRecHand(object e, EventArgs args)
        {
            EditText name = (EditText)FindViewById(Resource.Id.ActName);
            actName = name.Text;
            EditText priceField = (EditText)FindViewById(Resource.Id.ActPrice);
            price = double.Parse(priceField.Text);
            EditText Year = (EditText)FindViewById(Resource.Id.ActYear);
            EditText Month = (EditText)FindViewById(Resource.Id.ActMonth);
            EditText Day = (EditText)FindViewById(Resource.Id.ActDay);
            DateTime date;
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
            DataBase.AddRec(date.ToShortDateString(), price.ToString(), actName);
            AlertDialog.Builder dial = new AlertDialog.Builder(this);
            dial.SetTitle("Äîäàâàííÿ âèòðàòè");
            dial.SetMessage("Âèäàòîê " + actName + " çðîáëåíèé " + date.ToShortDateString() + " äîäàíî.");
            dial.SetPositiveButton("OK", (senderAlert, ar) => 
            {
                priceField.Text = String.Empty;
                name.Text = String.Empty;
            });
            dial.SetCancelable(true);
            dial.Create().Show();
        }

       

        private void DefaultDateValue()
        {
            EditText Year = (EditText)FindViewById(Resource.Id.ActYear);
            EditText Month = (EditText)FindViewById(Resource.Id.ActMonth);
            EditText Day = (EditText)FindViewById(Resource.Id.ActDay);
            int year = curTime.Year;
            Year.Text = year.ToString();
            Month.Text = curTime.Month.ToString();
            Day.Text = curTime.Day.ToString();
        }
    }
}

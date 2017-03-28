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
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App2.Resources
{
    [Activity(Label = "LookForPeriodRequested")]
    public class LookForPeriodRequested : Activity
    {
        private DateTime BDate;
        private DateTime EDate;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            string Year = Intent.GetStringExtra("BYear");
            string Month = Intent.GetStringExtra("BMonth");
            string Day = Intent.GetStringExtra("BDay");
            SetContentView(Resource.Layout.FindForPeriodRequested);
            try
            {
                BDate = new DateTime(int.Parse(Year), int.Parse(Month), int.Parse(Day));
            }
            catch
            {
                AlertDialog.Builder errMsg = new AlertDialog.Builder(this);
                errMsg.SetTitle("Îé...");
                errMsg.SetMessage("Âè ïîìèëèëèñÿ ç ââåäåíÿì äàòè. Áóäü-ëàñêà ïåðåâ³ðòå äàí³ é ñïðîáóéòå çíîâó");
                errMsg.SetPositiveButton("OK", (senderAlert, ar) => { OnBackPressed(); });
                errMsg.SetCancelable(true);
                errMsg.Create().Show();
                return;
            }
            Year = Intent.GetStringExtra("EYear");
            Month = Intent.GetStringExtra("EMonth");
            Day = Intent.GetStringExtra("EDay");
            try
            {
                EDate = new DateTime(int.Parse(Year), int.Parse(Month), int.Parse(Day));
            }
            catch
            {
                AlertDialog.Builder errMsg = new AlertDialog.Builder(this);
                errMsg.SetTitle("Îé...");
                errMsg.SetMessage("Âè ïîìèëèëèñÿ ç ââåäåíÿì äàòè. Áóäü-ëàñêà ïåðåâ³ðòå äàí³ é ñïðîáóéòå çíîâó");
                errMsg.SetPositiveButton("OK", (senderAlert, ar) => { OnBackPressed(); });
                errMsg.SetCancelable(true);
                errMsg.Create().Show();
                return;
            }
            DataBase.outputMeth1 LookForDay_out = DrawRows;
            
            TextView Header = (TextView)FindViewById(Resource.Id.TextViev1_LookForPer_Req);
            Header.Text = "Âèòðàòè çà ïåð³îä ç " + BDate.ToShortDateString() + " ïî " + EDate.ToShortDateString();
            TextView SumView = (TextView)FindViewById(Resource.Id.TextViev1_LookForPer_Sum);
            SumView.Text = "Âèòðàòè çà öåé ïåð³îä ñòàíîâëÿòü: " + DataBase.PerSum(BDate,EDate,LookForDay_out).ToString() + " ãðí";
        }

        private void DrawRows(string description, string price)
        {
            TableLayout Info = (TableLayout)FindViewById(Resource.Id.Table_Per);
            LayoutInflater inflator = LayoutInflater.From(this);
            if (Info.GetChildAt(0) != null)
            {
                LayoutInflater sep = LayoutInflater.From(this);
                TableRow rowSep = (TableRow)sep.Inflate(Resource.Layout.separator, null);
                Info.AddView(rowSep);
            }
            TableRow row = (TableRow)inflator.Inflate(Resource.Layout.RowTemplate, null);
            TextView FirstCol = (TextView)row.FindViewById(Resource.Id.descCol);
            TextView SecondCol = (TextView)row.FindViewById(Resource.Id.priceCol);
            FirstCol.Text = description;
            SecondCol.Text = price;
            Info.AddView(row);
        }
    }
}

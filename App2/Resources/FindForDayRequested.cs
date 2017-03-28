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
    [Activity(Label = "FindForDayRequested")]
    public class FindForDayRequested : Activity
    {
        private DateTime requestedTime;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            string Year = Intent.GetStringExtra("Year");
            int Month = int.Parse(Intent.GetStringExtra("Month"));
            int Day = int.Parse(Intent.GetStringExtra("Day"));
            requestedTime = new DateTime(int.Parse(Year), Month, Day);
            DataBase.outputMeth1 LookForDay_out = DrawRows;
            SetContentView(Resource.Layout.DayActRet);
            TextView Header = (TextView)FindViewById(Resource.Id.TextViev1_LookForDay_Req);
            Header.Text = "Âèòðàòè çà " + requestedTime.ToShortDateString();
            DataBase.LookFor(requestedTime, LookForDay_out);
            TextView SumView = (TextView)FindViewById(Resource.Id.TextViev1_LookForDay_Sum);
            SumView.Text = "Âèòðàòè çà öåé äåíü ñòàíîâëÿòü: " + DataBase.DaySum(requestedTime).ToString() + " ãðí";
        }

        private void DrawRows(string description, string price)
        {
            TableLayout Info = (TableLayout)FindViewById(Resource.Id.Table_Day);
            //Info.SetBackgroundResource(Resource.Drawable.rectangle);
            //TableRow row = new TableRow(this);
            //TableRow.LayoutParams rowPar = new TableRow.LayoutParams(TableRow.LayoutParams.WrapContent);
            //row.LayoutParameters = rowPar;
            //TextView col1 = new TextView(this);
            //col1.Text = description;
            //TextView col2 = new TextView(this);
            //col2.Text = price;
            //row.AddView(col1);
            //row.AddView(col2);
            //Info.AddView(row);
            LayoutInflater inflator = LayoutInflater.From(this);
            TableRow row = (TableRow)inflator.Inflate(Resource.Layout.RowTemplate, null);
            TextView FirstCol = (TextView)row.FindViewById(Resource.Id.descCol);
            TextView SecondCol = (TextView)row.FindViewById(Resource.Id.priceCol);
            FirstCol.Text = description;
            FirstCol.Gravity = GravityFlags.CenterHorizontal;
            SecondCol.Text = price;
            Info.AddView(row);
        }
    }
}

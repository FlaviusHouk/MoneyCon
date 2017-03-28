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
    [Activity(Label = "AllCosts")]
    public class AllCosts : Activity
    {
        Bundle savedInstanse;
        TableLayout Info;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            savedInstanse = savedInstanceState;
            SetContentView(Resource.Layout.AllCosts);
            TextView Header = (TextView)FindViewById(Resource.Id.TextViev1_All);
            Header.Text = "Âñ³ âèäàòêè";
            Info = (TableLayout)FindViewById(Resource.Id.Table_All);
            DataBase.ReadRec(DrawRows);
            FirstRowDecoration();
        }

        private void FirstRowDecoration()
        {
            Info.GetChildAt(0).FindViewById<TextView>(Resource.Id.yearCol).Gravity = GravityFlags.Center;
            Info.GetChildAt(0).FindViewById<TextView>(Resource.Id.priceCol).Gravity = GravityFlags.Center;
            Info.GetChildAt(0).FindViewById<TextView>(Resource.Id.descCol).Gravity = GravityFlags.Center;
            Info.GetChildAt(0).FindViewById<TextView>(Resource.Id.yearCol).SetPadding(0, 0, 0, 0);
            Info.GetChildAt(0).FindViewById<TextView>(Resource.Id.descCol).SetPadding(0, 0, 0, 0);
        }

        private void DrawRows(string date, string price, string description)
        {
            LayoutInflater inflator = LayoutInflater.From(this);
            if (Info.GetChildAt(0) != null)
            {
                LayoutInflater sep = LayoutInflater.From(this);
                TableRow rowSep = (TableRow)sep.Inflate(Resource.Layout.separator, null);
                Info.AddView(rowSep);
            }
            TableRow row = (TableRow)inflator.Inflate(Resource.Layout.RowTemplate2, null);
            TextView LastCol = (TextView)row.FindViewById(Resource.Id.descCol);
            TextView PreLastCol = (TextView)row.FindViewById(Resource.Id.priceCol);
            TextView FirstCol = (TextView)row.FindViewById(Resource.Id.yearCol);
            LastCol.Text = description;
            PreLastCol.Text = price;
            FirstCol.Text = date;
            row.Clickable = true;
            row.LongClick += DeleteRowHandler;
            Info.AddView(row);
        }

        private void DeleteRowHandler(object sender, View.LongClickEventArgs e)
        {
            AlertDialog.Builder deleteDial = new AlertDialog.Builder(this);
            TextView date = (TextView)((TableRow)sender).GetChildAt(0);
            TextView descr = (TextView)((TableRow)sender).GetChildAt(2);
            TextView price = (TextView)((TableRow)sender).GetChildAt(4);
            deleteDial.SetTitle("Âè ä³éñíî õî÷åòå âèäàëèòè çàïèñ " + descr.Text + "?");
            deleteDial.SetPositiveButton("Òàê", (innSender, innArr) => 
            {
                DateTime tempo = DateTime.Parse(date.Text);
                DataBase.Delete(tempo.ToShortDateString(), price.Text, descr.Text);
                this.OnCreate(savedInstanse);
            });
            deleteDial.SetNegativeButton("Í³", (innSender, innArr) => { });
            deleteDial.Create().Show();
        }
    }
}

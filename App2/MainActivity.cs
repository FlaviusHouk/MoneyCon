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

﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace App2
{
    [Activity(Label = "Проект")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var metrics = Resources.DisplayMetrics;
            SetContentView(Resource.Layout.Start);
            LinearLayout startButLay = FindViewById<LinearLayout>(Resource.Id.StartButLay);
            LinearLayout.LayoutParams SBLPar = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            SBLPar.TopMargin = (int)(metrics.HeightPixels * 0.75);
            startButLay.LayoutParameters = SBLPar;
            Button button = FindViewById<Button>(Resource.Id.button1);
            button.Click += DrawMainWindow;
        }
        private int ConvertPixelsToDp(float pixelValue)
        {
            var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
            return dp;
        }
        private int ConvertDPToPixels(float DPValue)
        {
            var dp = (int)((DPValue) * Resources.DisplayMetrics.Density);
            return dp;
        }

        private void DrawMainWindow(object e, EventArgs args)
        {
            //base.OnBackPressed();
            StartActivity(typeof(Resources.MenuActivity));
        }
    }
}


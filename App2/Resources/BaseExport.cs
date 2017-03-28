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
    [Activity(Label = "Ôàéëîâèé ïðîâ³äíèê")]
    public class FileExplorerExp : Activity
    {
        static int count = 0;
        static string path;
        private void SetPath()
        {
            var pathfile = Android.OS.Environment.ExternalStorageDirectory;
            path = pathfile.AbsolutePath;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FileExplorer);
            SetPath();
            Update();
            Button back = FindViewById<Button>(Resource.Id.backButt);
            back.Click += BackButtHandler;
            Button export = FindViewById<Button>(Resource.Id.chooseButt);
            export.Click += Export;
        }

        private void BackButtHandler(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        private void Export(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            string dbpath = DataBase.GetStartPath() + "/localbase.db";
            System.IO.File.Copy(dbpath, path + "/localbase " + now.ToShortDateString() + ".db");
            AlertDialog.Builder dial = new AlertDialog.Builder(this);
            dial.SetMessage("Áàçó áóëî åêñïîðòîâàíî");
            dial.SetPositiveButton("OK", (inSender, inE) => { });
        }

        private void BuildTree(string files)
        {
            if (files[0] == '.' && files[1] != '.')
            {
                return;
            }
            TableLayout browserTable = FindViewById<TableLayout>(Resource.Id.TableBrow);
            LayoutInflater inflator = LayoutInflater.From(this);
            TableRow row = (TableRow)inflator.Inflate(Resource.Layout.RowTemplate3, null);
            Button but = (Button)row.FindViewById(Resource.Id.browserElement);
            but.SetWidth(Resources.DisplayMetrics.WidthPixels);
            LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(-1, -2);
            row.LayoutParameters = param;
            but.Text = files;
            if (count == 0)
            {
                but.Click += ToUpperLevel;
            }
            else
            {
                but.Click += InTo;
            }
            browserTable.AddView(row);
            count++;
        }

        private void Update()
        {
            if (count != 0)
            {
                CleanTable();
            }
            var Efiles = System.IO.Directory.EnumerateDirectories(path);
            string[] files = Efiles.ToArray<string>();
            if (path != "/")
            {
                BuildTree("..");
            }
            for (int i = 0; i < files.Length; i++)
            {
                BuildTree(System.IO.Path.GetFileName(files[i]));
            }
            Efiles = System.IO.Directory.EnumerateFiles(path);
            files = Efiles.ToArray<string>();
            for (int i = 0; i < files.Length; i++)
            {
                BuildTree(System.IO.Path.GetFileName(files[i]));
            }
        }

        private void ToUpperLevel(object e, EventArgs args)
        {
            GoOut();
        }
        private void InTo(object e, EventArgs args)
        {
            GoIn(((Button)e).Text);
        }
        private void GoOut()
        {
            path = System.IO.Path.GetDirectoryName(path);
            Update();
        }

        private void GoIn(string additional)
        {
            try
            {
                path = System.IO.Path.Combine(path, additional);
                if (System.IO.File.Exists(path))
                {
                    GoOut();
                }
                Update();
            }
            catch (UnauthorizedAccessException)
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                dialog.SetMessage("Âè íå ìàºòå äîçâîëó äëÿ ïåðåãëÿäó ö³º¿ ïàïêè");
                dialog.SetPositiveButton("Äîáðå", (sender, ar) => { GoOut(); });
                dialog.Create().Show();
            }
        }

        private void CleanTable()
        {
            count = 0;
            TableLayout tab = FindViewById<TableLayout>(Resource.Id.TableBrow);
            tab.RemoveAllViews();
        }
    }
}

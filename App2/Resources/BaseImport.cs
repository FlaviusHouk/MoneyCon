using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App2.Resources
{
    [Activity(Label = "Файловий провідник")]
    public class FileExplorerImp : Activity
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
        }

        private void BackButtHandler(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        private void Import(object sender, EventArgs e)
        {
            Button send = (Button)sender;
            string tpass = path + "/" + send.Text;
            bool st1 = System.IO.File.Exists(tpass);
            bool st2 = send.Text.Contains(".db");
            if (st1 && st2)
            {
                path = path + "/" + send.Text;
                string dbpath = DataBase.GetStartPath() + "/localbase.db";
                if (!File.Exists(DataBase.GetStartPath() + "/oldBase.db") && File.Exists(DataBase.GetStartPath() + "/localbase.db"))
                {
                    File.Copy(dbpath, DataBase.GetStartPath() + "/oldBase.db");
                }
                else if (File.Exists(DataBase.GetStartPath() + "/oldBase.db") && File.Exists(DataBase.GetStartPath() + "/localbase.db"))
                {
                    File.Delete(DataBase.GetStartPath() + "/oldBase.db");
                    File.Copy(dbpath, DataBase.GetStartPath() + "/oldBase.db");
                }
                DataBase.Close();
                System.IO.File.Delete(dbpath);
                System.IO.File.Copy(path, dbpath);
                DataBase.Open();
            }
            //string dbpath = DataBase.GetStartPath() + "/localbase.db";
            //System.IO.File.Copy(dbpath, path + "/localbase " + now.ToShortDateString() + ".db");
            AlertDialog.Builder dial = new AlertDialog.Builder(this);
            dial.SetMessage("Базу було імпортовано");
            dial.SetPositiveButton("OK", (inSender, inE) => { OnBackPressed(); });
            dial.Create().Show();
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
            but.LongClick += Import;
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
                dialog.SetMessage("Ви не маєте дозволу для перегляду цієї папки");
                dialog.SetPositiveButton("Добре", (sender, ar) => { GoOut(); });
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
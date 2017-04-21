using System;
using System.Collections.Generic;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Graphics;
using Android.Widget;

namespace App2.Resources
{
    class TagSpinnerWrapper : TextView
    {
        public override string ToString()
        {
            return base.Text;
        }
        public TagSpinnerWrapper(Context cont, Typeface typeface) : base(cont)
        {
            Typeface = typeface;
        }
    }

    [Activity(Label = "AddRecAct")]
    public class AddRecAct : Activity
    {
        Typeface boldFont;
        Typeface mediumFont;
        Typeface lightFont;
        private string actName;
        private double price;
        private DateTime date = DateTime.Now;
        private Spinner comboBox;
        private List<TagSpinnerWrapper> tags;
        bool start;
        DatePickerFragment picker;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            boldFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Bold.otf");
            mediumFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Medium.otf");
            lightFont = Typeface.CreateFromAsset(Assets, "Fonts/Exo_2_Light.otf");
            base.OnCreate(savedInstanceState);
            start = true;
            tags = new List<TagSpinnerWrapper>();
            SetContentView(Resource.Layout.AddRecLay);
            UseFonts();
            //DefaultDateValue();
            Button datePickerButton = FindViewById<Button>(Resource.Id.SetDateBut);
            datePickerButton.Click += StartPicker;
            Button AddBut = (Button)FindViewById(Resource.Id.Add);
            AddBut.Click += AddRecHand;
            DataBase.ReadTags(BuildTagCol);
            TagSpinnerWrapper text = new TagSpinnerWrapper(this, mediumFont);
            text.Text = "Додати Тег";
            tags.Add(text);
            comboBox = new NormalSpinner(this);
            ArrayAdapter<TagSpinnerWrapper> someAdapter = new ArrayAdapter<TagSpinnerWrapper>(this, Resource.Layout.ComboBoxElement, tags);
            someAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            comboBox.Adapter = someAdapter;
            comboBox.ItemSelected += CheckItem;
            LinearLayout SpinnerLay = FindViewById<LinearLayout>(Resource.Id.SpinnerLayout);
            SpinnerLay.AddView(comboBox, new ViewGroup.LayoutParams(-1, -1));
            Button addTemp = FindViewById<Button>(Resource.Id.AddTemp);
            addTemp.Click += AddTemplateHand;
            Button addAsTemp = FindViewById<Button>(Resource.Id.AddTemplate);
            addAsTemp.Click += AddAsTempHand;
            picker = DatePickerFragment.NewInstanse(SetDate);
        }

        private void SetDate(DateTime fromPiker)
        {
            date = fromPiker;
        }

        private void StartPicker(object sender, EventArgs e)
        {
            picker.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void UseFonts()
        {
            FindViewById<TextView>(Resource.Id.TextViev1).Typeface = boldFont;
            FindViewById<TextView>(Resource.Id.TextViev2).Typeface = mediumFont;
            FindViewById<EditText>(Resource.Id.ActName).Typeface = lightFont;
            FindViewById<TextView>(Resource.Id.TextViev3).Typeface = mediumFont;
            FindViewById<EditText>(Resource.Id.ActPrice).Typeface = lightFont;
        }

        private void AddAsTempHand(object sender, EventArgs e)
        {
            StartActivity(typeof(TemplateScreen));
        }

        private void AddTemplateHand(object sender, EventArgs e)
        {
            EditText name = (EditText)FindViewById(Resource.Id.ActName);          
            EditText priceField = (EditText)FindViewById(Resource.Id.ActPrice);
            if (name.Text.Length == 0 || priceField.Text.Length == 0)
            {
                ShowErrorMsg();
                return;
            }
            price = double.Parse(priceField.Text);
            if (((TagSpinnerWrapper)comboBox.SelectedItem).Text != "Додати тег")
            {
                DataBase.AddTemp(price.ToString(), name.Text, ((TagSpinnerWrapper)comboBox.SelectedItem).Text);
            }
            else
            {
                DataBase.AddTemp(price.ToString(), name.Text, "ЗВ");
            }
            AlertDialog.Builder dial = new AlertDialog.Builder(this);
            dial.SetTitle("Додавання шаблону");
            dial.SetMessage("Видаток " + actName +  " додано як шаблон.");
            dial.SetPositiveButton("OK", (senderAlert, ar) =>
            {

            });
            dial.SetCancelable(true);
            dial.Create().Show();
        }

        private void ShowErrorMsg()
        { 
            AlertDialog.Builder erroeMsg = new AlertDialog.Builder(this);
            erroeMsg.SetTitle("Ой...");
            erroeMsg.SetMessage("Ви помилилися з введенням даних");
            erroeMsg.SetPositiveButton("OK", (senderAlert, ar) => {});
            erroeMsg.SetCancelable(true);
            erroeMsg.Create().Show();
        }

        private void CheckItem(object sender, EventArgs e)
        {
            string type = ((TagSpinnerWrapper)((NormalSpinner)sender).SelectedItem).Text;
            if (!start && type == "Додати Тег")
            {
                StartActivity(typeof(AddTagAct));
            }
            start = false;
        }

        private void BuildTagCol(string tag)
        {
            TagSpinnerWrapper text = new TagSpinnerWrapper(this, mediumFont);
            text.Text = tag;
            tags.Add(text);
        }

        protected void AddRecHand(object e, EventArgs args)
        {
            EditText name = (EditText)FindViewById(Resource.Id.ActName);
            actName = name.Text;
            EditText priceField = (EditText)FindViewById(Resource.Id.ActPrice);
            if (name.Text.Length == 0 || priceField.Text.Length == 0)
            {
                ShowErrorMsg();
                return;
            }
            price = double.Parse(priceField.Text);
            if (((TagSpinnerWrapper)comboBox.SelectedItem).Text != "Додати тег")
            {
                DataBase.AddRec(date.ToShortDateString(), price.ToString(), actName, ((TagSpinnerWrapper)comboBox.SelectedItem).Text);
            }
            else
            {
                DataBase.AddRec(date.ToShortDateString(), price.ToString(), actName, "ЗВ");
            }
            AlertDialog.Builder dial = new AlertDialog.Builder(this);
            dial.SetTitle("Додавання витрати");
            dial.SetMessage("Видаток " + actName + " зроблений " + date.ToShortDateString() + " додано.");
            dial.SetPositiveButton("OK", (senderAlert, ar) => 
            {
                priceField.Text = String.Empty;
                name.Text = String.Empty;
            });
            dial.SetCancelable(true);
            dial.Create().Show();
        }

       

        /*private void DefaultDateValue()
        {
            EditText Year = (EditText)FindViewById(Resource.Id.ActYear);
            EditText Month = (EditText)FindViewById(Resource.Id.ActMonth);
            EditText Day = (EditText)FindViewById(Resource.Id.ActDay);
            int year = curTime.Year;
            Year.Text = year.ToString();
            Month.Text = curTime.Month.ToString();
            Day.Text = curTime.Day.ToString();
        }*/
    }
}
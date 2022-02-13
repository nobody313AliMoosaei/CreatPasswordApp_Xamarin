using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.AppCompat.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CreatPasswordApp.DataBase;

namespace CreatPasswordApp
{
    [Activity(Label = "ساخت اکانت", NoHistory = true)]
    public class Activity_CreatNewAccount : AppCompatActivity
    {
        public EditText ETuserName, ETPassword, ETdescription, ETProtal;
        public Button btn_Submit;
        string password = string.Empty;
        public Database db = new Database();
        public int Key = -1;
        public Spinner spinner;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.layout_creatNewAccount);

            Key = Intent.GetIntExtra("KEY_GETMODELDATA", -1);
            password = Intent.GetStringExtra("KEY_PASSWORD");
            ArrayAdapter adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Item_spinnerForType, Android.Resource.Layout.SimpleSpinnerDropDownItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            // Set widget
            btn_Submit = FindViewById<Button>(Resource.Id.btn_Submit);
            ETuserName = FindViewById<EditText>(Resource.Id.editText_UserName);
            ETPassword = FindViewById<EditText>(Resource.Id.editText_Password);
            ETProtal = FindViewById<EditText>(Resource.Id.editText_Protal);
            spinner = FindViewById<Spinner>(Resource.Id.spinner_Type);
            ETdescription = FindViewById<EditText>(Resource.Id.editText_Description);
            spinner.Adapter = adapter;
            if (Key == -1)
            {
                ETPassword.Text = password;
            }
            else
            {
                var data = db.Get_Item(Key);
                ETuserName.Text = data.UserName;
                ETPassword.Text = data.Password;
                ETProtal.Text = data.Protal;
                ETdescription.Text = data.Description;
                spinner.SetSelection((int)data.SpinnerID);
            }
            spinner.ItemSelected += Spinner_ItemSelected;
            btn_Submit.Click += Btn_Submit_Click;

        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            long id = e.Id;
            if ((int)SpinnerItem.Instagram == id)
            {
                ETProtal.Text = "Instagram";
            }
            else if ((int)SpinnerItem.Whatsapp == id)
            {
                ETProtal.Text = "Whatsapp";
            }
            else if ((int)SpinnerItem.Facebook == id)
            {
                ETProtal.Text = "Facebook";
            }
            else if ((int)SpinnerItem.Telegram == id)
            {
                ETProtal.Text = "Telegram";
            }
            else if ((int)SpinnerItem.Twitter == id)
            {
                ETProtal.Text = "Twitter";
            }
            else if ((int)SpinnerItem.Skype == id)
            {
                ETProtal.Text = "Skype";
            }
            else if ((int)SpinnerItem.Gmail == id)
            {
                ETProtal.Text = "gmail";
            }
            else if ((int)SpinnerItem.Email == id)
            {
                ETProtal.Text = "Email";
            }
            else if ((int)SpinnerItem.TikTok == id)
            {
                ETProtal.Text = "Tik Tok";
            }
            else if ((int)SpinnerItem.YouTube == id)
            {
                ETProtal.Text = "YouTube";
            }
            else if ((int)SpinnerItem.Persian_Website == id)
            {
                ETProtal.Text = "Persian Web";
            }
            else if ((int)SpinnerItem.English_Website == id)
            {
                ETProtal.Text = "English Web";
            }
            else if ((int)SpinnerItem.Other == id)
            {
                ETProtal.Text = "Others ...";
            }
        }

        private async void Btn_Submit_Click(object sender, EventArgs e)
        {
            if (Key == -1)
            {
                if (string.IsNullOrEmpty(ETProtal.Text))
                {
                    ETProtal.Error = "این قسمت را پر کنید";
                    Toast.MakeText(this, "دوباره تلاش کنید", ToastLength.Short).Show();
                }
                else
                {
                    if (password != ETPassword.Text)
                    {
                        Toast.MakeText(this, "شما رمز را تغییر داده اید!!!", ToastLength.Short).Show();
                        await System.Threading.Tasks.Task.Delay(200);
                    }
                    ModelData model = new ModelData()
                    {
                        UserName = ETuserName.Text,
                        Password = ETPassword.Text,
                        Description = ETdescription.Text,
                        Protal = ETProtal.Text,
                        SpinnerID = (int)spinner.SelectedItemId
                    };
                    db.Insert(model);
                    Toast.MakeText(this, "ذخیره شد!!", ToastLength.Short).Show();
                    Finish();
                }
            }
            else
            {
                var data = db.Get_Item(Key);
                data.UserName = ETuserName.Text;
                data.Password = ETPassword.Text;
                data.Description = ETdescription.Text;
                data.Protal = ETProtal.Text;
                data.SpinnerID = (int)spinner.SelectedItemId;
                db.UpdateItem(data);
                Toast.MakeText(this, "بروزرسانی شد", ToastLength.Short).Show();
                Finish();
            }
        }
    }
}
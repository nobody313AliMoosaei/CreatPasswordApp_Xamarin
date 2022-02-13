using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using System;
using System.Collections.Generic;
using Android.Views;

namespace CreatPasswordApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true,Icon ="@mipmap/app_Icon")]
    public class MainActivity : AppCompatActivity
    {

        public Button btnCreat, btnCreatNewAccount;
        public EditText txtNumber;
        public TextView txtShow;
        public CheckBox Checkbox_UpperCase, Checkbox_LowerCase, Checkbox_Number, Checkbox_SpecialCase, Checkbox_All;
        public ImageButton Button_Copy, Button_Share;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            SetWidget();

            Checkbox_All.Checked = true;

            List<int> Numbers = new List<int>();

            btnCreat.Click += delegate
            {
                //number: 48 - 57
                // Lower Case : 97 - 122
                // Upper case : 65 - 90
                //special case : 32 - 47 , 58 - 64 , 91 - 96 , 123 - 126
                Numbers.Clear();
                if (Checkbox_All.Checked || Checkbox_LowerCase.Checked
                    || Checkbox_UpperCase.Checked || Checkbox_Number.Checked
                    || Checkbox_SpecialCase.Checked)
                {
                    if (Checkbox_All.Checked || (Checkbox_LowerCase.Checked && Checkbox_UpperCase.Checked
                && Checkbox_Number.Checked && Checkbox_SpecialCase.Checked))
                    {
                        txtShow.Text = Password_All();
                    }
                    else
                    {
                        if (Checkbox_LowerCase.Checked)
                        {
                            Password_LowerCase(Numbers);
                        }
                        if (Checkbox_UpperCase.Checked)
                        {
                            Password_UpperCase(Numbers);
                        }
                        if (Checkbox_Number.Checked)
                        {
                            Password_Number(Numbers);
                        }
                        if (Checkbox_SpecialCase.Checked)
                        {
                            Password_specialCase(Numbers);
                        }
                        int n = 0;
                        int.TryParse(txtNumber.Text, out n);
                        if (n != 0)
                        {
                            txtShow.Text = CreatPassword(Numbers, n);
                        }
                        else
                        {
                            Toast.MakeText(this, "تعداد کاراکتر نا معتبر است", ToastLength.Short).Show();
                        }
                    }
                }
                else
                    Toast.MakeText(this, "یکی از گزینه های بالا را انتخاب کنید!", ToastLength.Short).Show();
            };

            Button_Copy.Click += Button_Copy_Click;
            Button_Share.Click += Button_Share_Click;
            btnCreatNewAccount.Click += BtnCreatNewAccount_Click;
        }
        protected override void OnPause()
        {
            ClearWidget();
            base.OnPause();
        }
        private void BtnCreatNewAccount_Click(object sender, EventArgs e)
        {
            Android.Content.Intent intent = new Android.Content.Intent(this,typeof(Activity_CreatNewAccount));
            intent.PutExtra("KEY_PASSWORD", txtShow.Text);
            StartActivity(intent);
        }
        private void Button_Share_Click(object sender, EventArgs e)
        {
            if (!txtShow.Text.Contains("متن رمز"))
                Xamarin.Essentials.Share.RequestAsync(txtShow.Text);
            else
                Toast.MakeText(this, "رمزی وجود ندارد!!", ToastLength.Short).Show();
        }
        private void Button_Copy_Click(object sender, EventArgs e)
        {
            if (!txtShow.Text.Contains("متن رمز"))
            {
                string Password = txtShow.Text;
                Xamarin.Essentials.Clipboard.SetTextAsync(Password);
                Toast.MakeText(this, "رمز کپی شد!!", ToastLength.Short).Show();
            }else
            {
                Toast.MakeText(this, "رمزی وجود ندارد!!", ToastLength.Short).Show();
            }
        }
        private void SetWidget()
        {
            btnCreat = FindViewById<Button>(Resource.Id.btnCreat);
            txtNumber = FindViewById<EditText>(Resource.Id.txtNumber);
            txtShow = FindViewById<TextView>(Resource.Id.txtShoPassword);
            btnCreatNewAccount = FindViewById<Button>(Resource.Id.btnCreatNewAcount);
            Button_Copy = FindViewById<ImageButton>(Resource.Id.Button_Copy);
            Button_Share = FindViewById<ImageButton>(Resource.Id.Button_Share);
            Checkbox_UpperCase = FindViewById<CheckBox>(Resource.Id.checkBox_UpperCase);
            Checkbox_LowerCase = FindViewById<CheckBox>(Resource.Id.checkBox_LowerCase);
            Checkbox_Number = FindViewById<CheckBox>(Resource.Id.checkBox_Number);
            Checkbox_SpecialCase = FindViewById<CheckBox>(Resource.Id.checkBox_SpecialCase);
            Checkbox_All = FindViewById<CheckBox>(Resource.Id.checkBox_All);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_MainLayout, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_List:
                    {
                        StartActivity(typeof(Activity_AllRecord));
                        break;
                    }
                case Resource.Id.action_Exit:
                    {
                        Finish();
                        break;
                    }
            }
            return base.OnOptionsItemSelected(item);
        }
        // Methods
        private string Password_All()
        {
            int n = 0;
            int.TryParse(txtNumber.Text, out n);
            if (n != 0)
            {
                string pas = string.Empty;
                System.Collections.Generic.List<string> Password = new System.Collections.Generic.List<string>();
                Random random = new Random();
                for (int i = 0; i < n; i++)
                {
                    int number = random.Next(32, 126);
                    var charpas = (char)number;
                    pas += charpas.ToString();
                    Password.Add(charpas.ToString());
                }
                return pas;
            }
            else
            {
                txtNumber.Text = "";
                Toast.MakeText(this, "تعداد کاراکتر را دوباره وارد کنید!", ToastLength.Short).Show();
                return string.Empty;
            }
        }
        private void Password_Number(List<int> numbers)
        {
            for (int i = 48; i <= 57; i++)
            {
                numbers.Add(i);
            }
        }
        private void Password_LowerCase(List<int> numbers)
        {
            for (int i = 97; i <= 122; i++)
            {
                numbers.Add(i);
            }
        }
        private void Password_UpperCase(List<int> numbers)
        {
            for (int i = 65; i <= 90; i++)
            {
                numbers.Add(i);
            }
        }
        private void Password_specialCase(List<int> numbers)
        {
            for (int i = 32; i <= 47; i++)
            {
                numbers.Add(i);
            }
            for (int i = 58; i <= 64; i++)
            {
                numbers.Add(i);
            }
            for (int i = 91; i <= 96; i++)
            {
                numbers.Add(i);
            }
            for (int i = 123; i <= 126; i++)
            {
                numbers.Add(i);
            }
        }
        private string CreatPassword(List<int> number, int count)
        {
            string password = string.Empty;
            int len = number.Count;
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {

                int index = random.Next(0, len);
                var pas = (char)number[index];
                password += pas.ToString();
            }
            return password;
        }
        private void ClearWidget()
        {
            txtNumber.Text = "";
            txtShow.Text = "متن رمز";
            Checkbox_All.Checked = true;
            Checkbox_LowerCase.Checked = false;
            Checkbox_Number.Checked = false;
            Checkbox_SpecialCase.Checked = false;
            Checkbox_UpperCase.Checked = false;
        }

    }
}
using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CreatPasswordApp.DataBase;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreatPasswordApp
{
    [Activity(Label = "لیست", NoHistory = true)]
    public class Activity_AllRecord : AndroidX.AppCompat.App.AppCompatActivity
    {
        public ListView listView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.layout_ShowAllRecord);
            listView = FindViewById<ListView>(Resource.Id.listView_AllRecord);
            listView.Adapter = new ListView_Adapter(this);

        }
        public void Refresh(Activity context)
        {
            context.StartActivity(typeof(Activity_AllRecord));
        }

    }
    public class ListView_Adapter : BaseAdapter<DataBase.ModelData>
    {
        private List<DataBase.ModelData> AllList;
        private Activity context;
        private DataBase.Database db;
        public ListView_Adapter(Activity ac)
        {
            AllList = new List<DataBase.ModelData>();
            db = new DataBase.Database();
            AllList = db.GET_All();
            context = ac;
        }

        public override ModelData this[int position] => AllList[position];

        public override int Count => AllList.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        private int GetID_ImageBySpinnerItem(int spinerID)
        {
            switch (spinerID)
            {
                case 0:
                    {
                        return Resource.Mipmap.Instagram_Icon;
                    }
                case 1:
                    {
                        return Resource.Mipmap.Whatsapp_Icon;
                    }
                case 2:
                    {
                        return Resource.Mipmap.Facebook_Icon;
                    }
                case 3:
                    {
                        return Resource.Mipmap.Telegram_Icon;
                    }
                case 4:
                    {
                        return Resource.Mipmap.Twitter_Icon;
                    }
                case 5:
                    {
                        return Resource.Mipmap.Skype_Icon;
                    }
                case 6:
                    {
                        return Resource.Mipmap.Gmail_Icon;
                    }
                case 7:
                    {
                        return Resource.Mipmap.Email_Icon;
                    }
                case 8:
                    {
                        return Resource.Mipmap.YoutTube_Icon;
                    }
                case 9:
                    {
                        return Resource.Mipmap.TikTok_Icon;
                    }
                case 10:
                    {
                        return Resource.Mipmap.PersianWebsite_Icon;
                    }
                case 11:
                    {
                        return Resource.Mipmap.EnglishWebsite_Icon;
                    }
                default:
                    {
                        return Resource.Mipmap.Others_icon;
                    }

            }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var data = AllList[position];
            if (convertView == null)
            {
                convertView = View.Inflate(context, Resource.Layout.layout_ItemRecord, null);
            }
            TextView txtProtal = convertView.FindViewById<TextView>(Resource.Id.txtProtal1);
            TextView txtDescript = convertView.FindViewById<TextView>(Resource.Id.txtDescription);
            Button btn_Delete = convertView.FindViewById<Button>(Resource.Id.btn_Delete);
            Button btn_ShowAllDetail = convertView.FindViewById<Button>(Resource.Id.btn_ShowDetail);
            Button btn_Edit = convertView.FindViewById<Button>(Resource.Id.btn_Edit);
            Button btn_CopyPassword = convertView.FindViewById<Button>(Resource.Id.btn_CopyPassword);
            ImageButton Image_btn = convertView.FindViewById<ImageButton>(Resource.Id.imageButton1);

            btn_Delete.Click += delegate
            {
                db.DeleteItem(data.Id);
                context.StartActivity(typeof(Activity_AllRecord));
            };
            btn_Edit.Click += delegate
           {
               Android.Content.Intent intent = new Intent(context, typeof(Activity_CreatNewAccount));
               intent.PutExtra("KEY_GETMODELDATA", data.Id);
               context.StartActivity(intent);
           };
            btn_ShowAllDetail.Click += delegate
            {
                Dialog dialog = new Dialog(context);
                dialog.SetContentView(Resource.Layout.layout_ShowAllDetails);
                TextView txt_USERNAME = dialog.FindViewById<TextView>(Resource.Id.textView_USERNAME);
                TextView txt_PASSWORD = dialog.FindViewById<TextView>(Resource.Id.textView_PASSWORD);
                TextView txt_PLATFORM = dialog.FindViewById<TextView>(Resource.Id.textView_PLATFORM);
                TextView txt_DESCRIPTION = dialog.FindViewById<TextView>(Resource.Id.textView_DESCRIPTION);

                // set data
                txt_USERNAME.Text = data.UserName;
                txt_PASSWORD.Text = data.Password;
                txt_PLATFORM.Text = data.Protal;
                txt_DESCRIPTION.Text = data.Description;
                dialog.SetCancelable(true);
                dialog.SetTitle("مشاهده جزئیات ");
                dialog.Show();
            };
            btn_CopyPassword.Click += async delegate
            {
                await Xamarin.Essentials.Clipboard.SetTextAsync(data.Password);
                Toast.MakeText(context, "رمز کپی شد", ToastLength.Short).Show();
            };


            // set data in Item in ListView
            string DesCript = string.Format("نام کاربری : {0} \nپلتفرم : {1}\nتوضیحات : {2}", data.UserName, data.Protal, data.Description);
            txtDescript.Text = DesCript;
            txtProtal.Text = data.Protal;
            Image_btn.SetImageResource(GetID_ImageBySpinnerItem(data.SpinnerID));

            return convertView;
        }
    }
}
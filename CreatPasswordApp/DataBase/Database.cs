using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace CreatPasswordApp.DataBase
{
    public class ModelData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        [NotNull]
        public string Protal { set; get; }
        public string Description { set; get; }
        public int SpinnerID { set; get; }
        /*
        public SpinnerItem GetSpinnerItem(int SpinerID)
        {
            switch (SpinerID)
            {
                case 0:
                    return SpinnerItem.Instagram;
                case 1:
                    return SpinnerItem.Whatsapp;
                case 2:
                    return SpinnerItem.Facebook;
                case 3:
                    return SpinnerItem.Telegram;
                case 4:
                    return SpinnerItem.GooglePlus;
                case 5:
                    return SpinnerItem.Skype;
                case 6:
                    return SpinnerItem.LinkedIn;
                case 7:
                    return SpinnerItem.YouTube;
                case 8:
                    return SpinnerItem.TikTok;
                case 9:
                    return SpinnerItem.Persian_Website;
                default:
                    {
                        return SpinnerItem.English_Website;
                    }
            }
        }
    */
        }

    public class Database
    {
        private SQLiteConnection database;
        private string NameDatabase = "DB_CREATPASSWORD.db3";
        public Database()
        {
            var name = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var FullNamedb = System.IO.Path.Combine(name, NameDatabase);
            database = new SQLiteConnection(FullNamedb, true);
            database.CreateTable<ModelData>();
        }
        public void Insert(ModelData item)
        {
            database.Insert(item);
        }
        public bool DeleteItem(int id)
        {
            var item = database.Table<ModelData>().SingleOrDefault(t => t.Id == id);
            if (item != null)
            {
                database.Delete(item);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteItem(ModelData item)
        {
            if (item != null)
            {
                database.Delete(item.Id);
                return true;
            }
            return false;
        }
        public void Delete_All()
        {
            database.DeleteAll<ModelData>();
        }
        public void UpdateItem(ModelData item)
        {
            database.Update(item);
        }
        public ModelData Get_Item(int id)
        {
            return database.Table<ModelData>().SingleOrDefault(t => t.Id == id);
        }
        public ModelData Get_Item(ModelData item)
        {
            return database.Table<ModelData>().SingleOrDefault(test => test == item);
        }
        public List<ModelData> GET_All()
        {
            return database.Table<ModelData>().ToList();
        }
    }
}
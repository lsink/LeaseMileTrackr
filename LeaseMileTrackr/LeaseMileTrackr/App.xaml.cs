using System;
using System.IO;
using Xamarin.Forms;
using LeaseMileTrackr.Data;

namespace LeaseMileTrackr
{
    public partial class App : Application
    {
        static VehicleDatabase database;

        public static VehicleDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new VehicleDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Vehicles.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new VehiclesPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

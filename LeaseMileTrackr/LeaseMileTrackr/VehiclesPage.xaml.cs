using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using LeaseMileTrackr.Models;

namespace LeaseMileTrackr
{
    public partial class VehiclesPage : ContentPage
    {
        public VehiclesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<Vehicle> vehicleList = await App.Database.GetVehiclesAsync();
            foreach (Vehicle veh in vehicleList)
            {
                veh.CalculatedMiles = CalculatedMiles(veh.LeaseStart, veh.LengthMonths, veh.AllotedMiles, veh.StartMiles);
            }
            listView.ItemsSource = vehicleList;
        }

        async void OnVehicleAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VehicleEntryPage
            {
                BindingContext = new Vehicle()
            });
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new VehicleEditPage
                {
                    BindingContext = e.SelectedItem as Vehicle
                });
            }
        } 

        // Calculates number of miles allowed to date
        private int CalculatedMiles(DateTime StartDate, int Months, int AllotedMiles, int StartingMiles)
        {
            int days = GetDateRange(StartDate, StartDate.AddMonths(Months));
            double totalMilesAllowed = AllotedMiles / 12.0 * Months;
            double milesAllowedPerDay = totalMilesAllowed / days;
            int daysSinceLeaseStart = GetDateRange(StartDate, DateTime.Today);
            int result = (int)(daysSinceLeaseStart * milesAllowedPerDay + StartingMiles);
            return result;
        }

        private int GetDateRange(DateTime StartingDate, DateTime EndingDate)
        {
            if (StartingDate > EndingDate)
            {
                return 0;
            }

            List<DateTime> rv = new List<DateTime>();
            DateTime tmpDate = StartingDate;

            do
            {
                rv.Add(tmpDate);
                tmpDate = tmpDate.AddDays(1);
            } while (tmpDate <= EndingDate);
            return rv.Count;
        }
    }


}
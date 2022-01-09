using System;
using Xamarin.Forms;
using LeaseMileTrackr.Models;
using System.Collections.Generic;

namespace LeaseMileTrackr
{
    public partial class VehicleEntryPage : ContentPage
    {
        public VehicleEntryPage()
        {
            InitializeComponent();
            PickerYearList();
        }

        void PickerYearList()
        {
            List<String> yearList = new List<String>();
            for (int i = -1; i < 10; i++)
            {
                int curr = DateTime.Now.Year;
                yearList.Add((curr - i).ToString());
            }
            Year.ItemsSource = yearList;
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {

            Vehicle vehicle = new Vehicle();
            vehicle.Year = Year.SelectedItem.ToString();
            vehicle.Make = Make.Text;
            vehicle.Model = Model.Text;
            vehicle.LeaseStart = LeaseStart.Date;
            vehicle.LengthMonths = int.Parse(LeaseLengthMonths.Text);
            vehicle.StartMiles = int.Parse(LeaseStartMiles.Text);
            vehicle.AllotedMiles = int.Parse(LeaseAllotedMiles.Text);

            await App.Database.SaveVehicleAsync(vehicle);
            await Navigation.PopAsync();
        }

        /*
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var vehicle = (Vehicle)BindingContext;
            await App.Database.DeleteVehicleAsync(vehicle);
            await Navigation.PopAsync();
        }
        */
    }

    public class NumericValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            int result;
            bool isValid = int.TryParse(args.NewTextValue, out result);
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }
    }
}
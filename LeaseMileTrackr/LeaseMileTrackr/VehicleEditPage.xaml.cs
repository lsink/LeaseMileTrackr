using System;
using Xamarin.Forms;
using LeaseMileTrackr.Models;

namespace LeaseMileTrackr
{
    public partial class VehicleEditPage : ContentPage
    {
        public VehicleEditPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            Vehicle vehicle = (Vehicle)BindingContext;
            vehicle.Year = Year.Text;
            vehicle.Make = Make.Text;
            vehicle.Model = Model.Text;
            vehicle.LeaseStart = LeaseStart.Date;
            vehicle.LengthMonths = int.Parse(LeaseLengthMonths.Text);
            vehicle.StartMiles = int.Parse(LeaseStartMiles.Text);
            vehicle.AllotedMiles = int.Parse(LeaseAllotedMiles.Text);

            await App.Database.SaveVehicleAsync(vehicle);
            await Navigation.PopAsync();
        }

        
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            bool response = await DisplayAlert("Delete?", "Are you sure you want to delete this vehicle?", "Delete", "Cancel");
            if (response)
            {
                var vehicle = (Vehicle)BindingContext;
                await App.Database.DeleteVehicleAsync(vehicle);
                await Navigation.PopAsync();
            }
            
        }
        
    }
    /*
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
    */
}
﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using ADLApp.ViewModel;
using ADLApp.Models;

namespace ADLApp.Views
{
    public partial class HomePage : ContentPage
    {
        private readonly IScanner _qrScanner = new QRScanner();
        private readonly IAssignmentLoader _assignmentLoader = new RequestManager();
        private readonly ILocationLoader _locationLoader = new RequestManager();
        private List<Location> _locations;
        public HomePage()
        {
            InitializeComponent();
            LoadLocations();
        }

        private async void LoadLocations()
        {
            _locations = await _locationLoader.GetLocations("Locationlist");
        }

        private async void OnScanButtonClicked(object sender, EventArgs e)
        {
            ScanButton.IsEnabled = false;
            string scanString = await _qrScanner.ScanAndGetString();
            if (scanString != "" && scanString != "error")
            {
                string[] strings = scanString.Split(';');
                Assignment currentassignment = await _assignmentLoader
                    .GetAssignment(strings[1]);
                if (currentassignment != null)
                {
                    if (currentassignment is MultipleChoiceAssignment)
                    {
                        SolvePage nextPage = new SolvePage(currentassignment as MultipleChoiceAssignment);
                        await Navigation.PushAsync(nextPage);
                    }
                    else
                    {
                        SolvePage nextPage = new SolvePage(currentassignment);
                        await Navigation.PushAsync(nextPage);
                    }
                }
                else
                {
                    await DisplayAlert("Kode er ikke koblet på opgave", "Koden har ikke en opgave", "Prøv igen");
                }
            }
            else if (scanString == "error")
            {
                await DisplayAlert("Fejl ved scanning af opgaver", "Det er ikke en adl qr kode", "Prøv med en anden");
            }
            ScanButton.IsEnabled = true;
        }

        private async void OnFindButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FindQrPage(_locations));
        }
    }
}
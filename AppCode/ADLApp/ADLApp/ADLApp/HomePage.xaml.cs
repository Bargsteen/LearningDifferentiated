﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ADLApp
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }
        private async void OnScanButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SolvePage());
        }
    }
}

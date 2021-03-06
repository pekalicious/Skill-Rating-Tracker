﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using Microcharts.Forms;
using Pekalicious.SrTracker.Core;
using Pekalicious.SrTracker.Models;
using Pekalicious.SrTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pekalicious.SrTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameSeasonContentView : ContentView
    {
        public GameSeasonContentViewModel viewModel { get; private set; }
        public GameSeasonContentView()
        {
            InitializeComponent();
            BindingContext = viewModel = new GameSeasonContentViewModel();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectGameSeasonPage());
        }

        public async void OnAppearing()
        {
            await viewModel.ExecuteLoadCurrentGameSeasonCommand();
            testChart.Chart = new LineChart
            {
                Entries = viewModel.Entries,
                LineMode = LineMode.Straight,
                PointMode = PointMode.Circle,
                PointSize = 26,
                MinValue = viewModel.MinSr - 100,
                MaxValue = viewModel.MaxSr + 100
            };
        }
    }
}
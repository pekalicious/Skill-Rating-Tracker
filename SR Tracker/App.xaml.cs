﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pekalicious.SrTracker.Services;
using Pekalicious.SrTracker.Views;

namespace Pekalicious.SrTracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<Database>();
            MainPage = new NavigationPage(new MainPageView());
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

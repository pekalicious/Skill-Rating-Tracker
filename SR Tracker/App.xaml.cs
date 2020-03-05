using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SR_Tracker.Services;
using SR_Tracker.Views;

namespace SR_Tracker
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pekalicious.SrTracker.Models;
using Pekalicious.SrTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pekalicious.SrTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartSessionPage : ContentPage
    {
        private StartSessionViewModel viewModel;

        public StartSessionPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new StartSessionViewModel();
        }

        async void StartSessionButton_Clicked(object sender, EventArgs e)
        {
            var db = DependencyService.Get<Database>();
            var lastSeason = await db.AppState.LastUsedSeason();
            await Navigation.PushAsync(new CurrentSessionPage(lastSeason));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.CheckSeason();
        }
    }
}
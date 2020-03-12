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
    public partial class EditGameSeasonPage : ContentPage
    {
        private EditGameSeasonViewModel viewModel;

        public EditGameSeasonPage(GameSeason season = null)
        {
            InitializeComponent();

            BindingContext = viewModel = new EditGameSeasonViewModel(season);
        }


        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            viewModel.Save();
            Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
        }
    }
}
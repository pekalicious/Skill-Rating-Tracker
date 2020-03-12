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
    public partial class SelectGameSeasonPage : ContentPage
    {
        public SelectGameSeasonViewModel viewModel;

        public SelectGameSeasonPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SelectGameSeasonViewModel();
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new EditGameSeasonPage()));
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as GameSeason;
            if (item == null)
                return;

            viewModel.SelectGameSeason(item);

            // Manually deselect playSession.
            ItemsListView.SelectedItem = null;
            await Navigation.PopAsync();
        }

    }
}
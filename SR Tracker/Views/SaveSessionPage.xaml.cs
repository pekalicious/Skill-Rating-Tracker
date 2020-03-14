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
    public partial class SaveSessionPage : ContentPage
    {
        private SaveSessionViewModel viewModel;

        public SaveSessionPage(PlaySession session, GameSeason currentSeason)
        {
            InitializeComponent();

            BindingContext = viewModel = new SaveSessionViewModel(session, currentSeason);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.SaveLastPlaySessionCommand.Execute(null);
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            try
            {
                int newSeasonHigh = int.Parse(NewSeasonHigh.Text);
                await viewModel.UpdateSeasonHigh(newSeasonHigh);
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception);
            }

            await Navigation.PopModalAsync();
        }
    }
}
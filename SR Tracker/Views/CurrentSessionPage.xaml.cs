using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Pekalicious.SrTracker.Models;
using Pekalicious.SrTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pekalicious.SrTracker.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class CurrentSessionPage : ContentPage
    {
        private CurrentSessionViewModel viewModel;

        public CurrentSessionPage(GameSeason currentSeason)
        {
            InitializeComponent();

            BindingContext = viewModel = new CurrentSessionViewModel(currentSeason);
            viewModel.StartNewSession();
        }

        async void Win_Clicked(object sender, EventArgs e)
        {
            viewModel.RecordWin();
        }
        
        async void Draw_Clicked(object sender, EventArgs e)
        {
            viewModel.Draw();
        }

        async void Loss_Clicked(object sender, EventArgs e)
        {
            viewModel.Loss();
        }

        async void Undo_Clicked(object sender, EventArgs e)
        {
            viewModel.Undo();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            Navigation.RemovePage(Navigation.NavigationStack.Last());
            await Navigation.PushModalAsync(new SaveSessionPage(viewModel.EndSession()));
        }

    }
}
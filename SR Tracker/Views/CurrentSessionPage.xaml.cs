using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microcharts;
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
            UpdateChart();
        }
        
        async void Draw_Clicked(object sender, EventArgs e)
        {
            viewModel.Draw();
            UpdateChart();
        }

        async void Loss_Clicked(object sender, EventArgs e)
        {
            viewModel.Loss();
            UpdateChart();
        }

        async void Undo_Clicked(object sender, EventArgs e)
        {
            viewModel.Undo();
            UpdateChart();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            Navigation.RemovePage(Navigation.NavigationStack.Last());
            await Navigation.PushAsync(new SaveSessionPage(viewModel.EndSession(), viewModel.CurrentGameSeason));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            UpdateChart();
        }

        private void UpdateChart()
        {
            testChart.Chart = new LineChart()
            {
                Entries = viewModel.Entries,
                LineMode = LineMode.Straight,
                PointMode = PointMode.Square,
            };
        }
    }
}
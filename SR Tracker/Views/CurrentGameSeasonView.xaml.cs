using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pekalicious.SrTracker.Core;
using Pekalicious.SrTracker.Models;
using Pekalicious.SrTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pekalicious.SrTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentGameSeasonView : ContentView
    {
        private CurrentGameSeasonViewModel viewModel;
        public CurrentGameSeasonView()
        {
            InitializeComponent();
            BindingContext = viewModel = new CurrentGameSeasonViewModel();
            viewModel.LoadCurrentGameSeasonCommand.Execute(null);
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectGameSeasonPage());
        }
    }
}
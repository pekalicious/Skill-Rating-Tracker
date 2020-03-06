using System;
using System.ComponentModel;
using Pekalicious.SrTracker.Models;
using Pekalicious.SrTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pekalicious.SrTracker.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        private CurrentSessionViewModel viewModel;

        public AboutPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CurrentSessionViewModel();
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
            viewModel.EndSession();
        }

        async void New_Clicked(object sender, EventArgs e)
        {
            viewModel.StartNewSession();
        }
    }
}
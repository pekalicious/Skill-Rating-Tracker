using System;
using System.ComponentModel;
using SR_Tracker.Models;
using SR_Tracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SR_Tracker.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        private AboutViewModel viewModel;

        public AboutPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new AboutViewModel();
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

        }

        async void New_Clicked(object sender, EventArgs e)
        {
            viewModel.Reset();
        }
    }
}
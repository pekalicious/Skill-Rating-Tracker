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

        public SaveSessionPage(PlaySession session)
        {
            InitializeComponent();

            BindingContext = viewModel = new SaveSessionViewModel(session);
        }
    }
}
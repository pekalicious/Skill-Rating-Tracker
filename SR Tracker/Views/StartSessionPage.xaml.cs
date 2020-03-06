using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pekalicious.SrTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartSessionPage : ContentPage
    {
        public StartSessionPage()
        {
            InitializeComponent();
        }

        async void StartSessionButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CurrentSessionPage());
        }
    }
}
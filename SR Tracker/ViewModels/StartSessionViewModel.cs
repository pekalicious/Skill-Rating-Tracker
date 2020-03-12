using System;
using System.Collections.Generic;
using System.Text;

namespace Pekalicious.SrTracker.ViewModels
{
    public class StartSessionViewModel : BaseViewModel
    {
        public bool CanStartSession { get; private set; }
        public bool ShowLabel { get; private set; }

        public async void CheckSeason()
        {
            var season = await Database.AppState.LastUsedSeason();
            CanStartSession = season != null;
            ShowLabel = season == null;
            OnPropertyChanged("CanStartSession");
            OnPropertyChanged("ShowLabel");
        }
    }
}

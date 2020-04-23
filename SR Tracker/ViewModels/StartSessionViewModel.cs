using System;
using System.Collections.Generic;
using System.Text;

namespace Pekalicious.SrTracker.ViewModels
{
    public class StartSessionViewModel : BaseViewModel
    {
        public bool CanStartSession { get; private set; } = false;
        public bool ShowLabel => !CanStartSession;

        public async void CheckSeason()
        {
            var season = await Database.User.LastUsedSeason();
            CanStartSession = season.HasItem;
            OnPropertyChanged(nameof(CanStartSession));
            OnPropertyChanged(nameof(ShowLabel));
        }
    }
}

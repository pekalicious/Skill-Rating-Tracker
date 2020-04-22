using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Pekalicious.SrTracker.Core;
using Pekalicious.SrTracker.Models;
using Xamarin.Forms;

namespace Pekalicious.SrTracker.ViewModels
{
    public class SaveSessionViewModel : BaseViewModel
    {
        public class SaveParams
        {
            public int SessionSkillRating;
            public int NewSeasonHigh;
        }
        public int SeasonHigh { get; private set; }
        public PlaySession PlaySession { get; private set; }
        public Command SaveLastPlaySessionCommand { get; set; }

        private GameSeason currentSeason;

        public SaveSessionViewModel(PlaySession session, GameSeason currentSeason)
        {
            PlaySession = session;
            this.currentSeason = currentSeason;
            SaveLastPlaySessionCommand = new Command(async (param) => await ExecuteSaveLastPlaySessionCommand((SaveParams)param));
        }

        private async Task ExecuteSaveLastPlaySessionCommand(SaveParams saveParams)
        {
            PlaySession.FinalSkillRating = saveParams.SessionSkillRating;
            await Database.AddPlaySession(PlaySession);
            currentSeason.HighestSkillRating = saveParams.NewSeasonHigh;
            await Database.UpdateGameSeason(currentSeason);
            SeasonHigh = this.currentSeason.HighestSkillRating;
            OnPropertyChanged(nameof(SeasonHigh));
        }
    }
}

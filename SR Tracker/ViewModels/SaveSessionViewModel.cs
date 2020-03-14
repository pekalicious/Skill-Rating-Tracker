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
        public int SeasonHigh { get; private set; }
        public PlaySession PlaySession { get; private set; }
        public Command SaveLastPlaySessionCommand { get; set; }

        private GameSeason currentSeason;

        public SaveSessionViewModel(PlaySession session, GameSeason currentSeason)
        {
            PlaySession = session;
            this.currentSeason = currentSeason;
            SaveLastPlaySessionCommand = new Command(async () => await ExecuteSaveLastPlaySessionCommand());
        }

        private async Task ExecuteSaveLastPlaySessionCommand()
        {
            await Database.AddPlaySession(PlaySession);
            SeasonHigh = this.currentSeason.HighestSkillRating;
            OnPropertyChanged(nameof(SeasonHigh));
        }

        public async Task UpdateSeasonHigh(int newSeasonHigh)
        {
            currentSeason.HighestSkillRating = newSeasonHigh;
            await Database.UpdateGameSeason(currentSeason);
        }
    }
}

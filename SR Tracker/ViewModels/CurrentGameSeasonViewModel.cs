using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Pekalicious.SrTracker.Models;
using Pekalicious.SrTracker.Core;
using Xamarin.Forms;

namespace Pekalicious.SrTracker.ViewModels
{
    public class CurrentGameSeasonViewModel : BaseViewModel
    {
        public string CurrentRating { get; private set; }
        public string SeasonHigh { get; private set; }
        public string CurrentSeasonName { get; private set; }
        public Command LoadCurrentGameSeasonCommand { get; set; }

        public CurrentGameSeasonViewModel()
        {
            LoadCurrentGameSeasonCommand = new Command(async () => await ExecuteLoadCurrentGameSeasonCommand());
        }

        private async Task ExecuteLoadCurrentGameSeasonCommand()
        {
            Maybe<GameSeason> currentSeason = await Database.User.LastUsedSeason();
            if (currentSeason.HasItem)
            {
                CurrentRating = currentSeason.Item.LastSkillRating.ToString();
                SeasonHigh = currentSeason.Item.HighestSkillRating.ToString();
                CurrentSeasonName = currentSeason.Item.Name;
            }
            else
            {
                CurrentRating = "";
                SeasonHigh = "";
                CurrentSeasonName = "<NO SEASON SELECTED>";
            }

            OnPropertyChanged(nameof(CurrentRating));
            OnPropertyChanged(nameof(SeasonHigh));
            OnPropertyChanged(nameof(CurrentSeasonName));
        }
    }
}

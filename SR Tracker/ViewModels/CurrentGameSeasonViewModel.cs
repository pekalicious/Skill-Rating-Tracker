using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using Pekalicious.SrTracker.Models;
using Pekalicious.SrTracker.Core;
using SkiaSharp;
using Xamarin.Forms;

namespace Pekalicious.SrTracker.ViewModels
{
    public class CurrentGameSeasonViewModel : BaseViewModel
    {
        public string CurrentRating { get; private set; }
        public string SeasonHigh { get; private set; }
        public string CurrentSeasonName { get; private set; }
        public ChartEntry[] Entries { get; private set; } = new ChartEntry[0];

        public Command LoadCurrentGameSeasonCommand { get; set; }

        public CurrentGameSeasonViewModel()
        {
            LoadCurrentGameSeasonCommand = new Command(async () => await ExecuteLoadCurrentGameSeasonCommand());
        }

        public async Task ExecuteLoadCurrentGameSeasonCommand()
        {
            Maybe<GameSeason> currentSeason = await Database.User.LastUsedSeason();
            if (currentSeason.HasItem)
            {
                CurrentRating = currentSeason.Item.LastSkillRating.ToString();
                SeasonHigh = currentSeason.Item.HighestSkillRating.ToString();
                CurrentSeasonName = currentSeason.Item.Name;

                List<ChartEntry> sessions = new List<ChartEntry>();
                foreach (PlaySession session in currentSeason.Item.SessionHistory)
                {
                    sessions.Add(new ChartEntry(session.FinalSkillRating)
                    {
                        Label = "", ValueLabel = "",
                        Color = session.FinalSkillRating > 0 ? SKColor.Parse("#00FF00") : SKColor.Parse("#FF0000")
                    });
                }
                Entries = sessions.ToArray();

            }
            else
            {
                CurrentRating = "";
                SeasonHigh = "";
                CurrentSeasonName = "<NO SEASON SELECTED>";
            }

            OnPropertyChanged(nameof(Entries));
            OnPropertyChanged(nameof(CurrentRating));
            OnPropertyChanged(nameof(SeasonHigh));
            OnPropertyChanged(nameof(CurrentSeasonName));
        }
    }
}

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
    public class GameSeasonContentViewModel : BaseViewModel
    {
        public string CurrentRating { get; private set; }
        public string SeasonHigh { get; private set; }
        public string CurrentSeasonName { get; private set; }
        public ChartEntry[] Entries { get; private set; } = new ChartEntry[0];
        public int MinSr { get; private set; }
        public int MaxSr { get; private set; }

        public Command LoadCurrentGameSeasonCommand { get; set; }

        public GameSeasonContentViewModel()
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

                MinSr = 5000;
                MaxSr = 0;

                List<ChartEntry> sessions = new List<ChartEntry>();
                for (int i = 0; i < currentSeason.Item.SessionHistory.Count; i++)
                {
                    int sr = currentSeason.Item.SessionHistory[i].FinalSkillRating;
                    ChartEntry entry = new ChartEntry(sr);
                    entry.Label = "";
                    entry.ValueLabel = "";
                    if (i == 0)
                    {
                        entry.Color = SKColor.Parse("#0000FF");
                    }
                    else if (sr > currentSeason.Item.SessionHistory[i - 1].FinalSkillRating)
                    {
                        entry.Color = SKColor.Parse("#00FF00");
                    }
                    else if (sr < currentSeason.Item.SessionHistory[i - 1].FinalSkillRating)
                    {
                        entry.Color = SKColor.Parse("#FF0000");
                    }
                    else
                    {
                        entry.Color = SKColor.Parse("#0000FF");
                    }

                    if (sr > MaxSr)
                    {
                        MaxSr = sr;
                    }

                    if (sr < MinSr)
                    {
                        MinSr = sr;
                    }
                    sessions.Add(entry);
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

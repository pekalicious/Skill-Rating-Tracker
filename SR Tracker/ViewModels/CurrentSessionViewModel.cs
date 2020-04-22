using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Microcharts;
using Pekalicious.SrTracker.Models;
using SkiaSharp;
using Xamarin.Essentials;

namespace Pekalicious.SrTracker.ViewModels
{
    public class StreakController
    {
        public int CurrentStreak { get; private set; }
        private int previousValue;

        public int UpdateStreak(int direction)
        {
            if (direction > 0)
            {
                previousValue = CurrentStreak;
                if (CurrentStreak > 0)
                {
                    CurrentStreak += direction;
                }
                else
                {
                    CurrentStreak = direction;
                }
            }
            else if (direction < 0)
            {
                previousValue = CurrentStreak;
                if (CurrentStreak < 0)
                {
                    CurrentStreak += direction;
                }
                else
                {
                    CurrentStreak = direction;
                }
            }

            return CurrentStreak;
        }

        public int Undo()
        {
            CurrentStreak = previousValue;
            return CurrentStreak;
        }

        public int Reset()
        {
            CurrentStreak = 0;
            previousValue = 0;
            return CurrentStreak;
        }
    }
    public class CurrentSessionViewModel : BaseViewModel
    {
        private int lastOverallDiffValue;
        private int lastGamesPlayedValue;
        private StreakController streak;

        public int Streak
        {
            get { return streak.CurrentStreak; }
            set
            {
                OnPropertyChanged();
            }
        }

        public int OverallDiff
        {
            set
            {
                PlaySession.OverallDiff = value;
                OnPropertyChanged();
            }
            get { return PlaySession.OverallDiff; }
        }

        public int GamesPlayed
        {
            set
            {
                PlaySession.GamesPlayed = value;
                OnPropertyChanged();
            }
            get { return PlaySession.GamesPlayed; }
        }

        public int HighestSkillRating { get; private set; }
        public ChartEntry[] Entries {
            get
            {
                return PlaySession.MatchHistory
                    .Select(e => new ChartEntry(e)
                    {
                        Label = "", 
                        ValueLabel = "", 
                        Color = e > 0 ? SKColor.Parse("#00FF00") : e < 0 ? SKColor.Parse("#FF0000") : SKColor.Parse("#0000FF")
                    }).ToArray();
            }
        }

        public PlaySession PlaySession { get; set; }
        public GameSeason CurrentGameSeason { get; private set; }
        
        public CurrentSessionViewModel(GameSeason currentSeason)
        {
            PlaySession = new PlaySession();
            streak = new StreakController();
            Title = "Today";
            CurrentGameSeason = currentSeason;
            HighestSkillRating = CurrentGameSeason.HighestSkillRating;

            StartNewSession();
        }

        public void RecordWin()
        {
            Streak = streak.UpdateStreak(1);
            UpdateValues(1);
        }

        public void Draw()
        {
            Streak = streak.UpdateStreak(0);
            UpdateValues(0);
        }

        public void Loss()
        {
            Streak = streak.UpdateStreak(-1);
            UpdateValues(- 1);
        }

        private void UpdateValues(int change)
        {
            int overallDiff = OverallDiff + change;

            lastOverallDiffValue = PlaySession.OverallDiff;
            OverallDiff = overallDiff;

            lastGamesPlayedValue = PlaySession.GamesPlayed;
            GamesPlayed += 1;

            PlaySession.MatchHistory.Add(overallDiff);
        }

        public void Undo()
        {
            if (GamesPlayed != lastGamesPlayedValue)
            {
                OverallDiff = lastOverallDiffValue;
                GamesPlayed = lastGamesPlayedValue;
                PlaySession.MatchHistory.Remove(PlaySession.MatchHistory.Last());
                Streak = streak.Undo();
                Console.WriteLine("UNDONE!");
            }
        }

        public void StartNewSession()
        {
            lastOverallDiffValue = 0;
            lastGamesPlayedValue = 0;
            PlaySession.Date = DateTime.Now;
            Streak = streak.Reset();
        }

        public PlaySession EndSession()
        {
            return PlaySession;
        }
    }
}
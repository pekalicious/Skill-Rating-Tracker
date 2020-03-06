using System;
using System.ComponentModel;
using System.Windows.Input;
using Pekalicious.SrTracker.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

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
    public class AboutViewModel : BaseViewModel
    {
        private int lastOverallDiffValue;
        private int lastGamesPlayedValue;
        private bool sessionStarted;
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
        public PlaySession PlaySession { get; set; }
        public AboutViewModel()
        {
            PlaySession = new PlaySession();
            streak = new StreakController();
            Title = "Today";

            StartNewSession();
        }

        public void RecordWin()
        {
            Streak = streak.UpdateStreak(1);
            UpdateValues(OverallDiff + 1, GamesPlayed + 1);
        }

        public void Draw()
        {
            Streak = streak.UpdateStreak(0);
            UpdateValues(OverallDiff, GamesPlayed + 1);
        }

        public void Loss()
        {
            Streak = streak.UpdateStreak(-1);
            UpdateValues(OverallDiff - 1, GamesPlayed + 1);
        }

        private void UpdateValues(int overallDiff, int gamesPlayed)
        {
            lastOverallDiffValue = PlaySession.OverallDiff;
            OverallDiff = overallDiff;

            lastGamesPlayedValue = PlaySession.GamesPlayed;
            GamesPlayed = gamesPlayed;
        }

        public void Undo()
        {
            OverallDiff = lastOverallDiffValue;
            GamesPlayed = lastGamesPlayedValue;
            Streak = streak.Undo();
        }

        public void StartNewSession()
        {
            UpdateValues(0, 0);
            lastOverallDiffValue = 0;
            lastGamesPlayedValue = 0;
            PlaySession.Date = DateTime.Now;
            Streak = streak.Reset();

            sessionStarted = true;
        }

        public void EndSession()
        {
            sessionStarted = false;
        }
    }
}
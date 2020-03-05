using System;
using System.ComponentModel;
using System.Windows.Input;
using SR_Tracker.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SR_Tracker.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private int lastOverallDiffValue;
        private int lastGamesPlayedValue;

        public int OverallDiff
        {
            set { SetProperty(ref PlaySession.OverallDiff, value); }
            get { return PlaySession.OverallDiff; }
        }

        public int GamesPlayed
        {
            set { SetProperty(ref PlaySession.GamesPlayed, value); }
            get { return PlaySession.GamesPlayed; }
        }
        public PlaySession PlaySession { get; set; }
        public AboutViewModel()
        {
            PlaySession = new PlaySession();
            PlaySession.Date = DateTime.Now;
            Title = "Today";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
        }

        public ICommand OpenWebCommand { get; }

        public void RecordWin()
        {
            UpdateValues(OverallDiff + 1, GamesPlayed + 1);
        }

        public void Draw()
        {
            UpdateValues(OverallDiff, GamesPlayed + 1);
        }

        public void Loss()
        {
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
        }

        public void Reset()
        {
            UpdateValues(0, 0);
            lastOverallDiffValue = 0;
            lastGamesPlayedValue = 0;
            PlaySession.Date = DateTime.Now;
        }
    }
}
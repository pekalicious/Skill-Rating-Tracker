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
        }
        public Command SaveLastPlaySessionCommand { get; set; }

        private GameSeason currentSeason;
        private PlaySession playsession;

        public SaveSessionViewModel(PlaySession session, GameSeason currentSeason)
        {
            playsession = session;
            this.currentSeason = currentSeason;
            SaveLastPlaySessionCommand = new Command(async (param) => await ExecuteSaveLastPlaySessionCommand((SaveParams)param));
        }

        public async Task ExecuteSaveLastPlaySessionCommand(SaveParams saveParams)
        {
            playsession.FinalSkillRating = saveParams.SessionSkillRating;
            currentSeason.LastSkillRating = saveParams.SessionSkillRating;
            await Database.AddPlaySession(currentSeason, playsession);
            if (currentSeason.HighestSkillRating < saveParams.SessionSkillRating)
            {
                currentSeason.HighestSkillRating = saveParams.SessionSkillRating;
                await Database.UpdateGameSeason(currentSeason);
            }
        }
    }
}

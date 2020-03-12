using System;
using System.Collections.Generic;
using System.Text;
using Pekalicious.SrTracker.Models;

namespace Pekalicious.SrTracker.ViewModels
{
    public class EditGameSeasonViewModel : BaseViewModel
    {
        public GameSeason EditingGameSeason { get; private set; }

        public EditGameSeasonViewModel(GameSeason season)
        {
            EditingGameSeason = season ?? new GameSeason();
        }

        public async void Save()
        {
            if (EditingGameSeason.Id == -1)
            {
                await Database.AddGameSeason(EditingGameSeason);
            }
            else
            {
                await Database.UpdateGameSeason(EditingGameSeason);
            }
        }
    }
}

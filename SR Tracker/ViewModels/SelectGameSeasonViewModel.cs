using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Pekalicious.SrTracker.Models;
using Pekalicious.SrTracker.Views;
using Xamarin.Forms;

namespace Pekalicious.SrTracker.ViewModels
{
    public class SelectGameSeasonViewModel : BaseViewModel
    {
        public ObservableCollection<GameSeason> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public SelectGameSeasonViewModel()
        {
            Items = new ObservableCollection<GameSeason>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<EditGameSeasonPage, GameSeason>(this, "AddItem", async (obj, item) =>
            {
                Items.Add(item);
                await Database.AddGameSeason(item);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await Database.GetAllSeasons();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void SelectGameSeason(GameSeason item)
        {
            await Database.AppState.SetLastUsedSeason(item);
        }
    }
}

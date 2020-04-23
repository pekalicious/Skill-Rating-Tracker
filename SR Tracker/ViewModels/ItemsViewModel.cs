using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Pekalicious.SrTracker.Models;
using Pekalicious.SrTracker.Core;
using Pekalicious.SrTracker.Views;

namespace Pekalicious.SrTracker.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<PlaySession> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public string SelectedGameSeason { get; private set; }
        public string SeasonHigh { get; private set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<PlaySession>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadCurrentGameSeason();
        }

        async void LoadCurrentGameSeason()
        {
            Maybe<GameSeason> lastUsedSeason = await Database.User.LastUsedSeason();
            if (lastUsedSeason.HasItem)
            {
                SelectedGameSeason = lastUsedSeason.Item.Name;
                SeasonHigh = lastUsedSeason.Item.HighestSkillRating.ToString();
            }
            else
            {
                SelectedGameSeason = "Season Not Selected";
                SeasonHigh = "XXXX";
            }
            OnPropertyChanged("SelectedGameSeason");
            OnPropertyChanged("SeasonHigh");
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
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
    }
}
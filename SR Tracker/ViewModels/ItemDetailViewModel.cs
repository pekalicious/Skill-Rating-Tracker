using System;

using SR_Tracker.Models;

namespace SR_Tracker.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public PlaySession PlaySession { get; set; }
        public ItemDetailViewModel(PlaySession playSession = null)
        {
            Title = playSession?.Text;
            PlaySession = playSession;
        }
    }
}

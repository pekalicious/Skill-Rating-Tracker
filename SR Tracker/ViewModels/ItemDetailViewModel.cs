using System;

using Pekalicious.SrTracker.Models;

namespace Pekalicious.SrTracker.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public PlaySession PlaySession { get; set; }
        public ItemDetailViewModel(PlaySession playSession = null)
        {
            PlaySession = playSession;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Pekalicious.SrTracker.Models;

namespace Pekalicious.SrTracker.ViewModels
{
    public class SaveSessionViewModel : BaseViewModel
    {
        public PlaySession PlaySession { get; private set; }

        public SaveSessionViewModel(PlaySession session)
        {
            PlaySession = session;
        }
    }
}

using System;

namespace SpotifyService.Model.Interfaces
{
    public interface IActiveTracksListener
    {
        void ActiveTracksChanged(object sender, EventArgs eventArgs);
    }
}
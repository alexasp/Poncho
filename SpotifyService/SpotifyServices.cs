using System;
using System.Collections.Generic;
using Caliburn.Micro;
using SpotifyService.Cargo;
using SpotifyService.Enums;
using SpotifyService.Interfaces;
using SpotifyService.Model.Interfaces;

namespace SpotifyService
{
    public class SpotifyServices : ISpotifyServices
    {
        private readonly ISearchManager _searchManager;
        private readonly ITrackHandler _trackHandler;

        public SpotifyServices(ISearchManager searchManager, ITrackHandler trackHandler, IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            _searchManager = searchManager;
            _trackHandler = trackHandler;
        }

        public void Search(string searchText)
        {
            if (String.IsNullOrEmpty(searchText))
                throw new ArgumentException("Search string was null or empty.");

            _searchManager.Search(searchText);
        }

        public void PlayTrack(Track track)
        {
            _trackHandler.PlayTrack(track);
        }

        public void QueueTracks(List<Track> trackList)
        {
            _trackHandler.QueueTracks(trackList);
        }

        public void ChangePlaybackStatus(PlaybackStatus playing)
        {
            _trackHandler.ChangePlaybackStatus(playing);
        }

        public IEventAggregator EventAggregator{ get; private set; }
    }
}
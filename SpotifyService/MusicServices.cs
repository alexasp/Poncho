using System;
using SpotifyService.Cargo;
using SpotifyService.Enums;
using SpotifyService.Interfaces;
using SpotifyService.Models.Interfaces;

namespace SpotifyService
{
    public class MusicServices : IMusicServices
    {
        private readonly ISearchManager _searchManager;
        

        private static SpotifyWrapper _instance;
        private ISpotifyWrapper _spotifyWrapper;

        public MusicServices(ISearchManager searchManager, ISpotifyWrapper spotifyWrapper)
        {
            _searchManager = searchManager;
            _spotifyWrapper = spotifyWrapper;
        }

        public void InitializeSession(string username, string password)
        {
            sp_error error = _spotifyWrapper.CreateSession();

            if(error == (UInt32)sp_error.SP_ERROR_OK)
                _spotifyWrapper.RequestLogin(username, password);
        }

        public void SearchRetrieved(SearchResult result)
        {
            throw new NotImplementedException();
        }

        public void EndSession()
        {
            _spotifyWrapper.EndSession();
        }

        public void FetchPlaylistTracks(PlayList playlist)
        {
            throw new NotImplementedException();
        }

        public void Search(string searchText)
        {
            _spotifyWrapper.CreateSearch(searchText);
        }


        public void Dispose()
        {
            _spotifyWrapper.Dispose();
        }
    }


}

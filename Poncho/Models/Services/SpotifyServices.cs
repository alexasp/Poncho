using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Poncho.Models.services;


namespace Poncho.Models.Services
{
    class SpotifyServices : ISpotifyServices
    {
        private IntPtr _sessionHandle;
        private IntPtr _searchHandle;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SearchCallbackDelegate(IntPtr searchHandle, IntPtr userdataPointer);
        
        public void FetchPlaylistTracks(PlayList playlist)
        {
            throw new NotImplementedException();
        }

        [DllImport("spotify.dll")]
        private static extern void sp_session_login(IntPtr session, ref SByte username, ref SByte password,
                                                   bool rememberMe);

        [DllImport("spotify.dll")]
        private static extern void sp_search_create(IntPtr session, ref SByte query, Int32 track_offset,
                                                    Int32 track_count, Int32 album_offset, Int32 album_count,
                                                    Int32 artist_offset, Int32 artist_count,
                                                    SearchCallbackDelegate callbackDelegate, IntPtr userdataPointer);

        //(sp_error) sp_search_error(sp_search *search);

        //(const char *) sp_search_did_you_mean(sp_search *search);

        //(const char *) sp_search_query(sp_search *search);

        //(int) sp_search_total_tracks(sp_search *search);

        //(int) sp_search_num_tracks(sp_search *search);

        //(sp_track *) sp_search_track(sp_search *search, int index);

        //(int) sp_search_num_albums(sp_search *search);

        //(sp_album *) sp_search_album(sp_search *search, int index);

        //(int) sp_search_num_artists(sp_search *search);

        //(sp_artist *) sp_search_artist(sp_search *search, int index);

        //(const char *) sp_artist_name(sp_artist *artist);

        //(const char *) sp_album_name(sp_album *album);

        //(int) sp_album_year(sp_album *album);

        //(const char*) sp_error_message(sp_error error);

        public void RequestLogin(string username, string password)
        {
            SByte usernameAsSbyte = Convert.ToSByte(username);
            SByte passwordASbyte = Convert.ToSByte(password);

            sp_session_login(_sessionHandle, ref usernameAsSbyte, ref passwordASbyte, true);
        }

        public void Search(string searchText)
        {
            var queryAsSByte = Convert.ToSByte(searchText);
            var callbackDelegate = new SearchCallbackDelegate(SearchCallback);
            sp_search_create(_sessionHandle, ref queryAsSByte, 0, 100, 0, 100, 0, 100, callbackDelegate, new IntPtr());
        }

        private void SearchCallback(IntPtr searchHandle, IntPtr userdataPointer)
        {
            //handle searchreturn
        }
    }
}

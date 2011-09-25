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

        public void FetchPlaylistTracks(PlayList playlist)
        {
            throw new NotImplementedException();
        }

        [DllImport("spotify.dll")]
        public static extern void sp_session_login(IntPtr session, ref SByte username, ref SByte password,
                                                   bool rememberMe);
        [DllImport("spotify.dll")]
        public static extern void sp_search_create(IntPtr session, ref SByte query, Int32 track_offset, Int32 track_count, Int32 album_offset, Int32 album_count, Int32 artist_offset, Int32 artist_count, ,)

        // sp_search_create(sp_session *session, const char *query, int track_offset, int track_count, int album_offset, 
        ¨//int album_count, int artist_offset, int artist_count, search_complete_cb *callback, void *userdata);

        public void RequestLogin(string username, string password)
        {
            SByte usernameAsSbyte = Convert.ToSByte(username);
            SByte passwordASbyte = Convert.ToSByte(password);

            sp_session_login(_sessionHandle, ref usernameAsSbyte, ref passwordASbyte, true);
        }

        public void Search(string searchText)
        {
            throw new NotImplementedException();
        }
    }
}

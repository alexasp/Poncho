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

        public void FetchPlaylistTracks(PlayList playlist)
        {
            throw new NotImplementedException();
        }

        [DllImport("spotify.dll")]
        public static extern void sp_session_login(IntPtr session, ref SByte username, ref SByte password,
                                                   bool rememberMe);

        public void RequestLogin(string username, string password)
        {
            SByte usernameAsSbyte = Convert.ToSByte(username);
            SByte passwordASbyte = Convert.ToSByte(password);
            _sessionHandle = new IntPtr();

            sp_session_login(_sessionHandle, ref usernameAsSbyte, ref passwordASbyte, true);
        }

        public void Search(string searchText)
        {
            throw new NotImplementedException();
        }
    }
}

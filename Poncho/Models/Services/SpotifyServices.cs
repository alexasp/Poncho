using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Poncho.Models.services;
using Poncho.Models.Services.Enums;
using Poncho.Models.Services.Structs;


namespace Poncho.Models.Services
{
    public class SpotifyServices : ISpotifyServices
    {
        private IntPtr _sessionHandle;
        private IntPtr _searchHandle;
        private sp_session_config _sessionConfig;
        private string cacheLocation;
        private string settingsLocation;
        private string _userAgent = "poncho";
        private sp_session_callbacks _sessionCallbacks;
        private const int SpotifyApiVersion = 9;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SearchCallbackDelegate(IntPtr searchHandle, IntPtr userdataPointer);

        public sp_error InitializeSession()
        {
            cacheLocation = "tmp";
            settingsLocation = "tmp";

            _sessionHandle = IntPtr.Zero;

            IntPtr appKeyPointer = Marshal.AllocHGlobal(KeyManager.ApplicationKey.Length);
            Marshal.Copy(KeyManager.ApplicationKey, 0, appKeyPointer, KeyManager.ApplicationKey.Length);

            IntPtr sessionCallbacksPtr = Marshal.AllocHGlobal(Marshal.SizeOf(_sessionCallbacks));
            Marshal.StructureToPtr(_sessionCallbacks, sessionCallbacksPtr, true);

            IntPtr cacheLocPtr = Marshal.StringToHGlobalUni(cacheLocation);

            IntPtr userAgentPtr = Marshal.StringToHGlobalUni(_userAgent);

            _sessionConfig = new sp_session_config
            {
                api_version = SpotifyApiVersion,
                cache_location = cacheLocation,
                settings_location = cacheLocation,
                application_key = appKeyPointer,
                application_key_size = KeyManager.ApplicationKey.Length,
                user_agent = _userAgent,
                callbacks = IntPtr.Zero,
                userdata = IntPtr.Zero,
                compress_playlists = Convert.ToInt32(false),
                dont_save_metadata_for_playlists = Convert.ToInt32(true),
                initially_unload_playlists = Convert.ToInt32(true)
            };


            var error = sp_session_create(ref _sessionConfig, out _sessionHandle);

            return error;
        }

        public void EndSession()
        {
            sp_session_release(out _sessionHandle);
        }

        public void FetchPlaylistTracks(PlayList playlist)
        {
            throw new NotImplementedException();
        }

        [DllImport("spotify.dll")]
        //(sp_error) sp_session_create(const sp_session_config *config, sp_session **sess);
        private static extern sp_error sp_session_create(ref sp_session_config config, out IntPtr sessionHandle);

        [DllImport("spotify.dll")]
        //(void) sp_session_release(sp_session *sess);
        private static extern void sp_session_release(out IntPtr sessionHandle);

        [DllImport("spotify.dll")]
        private static extern void sp_session_login(IntPtr session, ref char[] username, ref char[] password,
                                                   bool rememberMe);

        [DllImport("spotify.dll")]
        //(void) sp_session_logout(sp_session *session);
        private static extern void sp_session_logout(IntPtr session);
        

        [DllImport("spotify.dll")]
        private static extern void sp_search_create(IntPtr session, ref char[] query, int trackOffset,
                                                    int trackCount, int albumOffset, int albumCount,
                                                    int artistOffset, int artistCount,
                                                    SearchCallbackDelegate callbackDelegate, IntPtr userdataPointer);

        [DllImport("spotify.dll")]
        //(sp_error) sp_search_error(sp_search *search);
        private static extern UInt32 sp_search_error(IntPtr searchHandle);

        [DllImport("spotify.dll")]
        //(const char *) sp_search_did_you_mean(sp_search *search);
        private static extern IntPtr sp_search_did_you_mean(IntPtr searchHandle);

        [DllImport("spotify.dll")]
        //(const char *) sp_search_query(sp_search *search);
        private static extern IntPtr sp_search_query(IntPtr searchHandle);

        [DllImport("spotify.dll")]
        private static extern int sp_search_total_tracks(IntPtr searchHandle);

        [DllImport("spotify.dll")]
        private static extern int sp_search_num_tracks(IntPtr searchHandle);

        [DllImport("spotify.dll")]
        //(sp_track *) sp_search_track(sp_search *search, int index);
        private static extern IntPtr sp_search_track(IntPtr searchHandle, int index);

        [DllImport("spotify.dll")]
        //(int) sp_search_num_albums(sp_search *search);
        private static extern int sp_search_num_albums(IntPtr searchHandle);

        [DllImport("spotify.dll")]
        //(sp_album *) sp_search_album(sp_search *search, int index);
        private static extern IntPtr sp_search_album(IntPtr searchHandle, int index);

        [DllImport("spotify.dll")]
        //(int) sp_search_num_artists(sp_search *search);
        private static extern int sp_search_num_artists(IntPtr searchHandle);

        [DllImport("spotify.dll")]
        //(sp_artist *) sp_search_artist(sp_search *search, int index);
        private static extern IntPtr sp_search_artist(IntPtr searchHandle, int index);

        [DllImport("spotify.dll")]
        //(const char *) sp_artist_name(sp_artist *artist);
        private static extern char sp_artist_name(IntPtr artistPointer);

        [DllImport("spotify.dll")]
        //(const char *) sp_album_name(sp_album *album);
        private static extern IntPtr sp_album_name(IntPtr albumPointer);

        [DllImport("spotify.dll")]
        //(int) sp_album_year(sp_album *album);
        private static extern int sp_album_year(IntPtr albumPointer);

        [DllImport("spotify.dll")]
        //(const char*) sp_error_message(sp_error error);
        private static extern IntPtr sp_error_message(UInt32 errorType);
        

        public void RequestLogin(string username, string password)
        {
            var usernameAschar = username.ToCharArray();
            var passwordAschar = password.ToCharArray();

            sp_session_login(_sessionHandle, ref usernameAschar, ref passwordAschar, true);
        }

        private void LoginCallBack(IntPtr sessionHandle, sp_error error)
        {
            throw new NotImplementedException();
        }

        public void Search(string searchText)
        {
            var queryAsChar = searchText.ToCharArray();

            var callbackDelegate = new SearchCallbackDelegate(SearchCallback);
            sp_search_create(_sessionHandle, ref queryAsChar, 0, 100, 0, 100, 0, 100, callbackDelegate, new IntPtr());
        }

        private void SearchCallback(IntPtr searchHandle, IntPtr userdataPointer)
        {
            throw new NotImplementedException();
        }

        
    }


}

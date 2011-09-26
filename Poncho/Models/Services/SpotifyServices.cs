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

        public SpotifyServices()
        {
            //assuming a given session handle is constant throughout lifetime
            _sessionHandle = new IntPtr();
        }

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
        private static extern Int32 sp_search_total_tracks(IntPtr searchHandle);

        [DllImport("spotify.dll")]
        private static extern Int32 sp_search_num_tracks(IntPtr searchHandle);

        [DllImport("spotify.dll")]
        //(sp_track *) sp_search_track(sp_search *search, int index);
        private static extern IntPtr sp_search_track(IntPtr searchHandle, Int32 index);

        [DllImport("spotify.dll")]
        //(int) sp_search_num_albums(sp_search *search);
        private static extern Int32 sp_search_num_albums(IntPtr searchHandle);

        [DllImport("spotify.dll")]
        //(sp_album *) sp_search_album(sp_search *search, int index);
        private static extern IntPtr sp_search_album(IntPtr searchHandle, Int32 index);

        [DllImport("spotify.dll")]
        //(int) sp_search_num_artists(sp_search *search);
        private static extern Int32 sp_search_num_artists(IntPtr searchHandle);

        [DllImport("spotify.dll")]
        //(sp_artist *) sp_search_artist(sp_search *search, int index);
        private static extern IntPtr sp_search_artist(IntPtr searchHandle, Int32 index);

        [DllImport("spotify.dll")]
        //(const char *) sp_artist_name(sp_artist *artist);
        private static extern SByte sp_artist_name(IntPtr artistPointer);

        [DllImport("spotify.dll")]
        //(const char *) sp_album_name(sp_album *album);
        private static extern IntPtr sp_album_name(IntPtr albumPointer);

        [DllImport("spotify.dll")]
        //(int) sp_album_year(sp_album *album);
        private static extern Int32 sp_album_year(IntPtr albumPointer);

        [DllImport("spotify.dll")]
        //(const char*) sp_error_message(sp_error error);
        private static extern IntPtr sp_error_message(UInt32 errorType);
        

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

// ReSharper disable InconsistentNaming
    internal enum sp_error
    {
        SP_ERROR_OK = 0,  //< No errors encountered
        SP_ERROR_BAD_API_VERSION = 1,  //< The library version targeted does not match the one you claim you support
        SP_ERROR_API_INITIALIZATION_FAILED = 2,  //< Initialization of library failed - are cache locations etc. valid?
        SP_ERROR_TRACK_NOT_PLAYABLE = 3,  //< The track specified for playing cannot be played
        SP_ERROR_BAD_APPLICATION_KEY = 5,  //< The application key is invalid
        SP_ERROR_BAD_USERNAME_OR_PASSWORD = 6,  //< Login failed because of bad username and/or password
        SP_ERROR_USER_BANNED = 7,  //< The specified username is banned
        SP_ERROR_UNABLE_TO_CONTACT_SERVER = 8,  //< Cannot connect to the Spotify backend system
        SP_ERROR_CLIENT_TOO_OLD = 9,  //< Client is too old, library will need to be updated
        SP_ERROR_OTHER_PERMANENT = 10, //< Some other error occurred, and it is permanent (e.g. trying to relogin will not help)
        SP_ERROR_BAD_USER_AGENT = 11, //< The user agent string is invalid or too long
        SP_ERROR_MISSING_CALLBACK = 12, //< No valid callback registered to handle events
        SP_ERROR_INVALID_INDATA = 13, //< Input data was either missing or invalid
        SP_ERROR_INDEX_OUT_OF_RANGE = 14, //< Index out of range
        SP_ERROR_USER_NEEDS_PREMIUM = 15, //< The specified user needs a premium account
        SP_ERROR_OTHER_TRANSIENT = 16, //< A transient error occurred.
        SP_ERROR_IS_LOADING = 17, //< The resource is currently loading
        SP_ERROR_NO_STREAM_AVAILABLE = 18, //< Could not find any suitable stream to play
        SP_ERROR_PERMISSION_DENIED = 19, //< Requested operation is not allowed
        SP_ERROR_INBOX_IS_FULL = 20, //< Target inbox is full
        SP_ERROR_NO_CACHE = 21, //< Cache is not enabled
        SP_ERROR_NO_SUCH_USER = 22, //< Requested user does not exist
        SP_ERROR_NO_CREDENTIALS = 23, //< No credentials are stored
    }
    // ReSharper restore InconsistentNaming
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using SpotifyService.Cargo;
using SpotifyService.Enums;
using SpotifyService.Interfaces;
using SpotifyService.Structs;

namespace SpotifyService
{
    public class SpotifyWrapper : ISpotifyWrapper
    {
        private IntPtr _sessionHandle;
        private IntPtr _searchHandle;
        private sp_session_config _sessionConfig;
        private string _cacheLocation;
        private string _settingsLocation;
        private const string UserAgent = "poncho";
        private sp_session_callbacks _sessionCallbacks;
        private const int SpotifyApiVersion = 9;

        private static readonly object Mutex = new object();
        private notify_main_thread _notifyMainThreadCallback;
        private logged_in _loginCallback;
        private search_complete_cb _searchCallback;
        private const string DllName = "spotify.dll";

        public event Action SearchRetrieved;
        public event Action<sp_error> LoginResponseRetrieved;

        public void LoadTrack(int trackIndex)
        {
            throw new NotImplementedException();
        }

        public void Play(bool b)
        {
            throw new NotImplementedException();
        }

        public bool ActiveSession()
        {
            return _sessionHandle != IntPtr.Zero;
        }

        public sp_error CreateSession()
        {
            if (ActiveSession())
                throw new InvalidOperationException("There can only be one instance of the Spotify service.");

            _cacheLocation = "tmp";
            _settingsLocation = "tmp";


            IntPtr appKeyHandle = Marshal.AllocHGlobal(SpotifyAppKey.ApplicationKey.Length);
            Marshal.Copy(SpotifyAppKey.ApplicationKey, 0, appKeyHandle, SpotifyAppKey.ApplicationKey.Length);

            _notifyMainThreadCallback = NotifyMainThreadCallback;
            _loginCallback = LoginCallback;
            _searchCallback = SearchCallback;

            _sessionCallbacks.notify_main_thread =
                Marshal.GetFunctionPointerForDelegate(_notifyMainThreadCallback);
            _sessionCallbacks.connection_error =
                Marshal.GetFunctionPointerForDelegate(new connection_error(ConnectionErrorCallback));
            _sessionCallbacks.logged_in = Marshal.GetFunctionPointerForDelegate(_loginCallback);

            IntPtr sessionCallbacksPtr = Marshal.AllocHGlobal(Marshal.SizeOf(_sessionCallbacks));
            Marshal.StructureToPtr(_sessionCallbacks, sessionCallbacksPtr, true);


            _sessionConfig = new sp_session_config
            {
                api_version = SpotifyApiVersion,
                cache_location = _cacheLocation,
                settings_location = _cacheLocation,
                application_key = appKeyHandle,
                application_key_size = SpotifyAppKey.ApplicationKey.Length,
                user_agent = UserAgent,
                callbacks = sessionCallbacksPtr,
                userdata = IntPtr.Zero,
                compress_playlists = false,
                dont_save_metadata_for_playlists = true,
                initially_unload_playlists = true
            };


            var error = sp_session_create(ref _sessionConfig, out _sessionHandle);

            if (error == sp_error.SP_ERROR_OK)
                Debug.WriteLine("Session successfully created.");
            else
                Debug.WriteLine("Session creation failed with error {0}", error);

            return error;
        }

        private void NotifyMainThreadCallback(IntPtr sessionhandle)
        {
            Debug.WriteLine("Notify main thread fired.");
            lock(Mutex)
            {
                int nextTimeOut;
                do
                {
                    sp_session_process_events(_sessionHandle, out nextTimeOut);
                } while (nextTimeOut == 0);
            }
        }

        private void ConnectionErrorCallback(IntPtr sessionhandle, sp_error error)
        {
            Debug.WriteLine("There is an issue with the connection to Spotify servers.");
        }

        public void EndSession()
        {
            sp_session_release(_sessionHandle);
            _sessionHandle = IntPtr.Zero;
            Debug.WriteLine("Session ended.");
        }

        public void FetchPlaylistTracks(PlayList playlist)
        {
            throw new NotImplementedException();
        }

        public void RequestLogin(string username, string password)
        {
            lock (Mutex)
            {
                Debug.WriteLine("Requesting login..");
                sp_session_login(_sessionHandle, username, password, true);
            }
        }

        public void Logout()
        {
            lock (Mutex)
            {
                sp_session_logout(_sessionHandle);
            }
        }

        private void LoginCallback(IntPtr sessionHandle, sp_error error)
        {
            Debug.WriteLine("Login callback called.");
            LoginResponseRetrieved(error);
        }

        public void CreateSearch(string searchText)
        {
            Debug.WriteLine("Creating search.");
            sp_search_create(_sessionHandle, searchText, 0, 100, 0, 100, 0, 100, Marshal.GetFunctionPointerForDelegate(_searchCallback), new IntPtr());
        }

        public string GetSearchQuery()
        {
            return sp_search_query(_searchHandle);
        }

        public string GetSearchDidYouMean()
        {
            return sp_search_did_you_mean(_searchHandle);
        }

        public int GetSearchTotalTracksFound()
        {
            return sp_search_total_tracks(_searchHandle);
        }

        public int GetSearchCountTracksRetrieved()
        {
            return sp_search_num_tracks(_searchHandle);
        }

        public int GetSearchCountAlbumsRetrieved()
        {
            return sp_search_num_albums(_searchHandle);
        }

        public IntPtr GetAlbumHandle(int index)
        {
            return sp_search_album(_searchHandle, index);
        }

        public IntPtr GetArtistHandle(int index)
        {
            return sp_search_artist(_searchHandle, index);
        }

        public IntPtr GetTrackHandle(int index)
        {
            return sp_search_track(_searchHandle, index);
        }

        public List<Track> GetSearchTracks()
        {
            var trackCount = GetSearchCountTracksRetrieved();
            var trackList = new List<Track>();

            for (int i = 0; i < trackCount; i++)
            {
                trackList.Add(GetTrackInfo(i));
            }

            return trackList;
        }

        private IEnumerable<string> GetTrackArtists(IntPtr trackHandle)
        {
            var artistCount = GetTrackArtistCount(trackHandle);
            for (int i = 0; i < artistCount; i++)
            {
                var artistHandle = sp_track_artist(trackHandle, i);
                var artistName = sp_artist_name(artistHandle);
                yield return artistName;
            }
        }

        private int GetTrackArtistCount(IntPtr trackHandle)
        {
            return sp_track_num_artists(trackHandle);
        }

        private Track GetTrackInfo(int i)
        {
            var trackHandle = GetTrackHandle(i);
            var artists = GetTrackArtists(trackHandle).Aggregate("", (left,right) => left + "," + right);

            var name = sp_track_name(trackHandle);

            return new Track((int)trackHandle, name, artists, "", true);
        }


        private void SearchCallback(IntPtr searchHandle, IntPtr userdataHandle)
        {
            if (sp_search_error(searchHandle) == (UInt32)sp_error.SP_ERROR_OK)
            {
                _searchHandle = searchHandle;
                SearchRetrieved();
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void search_complete_cb(IntPtr searchHandle, IntPtr userdataHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void logged_in(IntPtr sessionHandle, sp_error error);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void logged_out(IntPtr sessionHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void metadata_updated(IntPtr sessionHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void connection_error(IntPtr sessionHandle, sp_error error);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void message_to_user(IntPtr sessionHandle, string message);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void notify_main_thread(IntPtr sessionHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void music_delivery(
            IntPtr sessionHandle, ref sp_audioformat format, IntPtr frames, int num_frames);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void play_token_lost(IntPtr sessionHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void log_message(IntPtr sessionHandle, ref char[] data);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void end_of_track(IntPtr sessionHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void streaming_error(IntPtr sessionHandle, sp_error error);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void userinfo_updated(IntPtr sessionHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void start_playback(IntPtr sessionHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void stop_playback(IntPtr sessionHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void get_audio_buffet_stats(IntPtr sessionHandle, ref sp_audio_buffer_stats stats);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void offline_status_updated(IntPtr sessionHandle);

        [DllImport(DllName)]
        //(sp_error) sp_session_create(const sp_session_config *config, sp_session **sess);
        public static extern sp_error sp_session_create(ref sp_session_config config, out IntPtr sessionHandle);

        [DllImport(DllName)]
        //(sp_error) sp_session_player_load(sp_session *session, sp_track *track);
        public static extern sp_error sp_session_player_load(IntPtr sessionHandle, IntPtr trackHandle);

        [DllImport(DllName)]
        //(void) sp_session_player_play(sp_session *session, bool play);
        public static extern void sp_session_player_play(IntPtr sessionHandle, bool play);

        [DllImport(DllName)]
        //(void) sp_session_player_unload(sp_session *session);
        public static extern void sp_session_player_unload(IntPtr sessionHandle);

        [DllImport(DllName)]
        //(sp_error) sp_session_player_prefetch(sp_session *session, sp_track *track);
        public static extern sp_error sp_session_player_prefetch(IntPtr sessionHandle, IntPtr trackHandle);

        [DllImport(DllName)]
        //(void) sp_session_process_events(sp_session *session, int *next_timeout);
        public static extern sp_error sp_session_process_events(IntPtr sessionHandle, out int nextTimeout);

        [DllImport(DllName)]
        //(void) sp_session_release(sp_session *sess);
        public static extern void sp_session_release(IntPtr sessionHandle);

        [DllImport(DllName)]
        //(void) sp_session_login(sp_session *session, const char *username, const char *password, bool remember_me);
        public static extern void sp_session_login(IntPtr session, string username, string password,
                                                   bool rememberMe);

        [DllImport(DllName)]
        //(void) sp_session_logout(sp_session *session);
        public static extern void sp_session_logout(IntPtr session);


        [DllImport(DllName)]
        public static extern void sp_search_create(IntPtr session, string query, int trackOffset,
                                                    int trackCount, int albumOffset, int albumCount,
                                                    int artistOffset, int artistCount,
                                                    IntPtr callbackDelegate, IntPtr userdataHandle);

        [DllImport(DllName)]
        //(sp_error) sp_search_error(sp_search *search);
        public static extern UInt32 sp_search_error(IntPtr searchHandle);

        [DllImport(DllName)]
        //(const char *) sp_search_did_you_mean(sp_search *search);
        public static extern string sp_search_did_you_mean(IntPtr searchHandle);

        [DllImport(DllName)]
        //(const char *) sp_search_query(sp_search *search);
        public static extern string sp_search_query(IntPtr searchHandle);

        [DllImport(DllName)]
        public static extern int sp_search_total_tracks(IntPtr searchHandle);

        [DllImport(DllName)]
        public static extern int sp_search_num_tracks(IntPtr searchHandle);

        [DllImport(DllName)]
        //(sp_track *) sp_search_track(sp_search *search, int index);
        public static extern IntPtr sp_search_track(IntPtr searchHandle, int index);

        [DllImport(DllName)]
        //(int) sp_search_num_albums(sp_search *search);
        public static extern int sp_search_num_albums(IntPtr searchHandle);

        [DllImport(DllName)]
        //(sp_album *) sp_search_album(sp_search *search, int index);
        public static extern IntPtr sp_search_album(IntPtr searchHandle, int index);

        [DllImport(DllName)]
        //(int) sp_search_num_artists(sp_search *search);
        public static extern int sp_search_num_artists(IntPtr searchHandle);

        [DllImport(DllName)]
        //(sp_artist *) sp_search_artist(sp_search *search, int index);
        public static extern IntPtr sp_search_artist(IntPtr searchHandle, int index);

        [DllImport(DllName)]
        //(const char *) sp_artist_name(sp_artist *artist);
        public static extern string sp_artist_name(IntPtr artistHandle);

        [DllImport(DllName)]
        //(const char *) sp_album_name(sp_album *album);
        public static extern string sp_album_name(IntPtr albumHandle);

        [DllImport(DllName)]
        //(int) sp_album_year(sp_album *album);
        public static extern int sp_album_year(IntPtr albumHandle);

        [DllImport(DllName)]
        //(const char*) sp_error_message(sp_error error);
        public static extern string sp_error_message(UInt32 errorType);

        [DllImport(DllName)]
        //(const char *) sp_track_name(sp_track *track);
        public static extern string sp_track_name(IntPtr trackHandle);

        [DllImport(DllName)]
        public static extern int sp_track_num_artists(IntPtr trackHandle);

        [DllImport(DllName)]
        public static extern IntPtr sp_track_artist(IntPtr trackHandle, int index);

        public void Dispose()
        {
            EndSession();
        }

        ~SpotifyWrapper()
        {
            Dispose();
        }

    }

}

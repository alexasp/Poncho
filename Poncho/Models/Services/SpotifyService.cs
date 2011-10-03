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
    public class SpotifyService : ISpotifyServices, IDisposable
    {
        private IntPtr _sessionHandle;
        private IntPtr _searchHandle;
        private sp_session_config _sessionConfig;
        private string _cacheLocation;
        private string _settingsLocation;
        private const string UserAgent = "poncho";
        private sp_session_callbacks _sessionCallbacks;
        private const int SpotifyApiVersion = 9;


        public SpotifyService()
        {
            InitializeSession();
        }

        private sp_error InitializeSession()
        {
            _cacheLocation = "tmp";
            _settingsLocation = "tmp";


            _sessionHandle = IntPtr.Zero;

            IntPtr appKeyPointer = Marshal.AllocHGlobal(KeyManager.ApplicationKey.Length);
            Marshal.Copy(KeyManager.ApplicationKey, 0, appKeyPointer, KeyManager.ApplicationKey.Length);

            _sessionCallbacks.logged_in = Marshal.GetFunctionPointerForDelegate(new logged_in(LoginCallBack));
            IntPtr sessionCallbacksPtr = Marshal.AllocHGlobal(Marshal.SizeOf(_sessionCallbacks));
            Marshal.StructureToPtr(_sessionCallbacks, sessionCallbacksPtr, true);


            _sessionConfig = new sp_session_config
            {
                api_version = SpotifyApiVersion,
                cache_location = _cacheLocation,
                settings_location = _cacheLocation,
                application_key = appKeyPointer,
                application_key_size = KeyManager.ApplicationKey.Length,
                user_agent = UserAgent,
                callbacks = IntPtr.Zero,
                userdata = IntPtr.Zero,
                compress_playlists = false,
                dont_save_metadata_for_playlists = true,
                initially_unload_playlists = true
            };


            var error = libspotify.sp_session_create(ref _sessionConfig, out _sessionHandle);

            return error;
        }

        public void EndSession()
        {
            libspotify.sp_session_release(_sessionHandle);
        }

        public void FetchPlaylistTracks(PlayList playlist)
        {
            throw new NotImplementedException();
        }

        public void RequestLogin(string username, string password)
        {
            lock (libspotify.Mutex)
            {
                libspotify.sp_session_login(_sessionHandle,username, password, true);
            }
        }

        public void Logout()
        {
            lock (libspotify.Mutex)
            {
                libspotify.sp_session_logout(_sessionHandle);
            }
        }

        private void LoginCallBack(IntPtr sessionHandle, sp_error error)
        {
            Console.WriteLine("Login result: " + error);
        }

        public void Search(string searchText)
        {
            var queryAsChar = searchText.ToCharArray();

            var callbackDelegate = new SearchCallbackDelegate(SearchCallback);
            libspotify.sp_search_create(_sessionHandle, ref queryAsChar, 0, 100, 0, 100, 0, 100, Marshal.GetFunctionPointerForDelegate(callbackDelegate), new IntPtr());
        }

        private void SearchCallback(IntPtr searchHandle, IntPtr userdataPointer)
        {
            throw new NotImplementedException();
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void SearchCallbackDelegate(IntPtr searchHandle, IntPtr userdataPointer);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void logged_in(IntPtr sessionHandle, sp_error error);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void logged_out(IntPtr sessionHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void metadata_updated(IntPtr sessionHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void connection_error(IntPtr sessionHandle, sp_error error);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void message_to_user(IntPtr sessionHandle, ref char[] message);

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


        public void Dispose()
        {
            EndSession();
        }

        
    }


}

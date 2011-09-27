using System;
using System.Runtime.InteropServices;
using Poncho.Models.Services.Enums;

namespace Poncho.Models.Services.Structs
{
// ReSharper disable InconsistentNaming
    public struct sp_session_callbacks
    {

        public sp_session_callbacks(logged_in loggedInDel)
        {
            _loggedInDelegate = loggedInDel;
        }

        public logged_in _loggedInDelegate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void logged_in(IntPtr sessionHandle, sp_error error);

        public delegate void logged_out(IntPtr sessionHandle);

        public delegate void metadata_updated(IntPtr sessionHandle);

        public delegate void connection_error(IntPtr sessionHandle, sp_error error);

        public delegate void message_to_user(IntPtr sessionHandle, ref char[] message);

        public delegate void notify_main_thread(IntPtr sessionHandle);

        public delegate void music_delivery(
            IntPtr sessionHandle, ref sp_audioformat format, IntPtr frames, int num_frames);

        public delegate void play_token_lost(IntPtr sessionHandle);

        public delegate void log_message(IntPtr sessionHandle, ref char[] data);

        public delegate void end_of_track(IntPtr sessionHandle);

        public delegate void streaming_error(IntPtr sessionHandle, sp_error error);

        public delegate void userinfo_updated(IntPtr sessionHandle);

        public delegate void start_playback(IntPtr sessionHandle);

        public delegate void stop_playback(IntPtr sessionHandle);

        public delegate void get_audio_buffet_stats(IntPtr sessionHandle, ref sp_audio_buffer_stats stats);

        public delegate void offline_status_updated(IntPtr sessionHandle);

    }

    // ReSharper restore InconsistentNaming

}
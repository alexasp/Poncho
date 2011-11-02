using System;
using System.Runtime.InteropServices;

namespace SpotifyService.Structs
{
// ReSharper disable InconsistentNaming
    [StructLayout(LayoutKind.Sequential)]
    public struct sp_session_callbacks
    {
        internal IntPtr logged_in;
        internal IntPtr logged_out;
        internal IntPtr metadata_updated;
        internal IntPtr connection_error;
        internal IntPtr message_to_user;
        internal IntPtr notify_main_thread;
        internal IntPtr music_delivery;
        internal IntPtr play_token_lost;
        internal IntPtr log_message;
        internal IntPtr end_of_track;
        internal IntPtr streaming_error;
        internal IntPtr userinfo_updated;
        internal IntPtr start_playback;
        internal IntPtr stop_playback;
        internal IntPtr get_audio_buffer_stats;
        internal IntPtr offline_status_updated;
    }

    // ReSharper restore InconsistentNaming

}
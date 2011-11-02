using System;
using System.Runtime.InteropServices;

namespace SpotifyService.Structs
{
// ReSharper disable InconsistentNaming
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    public struct sp_session_config
    {
        public int api_version;
        public string cache_location;
        public string settings_location;
        public IntPtr application_key;
        public int application_key_size;
        public string user_agent;
        public IntPtr callbacks;
        public IntPtr userdata;
        public bool compress_playlists;
        public bool dont_save_metadata_for_playlists;
        public bool initially_unload_playlists;
    }
    // ReSharper restore InconsistentNaming
}

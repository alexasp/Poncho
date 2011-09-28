using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Poncho.Models.Services.Structs
{
// ReSharper disable InconsistentNaming
    [StructLayout(LayoutKind.Sequential)]
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
        public int compress_playlists;
        public int dont_save_metadata_for_playlists;
        public int initially_unload_playlists;
    }
    // ReSharper restore InconsistentNaming
}

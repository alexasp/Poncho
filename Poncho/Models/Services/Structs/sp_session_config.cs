using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Poncho.Models.Services.Structs
{
// ReSharper disable InconsistentNaming
    
    struct sp_session_config
    {
        internal int api_version;
        internal string cache_location;
        internal string settings_location;
        internal IntPtr application_key;
        internal int application_key_size;
        internal string user_agent;
        internal IntPtr callbacks;
        internal IntPtr userdata;
        public int compress_playlists;
        public int dont_save_metadata_for_playlists;
        public int initially_unload_playlists;
    }
    // ReSharper restore InconsistentNaming
}

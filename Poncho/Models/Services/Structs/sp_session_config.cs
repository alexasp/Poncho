using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Poncho.Models.Services.Structs
{
// ReSharper disable InconsistentNaming
    [StructLayout(LayoutKind.Sequential)]
    struct sp_session_config
    {
        public sp_session_config(int apiVersion, string cacheLocation, string settingsLocation, IntPtr applicationKey, int applicationKeySize, string userAgent, IntPtr callbacks)
        {
            api_version = apiVersion;
            cache_location = cacheLocation;
            settings_location = settingsLocation;
            application_key = applicationKey;
            application_key_size = applicationKeySize;
            user_agent = userAgent;
            this.callbacks = callbacks;
        }

        public int api_version;
        public string cache_location;
        public string settings_location;
        public IntPtr application_key;
        public int application_key_size;
        public string user_agent;
        public IntPtr callbacks;
    }
    // ReSharper restore InconsistentNaming
}

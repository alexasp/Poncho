using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Poncho.Models.Services.Enums;
using Poncho.Models.Services.Structs;

namespace Poncho.Models.Services
{
    public class libspotify
    {

        internal static object Mutex = new object();

        [DllImport("spotify.dll")]
        //(sp_error) sp_session_create(const sp_session_config *config, sp_session **sess);
        public static extern sp_error sp_session_create(ref sp_session_config config, out IntPtr sessionHandle);

        [DllImport("spotify.dll")]
        //(void) sp_session_release(sp_session *sess);
        public static extern void sp_session_release(IntPtr sessionHandle);

        [DllImport("spotify.dll")]
        //(void) sp_session_login(sp_session *session, const char *username, const char *password, bool remember_me);
        public static extern void sp_session_login(IntPtr session, string username, string password,
                                                   bool rememberMe);

        [DllImport("spotify.dll")]
        //(void) sp_session_logout(sp_session *session);
        public static extern void sp_session_logout(IntPtr session);


        [DllImport("spotify.dll")]
        public static extern void sp_search_create(IntPtr session, ref char[] query, int trackOffset,
                                                    int trackCount, int albumOffset, int albumCount,
                                                    int artistOffset, int artistCount,
                                                    IntPtr callbackDelegate, IntPtr userdataPointer);

        [DllImport("spotify.dll")]
        //(sp_error) sp_search_error(sp_search *search);
        public static extern UInt32 sp_search_error(IntPtr searchHandle);

        [DllImport("spotify.dll")]
        //(const char *) sp_search_did_you_mean(sp_search *search);
        public static extern IntPtr sp_search_did_you_mean(IntPtr searchHandle);

        [DllImport("spotify.dll")]
        //(const char *) sp_search_query(sp_search *search);
        public static extern IntPtr sp_search_query(IntPtr searchHandle);

        [DllImport("spotify.dll")]
        public static extern int sp_search_total_tracks(IntPtr searchHandle);

        [DllImport("spotify.dll")]
        public static extern int sp_search_num_tracks(IntPtr searchHandle);

        [DllImport("spotify.dll")]
        //(sp_track *) sp_search_track(sp_search *search, int index);
        public static extern IntPtr sp_search_track(IntPtr searchHandle, int index);

        [DllImport("spotify.dll")]
        //(int) sp_search_num_albums(sp_search *search);
        public static extern int sp_search_num_albums(IntPtr searchHandle);

        [DllImport("spotify.dll")]
        //(sp_album *) sp_search_album(sp_search *search, int index);
        public static extern IntPtr sp_search_album(IntPtr searchHandle, int index);

        [DllImport("spotify.dll")]
        //(int) sp_search_num_artists(sp_search *search);
        public static extern int sp_search_num_artists(IntPtr searchHandle);

        [DllImport("spotify.dll")]
        //(sp_artist *) sp_search_artist(sp_search *search, int index);
        public static extern IntPtr sp_search_artist(IntPtr searchHandle, int index);

        [DllImport("spotify.dll")]
        //(const char *) sp_artist_name(sp_artist *artist);
        public static extern char sp_artist_name(IntPtr artistPointer);

        [DllImport("spotify.dll")]
        //(const char *) sp_album_name(sp_album *album);
        public static extern IntPtr sp_album_name(IntPtr albumPointer);

        [DllImport("spotify.dll")]
        //(int) sp_album_year(sp_album *album);
        public static extern int sp_album_year(IntPtr albumPointer);

        [DllImport("spotify.dll")]
        //(const char*) sp_error_message(sp_error error);
        public static extern IntPtr sp_error_message(UInt32 errorType);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poncho.Models.Services.Enums
{
    // ReSharper disable InconsistentNaming
    public enum sp_error
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

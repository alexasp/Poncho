using SpotifyService.Enums;

namespace SpotifyService.Messages
{
    public class LoginResultMessage
    {
        public LoginResultMessage(bool success, sp_error error)
        {
            Success = success;
            Message = error;
        }

        public bool Success { get; private set; }
        public sp_error Message { get; set; }
    }
}
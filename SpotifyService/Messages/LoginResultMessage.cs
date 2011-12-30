namespace SpotifyService.Messages
{
    public class LoginResultMessage
    {
        public LoginResultMessage(bool success)
        {
            Success = success;
        }

        public bool Success { get; private set; }
    }
}
namespace SpotifyService.Messages
{
    public class UserFeedbackMessage
    {
        public UserFeedbackMessage(string text)
        {
            Text = text;
        }

        public string Text { get; private set; }
    }
}
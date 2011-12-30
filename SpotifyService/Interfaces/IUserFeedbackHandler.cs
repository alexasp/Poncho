using SpotifyService.Model.Enums;

namespace SpotifyService.Model.Interfaces
{
    public interface IUserFeedbackHandler
    {
        void Display(UserFeedback feedback);
    }
}
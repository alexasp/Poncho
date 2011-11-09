using SpotifyService.Models.Enums;

namespace SpotifyService.Models.Interfaces
{
    public interface IUserFeedbackHandler
    {
        void Display(UserFeedback trackNotPlayable);
    }
}
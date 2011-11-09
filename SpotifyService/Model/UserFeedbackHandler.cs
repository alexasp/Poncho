using SpotifyService.Interfaces;
using SpotifyService.Model.Enums;

namespace SpotifyService.Model
{
    public class UserFeedbackHandler
    {
        private IUserFeedBackDisplay _userFeedbackViewModel;

        public UserFeedbackHandler(IUserFeedBackDisplay userFeedbackViewModel)
        {
            _userFeedbackViewModel = userFeedbackViewModel;
        }

        public void Display(UserFeedback userFeedback)
        {
            switch (userFeedback)
            {
                case UserFeedback.TrackNotPlayable:
                    _userFeedbackViewModel.DisplayMessage("This track is not playable.");
                    break;
                case UserFeedback.SomeTracksNotPlayable:
                    _userFeedbackViewModel.DisplayMessage("One or more of these tracks were not playable.");
                    break;
                case UserFeedback.NoSearchTextEntered:
                    _userFeedbackViewModel.DisplayMessage("You need to enter a search text.");
                    break;
                case UserFeedback.InvalidLoginInfo:
                    _userFeedbackViewModel.DisplayMessage("The login information you provided was incorrect.");
                    break;
            }
        }
    }
}
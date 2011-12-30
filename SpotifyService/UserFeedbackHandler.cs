using Caliburn.Micro;
using SpotifyService.Interfaces;
using SpotifyService.Messages;
using SpotifyService.Model.Enums;
using SpotifyService.Model.Interfaces;

namespace SpotifyService.Model
{
    public class UserFeedbackHandler : IUserFeedbackHandler 
    {
        private readonly IEventAggregator _eventAggregator;

        public UserFeedbackHandler(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Display(UserFeedback userFeedback)
        {
            switch (userFeedback)
            {
                case UserFeedback.TrackNotPlayable:
                    _eventAggregator.Publish(new UserFeedbackMessage("This track is not playable."));
                    break;
                case UserFeedback.SomeTracksNotPlayable:
                    _eventAggregator.Publish(new UserFeedbackMessage("One or more of these tracks were not playable."));
                    break;
                case UserFeedback.NoSearchTextEntered:
                    _eventAggregator.Publish(new UserFeedbackMessage("You need to enter a search text."));
                    break;
                case UserFeedback.InvalidLoginInfo:
                    _eventAggregator.Publish(new UserFeedbackMessage("The login information you provided was incorrect."));
                    break;
            }
        }
    }
}
using System;
using Caliburn.Micro;
using SpotifyService.Interfaces;
using SpotifyService.Messages;
using SpotifyService.Model.Enums;
using SpotifyService.Model.Interfaces;

namespace SpotifyService.Model
{
    public class LoginManager : ILoginManager, IHandle<LoginResultMessage>
    {
        private readonly IMusicServices _musicServices;
        private IUserFeedbackHandler _userFeedbackHandler;
        

        public LoginManager(IMusicServices musicServices, IUserFeedbackHandler userFeedbackHandler)
        {
            _musicServices = musicServices;
            _userFeedbackHandler = userFeedbackHandler;
        }


        public void AttemptLogin(string userName, string password)
        {
            if(String.IsNullOrEmpty(userName))
                _userFeedbackHandler.Display(UserFeedback.EmptyUsername);   
            if (String.IsNullOrEmpty(password))
                _userFeedbackHandler.Display(UserFeedback.EmptyPassword);

            _musicServices.InitializeSession(userName, password);
        }

        public void Handle(LoginResultMessage message)
        {
            throw new NotImplementedException();
        }
    }
}

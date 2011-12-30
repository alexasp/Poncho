using System;
using Caliburn.Micro;
using Poncho.ViewModels.Interfaces;
using SpotifyService.Interfaces;
using SpotifyService.Messages;
using SpotifyService.Model.Enums;
using SpotifyService.Model.Interfaces;

namespace Poncho.ViewModels
{
    public class LoginViewModel : PropertyChangedBase, ILoginViewModel
    {
        private readonly ILoginManager _loginManager;
        private readonly IUserFeedbackHandler _userFeedbackHandler;

        public LoginViewModel(ILoginManager loginManager, IUserFeedbackHandler userFeedbackHandler, IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
            _loginManager = loginManager;
            _userFeedbackHandler = userFeedbackHandler;
        }
        
        public string Password { get; set; }

        public string Username { get; set; }

        public void Login()
        {
            _loginManager.AttemptLogin(Username, Password);
        }


        public void Handle(LoginResultMessage message)
        {
            Console.WriteLine("Received message");
            _userFeedbackHandler.Display(UserFeedback.LoginFailed);
        }
    }
}
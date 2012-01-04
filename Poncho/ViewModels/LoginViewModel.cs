using System;
using System.Diagnostics;
using System.Windows;
using Caliburn.Micro;
using Poncho.ViewModels.Interfaces;
using SpotifyService.Enums;
using SpotifyService.Interfaces;
using SpotifyService.Messages;
using SpotifyService.Model.Interfaces;

namespace Poncho.ViewModels
{
    public class LoginViewModel : Screen, ILoginViewModel
    {
        private readonly ILoginManager _loginManager;

        public LoginViewModel(ILoginManager loginManager, IUserFeedbackHandler userFeedbackHandler, IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
            _loginManager = loginManager;
            Output = "Please enter your user information.";
        }

        public string Password { get; set; }
        public string Username { get; set; }
        private string _output;
        public string Output
        {
            get { return _output; }
            set { _output = value; NotifyOfPropertyChange(() => Output); }
        }


        public void Login()
        {
            _loginManager.AttemptLogin(Username, Password);

        }

        public void Handle(LoginResultMessage message)
        {
            Debug.WriteLine("Handling loginresult, error {0}", message.Message);
            switch (message.Message)
            {
                case sp_error.SP_ERROR_BAD_USERNAME_OR_PASSWORD:
                    Output = "Bad username or password.";
                    break;
                case sp_error.SP_ERROR_OK:
                    Output = "Login successful.";
                    break;
                default:
                    Output = "Login failed.";
                    break;
            }

        }
    }
}
using System;
using Poncho.Models.Enums;
using Poncho.Models.Interfaces;
using Poncho.ViewModels.Interfaces;

namespace Poncho.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        private readonly ILoginManager _loginManager;
        private readonly IUserFeedbackHandler _userFeedbackHandler;

        public LoginViewModel(ILoginManager loginManager, IUserFeedbackHandler userFeedbackHandler)
        {
            _loginManager = loginManager;
            _userFeedbackHandler = userFeedbackHandler;
        }

        public string Password { get; set; }

        public string Username { get; set; }

        public void InvalidLogin()
        {
            _userFeedbackHandler.Display(UserFeedback.InvalidLoginInfo);
        }

        public void Login()
        {
            _loginManager.AttemptLogin(Username, Password);
        }

    }
}
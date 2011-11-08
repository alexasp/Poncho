using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Poncho.ViewModels;
using Poncho.ViewModels.Interfaces;
using Rhino.Mocks;
using SpotifyService.Models.Enums;
using SpotifyService.Models.Interfaces;

namespace PonchoTests.ViewModelsTests
{
    [TestFixture]
    class LogInViewModelTests
    {
        private ILoginViewModel _loginViewModel;
        private ILoginManager _loginManager;
        private IUserFeedbackHandler _userFeedbackHandler;

        [SetUp]
        public void Init()
        {
            _userFeedbackHandler = MockRepository.GenerateMock<IUserFeedbackHandler>();
            _loginManager = MockRepository.GenerateMock<ILoginManager>();
       
            _loginViewModel = new LoginViewModel(_loginManager, _userFeedbackHandler);
        }

        [Test]
        public void Login_LoginEntered_QueriesLogInManager()
        {
            _loginViewModel.Username = "aspis";
            _loginViewModel.Password = "123asd";


            _loginManager.Expect(x => x.AttemptLogin(_loginViewModel.Username, _loginViewModel.Password));

            _loginViewModel.Login();

            _loginManager.VerifyAllExpectations();
        }

        [Test]
        public void InvalidLogin_MessagesUserFeedbackHandler()
        {
            _userFeedbackHandler.Expect(x => x.Display(UserFeedback.InvalidLoginInfo));

            _loginViewModel.InvalidLogin();

            _loginManager.VerifyAllExpectations();
        }

    }
}

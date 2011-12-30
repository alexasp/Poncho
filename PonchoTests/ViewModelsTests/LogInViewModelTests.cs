using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using NUnit.Framework;
using Poncho.ViewModels;
using Poncho.ViewModels.Interfaces;
using Rhino.Mocks;
using SpotifyService.Messages;
using SpotifyService.Model.Enums;
using SpotifyService.Model.Interfaces;

namespace PonchoTests.ViewModelsTests
{
    [TestFixture]
    class LogInViewModelTests
    {
        private ILoginViewModel _loginViewModel;
        private ILoginManager _loginManager;
        private IUserFeedbackHandler _userFeedbackHandler;
        private IEventAggregator _eventAggregator;

        [SetUp]
        public void Init()
        {
            _userFeedbackHandler = MockRepository.GenerateStub<IUserFeedbackHandler>();
            _loginManager = MockRepository.GenerateStub<ILoginManager>();
            _eventAggregator = MockRepository.GenerateStub<IEventAggregator>();
            _loginViewModel = new LoginViewModel(_loginManager, _userFeedbackHandler, _eventAggregator);
        }

        [Test]
        public void Login_LoginEntered_QueriesLogInManager()
        {
            _loginViewModel.Username = "aspis";
            _loginViewModel.Password = "123asd";



            _loginViewModel.Login();

            _loginManager.AssertWasCalled(x => x.AttemptLogin(_loginViewModel.Username, _loginViewModel.Password));

        }

        [Test]
        public void Handle_LoginNotSuccesful_CallUserFeedbackHandlerToDisplayLoginFailed()
        {
            _loginViewModel.Handle(new LoginResultMessage(false));

            _userFeedbackHandler.AssertWasCalled(x => x.Display(UserFeedback.LoginFailed));
        }
    }
}

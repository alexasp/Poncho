using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Poncho.ViewModels.Interfaces;
using SpotifyService.Messages;

namespace Poncho.ViewModels
{
    public class ShellViewModel : Conductor<Screen>.Collection.OneActive, IShellViewModel
    {
        private readonly ILoginViewModel _loginViewModel;
        private readonly IMainViewModel _mainViewModel;

        public ShellViewModel(ILoginViewModel loginViewModel, IMainViewModel mainViewModel, IEventAggregator eventAggregator)
        {
            _loginViewModel = loginViewModel;
            _mainViewModel = mainViewModel;

            eventAggregator.Subscribe(this);

            ShowLoginScreen();
        }

        private void ShowLoginScreen()
        {
            ActivateItem(_loginViewModel as Screen);
            Debug.WriteLine("Attempting to show login screen, active screen is now {0}", ActiveItem);
        }

        private void ShowMainScreen()
        {
            ActivateItem(_mainViewModel as Screen);
        }

        public void Handle(LoginResultMessage message)
        {
            if(message.Success)
                ShowMainScreen();
        }
    }
}

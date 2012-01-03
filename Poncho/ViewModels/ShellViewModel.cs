using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using SpotifyService.Messages;

namespace Poncho.ViewModels
{
    class ShellViewModel : Conductor<PropertyChangedBase>.Collection.OneActive, IHandle<LoginResultMessage>
    {
        private readonly PropertyChangedBase _mainViewModel;

        public ShellViewModel(PropertyChangedBase mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public void Handle(LoginResultMessage message)
        {
            if(message.Success)
                ActivateItem(_mainViewModel);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Poncho.Models;
using Poncho.Models.Enums;
using Poncho.Models.Interfaces;
using Rhino.Mocks;

namespace PonchoTests.ModelsTests
{
    [TestFixture]
    class UserFeedbackHandlerTests
    {
        private UserFeedbackHandler _userFeedbackHandler;
        private IUserFeedbackViewModel _userFeedbackViewModel;

        [SetUp]
        public void Init()
        {
            _userFeedbackViewModel = MockRepository.GenerateMock<IUserFeedbackViewModel>();
            _userFeedbackHandler = new UserFeedbackHandler(_userFeedbackViewModel);
        }

        [Test]
        public void Display_TrackNotPlayable_SetsUserMessageOnUserFeedbackViewModel()
        {
            _userFeedbackViewModel.Expect(x => x.DisplayMessage("This track is not playable."));

            _userFeedbackHandler.Display(UserFeedback.TrackNotPlayable);

            _userFeedbackViewModel.VerifyAllExpectations();
        }

        [Test]
        public void Display_TracksNotPlayable_SetsUserMessageOnUserFeedbackViewModel()
        {
            _userFeedbackViewModel.Expect(x => x.DisplayMessage("One or more of these tracks were not playable."));

            _userFeedbackHandler.Display(UserFeedback.SomeTracksNotPlayable);

            _userFeedbackViewModel.VerifyAllExpectations();

        }

        [Test]
        public void Display_NoSearchTextEntered_SetsUserMessageOnUserFeedbackViewModel()
        {
            _userFeedbackViewModel.Expect(x => x.DisplayMessage("You need to enter a search text."));

            _userFeedbackHandler.Display(UserFeedback.NoSearchTextEntered);

            _userFeedbackViewModel.VerifyAllExpectations();

        }

        [Test]
        public void Display_InvalidLoginInfo_SersUserMessageOnUserFeedbackViewModel()
        {
            _userFeedbackViewModel.Expect(x => x.DisplayMessage("The login information you provided was incorrect."));

            _userFeedbackHandler.Display(UserFeedback.InvalidLoginInfo);

            _userFeedbackViewModel.VerifyAllExpectations();
        }
    }
}

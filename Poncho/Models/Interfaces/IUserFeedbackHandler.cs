using Poncho.Models.Enums;

namespace Poncho.Models.Interfaces
{
    public interface IUserFeedbackHandler
    {
        void Display(UserFeedback trackNotPlayable);
    }
}
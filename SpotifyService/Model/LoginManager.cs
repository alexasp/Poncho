using SpotifyService.Interfaces;
using SpotifyService.Models.Interfaces;

namespace SpotifyService.Models
{
    public class LoginManager : ILoginManager
    {
        private readonly IMusicServices _musicServices;
        

        public LoginManager(IMusicServices musicServices)
        {
            _musicServices = musicServices;
        }

        
        public void AttemptLogin(string userName, string password)
        {
            _musicServices.InitializeSession(userName, password);
        }
    }
}

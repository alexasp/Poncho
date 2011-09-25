using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poncho.Models.Interfaces;
using System.Runtime.InteropServices;
using Poncho.Models.services;

namespace Poncho.Models
{
    public class LoginManager : ILoginManager
    {
        private readonly ISpotifyServices _spotifyServices;
        

        public LoginManager(ISpotifyServices spotifyServices)
        {
            _spotifyServices = spotifyServices;
        }

        
        public void AttemptLogin(string userName, string password)
        {
            _spotifyServices.RequestLogin(userName, password);
        }
    }
}

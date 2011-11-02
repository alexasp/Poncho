namespace SpotifyService.Cargo
{
    public class Track
    {
        private readonly bool _playable;

        public Track(bool playable)
        {
            _playable = playable;
        }

        public bool Playable
        {
            get { return _playable; }
        }
    }
}

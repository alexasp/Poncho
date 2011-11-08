namespace SpotifyService.Cargo
{
    public class Track
    {
        private readonly bool _playable;
        private int _handle;

        public Track(bool playable)
        {
            _playable = playable;
        }

        public bool Playable
        {
            get { return _playable; }
        }

        public int Handle
        {
            get {
                return _handle;
            }
            private set {
                _handle = value;
            }
        }
    }
}

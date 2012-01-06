namespace SpotifyService.Cargo

{
    public class Track
    {
        private readonly bool _playable;
        private int _handle;

        public string Name { get; set; }

        public Track(int handle, string name, bool playable)
        {
            _playable = playable;
            Name = name;
            Handle = handle;
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

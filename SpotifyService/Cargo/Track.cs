namespace SpotifyService.Cargo

{
    public class Track
    {
        private readonly bool _playable;
        private int _handle;

        public string Name { get; private set; }

        public string Artist { get; private set; }

        public string Album { get; private set; }

        public Track(int handle, string name, string artist, string album, bool playable)
        {
            _playable = playable;
            Album = album;
            Artist = artist;
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

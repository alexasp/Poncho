namespace Poncho.ViewModels.Interfaces
{
    public interface ITrackControlViewModel
    {
        bool IsPlaying { get; set; }
        void PlayPause();
    }
}
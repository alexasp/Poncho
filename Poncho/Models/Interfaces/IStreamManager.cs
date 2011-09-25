using Poncho.Models.Cargo;

namespace Poncho.Models.Interfaces
{
    public interface IStreamManager
    {
        void RequestTrackStream(Track track);
    }
}
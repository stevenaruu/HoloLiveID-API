using HoloLiveID_API.Model;

namespace HoloLiveID_API.Output
{
    public class HoloUserOutput
    {
        public HoloUser? payload { get; set; }
        public HoloUserOutput()
        {
            payload = new HoloUser();
        }
    }
}

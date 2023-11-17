using HoloLiveID_API.Model;

namespace HoloLiveID_API.Output
{
    public class ListHoloUserOutput
    {
        public List<HoloUser>? payload { get; set; }
        public ListHoloUserOutput()
        {
            payload = new List<HoloUser>();
        }
    }
}

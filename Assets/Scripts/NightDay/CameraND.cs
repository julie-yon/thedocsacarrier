using Com.LuisPedroFonseca.ProCamera2D;

namespace Docsa
{
    public class CameraND : NightDaySwitchEvent
    {
        public float nightSize;
        public float daySize;
        public float duration;
        public ProCamera2DRooms rooms;
        
        // Update is called once per frame
        public override void ChangeState(bool isNight)
        {
            if (isNight)
            {
                ProCamera2D.Instance.UpdateScreenSize(nightSize, duration);
                rooms.OriginalSize = nightSize;
            }
            else
            {
                ProCamera2D.Instance.UpdateScreenSize(daySize, duration);
                rooms.OriginalSize = daySize;
            }
        }
        
    }
}
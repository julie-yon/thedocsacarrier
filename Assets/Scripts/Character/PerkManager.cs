
using Utility;

namespace Docsa
{
    public class PerkManager : Singleton<PerkManager>
    {
        public PerkData Data;

        void Reset()
        {
            Data.Init();
        }

        void OnApplicationQuit()
        {
        }
    }
}
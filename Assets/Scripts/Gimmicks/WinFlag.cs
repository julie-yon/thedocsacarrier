using Utility;

namespace Docsa.Gimmick
{
    public class WinFlag : Gimmick
    {
        public SoundArgs ClearSoundArg;
        public override void GimmickInvoke()
        {
            if (ClearSoundArg)
                SoundManager.instance.Play(ClearSoundArg);
            Core.instance.ChunkClear();
        }
    }
}
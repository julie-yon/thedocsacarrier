using Utility;
using UnityEngine;

namespace Docsa.Gimmick
{
    public class WinFlag : Gimmick
    {
        public AudioClip ClearAudioClip;
        public SoundArgs ClearSoundArg;
        public override void GimmickInvoke()
        {
            if (ClearSoundArg)
                SoundManager.instance.Play(ClearAudioClip, ClearSoundArg);
            Core.instance.ChunkClear();
        }
    }
}
using Utility;
using UnityEngine;

namespace Docsa.Gimmick
{
    [RequireComponent(typeof(EventTrigger))]
    public class WinFlag : Gimmick
    {
        public AudioClip ClearAudioClip; 
        public SoundArgs ClearSoundArg;

        void Reset()
        {
            EventTrigger ET = GetComponent<EventTrigger>();
            ET.ClearEnterEvent();
            ET.AddEnterEvent(Invoke);

            ActivateOnAwake = true;
            StartOnAwake = true;
        }
        
        public override void Invoke()
        {
            base.Invoke();
            if (ClearAudioClip && ClearSoundArg)
                SoundManager.instance.Play(ClearAudioClip, ClearSoundArg);
            StageManager.instance.Clear();
        }
    }
}
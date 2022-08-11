using dkstlzu.Utility;
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
            if (!Started) return;
        
            if (ClearAudioClip && ClearSoundArg)
                GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>().Play(ClearAudioClip, ClearSoundArg);
            StageManager.instance.Clear();
        }
    }
}
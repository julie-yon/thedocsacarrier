using UnityEngine;
using UnityEditor.Events;

using Utility;

namespace Docsa.Events
{
    [RequireComponent(typeof(EventTrigger))]
    public class AnimationTrigger : EventTrigger
    {
        public Animation TargetAnimation;
        public string AnimClipName = "";

        void Reset()
        {
            UnityEventTools.AddVoidPersistentListener(OnTriggerEnterEvent, PlayAnimation);
        }

        public void PlayAnimation()
        {
            if (AnimClipName.Equals(string.Empty))
            {
                TargetAnimation.Play();
            } else
            {
                TargetAnimation.Play(AnimClipName);
            }
        }
    }
}
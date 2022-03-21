using UnityEngine;

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
#if UNITY_EDITOR
            AddEnterEvent(PlayAnimation);
#endif
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
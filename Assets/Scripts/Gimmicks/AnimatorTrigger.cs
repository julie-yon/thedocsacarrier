using UnityEngine;
using UnityEditor.Events;

using Utility;

namespace Docsa.Events
{
    public class AnimatorTrigger : EventTrigger
    {
        public Animator TargetAnimator;
        public string TriggerName = "PlayAnimator";

        void Reset()
        {
            UnityEventTools.AddVoidPersistentListener(OnTriggerEnterEvent, PlayAnimator);
        }

        public void PlayAnimator()
        {
            TargetAnimator.SetTrigger(TriggerName);
        }
    }
}
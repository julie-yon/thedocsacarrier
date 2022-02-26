using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Events;

using Utility;

namespace Docsa.Events
{
    public class AnimatorTrigger : EventTrigger
    {
        public Animator TargetAnimator;
        public List<string> TriggerNames = new List<string>();

        void Reset()
        {
            UnityEventTools.AddVoidPersistentListener(OnTriggerEnterEvent, PlayAnimator);
        }

        public void PlayAnimator()
        {
            foreach (var name in TriggerNames)
            {
                TargetAnimator.SetTrigger(name);
            }
        }
    }
}
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
        public List<string> TrueBoolNames = new List<string>();
        public List<string> FalseBoolNames = new List<string>();

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

            foreach (var name in TrueBoolNames)
            {
                TargetAnimator.SetBool(name, true);
            }

            foreach (var name in FalseBoolNames)
            {
                TargetAnimator.SetBool(name, false);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Events;

using Docsa.Character;

using Utility;

namespace Docsa.Gimmick
{
    [RequireComponent(typeof(EventTrigger))]
    public class AnimationTrigger : MonoBehaviour
    {
        public Animation TargetAnimation;
        public string AnimClipName = "";

        void Reset()
        {
            UnityEventTools.AddVoidPersistentListener(GetComponent<EventTrigger>().OnTriggerEnterEvent, PlayAnimation);
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
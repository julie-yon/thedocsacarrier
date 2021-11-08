using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Docsa.Character;

namespace Docsa.Gimmick
{
    public class AnimationTrigger : MonoBehaviour
    {
        public LayerMask TargetLayer;
        public GameObject AnimationObject;
        public bool PlayOnlyFirst;
        public string AnimClipName = "";

        private bool isPlayedOnce = false;

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (isPlayedOnce && PlayOnlyFirst)
            {
                return;
            }
            
            if (GetComponent<Collider2D>().IsTouchingLayers(TargetLayer))
            {
                Animation anim;
                if (AnimationObject.TryGetComponent<Animation>(out anim))
                {
                    if (AnimClipName == "")
                    {
                        anim.Play();
                    } else
                    {
                        anim.Play(AnimClipName);
                    }
                    isPlayedOnce = true;
                } else if (anim = AnimationObject.GetComponentInChildren<Animation>())
                {
                    if (AnimClipName == "")
                    {
                        anim.Play();
                    } else
                    {
                        anim.Play(AnimClipName);
                    }
                    isPlayedOnce = true;
                }
            }
        }
    }
}
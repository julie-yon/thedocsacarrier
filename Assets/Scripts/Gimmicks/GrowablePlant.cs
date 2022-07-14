using UnityEngine;
using Docsa;

namespace Docsa.Gimmick
{
    public class GrowablePlant : Gimmick
    {
        public Animator Animator;
        public const string TriggerName = "GrowTrigger";

        public override void Invoke()
        {
            if (!Started) return;

            Animator.SetTrigger(TriggerName);
        }
    }
}
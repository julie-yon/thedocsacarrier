using UnityEngine;
using Docsa.Events;
using Docsa.Character;

namespace Docsa.Gimmick
{
    [RequireComponent(typeof(AnimatorTrigger))]
    public class Crocodile : Gimmick
    {
        [SerializeField][HideInInspector] private AnimatorTrigger AT;

        public float Damage;

        void Reset()
        {
            AT = GetComponent<AnimatorTrigger>();
        }

        public override void Invoke()
        {
            if (!Started) return;

            UzuHama.Hama.GetDamage(Damage);
        }
    }
}
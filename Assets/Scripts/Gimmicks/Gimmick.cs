using UnityEngine;
using Docsa.Character;

using Utility;

namespace Docsa.Gimmick
{
    public class Gimmick : MonoBehaviour
    {
        public AudioClip DamageAudioClip;
        public SoundArgs DamageSoundArg;
        [SerializeField] protected LayerMask UzuhamaLayer;

        public bool gimmickStarted;

        public virtual void GimmickInit()
        {
            gimmickStarted = true;
        }

        public virtual void GimmickInvoke()
        {
        }

        protected virtual void GiveDamage(int damageValue)
        {
            SoundManager.instance.Play(DamageAudioClip, DamageSoundArg);
            UzuHama.Hama.GetDamage(damageValue);
        }
    }
}


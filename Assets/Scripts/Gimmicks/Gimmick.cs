using UnityEngine;
using Docsa.Character;

using Utility;

namespace Docsa.Gimmick
{
    public class Gimmick : MonoBehaviour
    {
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
            SoundManager.instance.Play(DamageSoundArg);
            UzuHama.Hama.GetDamage(damageValue);
        }
    }
}


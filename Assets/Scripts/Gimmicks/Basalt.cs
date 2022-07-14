using UnityEngine;

namespace Docsa.Gimmick
{
    public class Basalt : LinearGimmick
    {
        public int Damage = 10;
        private SpriteRenderer _spriteRenderer;
        
        protected override void Reset()
        {
            base.Reset();
            ET.PlayOnlyFirst = true;
        }

        protected override void Awake()
        {
            base.Awake();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public override bool StartGimmick()
        {
            if (base.StartGimmick())
            {
                _spriteRenderer.enabled = true;
                Animator AM = GetComponentInChildren<Animator>();
                AM.SetBool(AM.GetParameter(0).name, true);
                return true;
            }

            return false;
        }

        public override bool End()
        {
            if (base.End())
            {
                _spriteRenderer.enabled = false;
                Animator AM = GetComponentInChildren<Animator>();
                AM.SetBool(AM.GetParameter(0).name, false);
                return true;
            }

            return false;
        }

        public override void Invoke()
        {
            if (!Started) return;

            Docsa.Character.UzuHama.Hama.GetDamage(Damage);
        }
    }
}



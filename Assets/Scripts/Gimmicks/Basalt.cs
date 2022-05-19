using UnityEngine;

namespace Docsa.Gimmick
{
    public class Basalt : LinearGimmick
    {
        public int Damage = 10;
        protected override void Reset()
        {
            base.Reset();
            ET.PlayOnlyFirst = true;
        }

        public override void StartGimmick()
        {
            base.StartGimmick();
            Animator AM = GetComponentInChildren<Animator>();
            AM.SetBool(AM.GetParameter(0).name, true);
        }

        public override void End()
        {
            base.End();
            Animator AM = GetComponentInChildren<Animator>();
            AM.SetBool(AM.GetParameter(0).name, false);
        }

        public override void Invoke()
        {
            base.Invoke();
            Docsa.Character.UzuHama.Hama.GetDamage(Damage);
        }
    }
}



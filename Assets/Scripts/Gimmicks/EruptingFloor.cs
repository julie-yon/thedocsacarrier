using UnityEngine;

namespace Docsa.Gimmick
{
    public class EruptingFloor : Gimmick
    {
        [Range(1, 3)] public int Height = 1;

        private Animator AT;

        protected override void Awake()
        {
            base.Awake();
            AT = GetComponentInChildren<Animator>();
        }

        public override bool StartGimmick()
        {
            if (base.StartGimmick())
            {
                AT.SetTrigger(AT.GetParameter(Height-1).name);
                return true;
            }

            return false;
        }
    }
}
using UnityEngine;
using Docsa.Events;

namespace Docsa.Gimmick
{
    public class CrackingFloor : Gimmick
    {
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
                AT.SetBool(AT.GetParameter(0).name, true);
                return true;
            }

            return false;
        }

        public override bool End()
        {
            if (base.End())
            {
                AT.SetBool(AT.GetParameter(0).name, false);
                return true;
            }

            return false;
        }        
    }
}
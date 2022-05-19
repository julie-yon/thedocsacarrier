using UnityEngine;
using Docsa.Events;

namespace Docsa.Gimmick
{
    public class CrackingFloor : Gimmick
    {
        private Animator AT;

        void Awake()
        {
            AT = GetComponentInChildren<Animator>();
        }

        public override void StartGimmick()
        {
            base.StartGimmick();
            AT.SetBool(AT.GetParameter(0).name, true);
        }

        public override void End()
        {
            base.End();
            AT.SetBool(AT.GetParameter(0).name, false);
        }        
    }
}
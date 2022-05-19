using UnityEngine;

namespace Docsa.Gimmick
{
    public class EruptingFloor : Gimmick
    {
        [Range(1, 3)] public int Height = 1;

        private Animator AT;

        void Awake()
        {
            AT = GetComponentInChildren<Animator>();
        }

        public override void StartGimmick()
        {
            base.StartGimmick();
            AT.SetTrigger(AT.GetParameter(Height-1).name);
        }
    }
}
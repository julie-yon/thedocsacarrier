﻿using UnityEngine;
using Utility;

namespace Docsa.Gimmick
{
    public class ToxicCloud : LinearGimmick
    {
        public int Damage = 10;
        public float DamageCoolTime = 1;
        private float StayingTime = 0;

        protected override void Reset()
        {
            ET = GetComponent<EventTrigger>();
            ET.ClearStayEvent();
            ET.AddStayEvent(Invoke);
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
            StayingTime += Time.deltaTime;

            if (StayingTime > DamageCoolTime)
            {
                StayingTime = 0;
                Docsa.Character.UzuHama.Hama.GetDamage(Damage);
            }
        }
    }
}
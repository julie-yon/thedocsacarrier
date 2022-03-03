using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Docsa.Gimmick
{
    public class FallingRock : Gimmick
    {
        public int Damage = 10;
        [SerializeField] Animator mainAnimator;
        public Vector2 Direction;
        public int Speed;
        private float _animSpeedCoeff = 2;
        #pragma warning disable 0108

        void Awake()
        {
            mainAnimator.SetFloat("animSpeed", _animSpeedCoeff * Speed);
        }
        void FixedUpdate()
        {
            if (gimmickStarted)
                transform.Translate(Direction * Time.deltaTime * Speed);
        }
        
        public override void GimmickInvoke()
        {
            GiveDamage(Damage);
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docsa.Gimmick
{
    public class Basalt : Gimmick
    {
        public Vector2 Direction;
        void Awake()
        {
            base.Awake();
            Damage = 10;
        }

        void Update()
        {
            transform.Translate(Direction);
        }
        
        protected override void GimmickInvoke()
        {
            base.GimmickInvoke();
        }

        void OnCollisionEnter2D(Collision2D collision2D)
        {
            if (collision2D.gameObject.layer.Equals(UzuhamaLayer))
            {
                GiveDamage(Damage);
            }
        }
    }
}



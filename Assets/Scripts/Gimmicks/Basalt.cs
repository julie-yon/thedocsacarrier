using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docsa.Gimmick
{
    public class Basalt : Gimmick
    {
        void Awake()
        {
            Damage = 10;
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



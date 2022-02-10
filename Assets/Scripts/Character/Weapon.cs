using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docsa.Character
{
    public class Weapon : Projectile
    {
        public LayerMask TargetLayer;

        void OnCollisionEnter2D(Collision2D collision2D)
        {
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docsa.Character
{
    public class Weapon : Projectile
    {
        public float DamageValue = 10;
        public LayerMask TargetLayerMask;

        private List<Hunter> _attackedHunterList = new List<Hunter>();

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
            if (1 << collision.gameObject.layer == TargetLayerMask.value)
            {
                if (_attackedHunterList.Contains(collision.gameObject.GetComponent<Hunter>()))
                {
                    return;
                }

                _attackedHunterList.Add(collision.gameObject.GetComponent<Hunter>());
                collision.gameObject.GetComponent<Hunter>().GetDamage(DamageValue);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _attackedHunterList.Clear();
        }
    }
}
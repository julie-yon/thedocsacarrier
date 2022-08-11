using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docsa.Character
{
    public class Weapon : Projectile
    {
        private List<Hunter> _attackedHunterList = new List<Hunter>();

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
            if (_attackedHunterList.Contains(collision.gameObject.GetComponent<Hunter>()))
            {
                return;
            }

            _attackedHunterList.Add(collision.gameObject.GetComponent<Hunter>());
            collision.gameObject.GetComponent<Hunter>().GetDamage(DamageValue);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _attackedHunterList.Clear();
        }
    }
}
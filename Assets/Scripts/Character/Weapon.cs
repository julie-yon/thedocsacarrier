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

        void OnCollisionEnter2D(Collision2D collision2D)
        {
            if (1 << collision2D.gameObject.layer == TargetLayerMask.value)
            {
                if (_attackedHunterList.Contains(collision2D.gameObject.GetComponent<Hunter>()))
                {
                    return;
                }

                _attackedHunterList.Add(collision2D.gameObject.GetComponent<Hunter>());
                collision2D.gameObject.GetComponent<Hunter>().GetDamage(DamageValue);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _attackedHunterList.Clear();
        }
    }
}
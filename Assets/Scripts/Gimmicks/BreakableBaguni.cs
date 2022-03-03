using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docsa.Gimmick
{
    public class BreakableBaguni : MonoBehaviour
    {
        public float HP = 300;
        public LayerMask TargetLayerMask;
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (((1 << collision.gameObject.layer) & TargetLayerMask.value) > 0)
            {
                GetDamage(collision.gameObject.GetComponent<Projectile>().DamageValue);
            }
        }

        public void GetDamage(float damageValue)
        {
            HP -= damageValue;
            if (HP <= 0) Break();
        }

        public void Break()
        {
            Destroy(gameObject);
        }
    }
}
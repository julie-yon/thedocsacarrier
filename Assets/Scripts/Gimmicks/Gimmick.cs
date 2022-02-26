using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Docsa.Character;

namespace Docsa.Gimmick
{
    public class Gimmick : MonoBehaviour
    {
        public int Damage;
        [SerializeField] protected LayerMask UzuhamaLayer;

        protected virtual void Awake()
        {
        }
        
        public virtual void GimmickInvoke()
        {
            
        }

        protected virtual void GiveDamage(int damageValue) //virtual일 필요가 있을지?!
        {
            UzuHama.Hama.GetDamage(damageValue);
        }

    }

}


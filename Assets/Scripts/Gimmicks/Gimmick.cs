using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Docsa.Character;

namespace Docsa.Gimmick
{
    public class Gimmick : MonoBehaviour
    {
        // Start is called before the first frame update
        protected virtual void GimmickInvoke()
        {
            
        }

        protected virtual void GiveDamage(int damageValue) //virtual일 필요가 있을지?!
        {
            UzuHama.Hama.GetDamage(damageValue);
        }

    }

}


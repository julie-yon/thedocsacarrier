using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Docsa.Character
{

    [RequireComponent(typeof(CharacterBehaviour))]
    public class Character : MonoBehaviour
    {
        public HPBar HPBar;
        public CharacterBehaviour Behaviour;
        public Transform GrabDocsaPosition;
        private const int maxHP = 100;
        private int currentHP = 100;
        public int MaxHP{
            get {return maxHP;}
        }

        public int CurrentHP{
            get {return currentHP;}
            private set {
                currentHP = value;
                if (currentHP <= 0)
                {
                    Behaviour.Die();
                }
            }
        }

        public void GetDamage(int damageValue)
        {   
            CurrentHP -= damageValue ; 
        }


    }

}
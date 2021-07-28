using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Docsa.Character
{
    public class HPBar : MonoBehaviour
    {
        public Slider Bar;
        public Character Character;
        
        public int Value 
        {
            get
            {
                return Character.CurrentHP;
            }
            set
            {
                float var = value / Character.MaxHP;
                Bar.value = var;
            }
        }
        
        void OnGUI()
        {
            GUILayout.Label("HP : " + Value.ToString()); 
        }

        

    }
}
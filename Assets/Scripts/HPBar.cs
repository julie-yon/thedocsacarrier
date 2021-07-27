using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Docsa.Character
{
    public class HPBar : MonoBehaviour
    {
        // Start is called before the first frame update
        public Slider Bar;
        public Character Character;
        // public int Value = 0;
        
        public int Value 
        {
            get
            {
                return Character.CurrentHP;
            }
        }
        
        void OnGUI()
        {
            GUILayout.Label("HP : " + Value.ToString()); 
        }

        

    }
}
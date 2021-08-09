using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Michsky.UI.ModernUIPack;
using TMPro;

namespace Docsa.Character
{
    public class HPBar : MonoBehaviour
    {
        public Character Character;
        public SliderManager Bar;
        public Vector3 RelativeHPBarPosition = Vector2.up;
        
        public int Value 
        {
            get
            {
                return Character.CurrentHP;
            }
            set
            {
                Bar.mainSlider.value = value;
            }
        }

        void Awake()
        {
        }

        void Update()
        {
            transform.position = Character.transform.position + RelativeHPBarPosition;
        }
    }
}
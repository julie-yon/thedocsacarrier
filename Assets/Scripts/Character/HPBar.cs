using UnityEngine;

using Michsky.UI.ModernUIPack;

namespace Docsa.Character
{
    public class HPBar : MonoBehaviour
    {
        public Character Character;
        public SliderManager Bar;
        
        public int Value 
        {
            get
            {
                return (int)Bar.mainSlider.value;
            }
            set
            {
                Bar.mainSlider.value = value;
            }
        }

        void Update()
        {
            // transform.position = Character.HeaderPosition;
            transform.rotation = Quaternion.identity;
        }
    }
}
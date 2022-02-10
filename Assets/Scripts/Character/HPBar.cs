using UnityEngine;

using Michsky.UI.ModernUIPack;

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
                return (int)Bar.mainSlider.value;
            }
            set
            {
                Bar.mainSlider.value = value;
            }
        }

        void Reset()
        {
            if (!transform.parent.TryGetComponent<Character>(out Character))
            {
                Debug.LogWarning("HPBar could not find Character at parent. Make ref in inspector yourself");
            }
        }

        [ExecuteInEditMode]
        void Update()
        {
            transform.position = Character.transform.position + RelativeHPBarPosition;
        }
    }
}
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

        void Reset()
        {
            Bar = GetComponentInChildren<SliderManager>();
            if (!transform.parent.TryGetComponent<Character>(out Character))
            {
                Debug.LogWarning("HPBar could not find Character at parent. Make ref in inspector yourself");
            } else
            {
                transform.position = Character.HeaderPosition;
                Character.HPBar = this;
            }
        }

        void OnValidate()
        {
            if (Character)
                transform.position = Character.HeaderPosition;
        }

        [ExecuteInEditMode]
        void Update()
        {
            transform.position = Character.HeaderPosition;
            transform.rotation = Quaternion.identity;
        }
    }
}
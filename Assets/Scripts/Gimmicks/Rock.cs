using UnityEngine;

namespace Docsa.Gimmick
{
    public class Rock : Gimmick
    {
        public Sprite RockSprite;
        public Sprite RockSprite_Transparent;

        public SpriteRenderer DaySpriteRenderer;
        public SpriteRenderer NightSpriteRenderer;

        private SpriteRenderer _targetSpriteRenderer;

        // private bool isNight = false;

        void Awake()
        {
            _targetSpriteRenderer = DaySpriteRenderer;
        }

        // void Update()
        // {
        //     if (isNight != NightDaySwitch.instance.isNight)
        //     {
        //         isNight = !isNight;
        //         Switch();
        //     }
        // }
        // void Switch()
        // {
        //     NightSpriteRenderer.gameObject.SetActive(NightSpriteRenderer.gameObject.activeSelf);
        //     DaySpriteRenderer.gameObject.SetActive(DaySpriteRenderer.gameObject.activeSelf);
        // }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if(1 << collider.gameObject.layer == UzuhamaLayer.value)
            {
                _targetSpriteRenderer.sprite = RockSprite_Transparent;
            }
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            if(1 << collider.gameObject.layer == UzuhamaLayer.value)
            {
                _targetSpriteRenderer.sprite = RockSprite;
            }
        }
    }
}


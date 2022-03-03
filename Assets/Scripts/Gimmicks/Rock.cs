using UnityEngine;

namespace Docsa.Gimmick
{
    public class Rock : Gimmick
    {
        public Sprite RockSprite;
        public Sprite RockSprite_Transparent;

        SpriteRenderer SpriteRenderer;

        void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if(1 << collider.gameObject.layer == UzuhamaLayer.value)
            {
                SpriteRenderer.sprite = RockSprite_Transparent;
            }
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            if(1 << collider.gameObject.layer == UzuhamaLayer.value)
            {
                SpriteRenderer.sprite = RockSprite;
            }
        }
    }
}


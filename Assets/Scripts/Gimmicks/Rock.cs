using UnityEngine;
using dkstlzu.Utility;

namespace Docsa.Gimmick
{
    [RequireComponent(typeof(EventTrigger))]
    public class Rock : Gimmick
    {
        public Sprite RockSprite;
        public Sprite RockSprite_Transparent;

        public SpriteRenderer Renderer;

        [SerializeField][HideInInspector] private EventTrigger ET;

        void Reset()
        {
            ET = GetComponent<EventTrigger>();
            ET.ClearEnterEvent();
            ET.ClearExitEvent();
            ET.AddEnterEvent(Invoke);
            ET.AddExitEvent(ResetGimmick);
        }

        public override void Invoke()
        {
            if (!Started) return;

            Renderer.sprite = RockSprite_Transparent;
        }

        public override void ResetGimmick()
        {
            if (!Started) return;

            Renderer.sprite = RockSprite;
        }
    }
}


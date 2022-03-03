using UnityEngine;

using Docsa.Character;


namespace Docsa.Gimmick
{
    public class CaveGameStartStatue : MonoBehaviour, IInteractable
    {
        public LayerMask LayerMask;
        public void Interact()
        {
            Core.instance.GameStart();
        }

        public void SetInteractable()
        {
            UzuHama.Hama.Interactable = this;
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (((1 << collider.gameObject.layer) & LayerMask) > 0)
            {
                SetInteractable();
            }
        }
    }
}
using UnityEngine;

using Docsa.Character;
using dkstlzu.Utility;

namespace Docsa.Gimmick
{
    public class CaveGameStartStatue : EventTrigger, IInteractable
    {
        public void Interact()
        {
            Core.instance.StageStart();
        }

        /// <summary>
        /// On EventTrigger Reference
        /// </summary>
        public void SetInteractable()
        {
            UzuHama.Hama.Interactable = this;
            GetComponentInChildren<Animator>().SetTrigger("UpTrigger");
        }

        /// <summary>
        /// On EventTrigger Reference
        /// </summary>
        public void UnSetInteractable()
        {
            UzuHama.Hama.Interactable = null;
            GetComponentInChildren<Animator>().SetTrigger("DownTrigger");
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using dkstlzu.Utility;

namespace Docsa.Gimmick
{
    [RequireComponent(typeof(EventTrigger))]
    public class GimmickActivator : MonoBehaviour
    {
        public List<Gimmick> Gimmicks;

#if UNITY_EDITOR
        void Reset()
        {
            GetComponent<EventTrigger>().AddEnterEvent(Activate);
        }
#endif

        [ContextMenu("ActivateGimmicks")]
        public void Activate()
        {
            string message = string.Empty;
            message += "GimmickActivator " + gameObject.name + "\n";

            foreach (Gimmick G in Gimmicks)
            {
                if (!G) continue;
                message += G.gameObject.name + "\n";
                G.Activate();
            }

            print(message);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using dkstlzu.Utility;

namespace Docsa.Gimmick
{
    [RequireComponent(typeof(EventTrigger))]
    public class GimmickStarter : MonoBehaviour
    {
        public List<Gimmick> Gimmicks;

#if UNITY_EDITOR
        void Reset()
        {
            GetComponent<EventTrigger>().AddEnterEvent(StartGimmick);
        }
#endif

        [ContextMenu("StartGimmicks")]
        public void StartGimmick()
        {
            string message = string.Empty;
            message += "GimmickStarter " + gameObject.name + "\n";

            foreach (Gimmick G in Gimmicks)
            {
                if (!G) continue;
                message += G.gameObject.name + "\n";
                G.StartGimmick();
            }

            print(message);
        }
    }
}
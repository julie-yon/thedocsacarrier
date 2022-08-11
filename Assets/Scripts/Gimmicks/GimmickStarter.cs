using System.Collections.Generic;
using UnityEngine;
using dkstlzu.Utility;

namespace Docsa.Gimmick
{
    [RequireComponent(typeof(EventTrigger))]
    public class GimmickStarter : MonoBehaviour
    {
        public List<Gimmick> Gimmicks;

        void Reset()
        {
            GetComponent<EventTrigger>().AddEnterEvent(StartGimmick);
        }

        [ContextMenu("StartGimmicks")]
        void StartGimmick()
        {
            foreach (Gimmick G in Gimmicks)
            {
                if (!G) continue;
                G.StartGimmick();
            }
        }
    }
}
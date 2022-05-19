using System.Collections.Generic;
using UnityEngine;
using Utility;

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

        void StartGimmick()
        {
            foreach (Gimmick G in Gimmicks)
            {
                G.StartGimmick();
            }
        }
    }
}
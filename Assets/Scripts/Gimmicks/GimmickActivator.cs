using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Docsa.Gimmick
{
    [RequireComponent(typeof(EventTrigger))]
    public class GimmickActivator : MonoBehaviour
    {
        public List<Gimmick> Gimmicks;

        void Reset()
        {
            GetComponent<EventTrigger>().AddEnterEvent(Start);
        }

        void Start()
        {
            foreach (Gimmick G in Gimmicks)
            {
                G.Activate();
            }
        }
    }
}
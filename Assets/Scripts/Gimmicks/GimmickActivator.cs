﻿using System.Collections.Generic;
using UnityEngine;
using dkstlzu.Utility;

namespace Docsa.Gimmick
{
    [RequireComponent(typeof(EventTrigger))]
    public class GimmickActivator : MonoBehaviour
    {
        public List<Gimmick> Gimmicks;

        void Reset()
        {
            GetComponent<EventTrigger>().AddEnterEvent(Activate);
        }

        void Activate()
        {
            foreach (Gimmick G in Gimmicks)
            {
                G.Activate();
            }
        }
    }
}
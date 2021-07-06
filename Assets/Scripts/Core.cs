using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docsa
{
    public class Core : Singleton<Core>
    {
        void Start()
        {
            StageManager.instance.MakeStage(1);
        }
    }
}
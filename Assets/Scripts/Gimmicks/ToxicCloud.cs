using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docsa.Gimmick
{
    public class ToxicCloud : Gimmick
    {
        public float Speed = 1;
        void Update()
        {
            if (gimmickStarted)
                transform.Translate(Vector3.left * Speed * Time.deltaTime);
        }
    }
}
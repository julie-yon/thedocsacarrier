using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docsa
{
    public class SpriteRemover : MonoBehaviour
    {
        void Awake()
        {
            Destroy(gameObject);
        }
    }
}
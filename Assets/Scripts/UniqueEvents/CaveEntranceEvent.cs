using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docsa
{
    public class CaveEntranceEvent : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collider2D)
        {
            print("Trigger");
            print(collider2D.gameObject.layer + collider2D.name);
            print(LayerMask.GetMask("UzuHama"));
            if (1 << collider2D.gameObject.layer == LayerMask.GetMask("UzuHama"))
            {
                print("CaveEntrance");
            }
        }
    }
}
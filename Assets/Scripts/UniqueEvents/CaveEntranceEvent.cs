using UnityEngine;
using UnityEditor.Events;

using Utility;

namespace Docsa.Events
{
    public class CaveEntranceEvent : MonoBehaviour
    {
        void StopDocsaFlyAnimator()
        {
            GetComponent<Animator>().enabled = false;
        }
    }
}
using UnityEngine;
using UnityEditor.Events;

using Utility;

namespace Docsa.Events
{
    [RequireComponent(typeof(EventTrigger))]
    public class CaveEntranceEvent : MonoBehaviour
    {
        void Reset()
        {
            UnityEventTools.AddVoidPersistentListener(GetComponent<EventTrigger>().OnTriggerEnterEvent, PlayAnimation);
        }

        public void PlayAnimation()
        {
        }
    }
}
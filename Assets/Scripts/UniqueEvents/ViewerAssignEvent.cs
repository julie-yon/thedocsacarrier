using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif

using Utility;

namespace Docsa.Events
{
    [RequireComponent(typeof(EventTrigger))]
    public class ViewerAssignEvent : MonoBehaviour
    {

        void Reset()
        {
#if UNITY_EDITOR
            UnityEventTools.AddVoidPersistentListener(GetComponent<EventTrigger>().OnTriggerEnterEvent, Invoke);
#endif
        }

        public void Invoke()
        {
            // ViewerAssignUI.instance.OpenUI();
        }
    }
}
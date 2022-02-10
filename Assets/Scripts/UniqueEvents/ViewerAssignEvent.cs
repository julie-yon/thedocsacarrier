using UnityEngine;
using UnityEditor.Events;

using Utility;

namespace Docsa.Events
{
    [RequireComponent(typeof(EventTrigger))]
    public class ViewerAssignEvent : MonoBehaviour
    {

        void Reset()
        {
            UnityEventTools.AddVoidPersistentListener(GetComponent<EventTrigger>().OnTriggerEnterEvent, Invoke);
        }

        public void Invoke()
        {
            ViewerAssignUI.instance.OpenUI();
        }
    }
}
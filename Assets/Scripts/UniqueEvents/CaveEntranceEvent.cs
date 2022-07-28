using UnityEngine;

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
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif

namespace Docsa
{
    public enum ItemType
    {
        HamaAttack,
        HamaJump,
        HamaBaguni,
        GrabDocsa,
        Attend,
        Exit,
        StarLight,
        DocsaAttack,
        DocsaJump,
        HunterAttack,
        HunterJump,
    }
    [RequireComponent(typeof(BoxCollider2D))]
    public class Item : MonoBehaviour
    {
        public LayerMask TargetLayer;
        public UnityEvent ItemEvent = new UnityEvent();
        void OnTriggerEnter2D(Collider2D collider)
        {

            if (GetComponent<Collider2D>().IsTouchingLayers(TargetLayer))
            {
                ItemEvent.Invoke();
            }
        }

        protected virtual void Reset()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        protected virtual void Awake()
        {
#if UNITY_EDITOR
            UnityEventTools.AddPersistentListener(ItemEvent, Effect);
#endif
        }

        public virtual void Effect()
        {
            Destroy(gameObject);
        }
    }
}
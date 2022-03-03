using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Events;

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
        public AudioClip ItemSound;
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
            UnityEventTools.AddPersistentListener(ItemEvent, Effect);
        }

        public virtual void Effect()
        {
            // AudioSource.PlayClipAtPoint(ItemSound, transform.position);

            Destroy(gameObject);
        }
    }
}
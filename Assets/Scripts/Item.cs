using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Docsa
{
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

        protected void Reset()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        protected void Awake()
        {
            ItemEvent.AddListener(Effect);
        }

        public virtual void Effect()
        {
            AudioSource.PlayClipAtPoint(ItemSound, transform.position);

            Destroy(gameObject);
        }
    }
}
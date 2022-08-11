using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Docsa.Gimmick;
using dkstlzu.Utility;

namespace Docsa
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EventTrigger))]
    public class Projectile : MonoBehaviour
    {
        public Docsa.Character.Character ShooterCharacter;

        /// <summary>
        /// if Target is null projectile go through right direction
        /// </summary>
        public Vector3 TargetPosition;
        public Vector2 Direction;
        public float InitSpeedPower = 5;
        public Rigidbody2D rb2D;
        public float DamageValue = 10;
        [HideInInspector][SerializeField] private EventTrigger _eventTrigger;

        public bool UseReflection = false; 
        private bool isReflected = false;
        
        protected virtual void Reset()
        {
            rb2D = GetComponent<Rigidbody2D>();
            _eventTrigger = GetComponent<EventTrigger>();
#if UNITY_EDITOR
            _eventTrigger.AddEnterEvent(OnProjectileHit);
#endif
        }

        protected virtual void OnEnable()
        {
            isReflected = false;
            Direction = TargetPosition - transform.position;
            rb2D.AddForce(Direction.normalized * InitSpeedPower, ForceMode2D.Impulse);
        }

        protected virtual void OnDisable()
        {
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            isReflected = true;
        }
        
        void Update()
        {
            if (!isReflected || UseReflection)
            {
                transform.up = rb2D.velocity;
            }
        }

        protected virtual void OnProjectileHit()
        {

        }
    }
}
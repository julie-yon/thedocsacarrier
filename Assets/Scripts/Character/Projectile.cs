using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utility;

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
        [HideInInspector][SerializeField] private EventTrigger _eventTrigger;

        public bool UseReflection = false; 
        private bool isReflected = false;
        
        protected virtual void Reset()
        {
            rb2D = GetComponent<Rigidbody2D>();
            _eventTrigger = GetComponent<EventTrigger>();
            _eventTrigger.AddEnterEvent(OnProjectileHit);
        }

        protected virtual void OnEnable()
        {
            isReflected = false;
            Direction = TargetPosition - transform.position;
            if (TargetPosition == null)
            {
                rb2D.AddForce(Direction.normalized * InitSpeedPower, ForceMode2D.Impulse);
            } else
            {
                rb2D.AddForce((TargetPosition - transform.position).normalized * InitSpeedPower, ForceMode2D.Impulse);
                Vector2 targetPosition = Camera.main.ScreenToWorldPoint(TargetPosition);
                Vector2 projectileDirection = new Vector2(targetPosition.x - transform.position.x,
                                            targetPosition.y - transform.position.y); //net이 바라볼 방향 설정(타겟독사의 위치에서 헌터의 위치)
                transform.LookAt(projectileDirection);                
            }
            // transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, 0);
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
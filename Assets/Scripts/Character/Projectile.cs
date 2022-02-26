using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docsa
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        public Docsa.Character.Character ShooterCharacter;

        /// <summary>
        /// if Target is null projectile go through right direction
        /// </summary>
        public Transform TargetTransform;
        public Vector2 Direction;
        public float InitSpeedPower = 5;
        public Rigidbody2D rb2D;
        
        protected virtual void Reset()
        {
            rb2D = GetComponent<Rigidbody2D>();
        }

        protected virtual void OnEnable()
        {
            if (TargetTransform == null)
            {
                rb2D.AddForce(Direction * InitSpeedPower, ForceMode2D.Impulse);
                // rb2D.AddForce(transform.right * InitSpeedPower, ForceMode2D.Impulse);
            } else
            {
                rb2D.AddForce((TargetTransform.transform.position - transform.position).normalized * InitSpeedPower, ForceMode2D.Impulse);
                Vector2 targetPosition = Camera.main.ScreenToWorldPoint(TargetTransform.transform.position);
                Vector2 projectileDirection = new Vector2(targetPosition.x - transform.position.x,
                                            targetPosition.y - transform.position.y); //net이 바라볼 방향 설정(타겟독사의 위치에서 헌터의 위치)
                transform.LookAt(projectileDirection);                
            }
        }
        
        void Update()
        {
            transform.up = rb2D.velocity;
        }
    }
}
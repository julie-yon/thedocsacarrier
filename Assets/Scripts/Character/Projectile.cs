using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Docsa
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Projectile : MonoBehaviour
    {
        [System.NonSerialized] public GameObject ShooterGameObject;

        /// <summary>
        /// if Target is null projectile go through right direction
        /// </summary>
        [System.NonSerialized] public GameObject TargetGameObject;
        public Vector2 Direction;
        public float InitSpeedPower = 5;
        public Rigidbody2D rb2D;
        
        protected virtual void Reset()
        {
            rb2D = GetComponent<Rigidbody2D>();
        }

        protected virtual void OnEnable()
        {
            if (TargetGameObject == null)
            {
                rb2D.AddForce(Direction * InitSpeedPower, ForceMode2D.Impulse);
                // rb2D.AddForce(transform.right * InitSpeedPower, ForceMode2D.Impulse);
            } else
            {
                rb2D.AddForce((TargetGameObject.transform.position - transform.position).normalized * InitSpeedPower, ForceMode2D.Impulse);
                Vector2 targetPosition = Camera.main.ScreenToWorldPoint(TargetGameObject.transform.position);
                Vector2 projectileDirection = new Vector2(targetPosition.x - transform.position.x,
                                            targetPosition.y - transform.position.y); //net이 바라볼 방향 설정(타겟독사의 위치에서 헌터의 위치)
                transform.LookAt(projectileDirection);                
            }
        }
        
        void Update()
        {
            transform.right = rb2D.velocity;
        }
    }
}
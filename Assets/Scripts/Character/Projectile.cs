using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docsa
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Projectile : MonoBehaviour
    {
        [System.NonSerialized] public GameObject Shooter;
        [System.NonSerialized] public GameObject Target;
        public float InitSpeedPower = 5;
        protected Rigidbody2D rb2D;
        
        void Awake()
        {
            if (rb2D == null)
            {
                rb2D = GetComponent<Rigidbody2D>();
            }
        }

        protected virtual void OnEnable()
        {
            if (Target == null)
            {
                rb2D.AddForce(transform.right * InitSpeedPower, ForceMode2D.Impulse);
            } else
            {
                rb2D.AddForce((Target.transform.position - transform.position).normalized * InitSpeedPower, ForceMode2D.Impulse);
                Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Target.transform.position);
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
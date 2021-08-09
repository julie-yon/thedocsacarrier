using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Docsa.Character;

namespace Docsa
{
    public class ProjectileNet : Projectile
    {
        [System.NonSerialized] public new Hunter Shooter;
        [System.NonSerialized] public new DocsaSakki Target;

        void OnTriggerEnter2D(Collider2D collider)
        {
            //문제가 있음...Debug 요망
            if (collider.gameObject.GetComponent<DocsaSakki>() != null)
            {
                Shooter.Behaviour.GrabDocsa(Target);
            }
            Destroy(this.gameObject);
        }

        protected override void OnEnable()
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
    }
}
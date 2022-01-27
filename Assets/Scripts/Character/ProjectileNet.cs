using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Docsa.Character;

namespace Docsa
{
    public class ProjectileNet : Projectile
    {
        void OnTriggerEnter2D(Collider2D collider)
        {
            //문제가 있음...Debug 요망
            if (collider.gameObject.GetComponent<DocsaSakki>() != null)
            {
                ShooterGameObject.GetComponent<Hunter>().Behaviour.GrabDocsa(TargetGameObject.GetComponent<DocsaSakki>());
            }
            Destroy(this.gameObject);
        }
    }
}
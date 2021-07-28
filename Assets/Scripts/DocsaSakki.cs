using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docsa.Character
{
    public class DocsaSakki : Character
    {
        bool isOnHama = true;
        bool isRescued;
        bool isKidnapped;
        void Update()
        {
            if(isRescued) Rescued();
        }
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag.Equals("Player"))
            {
                isRescued = true;
                Rescued();
                
            }
        }

        public bool CanBeTargetDocsa()
        {
            return !isOnHama;
        }

        public void Chim(Hunter targetHunter)
        {
                   
        }

        public void Kidnapped(Hunter catcher)
        {
            isKidnapped = true;
            // Docsa가 Net에 충돌되면, Hunter가 AlcholPot 있는 곳으로 옮기면 그때 독사 죽음
            transform.position = catcher.GrabDocsaPosition.position;
        }

        public void Rescued()
        {
            // Docsa가 UzuHama에 충돌되면, baguni로 이동
        }

        public void DocsaDie()
        {
            Behaviour.Die();
        }
    }
}
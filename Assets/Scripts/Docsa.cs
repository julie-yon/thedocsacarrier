using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docsa.Character
{
    public class Docsa : Character
    {
        public GameObject OnHamaGameObject;
        public GameObject EscapeGameObject;
        public Vector2 targetDocPosition;
        bool isOnHama = true;
        bool isRescued;
        bool isKidnapped;
        public Docsa(){}
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
            if (col.gameObject.tag.Equals("Net"))
            {
                // 참고
                if (isOnHama)
                {
                    return;
                }
                isKidnapped = true;
                Kidnapped(col.GetComponent<ProjectileNet>().Thrower);
                
            }
        }

        // void OnTriggerEnter2D(Collider2D collision)
        // {
        //     Debug.Log(collision.name + "에게 닿았음!");
        // }

        void OnHama()
        {
            OnHamaGameObject.SetActive(true);
            EscapeGameObject.SetActive(false);
            isOnHama = true;
        }

        void Escaped()
        {
            EscapeGameObject.SetActive(true);
            OnHamaGameObject.SetActive(false);
            isOnHama = false;
        }
    
        public Docsa CanBeTargetDocsa()
        {
            Docsa docsa = null;
            
            if (isOnHama == false) Escaped();

            return docsa;
        }
        // public bool CanBeTargetDocsa()
        // {
        //     return !isOnHama;
        // }

        public Docsa(string viewerName)
        {

        }
        public Docsa(string viewerName, Vector2 position, Quaternion rotation)
        {

        }

        public void Chim(Hunter targetHunter)
        {
                   
        }

        public void Kidnapped(Hunter catcher)
        {
            // Docsa가 Net에 충돌되면, AlcholPot 있는 곳으로 옮겨지기 
            transform.position = catcher.KidnappedDocsaPosition;
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
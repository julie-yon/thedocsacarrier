using UnityEngine;

namespace Docsa.Character
{
    public class DocsaSakki : ViewerCharacter
    {
        public Transform OriginalParent;
        bool isOnHama;
        bool isKidnapped;

        public bool CanBeTargetDocsa
        {
            get {return !isOnHama;}
        }

        protected override void Awake()
        {
            base.Awake();
            OriginalParent = transform.parent;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag.Equals("Player"))
            {
                Rescued();
            }
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

    }
}
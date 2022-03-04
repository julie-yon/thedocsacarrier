using UnityEngine;

namespace Docsa.Character
{
    [RequireComponent(typeof(DocsaBehaviour))]
    public class DocsaSakki : ViewerCharacter
    {
        public Transform OriginalParent;

        protected override void Awake()
        {
            base.Awake();
            OriginalParent = transform.parent;
        }
    }
}
using UnityEngine;

namespace Docsa.Character
{
    [RequireComponent(typeof(DocsaBehaviour))]
    public class DocsaSakki : ViewerCharacter
    {
        public Transform OriginalParent;

        protected override void OnEnable()
        {
            base.OnEnable();
            OriginalParent = transform.parent;
        }
    }
}
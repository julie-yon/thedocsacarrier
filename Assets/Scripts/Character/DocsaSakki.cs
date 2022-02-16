using UnityEngine;

namespace Docsa.Character
{
    public class DocsaSakki : ViewerCharacter, IHasTrajectory
    {
        public Transform OriginalParent;
        [SerializeField] MeshRenderer _trajectoryRenderer;

        protected override void Awake()
        {
            base.Awake();
            OriginalParent = transform.parent;
        }

        public MeshRenderer TrajectoryRenderer
        {
            get {return _trajectoryRenderer;}
        }

        public void TrajectoryOn()
        {
            TrajectoryRenderer.enabled = true;
        }

        public void TrajectoryOff()
        {
            TrajectoryRenderer.enabled = false;
        }
    }
}

using UnityEngine;

namespace Docsa.Character
{
    public class ViewerCharacter : Character, IHasTrajectory
    {
        public string ViewerName;
        public bool isViewerAssigned;
        public bool Flip;
        [SerializeField] protected MeshRenderer _trajectoryRenderer;
        public MeshRenderer TrajectoryRenderer
        {
            get {return _trajectoryRenderer;}
        }
        protected override void Reset()
        {
            base.Reset();
        }

        protected override void Awake()
        {
            base.Awake();
        }

        protected virtual void OnEnable()
        {
            if (Flip) transform.localScale = new UnityEngine.Vector3(-1, 1, 1);
            else transform.localScale = new UnityEngine.Vector3(1, 1, 1);
        }

        protected virtual void OnDisable()
        {
            ViewerName = string.Empty;
            isViewerAssigned = false;
            Reset();
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
using UnityEngine;

namespace Docsa.Character
{
    [RequireComponent(typeof(HunterBehaviour))]
    public class Hunter : ViewerCharacter, IHasTrajectory
    {
        public DocsaSakki FocusingDocsa;
        [SerializeField] MeshRenderer _trajectoryRenderer;
        
        public bool isRecognizingUzuhama()
        {
            return false;
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
using UnityEngine;

namespace Docsa.Character
{
    public interface IHasTrajectory
    {
        MeshRenderer TrajectoryRenderer{get;}
        void TrajectoryOn();
        void TrajectoryOff();
    }
}
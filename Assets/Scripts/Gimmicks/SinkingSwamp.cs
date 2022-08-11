using UnityEngine;
using dkstlzu.Utility;

namespace Docsa.Gimmick
{
    [RequireComponent(typeof(EventTrigger))]
    public class SinkingSwamp : Gimmick
    {
        [SerializeField][HideInInspector] private EventTrigger ET;
        private Docsa.Character.Character TargetCharacter;
        private Rigidbody2D TargetRigid;
        public float SwallowingForce;

        void Reset()
        {
            ET = GetComponent<EventTrigger>();
            ET.AddEnterGOEvent(AssignTarget);
            ET.AddStayEvent(Invoke);
            ET.AddExitGOEvent(UnAssignTarget);
        }

        void AssignTarget(GameObject go)
        {
            TargetCharacter = go.GetComponent<Docsa.Character.Character>();
            TargetRigid = TargetCharacter.GetComponent<Rigidbody2D>();
        }

        void UnAssignTarget(GameObject go)
        {
            TargetRigid = null;
            TargetCharacter = null;
        }

        public override void Invoke()
        {
            if (!Started) return;

            Vector2 forceDirection = TargetCharacter.transform.position;
            forceDirection -= ET.Collider2D.offset;
            TargetRigid.AddForce(forceDirection * SwallowingForce, ForceMode2D.Force);
        }
    }
}
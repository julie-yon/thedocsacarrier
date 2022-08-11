using UnityEngine;

using dkstlzu.Utility;

namespace Docsa.Character
{
    public class HamaPhysicsHelper : MonoBehaviour
    {
        public Vector2 HamaMoveBoxCastSize = new Vector2(0.1f, 0.8f);
        public float HamaMoveBoxCastDistance = 1f;
        public UzuHamaBehaviour Behaviour; 

        void FixedUpdate()
        {
            PhysicsHelper.BoxCast(transform.position + new Vector3(0, 0.5f, 0), 
                HamaMoveBoxCastSize, 0, Vector2.right * Behaviour.MoveDirection, 
                HamaMoveBoxCastDistance, 
                ~Physics2D.GetLayerCollisionMask(gameObject.layer));
        }
    }
}
using UnityEngine;

namespace Docsa
{
    /// <summary>
    /// Use This with chunk and as a prefab brush of '2d extras'
    /// </summary>
    public class CharacterPositionSetter : MonoBehaviour
    {
        public DocsaPoolType CharacterType;
        public bool Flip;

        void OnDrawGizmos()
        {
            if (CharacterType == DocsaPoolType.Docsa)
            {
                Gizmos.color = Color.blue;
            } else if (CharacterType == DocsaPoolType.Hunter)
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawSphere(transform.position, 1);
            Gizmos.color = Color.black;

            if (!Flip)
                Gizmos.DrawLine(transform.position, transform.position + Vector3.left);
            else
                Gizmos.DrawLine(transform.position, transform.position + Vector3.right);
        }
    }
}
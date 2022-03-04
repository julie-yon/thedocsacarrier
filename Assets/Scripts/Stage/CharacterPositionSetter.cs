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

        void OnValidate()
        {
            if (CharacterType == DocsaPoolType.Docsa)
            {
                GetComponentInChildren<SpriteRenderer>().color = Color.blue;
            } else if (CharacterType == DocsaPoolType.Hunter)
            {
                GetComponentInChildren<SpriteRenderer>().color = Color.red;
            }
        }
    }
}
using UnityEngine;

using Docsa.Character;

namespace Docsa.Events
{
    public class CharacterDieAnimationEvent : MonoBehaviour
    {
        void Die()
        {
            transform.parent.GetComponent<Docsa.Character.Character>().Return();
        }
    }
}
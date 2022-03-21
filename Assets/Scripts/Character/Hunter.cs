using UnityEngine;

namespace Docsa.Character
{
    [RequireComponent(typeof(HunterBehaviour))]
    public class Hunter : ViewerCharacter
    {
        public Transform AttackTarget;
        
        public bool isRecognizingUzuhama()
        {
            return false;
        }
    }
}
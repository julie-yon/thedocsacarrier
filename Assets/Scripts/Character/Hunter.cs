using UnityEngine;

namespace Docsa.Character
{
    [RequireComponent(typeof(HunterBehaviour))]
    public class Hunter : ViewerCharacter
    {
        public DocsaSakki FocusingDocsa;
        
        public bool isRecognizingUzuhama()
        {
            return false;
        }
    }
}
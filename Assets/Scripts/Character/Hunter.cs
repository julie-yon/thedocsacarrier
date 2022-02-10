using UnityEngine;

namespace Docsa.Character
{
    public class Hunter : ViewerCharacter
    {
        public DocsaSakki FocusingDocsa;
        
        public bool isRecognizingUzuhama()
        {
            return false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docsa.Character
{
    public class Hunter : Character
    {
        public Vector3 KidnappedDocsaPosition;
        public Hunter(){}
        
        public Hunter(string viewerName)
        {
            
        }
        public Hunter(string viewerName, Vector2 position, Quaternion rotation)
        {

        }

        public bool isRecognizingUzuhama()
        {
            return false;
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    public CharacterBehaviour Behaviour;
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

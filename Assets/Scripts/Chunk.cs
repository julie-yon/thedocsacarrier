using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public GameObject RightChunkTriggerObject;
    public GameObject LeftChunkTriggerObject;
    public Transform RightStartPosition;
    public Transform LeftStartPosition;
    public Vector3 DefaultCameraPosition;
    void Awake()
    {
        DefaultCameraPosition = new Vector3(transform.position.x, 0, -10);
    }
    void OnEnable()
    {

    }

    void OnDisable()
    {

    }
}

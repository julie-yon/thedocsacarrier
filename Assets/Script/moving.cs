using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) 
            Debug.Log("위로 점프 중");
        
        if (Input.GetKeyDown(KeyCode.A)) 
            Debug.Log("왼쪽으로 이동 중");
        
        if (Input.GetKeyDown(KeyCode.S)) 
            Debug.Log("아래로 이동 중");

        if (Input.GetKeyDown(KeyCode.D)) 
            Debug.Log("오른쪽으로 이동 중");
        
        if (Input.GetMouseButton(0))
            Debug.Log("공격!");
    }
}

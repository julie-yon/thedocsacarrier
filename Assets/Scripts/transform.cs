using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transform : MonoBehaviour
{
    //public Vector2 speed_vec;
    //Behav behav;    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 우주하마 움직임 코드 1 -> 속도 조절을 어떻게 해야할지 모르겠음.
       Vector3 vec1 = new Vector3(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"), 
            0);
       transform.Translate(vec1);

       //speed_vec = Vector2.zero ; 

       //if (Input.GetKeyDown(KeyCode.D)) 
       // {
       //     speed_vec.x += 0.1f ; 

       // }
       //     Debug.Log("위로 점프 중"); 
       // transform.Translate(speed_vec) ; 
    }
}

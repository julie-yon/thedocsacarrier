using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    Vector3 net_target;
    // Start is called before the first frame update
    void Start()
    {
        net_target = GameObject.Find("targetDocsa").transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, net_target, 0.1f);
    }
}

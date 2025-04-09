using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPull : MonoBehaviour
{
    public bool beingPulled;
    public bool pullable;
    float xPos;
    void Start()
    {
        xPos = transform.position.x;
        pullable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(beingPulled == false)
        {
            transform.position = new Vector3(xPos, transform.position.y);
        }
        else
        {
            xPos = transform.position.x;
        }
    }
}

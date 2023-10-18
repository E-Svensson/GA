using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairController : MonoBehaviour
{
    bool Up = false;
    bool Down = false;
    void Start()
    {
        Debug.Log("Hello");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="StairUp")
        {
            Up = true;
            Down = false;
        }

        if(other.tag=="StairUp")
        {
            Down = true;
            Up = false;
        }

        if(other.tag=="StairConfirm" && Up==true)
        {
            Debug.Log("Up");
        }

        if(other.tag=="StairConfirm" && Down==true)
        {
            Debug.Log("Down");
        }
    }
}

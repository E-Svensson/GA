using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 8f;
    void Update() // Update is called once per frame
    {

        Vector3 movement = new Vector3();

        if(Input.GetKey(KeyCode.W))
        {
            movement.y = 1;
        }
        if(Input.GetKey(KeyCode.S))
        {
            movement.y = -1;
        }
        if(Input.GetKey(KeyCode.A))
        {
            movement.x = -1;
        }
        if(Input.GetKey(KeyCode.D))
        {
            movement.x = 1;
        }

        movement.Normalize();
        transform.position += movement * Time.deltaTime * speed;
        
    }
}

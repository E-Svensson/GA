using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCode : MonoBehaviour
{
    float speed = 3f;
    void Update()
    {
        Vector2 movement = new Vector2();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

}

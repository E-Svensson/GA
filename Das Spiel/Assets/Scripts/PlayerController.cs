using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Pistol;
    public Rigidbody2D rb;
    Rigidbody2D rb_gun;

    public HingeJoint2D gunhinge;
    Vector2 mousePos;
    public Camera cam;
    GameObject gun;

    void FixedUpdate()
    {
        Vector2 lookDirection = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        rb_gun.rotation = angle;
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
}

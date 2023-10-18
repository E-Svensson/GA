using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 8f;
    public Rigidbody2D rb;
    Vector2 movement;
    bool Up = false;
    bool Down = false;
    Vector2 newScale;
    void Start()
    {
            Debug.Log("Hello");
    }
    void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    void Update()// Update is called once per frame
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
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

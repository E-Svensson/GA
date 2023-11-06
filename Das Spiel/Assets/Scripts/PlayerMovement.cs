using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed;
    public double stamina = 1;
    public Rigidbody2D rb;
    Vector2 movement;

    void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    void Update()// Update is called once per frame
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        speed = 8f;
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0){
            speed = 12f;
            stamina -= 0.001;
        }
        if (stamina != 1 && !Input.GetKey(KeyCode.LeftShift))
            stamina += 0.001;
    }
    
}

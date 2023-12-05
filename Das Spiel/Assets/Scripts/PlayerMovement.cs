using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    private float speed;
    private bool running = false;
    public float maxStamina = 100f;
    public float currentStamina;
    public float currentHealth;
    public float maxHealth = 100f;  
    public float KBForce = 100f;
    public Rigidbody2D rb;
    Vector2 movement;
    Vector2 direction;

    void Start()
    {
        currentStamina = maxStamina;
        currentHealth = maxHealth;
    }
    void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    void Update() // Update is called once per frame
    {
        // Gör det möjligt för gubben att röra på sig med WASD eller piltangenterna

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize(); // Normaliserar gubbens rörelser enligt enhetscirkeln

        if (Input.GetKeyDown(KeyCode.LeftShift)) 
            running = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            running = false;

        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction.Normalize();

        if(running && currentStamina > 0)
        {
            speed = 6f;

            // Stamina ska endast minska om gubben rör på sig
            if(direction.x != 0 || direction.y != 0)
                currentStamina -= 10 * Time.deltaTime;
        }
        else
        {
            speed = 3f;
            currentStamina += 5 * Time.deltaTime;
        }

        currentHealth = Mathf.Clamp(currentHealth, -1, maxHealth);
        currentStamina = Mathf.Clamp(currentStamina, -1, maxStamina); 
    }
    public Vector2 GetHealth()
    {
        return new Vector2(currentHealth, maxHealth);
    }
    public Vector2 GetStamina()
    {
        return new Vector2(currentStamina, maxStamina);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Collider2D collider = other.collider;
 
        if(other.transform.tag == "Enemy")
        {
            rb.AddForce(rb.transform.up * KBForce);
            currentHealth -= 10;
        } 
    }
    
    
}
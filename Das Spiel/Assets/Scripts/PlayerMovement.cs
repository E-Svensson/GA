using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    private float speed;
    private bool running = false;
    public float maxStamina = 100;
    public float currentStamina;
    public float currentHealth;
    public float maxHealth = 100;  
    public Rigidbody2D rb;
    public UnityEvent<GameObject> OnHitWithReference;
    Vector2 movement;

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


    public void GetHit(GameObject sender)
    {
        OnHitWithReference?.Invoke(sender);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.tag == "Enemy")
        {
            currentHealth -= 10;
        } 
    }
    
    
}
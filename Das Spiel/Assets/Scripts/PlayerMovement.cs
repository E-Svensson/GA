using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float timer;
    private float nextAction = 0f;
    private float period = 0.5f;
    private float speed;
    private bool running = false;
    public float maxStamina = 100;
    public float currentStamina;
    public float currentHealth;
    public float maxHealth = 100;  
    public Rigidbody2D rb;
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
    void Update()// Update is called once per frame
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        running = true;
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        running = false;

        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction.Normalize();

        if(Input.GetKeyDown(KeyCode.Space)){
            currentHealth -= 10;
        }

        if(running && currentStamina > 0)
        {
            speed = 6f;
            if((direction.x != 0 || direction.y != 0))
            {
                currentStamina -= 10 * Time.deltaTime;
            }
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

}
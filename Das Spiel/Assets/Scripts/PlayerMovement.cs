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
    public int maxStamina = 100;
    public int currentStamina;
    public StaminaBar staminaBar;
    public Rigidbody2D rb;
    Vector2 movement;

    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
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

        if(running && (direction.x != 0 || direction.y != 0))
        {
            speed = 6f;
            if(Time.time > nextAction){
                nextAction += period;
                StaminaCost(10);
            }

            if(currentStamina == 0)
            running = false;
            
        }
        else
        {
            speed = 3f;
            if(Time.time > nextAction){
                nextAction += period;
                StaminaRecharge(5);
            }
        } 
    }

    void StaminaCost(int cost)
    {
        currentStamina -= cost;

        staminaBar.SetStamina(currentStamina);
    }

    void StaminaRecharge(int charge)
    {
        currentStamina += charge;

        staminaBar.SetStamina(currentStamina);
    }
}
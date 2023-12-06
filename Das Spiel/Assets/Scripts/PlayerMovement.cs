using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    private float speed;
    public bool running = false;
    public float maxStamina = 100f;
    public float currentStamina;
    public float currentHealth;
    public float maxHealth = 100f;  
    public float KBForce = 10f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    bool pushedBack = false;
    float pushBackTime = 0.5f;
    float pushBackTimer = 0.6f;

    private void Awake(){
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        currentStamina = maxStamina;
        currentHealth = maxHealth;
    }
    void FixedUpdate() 
    {
        if(!pushedBack) //När denna if sats körs kan vi inte röra oss för att rb.MovePosition är det som gör så att vår Rigidbody2D kan röra på sig. 
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        
        pushBackTimer += Time.deltaTime;
        pushedBack = pushBackTimer < pushBackTime; //Gör så att pushedBack är bara true om pushedBackTimer < pushBackTime
    }
    void Update() // Update is called once per frame
    {
        // Gör det möjligt för gubben att röra på sig med WASD eller piltangenterna
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize(); // Normaliserar gubbens rörelser enligt enhetscirkeln

        // Gör det möjligt att springa
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
            running = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            running = false;

        // "direction" 
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction.Normalize();

        if (running && currentStamina > 0)
        {
            speed = 6f;

            // Stamina ska endast minska om gubben rör på sig
            if (direction.x != 0 || direction.y != 0)
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
            Vector2 EnemyDirection = (transform.position - collider.transform.position).normalized; //Tar spelaren position minus det object som korckar med spelaren(Enemy) för att få det håll som fiende kollar mot.

            rb.AddForce(EnemyDirection * KBForce, ForceMode2D.Impulse); 
            currentHealth -= 10;
            SetPushedBack();
        } 
    }

    void SetPushedBack(){
        pushedBack = true;
        pushBackTimer = 0;
    }
}
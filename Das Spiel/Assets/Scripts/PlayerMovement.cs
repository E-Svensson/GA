using UnityEngine;
using UnityEngine.InputSystem;

public enum WalkDirection{
    Up,
    Rest
}
public class PlayerMovement : Entity
{
    public static WalkDirection walkDirection = WalkDirection.Rest;
    [SerializeField] private int speed = 3;
    public bool running = false;
    public float maxStamina = 100f;
    public float currentStamina;
    public float KBForce = 10f;
    bool pushedBack = false;
    float pushBackTime = 0.5f;
    float pushBackTimer = 0.6f;
    private Rigidbody2D rb;
    public Animator animator;
    private Vector2 movement;
    

    private void OnMovement (InputValue value) {
        movement = value.Get<Vector2>();

        if (movement.x != 0 || movement.y != 0){ // Animation när spelaren inte står still
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);

            animator.SetBool("IsWalking", true);
        } else { //Animation annars

        }
    }

    private void Awake(){
        // Kopplar variablerna till Unitys komponenter
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        currentHealth = 100f;
        currentStamina = maxStamina;
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
        // Gör det möjligt att springa
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
            running = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            running = false;

        // "direction" 
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction.Normalize();
        if(direction.y > 0){
            walkDirection = WalkDirection.Up; 
        }
        else if (direction.y < 0) {
            walkDirection = WalkDirection.Rest; 
        }
        if (running && currentStamina > 0)
        {
            speed = 6;

            // Stamina ska endast minska om gubben rör på sig
            if (direction.x != 0 || direction.y != 0)
                currentStamina -= 10 * Time.deltaTime;
        }
        else
        {
            speed = 3;
            currentStamina += 5 * Time.deltaTime;
        }

        currentHealth = Mathf.Clamp(currentHealth, -1, maxHealth);
        currentStamina = Mathf.Clamp(currentStamina, -1, maxStamina); 
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
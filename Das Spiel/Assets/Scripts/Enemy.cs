using UnityEngine;

public class Enemy : Entity
{

    public Transform player;
    private float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        currentHealth = 12;
    }
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;   

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        moveCharacter(movement);
    }
    void moveCharacter(Vector2 direction){
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other) {
            if(other.gameObject.CompareTag("Bullet")){
                currentHealth -= 3;
            }
        }
}

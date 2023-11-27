using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Transform player;
    private float moveSpeed = 3f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float currentHealth;
    private float maxHealth = 100;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;

        if(Input.GetKeyDown(KeyCode.Space)){
            currentHealth -= 10;
        }
    }

    void FixedUpdate()
    {
        moveCharacter(movement);
    }
    void moveCharacter(Vector2 direction){
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
    public Vector2 GetHealth()
    {
        return new Vector2(currentHealth, maxHealth);
    }
}

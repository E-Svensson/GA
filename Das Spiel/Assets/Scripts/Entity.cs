using UnityEngine;

public class Entity : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;  
    
    void Start(){
        currentHealth = maxHealth;
    }
    public Vector2 GetHealth()
    {
        return new Vector2(currentHealth, maxHealth);
    }
}

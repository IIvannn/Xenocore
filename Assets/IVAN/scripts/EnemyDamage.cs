using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float health;
    public float deathDelay = 3;
    float currentHealth;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > health)
        {
            currentHealth = health;
        }
    }


    public void TakeDamage(float damage)
    {
        Debug.Log("damage:  "+damage+"  health:  "+currentHealth);
        currentHealth -= damage;
        {
            if (currentHealth < 0)
            {
                Death();
            }
        }
    }

    public void Death()
    {
        EnemyScript body = GetComponent<EnemyScript>();
        body.dead = true;
        Destroy(gameObject, deathDelay);
    }
}

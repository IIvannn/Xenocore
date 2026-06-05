using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public float hp = 100f;
    public float currentHp = 1f;
    public static bool dead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHp > hp)
        {
            currentHp = hp;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        dead = true;
        Destroy(gameObject);
    }
}

using UnityEngine;
using System.Collections;

public class EnemyMelee : MonoBehaviour
{
    public float damage = 15;
    public float range = 3.3f;
    public float attackRange;
    public bool canattack = true;
    public float attackcooldown = 1.5f;
    public GameObject hitbox;
    public GameObject attIndicator;
    public Transform firePoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        if (canattack)
        {
            StartCoroutine(attack());
        }
    }

    IEnumerator attack()
    {
        canattack = false;
        attIndicator.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        attIndicator.SetActive(false);
        GameObject ball = Instantiate(hitbox, transform.position, transform.rotation);
        hitbox.GetComponent<EnemyHitbox>().damage = damage;
        hitbox.GetComponent<EnemyHitbox>().range = range;
        hitbox.GetComponent<EnemyHitbox>().onetime = true;
        StartCoroutine(cooldown());
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(attackcooldown);
        canattack = true;
    }
}

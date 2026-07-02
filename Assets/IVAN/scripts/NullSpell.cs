using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class NullSpell : MonoBehaviour
{
    public float damage = 100f;
    public GameObject shockwave;
    public GameObject particle;
    public LayerMask enemyLayer;
    float moveSpeed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(duration());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, 5f, enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            damage += (enemy.GetComponent<enemyProjectileScript>().damage)/6;
            GameObject ball = Instantiate(particle, enemy.transform.position, enemy.transform.rotation);
            ball.GetComponent<GoTo>().target = transform;
            Destroy(enemy.gameObject);
        }
        if (moveSpeed > 0)
        { moveSpeed -= 7 * Time.deltaTime; }
    }


    IEnumerator duration()
    {
        yield return new WaitForSeconds(6f);
        GameObject ball = Instantiate(shockwave, transform.position, transform.rotation);
        ball.GetComponent<Shockwave>().damage = damage;
        ball.GetComponent<Shockwave>().range = 10f;
        Destroy(gameObject);
    }

    
}

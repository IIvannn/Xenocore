using UnityEngine;

public class enemyProjectileScript : MonoBehaviour
{
    public GameObject source;
    public float moveSpeed;
    public float damage;
    public float lifetime;
    public LayerMask groundMask;
    public bool homing;
    public float homingPrecision;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        if (Physics.CheckSphere(transform.position, 0.5f, groundMask))
        {
            Death();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerDamage>().TakeDamage(damage);
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}

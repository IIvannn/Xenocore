using UnityEngine;

public class BoomerangSpecial : MonoBehaviour
{
    public float duration;
    public float damage;
    public string type;
    public float critChance;
    public float critDamage;
    public float rotationSpeed;
    public GameObject source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        transform.position = PlayerMovement.playerPosition.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyDamage>().TakeDamage(damage, type, critChance, critDamage, source);
            other.GetComponent<EnemyDamage>().ApplyStatus(type, source);
        }
    }
}

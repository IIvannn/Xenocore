using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public float damage = 1f;
    public float range = 1f;
    public bool onetime = false;
    public List<GameObject> he = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = new Vector3(range, 0.2f, range);
        if (onetime)
        {
            Destroy(gameObject,0.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !he.Contains(other.gameObject))
        {
            he.Add(other.gameObject);
            other.GetComponent<PlayerDamage>().TakeDamage(damage);
            
        }
    }
}

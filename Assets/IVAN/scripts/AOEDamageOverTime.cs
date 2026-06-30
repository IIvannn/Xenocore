using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDamageOverTime : MonoBehaviour
{
    public bool temporary = true;
    public LayerMask enemyLayer;
    public float damage = 1f;
    public float lifetime = 3f;
    public float range = 3f;
    public string type = "normal";
    public float attackSpeed = 1;
    List<GameObject> enemiesInRange = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Damage());
        if (temporary)
        {
            Destroy(gameObject, lifetime);
        }
        transform.localScale = new Vector3(range, 0.2f, range);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Damage()
    {
        
        foreach (GameObject enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                enemy.GetComponent<EnemyDamage>().TakeDamage(damage, type, 0, 0, null);
            }
        }
        //TakeDamage(BoonSTaticInfo.swarmDamage);
        yield return new WaitForSeconds(attackSpeed);
        StartCoroutine(Damage());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!enemiesInRange.Contains(other.gameObject))
            {
                enemiesInRange.Add(other.gameObject);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (enemiesInRange.Contains(other.gameObject))
            {
                enemiesInRange.Remove(other.gameObject);
            }
        }
    }
}

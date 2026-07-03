using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : MonoBehaviour
{
    
    List<GameObject> enemiesInRange = new List<GameObject>();
    public GameObject source;
    public float lifetime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
        StartCoroutine(Damage());
        transform.localScale = new Vector3(BoonSTaticInfo.swarmRange, 0.05f, BoonSTaticInfo.swarmRange);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (source == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.position = source.transform.position;
    }


    IEnumerator Damage()
    {
        
        foreach (GameObject enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                //Debug.Log("swarm HIT");
                source.GetComponent<EnemyDamage>().TakeDamage(BoonSTaticInfo.swarmDamage, "swarm", 0, 0, null);

            }

        }
        //TakeDamage(BoonSTaticInfo.swarmDamage);
        yield return new WaitForSeconds(BoonSTaticInfo.swarmAttackSpeed);
        StartCoroutine(Damage());
    }


    private void OnTriggerStay(Collider other)
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

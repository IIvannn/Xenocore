using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationRing : MonoBehaviour
{
    List<GameObject> enemieshit = new List<GameObject>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = new Vector3(BoonSTaticInfo.radiationRange, 0.2f, BoonSTaticInfo.radiationRange);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            //Debug.Log("enemies in radiation ring:  " + enemieshit.Count);

            foreach (GameObject enemy in enemieshit)
            {
                if (enemy == null)
                    continue;

                EnemyDamage enemyDamage = enemy.GetComponent<EnemyDamage>();

                if (enemyDamage == null)
                    continue;

                float dist = Vector3.Distance(transform.position, enemy.transform.position);


                float normalized = Mathf.Clamp01(1f - (dist / BoonSTaticInfo.radiationRange));

                if (!enemyDamage.irradiated)
                {
                    enemyDamage.radiationAmmount = normalized;
                }
                
                
            }
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!enemieshit.Contains(other.gameObject))
            {
                //Debug.Log("enemies in radiation ring:  "+(enemieshit.Count+1));
                enemieshit.Add(other.gameObject);

            }
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (enemieshit.Contains(other.gameObject))
            {
                //Debug.Log("OUT"+other);
                enemieshit.Remove(other.gameObject);
                EnemyDamage enemyDamage = other.GetComponent<EnemyDamage>();
                enemyDamage.radiationAmmount = 0;
            }
                
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && BoonSTaticInfo.nuclear)
        {
            PlayerDamage.currentEnergy += BoonSTaticInfo.nuclearRegeneration * Time.deltaTime;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NullBubble : MonoBehaviour
{
    public GameObject shockwave;
    public GameObject source;
    public List<GameObject> enemiesInRange = new List<GameObject>();
    private void Start()
    {
        transform.localScale = new Vector3(BoonSTaticInfo.nullRange, 0.3f, BoonSTaticInfo.nullRange);
        StartCoroutine(BubbleDuration());
        
    }

    public void Update()
    {
        Debug.Log(BoonSTaticInfo.nulledEnemies.Count);
        if (BoonSTaticInfo.pb && source != null)
        {
            transform.position = source.transform.position;
        }

        foreach (GameObject enemy in enemiesInRange)
        {
            if (!BoonSTaticInfo.nulledEnemies.Contains(enemy))
            {
                BoonSTaticInfo.nulledEnemies.Add(enemy);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            
            EnemyDamage enemy = other.GetComponent<EnemyDamage>();

            if (enemy != null)
            {
                Vector3 direction = transform.position - enemy.transform.position;


                float distance = direction.magnitude;


                distance = Mathf.Max(distance, 0.5f);


                float pullForce = 20f / distance;

                enemy.NullPull(direction * pullForce);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!BoonSTaticInfo.nulledEnemies.Contains(other.gameObject))
            {
                BoonSTaticInfo.nulledEnemies.Add(other.gameObject);
                
            }
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
            if (BoonSTaticInfo.nulledEnemies.Contains(other.gameObject))
            {
                BoonSTaticInfo.nulledEnemies.Remove(other.gameObject);
                
            }
            if (enemiesInRange.Contains(other.gameObject))
            {
                enemiesInRange.Remove(other.gameObject);
            }

        }
    }

    IEnumerator BubbleDuration()
    {
        yield return new WaitForSeconds(BoonSTaticInfo.nullDuration);
        Death();
    }

    public void Death()
    {

        foreach (GameObject enemy in enemiesInRange)
        {
            if (BoonSTaticInfo.nulledEnemies.Contains(enemy))
            {
                BoonSTaticInfo.nulledEnemies.Remove(enemy);
            }
        }

        if (BoonSTaticInfo.collapse)
        {
            GameObject ball = Instantiate(shockwave, transform.position, transform.rotation);
            ball.GetComponent<Shockwave>().damage = BoonSTaticInfo.collapseDamage;
            ball.GetComponent<Shockwave>().range = BoonSTaticInfo.nullRange;
        }

        BoonSTaticInfo.nullCurrentCount--;
        Debug.Log(BoonSTaticInfo.nullCurrentCount);
        Destroy(gameObject);

    }
}

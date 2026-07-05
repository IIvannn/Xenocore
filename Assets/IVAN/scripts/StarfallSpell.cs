using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class StarfallSpell : MonoBehaviour
{
    public List<GameObject> aven = new List<GameObject>();
    bool canhit = true;
    public GameObject last = null;
    public GameObject target = null;
    float travelSpeed = 15;
    float damage = 10;
    int limit = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(BoonSTaticInfo.enemiesAlive.Count);
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Reset();
            Debug.Log("changeTarget");
        }
        else
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;
            transform.position += dir * travelSpeed * Time.deltaTime;
        }
        //if (BoonSTaticInfo.enemiesAlive.Count < 1)
        //{
        //    Destroy(gameObject);
        //}
    }

    void Reset()
    {
        Debug.Log("reset");
        aven.Clear();
        foreach (GameObject obj in BoonSTaticInfo.enemiesAlive)
        {
            if (obj != last && obj != null)
            {
                aven.Add(obj);
                Debug.Log(obj.name);
            }
        }
        if (aven.Count < 1)
        {
            Debug.Log("death");
            Destroy(gameObject);
            return;
        }
        Debug.Log(aven.Count);
        if (aven.Count == 1)
        {
            target = aven[0];
        }
        else
        {
            int rchance = Random.Range(1, aven.Count);
            target = aven[rchance];
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Debug.Log("target: " + target);
            //Debug.Log("other: "+ other);
            if (other.gameObject == target)
            {
                if (limit== 24)
                {
                    Destroy(gameObject);
                }
                
                if (canhit)
                {
                    StartCoroutine(cooldown());
                    canhit = false;
                    float rchance = Random.Range(1f, 3f);
                    damage += rchance;
                    other.GetComponent<EnemyDamage>().TakeDamage(damage, "starfall", 0, 0, null);
                    last = other.gameObject;
                    Reset();
                    Debug.Log("hit");
                    limit++;
                }
                
                
            }
            //else
            //{
            //    Destroy(gameObject);
            //}
        }
    }


    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(0.1f);
        canhit = true;
    }
}

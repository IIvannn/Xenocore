using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDamageOverTime : MonoBehaviour
{
    public bool status = false;
    public string statusType = "normal";
    public bool temporary = true;
    public LayerMask enemyLayer;
    public float damage = 1f;
    public float lifetime = 3f;
    public float range = 3f;
    public string type = "normal";
    public float attackSpeed = 1;
    List<GameObject> enemiesInRange = new List<GameObject>();
    public bool growOnHit = false;
    float bscale = 0;
    public float scaleLimit = 10;
    public bool followPlayer = false;
    public bool acc = false;
    float ac = 0;
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
        if (followPlayer)
        {
            transform.position = PlayerMovement.playerPosition.position;
        }
        transform.localScale = new Vector3(range+bscale, 0.2f, range+bscale);
    }

    IEnumerator Damage()
    {

        foreach (GameObject enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                enemy.GetComponent<EnemyDamage>().TakeDamage(damage, type, 0, 0, null);
                if (acc && (attackSpeed+ac)>0.15f)
                {
                    ac -= 0.08f;
                }
                if (growOnHit && bscale < scaleLimit)
                {
                    bscale += 0.3f;
                }
                if (status)
                {
                    enemy.GetComponent<EnemyDamage>().ApplyStatus(statusType, null);
                    
                }
            }
    
        }
        //TakeDamage(BoonSTaticInfo.swarmDamage);
        yield return new WaitForSeconds(attackSpeed+ac);
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

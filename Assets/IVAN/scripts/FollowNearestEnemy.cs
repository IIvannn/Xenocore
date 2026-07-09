using UnityEngine;
using UnityEngine.AI;

public class FollowNearestEnemy : MonoBehaviour
{
    public GameObject source;
    public NavMeshAgent agent;

    public float movespeed = 8f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BoonSTaticInfo.enemiesAlive.RemoveAll(enemy => enemy == null);

        if (BoonSTaticInfo.enemiesAlive.Count == 0)
        {
            Debug.Log("NO TARGETS");

            Destroy(gameObject);
        }

        GameObject closestEnemy = GetClosestEnemy();
        

        if (closestEnemy != null)
        {
            agent.SetDestination(closestEnemy.transform.position);
            agent.speed = movespeed;
        }
        else if (source.GetComponent<EnemyDamage>() != null)
        {
            source = closestEnemy;
        }
    }

    GameObject GetClosestEnemy()
    {
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in BoonSTaticInfo.enemiesAlive)
        {
            if (enemy == null)
                continue;

            // Ignore the source object
            if (source != null && enemy.gameObject == source)
                continue;

            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }
}


using UnityEngine;

public class Ghost : MonoBehaviour
{
    
    public string enemyTag = "Enemy";
    public GameObject source;

    void Start()
    {
    }


    void Update()
    {
        // Remove destroyed enemies
        BoonSTaticInfo.enemiesInRange.RemoveAll(enemy => enemy == null);

        if (BoonSTaticInfo.enemiesInRange.Count == 0)
        {
            Debug.Log("NO TARGETS");
            Destroy(gameObject);
        }
            

        Transform closestEnemy = GetClosestEnemy();

        if (closestEnemy != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                closestEnemy.position,
                BoonSTaticInfo.hauntedGhostSpeed * Time.deltaTime
            );
        }

        if (closestEnemy != null)
        {
            float distance = Vector3.Distance(transform.position, closestEnemy.position);

            if (distance <= 0.5f)
            {
                Debug.Log("reached target");
                closestEnemy.GetComponent<EnemyDamage>().TakeDamage(BoonSTaticInfo.hauntedInitialDamage,"haunted",0,0,null);
                closestEnemy.GetComponent<EnemyDamage>().ApplyStatus("haunted", null);
                Destroy(gameObject);
            }
        }
    }

    Transform GetClosestEnemy()
    {
        Transform closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform enemy in BoonSTaticInfo.enemiesInRange)
        {
            if (enemy == null)
                continue;

            // Ignore the source object
            if (source != null && enemy.gameObject == source)
                continue;

            float distance = Vector3.Distance(transform.position, enemy.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }

    // Returns the current number of enemies inside the trigger.
    public int GetEnemyCount()
    {
        BoonSTaticInfo.enemiesInRange.RemoveAll(enemy => enemy == null);
        return BoonSTaticInfo.enemiesInRange.Count;
    }
}
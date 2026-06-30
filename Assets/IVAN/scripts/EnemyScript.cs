
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    float slowfactor;
    public float movementSpeed = 6f;

    public float attackCooldown = 0.5f;
    private float lastAttackTime;
    public float attackRange = 10;
    public float distanceBeforeStop = 3f;

    public Transform firePoint;
    public NavMeshAgent agent;
    public CharacterController controller;

    public bool dead = false;
    //string state = "idle";

    public bool pulled = true;
    float nslow = 2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BoonSTaticInfo.enemiesInRange.Add(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {

        if (BoonSTaticInfo.nulledEnemies.Contains(gameObject))
        {
            nslow = 2;
        }
        else
        {
            nslow = 0;
        }

            EnemyDamage enemyDamage = GetComponent<EnemyDamage>();

        if (enemyDamage.petrified || enemyDamage.fissured)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }


        if (!PlayerDamage.dead)
        {
            if (!enemyDamage.petrified)
            {
                float distance = Vector3.Distance(PlayerMovement.playerPosition.position, gameObject.transform.position);
                if (distance <= attackRange)
                {
                    if (dead) return;
                    MoveToPlayer(distance);

                }
            }

        }

        else
        {

        }

    }

    public void MoveToPlayer(float distance)
    {
        EnemyDamage enemyDamage = GetComponent<EnemyDamage>();

        if (PlayerMovement.playerPosition.position != null && !enemyDamage.petrified)
        {
            agent.SetDestination(PlayerMovement.playerPosition.position);
            if (distance > distanceBeforeStop)
            {
                if (enemyDamage.swarmed && BoonSTaticInfo.silky)
                {
                    agent.speed = (movementSpeed*BoonSTaticInfo.silkyBonus) - nslow;
                }
                else
                {
                    agent.speed = movementSpeed - nslow;
                }
                
            }
            
        }
    }
}

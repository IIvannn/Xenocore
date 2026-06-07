using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{

    public float movementSpeed = 6f;

    public float attackCooldown = 0.5f;
    private float lastAttackTime;
    public float attackRange = 10;
    public float distanceBeforeStop = 3f;

    public Transform firePoint;
    public NavMeshAgent agent;

    public bool dead = false;
    //string state = "idle";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerDamage.dead)
        {
            float distance = Vector3.Distance(PlayerMovement.playerPosition.position, gameObject.transform.position);
            if (distance <= attackRange)
            {
                if (dead) return;
                MoveToPlayer(distance);

            }

        }
        
        
    }

    public void MoveToPlayer(float distance)
    {
        if (PlayerMovement.playerPosition.position != null)
        {
            agent.SetDestination(PlayerMovement.playerPosition.position);
            if (distance > distanceBeforeStop)
            {
                agent.speed = movementSpeed;
            }
            
        }
    }
}

using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{

    public float movementSpeed = 6f;
    public float health = 30f;

    public float attackCooldown = 0.5f;
    private float lastAttackTime;

    public Transform firePoint;
    public NavMeshAgent agent;

    string state = "idle";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (PlayerMovement.playerPosition.position != null)
        {
            agent.SetDestination(PlayerMovement.playerPosition.position);
            agent.speed = movementSpeed;
        }
        
    }
}

using BarthaSzabolcs.IsometricAiming;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShootBrain : MonoBehaviour
{
    float distance;
    public bool LOS = false;
    public NavMeshAgent agent;
    public Animator animator;
    public GameObject spriteHolder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {
        

        if (PlayerDamage.dead)
        {
            return;
        }
        if (IsometricAiming.cameraTransform != null)
        {
            spriteHolder.transform.rotation = IsometricAiming.cameraTransform.rotation;
        }
        enemySound es = GetComponent<enemySound>();
        EnemyDamage ed = GetComponent<EnemyDamage>();
        EnemyScript esc = GetComponent<EnemyScript>();
        EnemyShoot esh = GetComponent<EnemyShoot>();
        EnemyMelee eme = GetComponent<EnemyMelee>();
        distance = Vector3.Distance(PlayerMovement.playerPosition.position, gameObject.transform.position);

        if (ed.petrified || ed.fissured || esh.firing)
        {
            agent.isStopped = true;
            animator.SetBool("canmove", false);

        }
        else
        {
            if (distance < esh.attackDistance && distance > esc.distanceBeforeStop - 3 && LOS)
            {

                if (distance > eme.attackRange)
                {
                    esh.fired = 0;
                    esh.Fire();
                    
                    agent.isStopped = true;
                }
                
                
            }
            else
            {
                if (eme.canattack && distance < eme.attackRange)
                {
                    eme.Attack();
                    es.melee();
                    animator.SetTrigger("attack");
                    agent.isStopped = true;
                }
                else if (distance < esh.attackDistance && LOS)
                {
                    Vector3 dirtop = (PlayerMovement.playerPosition.position - gameObject.transform.position);
                    agent.velocity = Vector3.Lerp(
                        agent.desiredVelocity,
                        -dirtop.normalized * agent.speed,
                        1
                        );
                }
                else
                {
                    animator.SetBool("canmove", true);
                    esc.MoveToPlayer(PlayerMovement.playerPosition);
                    agent.isStopped = false;
                    if (PlayerMovement.playerPosition.position.x <= transform.position.x)
                    {
                        spriteHolder.transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else
                    {
                        spriteHolder.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                
            }
            //agent.isStopped = false;
        }

       

        RaycastHit hit;

        if (Physics.Raycast(esh.firePoint.transform.position, esh.firePoint.transform.forward, out hit, distance))
        {

            if (hit.transform.CompareTag("Player"))
            {
                LOS = true;
            }
            else
            {
                LOS = false;
            }
        }
    }
}

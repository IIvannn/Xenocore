using BarthaSzabolcs.IsometricAiming;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeBrain : MonoBehaviour
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
        StayAtDistance esd = GetComponent<StayAtDistance>();
        EnemyMelee eme = GetComponent<EnemyMelee>();
        distance = Vector3.Distance(PlayerMovement.playerPosition.position, gameObject.transform.position);

        if (ed.petrified || ed.fissured)
        {
            agent.isStopped = true;
            animator.SetBool("canmove", false);
        }
        else
        {
            if (distance < eme.attackRange && LOS && eme.canattack)
            {
                eme.Attack();
                es.melee();
                agent.isStopped = true;
                animator.SetTrigger("attack");
            }
            else if (eme.canattack)
            {
                animator.SetBool("canmove", true);
                esc.MoveToPlayer(PlayerMovement.playerPosition);
                agent.isStopped = false;
                if (PlayerMovement.playerPosition.position.x <= transform.position.x)
                {
                    spriteHolder.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    spriteHolder.transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            else
            {
                animator.SetBool("canmove", false);
            }
            //agent.isStopped = false;
        }

        RaycastHit hit;

        if (Physics.Raycast(eme.firePoint.transform.position, eme.firePoint.transform.forward, out hit, distance))
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

using BarthaSzabolcs.IsometricAiming;
using UnityEngine;
using UnityEngine.AI;


public class EnemeyDashBrain : MonoBehaviour
{
    float distance;
    public bool LOS = false;
    public NavMeshAgent agent;
    public Vector3 move = Vector3.zero;
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


        move = (PlayerMovement.playerPosition.position - gameObject.transform.position);

        EnemyDamage ed = GetComponent<EnemyDamage>();
        EnemyScript esc = GetComponent<EnemyScript>();
        StayAtDistance esd = GetComponent<StayAtDistance>();
        EnemyDash eda = GetComponent<EnemyDash>();
        distance = Vector3.Distance(PlayerMovement.playerPosition.position, gameObject.transform.position);

        eda.move = move;
        if (ed.petrified || ed.fissured)
        {
            agent.isStopped = true;

            if (!eda.charging || !eda.dashing)
            {
                animator.SetBool("canmove", false);
            }
        }

        else
        {
            animator.SetBool("canmove", true);
            if (distance < eda.attackDistance && LOS && eda.candash && !eda.dashing)
            {
                eda.Dash();
                agent.isStopped = true;
            }
            else if (!eda.charging)
            {
                
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




            if (!agent.isStopped)
            {
                animator.SetBool("moving", true);
            }
            else
            {
                if (!eda.charging || !eda.dashing)
                {
                    animator.SetBool("moving", false);
                }
            }
        }

        RaycastHit hit;

        if (Physics.Raycast(eda.firePoint.transform.position, eda.firePoint.transform.forward, out hit, distance))
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

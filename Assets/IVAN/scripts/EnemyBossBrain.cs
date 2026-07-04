using BarthaSzabolcs.IsometricAiming;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;


public class EnemyBossBrain : MonoBehaviour
{
    float distance;
    public bool LOS = false;
    public NavMeshAgent agent;
    public Vector3 move = Vector3.zero;
    public Animator animator;
    public GameObject spriteHolder;
    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<GameObject> summons = new List<GameObject>();
    int phase = 1;
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
        EnemyShoot esh = GetComponent<EnemyShoot>();
        distance = Vector3.Distance(PlayerMovement.playerPosition.position, gameObject.transform.position);

        if (ed.currentHealth > ed.health/2)
        {
            phase = 1;
        }
        else
        {
            phase = 2;
        }


        eda.move = move;

        if (ed.petrified || ed.fissured || esh.firing)
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
            
            if (phase == 1)
            {
                esc.movementSpeed = 5;
                //Debug.Log(phase);
                if (distance < eda.attackDistance && LOS && eda.candash && !eda.dashing)
                {

                    if (BoonSTaticInfo.enemiesAlive.Count >1)
                    {
                        eda.Dash();
                    }
                    else
                    {
                        Summon();
                    }

                    
                    agent.isStopped = true;
                }
                else if (!eda.charging && phase == 1)
                {

                    esc.MoveToPlayer(PlayerMovement.playerPosition);
                    agent.isStopped = false;
                    if (PlayerMovement.playerPosition.position.x <= transform.position.x)
                    {
                        spriteHolder.transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else if (phase == 1)
                    {
                        spriteHolder.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
            }
            else if (!eda.charging || !eda.dashing)
            {
                //Debug.Log(phase);
                if (distance < esh.attackDistance && distance > esc.distanceBeforeStop - 3 && LOS)
                {
                    if (BoonSTaticInfo.enemiesAlive.Count > 1)
                    {
                        esh.fired = 0;
                        esh.Fire();
                        agent.isStopped = true;
                    }
                    else
                    {
                        Summon();
                    }
                }
                else
                {
                    if (distance < esh.attackDistance && LOS)
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

    void Summon ()
    {
        int rspawn = Random.Range(1, spawnPoints.Count);
        GameObject enemyToSpawn;
        if (phase == 1)
        {
            enemyToSpawn = summons[0];
        }
        else
        {
            enemyToSpawn = summons[1];
        }
        
        GameObject ball = Instantiate(enemyToSpawn, spawnPoints[rspawn].transform.position, transform.rotation);
        GameObject ball2 = Instantiate(enemyToSpawn, spawnPoints[rspawn].transform.position, transform.rotation);
        GameObject ball3 = Instantiate(enemyToSpawn, spawnPoints[rspawn].transform.position, transform.rotation);
    }

}

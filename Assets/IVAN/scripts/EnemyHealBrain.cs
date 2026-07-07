using BarthaSzabolcs.IsometricAiming;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemeyHealBrain : MonoBehaviour
{
    float distance;
    public bool LOS = false;
    public NavMeshAgent agent;
    public Vector3 move = Vector3.zero;
    public GameObject firePoint;
    public List<GameObject> aven = new List<GameObject>();

    public GameObject target;
    public GameObject last;
    bool healing = false;
    public GameObject healingZone;
    public Animator animator;
    public GameObject spriteHolder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(startBrain());
    }

    void Reset()
    {

        if (BoonSTaticInfo.enemiesAlive.Count == 1)
        {
            return;
        }

        last = target;
        //Debug.Log("reset");
        aven.Clear();
        foreach (GameObject obj in BoonSTaticInfo.enemiesAlive)
        {
            if (obj != null && obj != gameObject)
            {
                aven.Add(obj);
                Debug.Log(obj.name);
            }
        }
        
        //Debug.Log(aven.Count);
        if (aven.Count == 1)
        {
            target = aven[0];
        }
        else if (aven.Count == 0)
        {
            target = gameObject;
        }
        else
        {
            int rchance = Random.Range(1, aven.Count);
            target = aven[rchance];
        }
        
    }

    IEnumerator startBrain()
    {
        yield return new WaitForSeconds(0.3f);
        //Debug.Log("brainstart");
        Reset();
        StartCoroutine(changeTarget());
    }


    IEnumerator changeTarget()
    {
        yield return new WaitForSeconds(3);
        Reset();
        StartCoroutine(changeTarget());
    }

    IEnumerator heal()
    {
        EnemyDamage ed = GetComponent<EnemyDamage>();
        healingZone.SetActive(true);
        healing = true;
        healingZone.GetComponent<healingCircle>().sh();
        animator.SetBool("healing", true);
        yield return new WaitForSeconds(3);
        animator.SetBool("healing", false);
        healingZone.SetActive(false);
        healing = false;
        Reset();
        StartCoroutine(changeTarget());
    }



    // Update is called once per frame
    void Update()
    {
        

        

        if (PlayerDamage.dead || target == null)
        {
            return;
        }
        if (IsometricAiming.cameraTransform != null)
        {
            spriteHolder.transform.rotation = IsometricAiming.cameraTransform.rotation;
        }



        firePoint.transform.LookAt(target.transform.position);

        move = (target.transform.position - gameObject.transform.position);

        EnemyDamage ed = GetComponent<EnemyDamage>();
        EnemyScript esc = GetComponent<EnemyScript>();
        StayAtDistance esd = GetComponent<StayAtDistance>();
        EnemyHeal eh = GetComponent<EnemyHeal>();
        distance = Vector3.Distance(target.transform.position, gameObject.transform.position);

        if (ed.petrified || ed.fissured)
        {
            healingZone.SetActive(false);
        }

        if (eh != null)
        { eh.move = move; }
        if (ed.petrified || ed.fissured)
        {
            agent.isStopped = true;
            animator.SetBool("canmove", false);

        }
        else
        {
            if (healing)
            {
                agent.isStopped = true;
                
            }
            else
            {
                if (distance < eh.attackDistance && LOS && eh.candash && !healing)
                {

                    agent.isStopped = true;
                    StartCoroutine(heal());
                    animator.SetTrigger("heal");
                }
                else
                {
                    //eda.Dash();
                    esc.MoveToWeakest(target.transform);
                    agent.isStopped = false;
                    if (target != null)
                    {
                        if (target.transform.position.x <= transform.position.x)
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

            
            //agent.isStopped = false;
        }

        RaycastHit hit;

        if (Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out hit, distance))
        {

            if (hit.transform.CompareTag("Enemy"))
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

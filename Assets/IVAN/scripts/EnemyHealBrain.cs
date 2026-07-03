using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        StartCoroutine(changeTarget());
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
    IEnumerator changeTarget()
    {
        yield return new WaitForSeconds(5);
        Reset();
        StartCoroutine(changeTarget());
    }

    IEnumerator heal()
    {
        healingZone.SetActive(true);
        healing = true;
        healingZone.GetComponent<healingCircle>().sh();
        yield return new WaitForSeconds(5);
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

        firePoint.transform.LookAt(target.transform.position);

        move = (target.transform.position - gameObject.transform.position);

        EnemyDamage ed = GetComponent<EnemyDamage>();
        EnemyScript esc = GetComponent<EnemyScript>();
        StayAtDistance esd = GetComponent<StayAtDistance>();
        EnemyHeal eh = GetComponent<EnemyHeal>();
        distance = Vector3.Distance(target.transform.position, gameObject.transform.position);

        if (eh != null)
        { eh.move = move; }
        if (ed.petrified || ed.fissured)
        {
            agent.isStopped = true;

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
                }
                else
                {
                    //eda.Dash();
                    esc.MoveToWeakest(target.transform);
                    agent.isStopped = false;
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

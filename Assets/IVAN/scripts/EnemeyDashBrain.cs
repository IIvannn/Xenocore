using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;


public class EnemeyDashBrain : MonoBehaviour
{
    float distance;
    public bool LOS = false;
    public NavMeshAgent agent;
    public Vector3 move = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move = (PlayerMovement.playerPosition.position - gameObject.transform.position);

        if (PlayerDamage.dead)
        {
            return;
        }

        EnemyDamage ed = GetComponent<EnemyDamage>();
        EnemyScript esc = GetComponent<EnemyScript>();
        StayAtDistance esd = GetComponent<StayAtDistance>();
        EnemyDash eda = GetComponent<EnemyDash>();
        distance = Vector3.Distance(PlayerMovement.playerPosition.position, gameObject.transform.position);

        eda.move = move;
        if (ed.petrified || ed.fissured)
        {
            agent.isStopped = true;

        }
        else
        {
            if (distance < eda.attackDistance && LOS && eda.candash)
            {
                eda.Dash();
                agent.isStopped = true;
            }
            else
            {
                esc.MoveToPlayer(PlayerMovement.playerPosition);
                agent.isStopped = false;
            }
            //agent.isStopped = false;
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

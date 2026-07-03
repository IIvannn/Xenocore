using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeBrain : MonoBehaviour
{
    float distance;
    public bool LOS = false;
    public NavMeshAgent agent;
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

        EnemyDamage ed = GetComponent<EnemyDamage>();
        EnemyScript esc = GetComponent<EnemyScript>();
        StayAtDistance esd = GetComponent<StayAtDistance>();
        EnemyMelee eme = GetComponent<EnemyMelee>();
        distance = Vector3.Distance(PlayerMovement.playerPosition.position, gameObject.transform.position);

        if (ed.petrified || ed.fissured)
        {
            agent.isStopped = true;

        }
        else
        {
            if (distance < eme.attackRange && LOS && eme.canattack)
            {
                eme.Attack();
                agent.isStopped = true;
            }
            else if (eme.canattack)
            {
                esc.MoveToPlayer(PlayerMovement.playerPosition);
                agent.isStopped = false;
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

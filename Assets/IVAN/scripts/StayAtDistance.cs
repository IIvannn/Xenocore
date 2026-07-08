using UnityEngine;
using UnityEngine.AI;

public class StayAtDistance : MonoBehaviour
{
    public NavMeshAgent agent;

    public float desiredDistance = 10f;
    public float distanceVariation = 2f;
    public float arrivalDistance = 0.5f;
    public LayerMask obstacleMask;

    //bool searching;
    public bool LOS = false;

    void Start()
    {

        if (PlayerMovement.playerPosition != null)
        {
            //FindNewPosition();

        }

    }

    void Update()
    {

    }

    void FindNewPosition()
    {
        for (int i = 0; i < 20; i++)
        {
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float distance = desiredDistance + Random.Range(-distanceVariation, distanceVariation);

            Vector3 candidate = PlayerMovement.playerPosition.position + new Vector3(
                Mathf.Cos(angle),
                0,
                Mathf.Sin(angle)
            ) * distance;

            NavMeshHit hit;

            if (NavMesh.SamplePosition(candidate, out hit, 2f, NavMesh.AllAreas))
            {
                NavMeshPath path = new NavMeshPath();

                if (agent.CalculatePath(hit.position, path) &&
                    path.status == NavMeshPathStatus.PathComplete)
                {
                    agent.SetDestination(hit.position);
                    searching = true;
                    return;
                }
            }
            Debug.Log(hit.position);
        }



    }


    }

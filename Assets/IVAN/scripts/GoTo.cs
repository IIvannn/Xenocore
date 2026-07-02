using UnityEngine;

public class GoTo : MonoBehaviour
{
    public Transform target;
    float travelSpeed = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        float distance = Vector3.Distance(target.position, gameObject.transform.position);
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * travelSpeed * Time.deltaTime;

        if (distance <= 0.2f)
        {
            Destroy(gameObject);
        }
    }
}

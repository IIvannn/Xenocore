using UnityEngine;
using System.Collections;
public class NullBubble : MonoBehaviour
{
    private void Start()
    {
        transform.localScale = new Vector3(BoonSTaticInfo.nullRange, 0.3f, BoonSTaticInfo.nullRange);
        StartCoroutine(BubbleDuration());
        
    }
    private void OnTriggerStay(Collider other)
    {
        EnemyDamage enemy = other.GetComponent<EnemyDamage>();

        if (enemy != null)
        {
            Vector3 direction = transform.position - enemy.transform.position;
            

            float distance = direction.magnitude;

            
            distance = Mathf.Max(distance, 0.5f);

            
            float pullForce = 20f / distance;

            enemy.NullPull(direction*pullForce);
        }
    }

    IEnumerator BubbleDuration()
    {
        yield return new WaitForSeconds(BoonSTaticInfo.nullDuration);
        Death();
    }

    public void Death()
    {
        BoonSTaticInfo.nullCurrentCount--;
        Debug.Log(BoonSTaticInfo.nullCurrentCount);
        Destroy(gameObject);

    }
}

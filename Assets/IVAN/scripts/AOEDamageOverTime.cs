using UnityEngine;
using System.Collections;

public class AOEDamageOverTime : MonoBehaviour
{
    public bool temporary = true;
    public LayerMask enemyLayer;
    public float damage = 1f;
    public float lifetime = 3f;
    public float range = 3f;
    public string type = "normal";
    public float attackSpeed = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Damage());
        if (temporary)
        {
            Destroy(gameObject, lifetime);
        }
        transform.localScale = new Vector3(range, 0.2f, range);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Damage()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, range, enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyDamage>().TakeDamage(damage, type, 0, 0, null);
        }
        //TakeDamage(BoonSTaticInfo.swarmDamage);
        yield return new WaitForSeconds(attackSpeed);
        StartCoroutine(Damage());
    }
}

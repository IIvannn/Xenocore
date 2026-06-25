using System.Collections.Generic;
using UnityEngine;

public class ExorcismBlast : MonoBehaviour
{
    public bool temporary = true;
    public List<GameObject> enemieshit = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (temporary)
        {
            Destroy(gameObject, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!enemieshit.Contains(other.gameObject) && other.GetComponent<EnemyDamage>().haunted && !other.GetComponent<EnemyDamage>().dead)
            {
                enemieshit.Add(other.gameObject);
                //Debug.Log("EnemyDamage hit for:  " + damage);
                other.GetComponent<EnemyDamage>().TakeDamage(BoonSTaticInfo.exorcismBonus, "haunted", 0, 0, null);
            }
        }

    }
}
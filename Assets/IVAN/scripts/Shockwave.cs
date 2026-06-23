using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Shockwave : MonoBehaviour
{
    float damage = 10;
    List<GameObject> enemieshit = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!enemieshit.Contains(other.gameObject))
            {
                enemieshit.Add(other.gameObject);
                //Debug.Log("EnemyDamage hit for:  " + damage);
                other.GetComponent<EnemyDamage>().TakeDamage(damage, "normal", 0, 0, null);
            }
        }

    }
}
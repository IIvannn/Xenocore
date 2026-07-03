using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationSpell : MonoBehaviour
{
    float attackSpeed = 0.2f;
    List<GameObject> enemiesInRange = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Damage());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Damage()
    {

        foreach (GameObject enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                enemy.GetComponent<EnemyDamage>().radiationAmmount += 0.2f;

            }

        }
        
        yield return new WaitForSeconds(attackSpeed);
        StartCoroutine(Damage());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!enemiesInRange.Contains(other.gameObject))
            {
                enemiesInRange.Add(other.gameObject);
            }
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (enemiesInRange.Contains(other.gameObject))
            {
                enemiesInRange.Remove(other.gameObject);
            }
        }
    }


}

using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class healingCircle : MonoBehaviour
{
    public List<GameObject> he = new List<GameObject>();
    bool canheal = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void sh()
    {
        StartCoroutine(cooldown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && !he.Contains(other.gameObject))
        {
            he.Add(other.gameObject);
            if (other.GetComponent<EnemyDamage>().currentHealth < other.GetComponent<EnemyDamage>().health)
            {
                other.GetComponent<EnemyDamage>().currentHealth += 3;
            }
            

        }
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(0.2f);
        he.Clear();
        StartCoroutine(cooldown());
    }

}

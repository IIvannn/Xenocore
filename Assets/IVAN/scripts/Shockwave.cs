using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public bool temporary = true;
    public float damage = 10;
    public string type = "normal";
    public float range = 4.2f;
    public List<GameObject> enemieshit = new List<GameObject>();
    public bool mstrike = false;
    public float cc = 0;
    public float cd = 1;

    public bool status = false;

    public float lifetime = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log(type);
        if (temporary)
        {
            Destroy(gameObject, lifetime);
        }

        transform.localScale = new Vector3(range, range, range);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Prism") && !mstrike)
        {
            other.GetComponent<Prism>().Hurt();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!enemieshit.Contains(other.gameObject))
            {
                if (mstrike && BoonSTaticInfo.nulledEnemies.Contains(other.gameObject))
                {
                    enemieshit.Add(other.gameObject);
                    //Debug.Log("EnemyDamage hit for:  " + damage);
                    other.GetComponent<EnemyDamage>().TakeDamage(damage, type, cc, cd, null);
                    if (status)
                    {
                        other.GetComponent<EnemyDamage>().ApplyStatus(type, null);
                    }
                }
                else if (!mstrike)
                {
                    enemieshit.Add(other.gameObject);
                    //Debug.Log("EnemyDamage hit for:  " + damage);
                    other.GetComponent<EnemyDamage>().TakeDamage(damage, type, cc, cd, null);

                    if (status)
                    {
                        other.GetComponent<EnemyDamage>().ApplyStatus(type, null);
                    }
                }
                    
            }
        }
    }
}
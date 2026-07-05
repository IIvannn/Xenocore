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
    public SpriteRenderer sprite;

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

        switch (type)
        {
            case "normal":
                sprite.color = new Color(1f, 1f, 1f);
                break;
            case "swarm":
                sprite.color = new Color(0.7f, 1f, 0.2f);
                break;
            case "haunted":
                sprite.color = new Color(0.2f, 0.7f, 0.8f);
                break;
            case "crystallize":
                sprite.color = new Color(0.7f, 0.7f, 0.9f);
                break;
            case "null":
                sprite.color = new Color(0.1f, 0.0f, 0.4f);
                break;
            case "starfall":
                sprite.color = new Color(1f, 0.1f, 0.7f);
                break;
            case "rust":
                sprite.color = new Color(0.6f, 0.1f, 0.2f);
                break;
            case "tectonic":
                sprite.color = new Color(0.8f, 0.6f, 0.2f);
                break;
            case "radiation":
                sprite.color = new Color(1f, 1f, 0.2f);
                break;
            case "volcanic":
                sprite.color = new Color(1f, 1f, 0.5f);
                break;
            case "star":
                sprite.color = new Color(1f, 0.4f, 1f);
                break;
            case "gem":
                sprite.color = new Color(0.7f, 0.7f, 0.9f);
                break;
        }


        transform.localScale = new Vector3(range, range, range);
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
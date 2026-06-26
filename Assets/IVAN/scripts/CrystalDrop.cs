using System.Collections.Generic;
using UnityEngine;

public class CrystalDrop : MonoBehaviour
{
    float travelSpeed = 30f;
    public GameObject resonnance;
    List<GameObject> enemieshit = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (PlayerMovement.playerPosition.position - transform.position).normalized;
        transform.position += dir * travelSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BoonSTaticInfo.crystals += (int)(BoonSTaticInfo.crystallizeCrystalAmmount* BoonSTaticInfo.moneyMultiplier);
            other.GetComponent<PlayerDamage>().currentEnergy += BoonSTaticInfo.crystallizeEnergyAmmount;
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            
            if (BoonSTaticInfo.resonance)
            {
                
                if (!enemieshit.Contains(other.gameObject))
                {
                    enemieshit.Add(other.gameObject);
                    GameObject ball = Instantiate(resonnance, transform.position, transform.rotation);
                    ball.GetComponent<Shockwave>().damage = BoonSTaticInfo.resonanceDamage;
                    ball.GetComponent<Shockwave>().range = 6;
                    ball.GetComponent<Shockwave>().type = "crystallize";
                }
                    
            }
        }
    }

    }

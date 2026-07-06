using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalDrop : MonoBehaviour
{
    float travelSpeed = 18f;
    public GameObject resonnance;
    List<GameObject> enemieshit = new List<GameObject>();
    bool canshock = true;
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
            float rchance = Random.Range(1, 100);
            if (rchance < BoonSTaticInfo.excavationBonus*100)
            {
                BoonSTaticInfo.crystals += (int)(BoonSTaticInfo.crystallizeCrystalAmmount * BoonSTaticInfo.moneyMultiplier);
            }


            BoonSTaticInfo.crystals += (int)(BoonSTaticInfo.crystallizeCrystalAmmount* BoonSTaticInfo.moneyMultiplier);
            PlayerDamage.currentEnergy += BoonSTaticInfo.crystallizeEnergyAmmount;
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            
            if (BoonSTaticInfo.resonance)
            {
                //Debug.Log(canshock);
                if (!enemieshit.Contains(other.gameObject) && canshock)
                {
                    StartCoroutine(shockwaveCooldown());
                    canshock = false;
                    enemieshit.Add(other.gameObject);
                    GameObject ball = Instantiate(resonnance, transform.position, transform.rotation);
                    ball.GetComponent<Shockwave>().damage = BoonSTaticInfo.resonanceDamage;
                    ball.GetComponent<Shockwave>().range = 6;
                    ball.GetComponent<Shockwave>().type = "gem";
                    //Debug.Log("crysshock");
                }
                    
            }
        }
    }

    IEnumerator shockwaveCooldown()
    {
        yield return new WaitForSeconds(1);
        canshock = true;
    }

}

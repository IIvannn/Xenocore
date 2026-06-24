using UnityEngine;
using static Unity.VisualScripting.Member;

public class CrystalDrop : MonoBehaviour
{
    float travelSpeed = 30f;
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
    }

    }

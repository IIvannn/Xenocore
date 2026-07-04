using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public List<GameObject> rewardPool = new List<GameObject>();
    public Transform slot1;
    public Transform slot2;
    public Transform slot3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        slot1.gameObject.SetActive(false);
        slot2.gameObject.SetActive(false);
        slot3.gameObject.SetActive(false);

        int rnimber1 = Random.Range(1,rewardPool.Count);
        int rnimber2 = Random.Range(1, rewardPool.Count);
        int rnimber3 = Random.Range(1, rewardPool.Count);

        GameObject ball = Instantiate(rewardPool[rnimber1], slot1.position, transform.rotation);
        GameObject ball2 = Instantiate(rewardPool[rnimber2], slot2.position, transform.rotation);
        GameObject ball3 = Instantiate(rewardPool[rnimber3], slot3.position, transform.rotation);

        if (ball.GetComponent<Upgrade>() != null)
        {
            ball.GetComponent<Upgrade>().priced = true;
        }
        else
        {
            ball.GetComponent<Pickup>().priced = true;
        }

        if (ball3.GetComponent<Upgrade>() != null)
        {
            ball3.GetComponent<Upgrade>().priced = true;
        }
        else
        {
            ball3.GetComponent<Pickup>().priced = true;
        }

        if (ball2.GetComponent<Upgrade>() != null)
        {
            ball2.GetComponent<Upgrade>().priced = true;
        }
        else
        {
            ball2.GetComponent<Pickup>().priced = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

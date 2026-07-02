using UnityEngine;
using System.Collections;

public class Summoner : MonoBehaviour
{
    public bool automatic = true;
    public bool nest = true;
    public GameObject summon;
    public float summonSpeed = 2;
    public float lifetime = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (nest)
        {
            lifetime = BoonSTaticInfo.nestDuration;
            summonSpeed = BoonSTaticInfo.waspSpawnSpeed;
        }
        if (automatic)
        {
            StartCoroutine(Summon());
        }
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Summon()
    {
        GameObject ball = Instantiate(summon, transform.position, transform.rotation);
        if (nest)
        {
            ball.GetComponent<DamageOnContact>().damage = BoonSTaticInfo.waspDamage;
            ball.GetComponent<DamageOnContact>().type = "swarm";
            ball.GetComponent<FollowNearestEnemy>().movespeed = BoonSTaticInfo.waspSpeed;
        }
        yield return new WaitForSeconds(summonSpeed);
        if (automatic )
        { StartCoroutine(Summon()); }
    }
}

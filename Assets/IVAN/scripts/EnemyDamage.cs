using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using BarthaSzabolcs.IsometricAiming;

public class EnemyDamage : MonoBehaviour
{
    [Header("Values")]
    public float health;
    public float deathDelay;
    public float currentHealth;

    [Header("References")]
    public Slider healthbar;
    public Slider easeHealthbar;
    public Transform UIParent;
    public LayerMask enemyLayer;
    public GameObject damageNumber;
    public Transform damageNumberSpawn;


    [Header("Status effects")]
    public GameObject swarm;
    public bool swarmed = false;
    public bool haunted = false;
    public bool crystallized = false;
    public GameObject nullBubble;
    public bool nulled = false;
    public bool starfalled = false;
    public bool rusted;
    public GameObject tectonic;
    public bool irradiated;

    float lerpSpeed = 0.03f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        swarm.SetActive(false);
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
        UIParent.rotation = IsometricAiming.cameraTransform.rotation;

        healthbar.value = (currentHealth / health);
        easeHealthbar.value = Mathf.Lerp(easeHealthbar.value, healthbar.value, lerpSpeed);
        if (currentHealth > health)
        {
            currentHealth = health;
        }
    }


    public void TakeDamage(float damage, string type)
    {
        //Debug.Log("damage:  "+damage+"  health:  "+currentHealth);
        GameObject dmgNumber = Instantiate(damageNumber, damageNumberSpawn.position, damageNumberSpawn.rotation);
        dmgNumber.GetComponent<DamageNumber>().type = type;
        dmgNumber.GetComponent<DamageNumber>().damage = damage;
        currentHealth -= damage;
        {
            if (currentHealth < 0)
            {
                Death();
            }
        }
    }

    public void Death()
    {
        EnemyScript body = GetComponent<EnemyScript>();
        body.dead = true;
        Destroy(gameObject, deathDelay);
    }

    public void ApplyStatus(string status)
    {
        switch (status)
        {
            case "normal":
                break;
            case "swarm":
                
                swarm.SetActive(true);
                if (!swarmed)
                {
                    StartCoroutine(SwarmDuration());
                    StartCoroutine(SwarmDamage());
                }
                swarmed = true;

                break;
            case "haunted":

                break;
            case "crystallize":

                break;
            case "null":

                break;
            case "starfall":
                int rchance = Random.Range(0, 100);
                if (rchance < BoonSTaticInfo.starfallChance)
                {
                    Debug.Log("starfall chance:  "+ rchance);
                    
                    TakeDamage(BoonSTaticInfo.starfallDamage, "starfall");
                    StartCoroutine(StarfallDuration());
                }
                break;
            case "rust":

                break;
            case "tectonic":

                break;
            case "radiation":

                break;

        }

    }

    IEnumerator SwarmDuration()
    {
        yield return new WaitForSeconds(BoonSTaticInfo.swarmDuration);
        swarm.SetActive(false);
        swarmed = false;
    }
    IEnumerator SwarmDamage()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, BoonSTaticInfo.swarmRange, enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
           
            enemy.GetComponent<EnemyDamage>().TakeDamage(BoonSTaticInfo.swarmDamage, "swarm");
        }
        //TakeDamage(BoonSTaticInfo.swarmDamage);
        yield return new WaitForSeconds(BoonSTaticInfo.swarmAttackSpeed);
        if (swarmed)
        {
            StartCoroutine(SwarmDamage());
        }
    }

    IEnumerator StarfallDuration()
    {
        yield return new WaitForSeconds(BoonSTaticInfo.starfallDuration);
        starfalled = false;
    }

}




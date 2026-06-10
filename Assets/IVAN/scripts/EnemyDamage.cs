using BarthaSzabolcs.IsometricAiming;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class EnemyDamage : MonoBehaviour
{
    [Header("Values")]
    public float health;
    public float deathDelay;
    public float currentHealth;

    [Header("References")]
    public Slider healthbar;
    public Slider easeHealthbar;
    public GameObject radiationBarParent;
    public Slider radiationBar;
    public Transform UIParent;
    public LayerMask enemyLayer;
    public GameObject damageNumber;
    public Transform damageNumberSpawn;
    public CharacterController controller;


    [Header("Status effects")]
    public GameObject swarm;
    public bool swarmed = false;
    public bool haunted = false;
    public GameObject crystallizedDrop;
    public bool crystallized = false;
    public GameObject nullBubble;
    public bool nulled = false;
    public bool starfalled = false;
    public bool rusted;
    float rustboost;
    public GameObject tectonic;
    public bool tectoniked = false;
    public GameObject radiationRing;
    public float radiationAmmount;
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
        if (irradiated)
        {
            radiationAmmount = 1;
        }
        if (radiationAmmount>0)
        {
            radiationBarParent.SetActive(true);
            Slider slider = radiationBar.GetComponent<Slider>();
            radiationBar.value = radiationAmmount+0.1f;
            radiationAmmount -= 1f * Time.deltaTime;
        }
        else
        {
            radiationBar.value = radiationAmmount;
            radiationBarParent.SetActive(false);
        }

        //Debug.Log(swarmed);
        if (rusted)
        {
            rustboost = BoonSTaticInfo.rustCritChance;
        }
        else
        {
            rustboost = 0;
        }
        //Debug.Log(IsometricAiming.cameraTransform.rotation);


        Debug.Log(radiationAmmount);

        healthbar.value = (currentHealth / health);
        easeHealthbar.value = Mathf.Lerp(easeHealthbar.value, healthbar.value, lerpSpeed);
        if (currentHealth > health)
        {
            currentHealth = health;
            
        }

        if (IsometricAiming.cameraTransform != null)
        {
            UIParent.rotation = IsometricAiming.cameraTransform.rotation;
        }
    }


    public void TakeDamage(float damage, string type, float critC, float critD, GameObject source)
    {
        
        GameObject dmgNumber = Instantiate(damageNumber, damageNumberSpawn.position, damageNumberSpawn.rotation);
        float rcchance = Random.Range(0, 100);
        if (rcchance < (critC+rustboost)) 
        {
            damage = damage * critD;
            if (source != null)
            {
                source.GetComponent<PlayerShoot>().onCrit();
            }
            dmgNumber.GetComponent<DamageNumber>().textDmg.outlineColor = new Color(1,0,0);
        }
        //Debug.Log("crit chance: "+ (critC + rustboost));

        dmgNumber.GetComponent<DamageNumber>().type = type;
        dmgNumber.GetComponent<DamageNumber>().damage = damage;
        if (source != null)
        {
            source.GetComponent<PlayerShoot>().onHit();
        }
        
        if (crystallized)
        {
            float rcrystal = Random.Range(0, 100);
            if (rcrystal < BoonSTaticInfo.crystallizeCrystalChance)
            {
                GameObject ball = Instantiate(crystallizedDrop, transform.position, transform.rotation);
            }
        }

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

    public void ApplyStatus(string status, GameObject source)
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
                    source.GetComponent<PlayerShoot>().onStatus(status);
                }
                swarmed = true;

                break;
            case "haunted":
                if (!haunted)
                {
                    source.GetComponent<PlayerShoot>().onStatus(status);
                }
                break;
            case "crystallize":
                if (!crystallized)
                {
                    crystallized = true;
                    StartCoroutine(CrystallizeDuration());
                    source.GetComponent<PlayerShoot>().onStatus(status);
                }
                break;
            case "null":
                if (!nulled && BoonSTaticInfo.nullCurrentCount<BoonSTaticInfo.nullMaxCount)
                {
                    GameObject ball = Instantiate(nullBubble, transform.position, transform.rotation);
                    BoonSTaticInfo.nullCurrentCount++;
                    nulled = true;
                    StartCoroutine(NullDuration());
                    source.GetComponent<PlayerShoot>().onStatus(status);
                }
                break;

            case "starfall":
                if (!starfalled)
                {
                    int rchance = Random.Range(0, 100);
                    if (rchance < BoonSTaticInfo.starfallChance)
                    {
                        Debug.Log("starfall chance:  " + rchance);

                        TakeDamage(BoonSTaticInfo.starfallDamage, "starfall", 0, 0, null);
                        StartCoroutine(StarfallDuration());
                        source.GetComponent<PlayerShoot>().onStatus(status);
                    }
                }
                
                break;
            case "rust":
                if (!rusted)
                {
                    rusted = true;
                    StartCoroutine(RustDuration());
                    source.GetComponent<PlayerShoot>().onStatus(status);
                    EnemyShoot body = GetComponent<EnemyShoot>();
                    body.rusted = true;
                }
                
                break;
            case "tectonic":
                if (!tectoniked && BoonSTaticInfo.tectonicCurrentCount < BoonSTaticInfo.tectonicMaxCount)
                {
                    GameObject ball = Instantiate(tectonic, transform.position, transform.rotation);
                    BoonSTaticInfo.tectonicCurrentCount++;
                    tectoniked = true;
                    StartCoroutine(TectonicDuration());
                    source.GetComponent<PlayerShoot>().onStatus(status);
                }
                
                break;
            case "radiation":
                if (!irradiated)
                {
                    irradiated = true;
                    radiationRing.SetActive(true);
                    source.GetComponent<PlayerShoot>().onStatus(status);
                    StartCoroutine(RadiationDuration());
                }
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
           
            enemy.GetComponent<EnemyDamage>().TakeDamage(BoonSTaticInfo.swarmDamage, "swarm",0,0,null);
        }
        //TakeDamage(BoonSTaticInfo.swarmDamage);
        yield return new WaitForSeconds(BoonSTaticInfo.swarmAttackSpeed);
        if (swarmed)
        {
            Debug.Log("swarm ATTACK");
            StartCoroutine(SwarmDamage());
        }
    }

    IEnumerator StarfallDuration()
    {
        yield return new WaitForSeconds(BoonSTaticInfo.starfallDuration);
        starfalled = false;
    }

    public void NullPull(Vector3 direction)
    {
        EnemyScript enemy = gameObject.GetComponent<EnemyScript>();
        enemy.controller.Move(direction.normalized * BoonSTaticInfo.nullPullStrength * Time.deltaTime);
    }

    IEnumerator NullDuration()
    {
        yield return new WaitForSeconds(BoonSTaticInfo.nullDuration);
        nulled = false;
    }

    IEnumerator CrystallizeDuration()
    {
        yield return new WaitForSeconds(BoonSTaticInfo.crystallizeDuration);
        crystallized = false;
    }

    IEnumerator RustDuration()
    {
        yield return new WaitForSeconds(BoonSTaticInfo.rustDuration);
        rusted = false;
        EnemyShoot body = GetComponent<EnemyShoot>();
        body.rusted = false;
    }

    IEnumerator TectonicDuration()
    {
        yield return new WaitForSeconds(BoonSTaticInfo.tectonicDuration);
        tectoniked = false;
    }

    IEnumerator RadiationDuration()
    {
        yield return new WaitForSeconds(BoonSTaticInfo.radiationDuration+0.1f);
        radiationRing.SetActive(false);
        irradiated = false;
    }

}






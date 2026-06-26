using BarthaSzabolcs.IsometricAiming;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    [Header("Values")]
    public float health;
    public float deathDelay;
    public float currentHealth;
    public bool dead = false;
    public float armor;
    float startingArmor = 0;

    [Header("References")]
    public Transform UIParent;
    public LayerMask enemyLayer;
    public GameObject damageNumber;
    public Transform damageNumberSpawn;
    public CharacterController controller;
    public GameObject wasp;
    public GameObject nest;
    public GameObject phantom;
    public GameObject exorcism;
    public GameObject petrify;
    public GameObject prism;

    [Header("UI")]
    public Slider healthbar;
    public Slider easeHealthbar;
    public Slider radiationBar;
    public Slider ghostBar;
    public Slider armorBar;

    [Header("Status effects")]
    public GameObject swarm;
    public bool swarmed = false;
    public GameObject ghost;
    public bool haunted = false;
    float hauntedStoredDamage;
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
    public bool burning = false;
    public bool petrified = false;

    float lerpSpeed = 0.03f;
    float bcd = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        swarm.SetActive(false);
        currentHealth = health;
        startingArmor = armor;

        BoonSTaticInfo.enemiesAlive.Add(gameObject);
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
            radiationBar.gameObject.SetActive(true);
            Slider slider = radiationBar.GetComponent<Slider>();
            radiationBar.value = radiationAmmount+0.1f;
            radiationAmmount -= 1f * Time.deltaTime;
        }
        else
        {
            radiationBar.value = radiationAmmount;
            radiationBar.gameObject.SetActive(false);
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


        if (radiationAmmount<0.01)
        {
            radiationAmmount = 0;
        }

        ghostBar.value = (currentHealth / health);
        healthbar.value = ((currentHealth - hauntedStoredDamage) / health);
        easeHealthbar.value = Mathf.Lerp(easeHealthbar.value, healthbar.value, lerpSpeed);
        armorBar.value = (armor/startingArmor);
    
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
        if (!dead)
        {
            if (BoonSTaticInfo.emotionalDamage)
            {
                bcd = BoonSTaticInfo.emotionalDamageBonus;
            }
            float armorReduction = 0.7f;
            int finalDamage;
            GameObject dmgNumber = Instantiate(damageNumber, damageNumberSpawn.position, damageNumberSpawn.rotation);
            float rcchance = Random.Range(0, 100);
            if (rcchance < (critC + rustboost))
            {
                damage = damage * (critD + bcd);
                if (source != null)
                {
                    source.GetComponent<PlayerShoot>().onCrit();
                }
                dmgNumber.GetComponent<DamageNumber>().textDmg.outlineColor = new Color(1, 0, 0);
            }
            //Debug.Log("crit chance: "+ (critC + rustboost));


            if (source != null)
            {
                source.GetComponent<PlayerShoot>().onHit();
            }

            if (crystallized)
            {

                if (BoonSTaticInfo.overgrowth)
                {
                    float rprism = Random.Range(0, 100);
                    if (rprism < BoonSTaticInfo.overgrowthChance)
                    {
                        GameObject ball = Instantiate(prism, transform.position, transform.rotation);
                    }
                }

                float rcrystal = Random.Range(0, 100);
                if (rcrystal < BoonSTaticInfo.crystallizeCrystalChance)
                {
                    GameObject ball = Instantiate(crystallizedDrop, transform.position, transform.rotation);
                    if (BoonSTaticInfo.medusa)
                    {
                        float rm = Random.Range(0, 100);
                        if (rm < BoonSTaticInfo.medusaChance)
                        {
                            ApplyStatus("petrify", null);
                        }
                    }
                }
            }
            if (haunted)
            {
                hauntedStoredDamage += damage * BoonSTaticInfo.hauntedDamagePercentage / 100;
            }

            float monopolyBonus;
            if (BoonSTaticInfo.monopoly)
            {
                monopolyBonus = (((BoonSTaticInfo.crystals)/5)*0.01f)+1;
                damage*= monopolyBonus;
                //Debug.Log("monopoly: "+monopolyBonus);
            }

            finalDamage = (int)(damage * (1 + (((radiationAmmount + 0.02f) * BoonSTaticInfo.radiationWeakness) / 100)));

            if (BoonSTaticInfo.reaper)
            {
                finalDamage += (BoonSTaticInfo.reaperBonus) / 2;
            }

            if (type == "swarm" && BoonSTaticInfo.corrosive)
            {
                armorReduction += BoonSTaticInfo.corrosiveBonus;
            }
            if (BoonSTaticInfo.molten)
            {
                armorReduction += BoonSTaticInfo.moltenBonus;
            }

            

            if (armor > 0)
            {
                armor -= (finalDamage) * armorReduction;
                dmgNumber.GetComponent<DamageNumber>().type = type;
                dmgNumber.GetComponent<DamageNumber>().damage = (finalDamage) * armorReduction;
            }

            else
            {
                currentHealth -= finalDamage;
                dmgNumber.GetComponent<DamageNumber>().type = type;
                dmgNumber.GetComponent<DamageNumber>().damage = finalDamage;
            }


            if (BoonSTaticInfo.nested)
            {
                float nestChance = Random.Range(1, 100);
                if (nestChance < BoonSTaticInfo.nestedChance)
                {
                    GameObject ball = Instantiate(wasp, transform.position, transform.rotation);
                    ball.GetComponent<DamageOnContact>().damage = BoonSTaticInfo.waspDamage;
                    ball.GetComponent<DamageOnContact>().type = "swarm";
                    ball.GetComponent<FollowNearestEnemy>().movespeed = BoonSTaticInfo.waspSpeed;
                    ball.GetComponent<FollowNearestEnemy>().source = gameObject;
                    ball.GetComponent<DamageOnContact>().source = gameObject;
                }
            }

            int rschance = Random.Range(0, 100);
            if (rschance < BoonSTaticInfo.starfallChance && starfalled)
            {
                Debug.Log("starfall chance:  " + rschance);

                TakeDamage(BoonSTaticInfo.starfallDamage, "starfall", 0, 0, null);

            }

            if (PlayerDamage.currentHp < (PlayerDamage.hp * BoonSTaticInfo.adaptationLimit) && BoonSTaticInfo.adaptation)
            {
                PlayerDamage.currentHp++;
            }



            if (currentHealth < 0)
            {
                Death();
            }
        }
        
    }
    

    public void Death()
    {
        if (BoonSTaticInfo.exorcism && !dead)
        {
            GameObject exo = Instantiate(exorcism, transform.position, transform.rotation);
        }
            
        if (BoonSTaticInfo.necromancer && !dead)
        {
            float necroChance = Random.Range(1, 100);
            if (necroChance < BoonSTaticInfo.necroancerChance)
            {
                GameObject ball = Instantiate(phantom, transform.position, transform.rotation);
            }
        }

        if (BoonSTaticInfo.reaper)
        {
            BoonSTaticInfo.reaperBonus++;
        }
        if (BoonSTaticInfo.infestation)
        {
            float nestchance = Random.Range(1, 100);
            if (nestchance < BoonSTaticInfo.infestationChance)
            {
                GameObject nestSummon = Instantiate(nest, transform.position, transform.rotation);
            }
        }
        dead = true;
        if (haunted)
        {
            SummonGhost();
        }
        EnemyScript body = GetComponent<EnemyScript>();
        body.dead = dead;
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
                    swarm.transform.localScale = new Vector3(BoonSTaticInfo.swarmRange, 0.2f, BoonSTaticInfo.swarmRange);
                    StartCoroutine(SwarmDuration());
                    StartCoroutine(SwarmDamage());
                    if (source != null)
                    {
                        source.GetComponent<PlayerShoot>().onStatus(status);
                    }
                    
                }
                swarmed = true;

                break;
            case "haunted":
                if (!haunted)
                {
                    haunted = true;
                    StartCoroutine(HauntedDuration());
                    if (source != null)
                    {
                        source.GetComponent<PlayerShoot>().onStatus(status);
                    }

                }
                break;
            case "crystallize":
                if (!crystallized)
                {
                    crystallized = true;
                    StartCoroutine(CrystallizeDuration());
                    if (source != null)
                    {
                        source.GetComponent<PlayerShoot>().onStatus(status);
                    }
                }
                break;
            case "null":
                if (!nulled && BoonSTaticInfo.nullCurrentCount<BoonSTaticInfo.nullMaxCount)
                {
                    GameObject ball = Instantiate(nullBubble, transform.position, transform.rotation);
                    BoonSTaticInfo.nullCurrentCount++;
                    nulled = true;
                    StartCoroutine(NullDuration());
                    if (source != null)
                    {
                        source.GetComponent<PlayerShoot>().onStatus(status);
                    }
                }
                break;

            case "starfall":
                if (!starfalled)
                {
                    starfalled = true;
                    StartCoroutine(StarfallDuration());
                    if (source != null)
                    {
                       source.GetComponent<PlayerShoot>().onStatus(status);
                    }
                    
                }
                
                break;
            case "rust":
                if (!rusted)
                {
                    rusted = true;
                    StartCoroutine(RustDuration());
                    if (source != null)
                    {
                        source.GetComponent<PlayerShoot>().onStatus(status);
                    }
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
                    if (source != null)
                    {
                        source.GetComponent<PlayerShoot>().onStatus(status);
                    }
                }
                
                break;
            case "radiation":
                if (!irradiated && BoonSTaticInfo.radiationCurrentCount < BoonSTaticInfo.radiationMaxCount)
                {
                    BoonSTaticInfo.radiationCurrentCount++;
                    irradiated = true;
                    radiationRing.SetActive(true);
                    if (source != null)
                    {
                        source.GetComponent<PlayerShoot>().onStatus(status);
                    }
                    StartCoroutine(RadiationDuration());
                }
                break;
            case "volcanic":
                if (!burning)
                {
                    burning = true;
                    StartCoroutine(VolcanicDamage());
                    StartCoroutine(VolcanicDuration());
                }
                break;
            case "petrify":
                if (!petrified)
                {
                    petrified = true;
                    petrify.SetActive(true);
                    StartCoroutine(PetrifiedDuration());
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
            StartCoroutine(SwarmDamage());
        }
    }

    IEnumerator VolcanicDamage()
    {
        if (burning)
        {
            TakeDamage(BoonSTaticInfo.volcanicDamage, "volcanic",0,0,null);
            yield return new WaitForSeconds(BoonSTaticInfo.volcanicAttackSpeed);
            if (burning)
            {
                StartCoroutine(VolcanicDamage());
            }
        }
    }
    IEnumerator VolcanicDuration()
    {
        yield return new WaitForSeconds(2);
        burning = false;
    }
    IEnumerator PetrifiedDuration()
    {
        yield return new WaitForSeconds(2.5f);
        petrify.SetActive(false);
        petrified = false;
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
        BoonSTaticInfo.radiationCurrentCount--;
        irradiated = false;
    }

    IEnumerator HauntedDuration()
    {
        yield return new WaitForSeconds(BoonSTaticInfo.hauntedDuration);
        SummonGhost();
        haunted = false;
    }

    public void SummonGhost()
    {
        if (currentHealth>0)
        {
            TakeDamage(hauntedStoredDamage, "haunted", 0, 0, null);
            hauntedStoredDamage = 0;
        }
        GameObject ball = Instantiate(ghost, transform.position, transform.rotation);
        ball.GetComponent<Ghost>().source = gameObject; 
    }
}






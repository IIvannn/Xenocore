using BarthaSzabolcs.IsometricAiming;
using System.Collections;
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
    public GameObject shockwave;
    public GameObject rust;
    public GameObject stun;
    public GameObject radonBlood;
    public GameObject starfallindicator;
    public GameObject crystallizeindicator;
    public GameObject fireindicator;
    public GameObject hauntedicator;
    public Animator star;
    public Animator hit1;
    public Animator hit2;
    public Animator hit3;

    [Header("UI")]
    public Slider healthbar;
    public Slider easeHealthbar;
    public Slider radiationBar;
    public Slider ghostBar;
    public Slider armorBar;
    public Slider fissureBar;

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
    bool fated;

    public float fissure = 0;
    public bool fissured = false;
    bool canmonte = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        if (GetComponent<EnemyBossBrain>() == null)
        {
            float rarmor = Random.Range(DoorScript.currentRoom, 20);

            if (rarmor >= 14)
            {
                armor = (int)((health / 1.5f) + (DoorScript.currentRoom * 2));
            }
        }
        



        swarm.SetActive(false);
        currentHealth = health;
        startingArmor = armor;

        BoonSTaticInfo.enemiesAlive.Add(gameObject);
        //Debug.Log("Enemies alive:  " + BoonSTaticInfo.enemiesAlive.Count);
        //Debug.Log(BoonSTaticInfo.enemiesAlive.Count);
    }

    // Update is called once per frame
    void Update()
    {

        fissureBar.value = fissure/4;
        //Debug.Log(fissure);

        if (fissure <=0)
        {
            fissureBar.gameObject.SetActive(false);
        }
        else
        {
            fissureBar.gameObject.SetActive(true);
        }

        if (fissure >= 4)
        {
            fissured = true;
            fissure = 0;
            stun.SetActive(true);
            StartCoroutine(fissureDuration());
        }

        if (radiationAmmount>1)
        {
            radiationAmmount = 1;
        }

        if (irradiated)
        {
            radiationAmmount = 1;
        }
        if (radiationAmmount>0)
        {
            radiationBar.gameObject.SetActive(true);
            Slider slider = radiationBar.GetComponent<Slider>();
            radiationBar.value = radiationAmmount+0.1f;
            radiationAmmount -= 0.5f * Time.deltaTime;
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
        if (PlayerDamage.dead)
        {
            return;
        }
        if (BoonSTaticInfo.globalCritChance >0)
        {
            critD += 1 + (BoonSTaticInfo.globalCritChance / 120);
            critC += BoonSTaticInfo.globalCritChance;
        }
        

        //Debug.Log(type);
        float distance = Vector3.Distance(PlayerMovement.playerPosition.position, gameObject.transform.position);

        float foBonus = 0;
        if (BoonSTaticInfo.faceOff && distance <= BoonSTaticInfo.faceOffRange)
        {
            foBonus = BoonSTaticInfo.faceOffBonus / 100;
            //Debug.Log("Face Off: " + foBonus);
        }

        float fiBonus = 0;
        if (BoonSTaticInfo.finishHim)
        {
            fiBonus = (1-(currentHealth / health));
            fiBonus *= (BoonSTaticInfo.finishHimBonus)/100;
            Debug.Log("Finish Him: " + fiBonus);
        }

        float purgeBonus = 0;
        if (BoonSTaticInfo.purge)
        {
            purgeBonus = 0.3f;
            //Debug.Log("Purge: "+purgeBonus);
        }

        float cb = 0;
        if (BoonSTaticInfo.cracked)
        {
            cb = ((1 - (PlayerDamage.currentHp / PlayerDamage.hp)) * BoonSTaticInfo.crackedBonus);
            //Debug.Log("Cracked: " + cb);
        }

        float monopolyBonus = 0;
        if (BoonSTaticInfo.monopoly)
        {
            monopolyBonus = (((BoonSTaticInfo.crystals) / 5) * 0.01f);
            //Debug.Log("monopoly: "+monopolyBonus);
        }

        damage *= 1+(cb+ purgeBonus + monopolyBonus + foBonus + fiBonus);
        //Debug.Log(1 + (cb + purgeBonus));

        float fb = 1;
        if (fated)
        {
            fb = BoonSTaticInfo.fatedBonus;
        }
        
        if (!dead)
        {
            if (BoonSTaticInfo.emotionalDamage)
            {
                bcd = BoonSTaticInfo.emotionalDamageBonus;
            }

            float critDmult = 1;
            if (BoonSTaticInfo.galvanized)
            {
                critDmult *= BoonSTaticInfo.galvanizedBonus;
            }

            float armorReduction = 0.7f;
            int finalDamage;
            GameObject dmgNumber = Instantiate(damageNumber, damageNumberSpawn.position, damageNumberSpawn.rotation);
            float rcchance = Random.Range(0, 100);
            //Debug.Log(critC + rustboost + BoonSTaticInfo.globalCritChance);
            if (rcchance < (critC + rustboost) && critC !=0)
            {
                

                if (source !=null && source.name == "Player" && BoonSTaticInfo.soldering)
                {
                    PlayerDamage.currentEnergy += BoonSTaticInfo.solderingBonus;
                }

                if (BoonSTaticInfo.tetanus)
                {
                    float rchance = Random.Range(1, 100);
                    if (rcchance < BoonSTaticInfo.tetanusChance)
                    {
                        critD *= 2;
                    }
                }

                damage *= ((critD*fb* critDmult + bcd));
                if (source != null)
                {
                    source.GetComponent<PlayerShoot>().onCrit();
                }
                dmgNumber.GetComponent<DamageNumber>().textDmg.outlineColor = new Color(1, 0, 0);
                fated = false;
            }
            
            //Debug.Log("crit chance: "+ (critC + rustboost));

            if (BoonSTaticInfo.massAccumulation)
            {
                damage *= ((BoonSTaticInfo.UPGRADES * BoonSTaticInfo.massAccumulationBonus) / 100) + 1;
                Debug.Log(((BoonSTaticInfo.UPGRADES * BoonSTaticInfo.massAccumulationBonus) / 100) + 1);
            }


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
                if (rcrystal < BoonSTaticInfo.crystallizeCrystalChance && type != "gem")
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

            

            finalDamage = (int)((damage * (1 + ((radiationAmmount + 0.02f) * BoonSTaticInfo.radiationWeakness) / 100)));

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

            if (BoonSTaticInfo.multiversalStrike && BoonSTaticInfo.mstrike)
            {
                StartCoroutine(MStrikeDuration());
                BoonSTaticInfo.mstrike = false;
                GameObject exo = Instantiate(shockwave, transform.position, transform.rotation);
                exo.GetComponent<Shockwave>().damage = BoonSTaticInfo.multiversalStrikeDamage;
                exo.GetComponent<Shockwave>().range = 300;
                exo.GetComponent<Shockwave>().mstrike = true;
                exo.GetComponent<Shockwave>().type = "null";
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


            switch (type)
            {
                case "normal":
                    hit1.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
                    hit2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
                    hit3.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
                    break;
                case "swarm":
                    hit1.GetComponent<SpriteRenderer>().color = new Color(0.7f, 1f, 0.2f);
                    hit2.GetComponent<SpriteRenderer>().color = new Color(0.7f, 1f, 0.2f);
                    hit3.GetComponent<SpriteRenderer>().color = new Color(0.7f, 1f, 0.2f);
                    break;
                case "haunted":
                    hit1.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.7f, 0.8f);
                    hit2.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.7f, 0.8f);
                    hit3.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.7f, 0.8f);
                    break;
                case "crystallize":
                    hit1.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.9f);
                    hit2.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.9f);
                    hit3.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.9f);
                    break;
                case "null":
                    hit1.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.0f, 0.4f);
                    hit2.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.0f, 0.4f);
                    hit3.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.0f, 0.4f);
                    break;
                case "starfall":
                    hit1.GetComponent<SpriteRenderer>().color = new Color(1f, 0.1f, 0.7f);
                    hit2.GetComponent<SpriteRenderer>().color = new Color(1f, 0.1f, 0.7f);
                    hit3.GetComponent<SpriteRenderer>().color = new Color(1f, 0.1f, 0.7f);
                    break;
                case "rust":
                    hit1.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.1f, 0.2f);
                    hit2.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.1f, 0.2f);
                    hit3.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.1f, 0.2f);
                    break;
                case "tectonic":
                    hit1.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.6f, 0.2f);
                    hit2.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.6f, 0.2f);
                    hit3.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.6f, 0.2f);
                    break;
                case "radiation":
                    hit1.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0.2f);
                    hit2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0.2f);
                    hit3.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0.2f);
                    break;
                case "volcanic":
                    hit1.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0.5f);
                    hit2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0.5f);
                    hit3.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0.5f);
                    break;
                case "star":
                    hit1.GetComponent<SpriteRenderer>().color = new Color(1f, 0.4f, 1f);
                    hit2.GetComponent<SpriteRenderer>().color = new Color(1f, 0.4f, 1f);
                    hit3.GetComponent<SpriteRenderer>().color = new Color(1f, 0.4f, 1f);
                    break;
                case "gem":
                    hit1.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.9f);
                    hit2.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.9f);
                    hit3.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.9f);
                    break;
            }

            int rhit = Random.Range(1, 3);
            switch (rhit)
            {
                case 1:
                    hit1.SetTrigger("hit");
                    break;
                case 2:
                    hit2.SetTrigger("hit");
                    break;
                case 3:
                    hit3.SetTrigger("hit");
                    break;

            }


            if (BoonSTaticInfo.monteCarlo && canmonte)
            {
                StartCoroutine(montec());
                float monte = Random.Range(1, 100);
                if (monte < BoonSTaticInfo.monteCarloChance)
                {
                    float carlo = Random.Range(1, 11);
                    {
                        switch (carlo)
                        {
                            case 1:
                                ApplyStatus("swarm", null);
                                break;
                            case 2:
                                ApplyStatus("haunted", null);
                                break;
                            case 3:
                                ApplyStatus("crystallize", null);
                                break;
                            case 4:
                                ApplyStatus("null", null);
                                break;
                            case 5:
                                ApplyStatus("starfall", null);
                                break;
                            case 6:
                                ApplyStatus("rust", null);
                                break;
                            case 7:
                                ApplyStatus("tectonic", null);
                                break;
                            case 8:
                                ApplyStatus("petrify", null);
                                break;
                            case 9:
                                ApplyStatus("volcanic", null);
                                break;
                            case 10:
                                ApplyStatus("petrify", null);
                                break;
                        }

                    }
                }
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
            if (rschance < BoonSTaticInfo.starfallChance && starfalled && type != "star")
            {
                if (BoonSTaticInfo.fated)
                {
                    fated = true;
                }


                if (BoonSTaticInfo.crater && rschance < 15)
                {
                    GameObject ball = Instantiate(shockwave, transform.position, transform.rotation);
                    ball.GetComponent<Shockwave>().damage = BoonSTaticInfo.starfallDamage;
                    ball.GetComponent<Shockwave>().range = BoonSTaticInfo.craterRange;
                    ball.GetComponent<Shockwave>().type = "star";
                    star.SetTrigger("star");
                }
                else
                {
                    TakeDamage(BoonSTaticInfo.starfallDamage, "star", BoonSTaticInfo.starfallCritChance, BoonSTaticInfo.makeAWishCD, null);
                    star.SetTrigger("star");
                }

                float rcrystal = Random.Range(0, 100);
                if (rcrystal < BoonSTaticInfo.luckyStarChance && BoonSTaticInfo.luckyStar)
                {
                    BoonSTaticInfo.crystals+=2;
                }

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
        if (radiationAmmount>0 && BoonSTaticInfo.radonBlood)
        {
            GameObject rb = Instantiate(radonBlood, transform.position, transform.rotation);
            rb.GetComponent<AOEDamageOverTime>().damage = BoonSTaticInfo.radonDamage;
            rb.GetComponent<AOEDamageOverTime>().range = BoonSTaticInfo.radonRange;
            rb.GetComponent<AOEDamageOverTime>().lifetime = BoonSTaticInfo.radonDuration;
            rb.GetComponent<AOEDamageOverTime>().attackSpeed = BoonSTaticInfo.radonAttackSpeed;

        }
        radiationRing.SetActive(false);
        BoonSTaticInfo.radiationCurrentCount--;
        irradiated = false;

        if (BoonSTaticInfo.exorcism && !dead)
        {
            GameObject exo = Instantiate(exorcism, transform.position, transform.rotation);
            exo.GetComponent<Shockwave>().damage = BoonSTaticInfo.exorcismBonus;
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
        BoonSTaticInfo.enemiesAlive.Remove(gameObject);
        //Debug.Log("Enemies alive:  " + BoonSTaticInfo.enemiesAlive.Count);
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
                    ball.GetComponent<NullBubble>().source = gameObject;
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
                    rust.SetActive(true);
                    rusted = true;
                    StartCoroutine(RustDuration());
                    if (source != null)
                    {
                        source.GetComponent<PlayerShoot>().onStatus(status);
                    }
                    EnemyShoot body = GetComponent<EnemyShoot>();
                    if (body != null)
                    { body.rusted = true; }
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
                    petrify.GetComponent<Animator>().SetTrigger("once");
                    StartCoroutine(PetrifiedDuration());
                }
                break;

        }

    }

    IEnumerator SwarmDuration()
    {
        GameObject swarms = Instantiate(swarm, transform.position, transform.rotation);
        swarms.GetComponent<Swarm>().source = gameObject;
        swarms.GetComponent<Swarm>().lifetime = BoonSTaticInfo.swarmDuration;
        yield return new WaitForSeconds(BoonSTaticInfo.swarmDuration);
       
        swarmed = false;
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
        fireindicator.SetActive(true);
        yield return new WaitForSeconds(2);
        fireindicator.SetActive(false);
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
        starfallindicator.SetActive(true);
        yield return new WaitForSeconds(BoonSTaticInfo.starfallDuration);
        starfallindicator.SetActive(false);
        starfalled = false;
    }

    public void NullPull(Vector3 direction)
    {
        Debug.Log("nulled");
        
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
        crystallizeindicator.SetActive(true);
        yield return new WaitForSeconds(BoonSTaticInfo.crystallizeDuration);
        crystallizeindicator.SetActive(false);
        crystallized = false;
    }

    IEnumerator RustDuration()
    {
        yield return new WaitForSeconds(BoonSTaticInfo.rustDuration);
        rusted = false;
        EnemyShoot body = GetComponent<EnemyShoot>();
        if (body !=null)
        { body.rusted = false; }
        rust.SetActive(false);
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
        hauntedicator.SetActive(true);
        yield return new WaitForSeconds(BoonSTaticInfo.hauntedDuration);
        hauntedicator.SetActive(false);
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

    IEnumerator MStrikeDuration()
    {
        yield return new WaitForSeconds(BoonSTaticInfo.multiversalStrikeCooldown);
        BoonSTaticInfo.mstrike = true;
    }

    IEnumerator fissureDuration()
    {
        yield return new WaitForSeconds(BoonSTaticInfo.multiversalStrikeCooldown);
        stun.SetActive(false);
        fissured = false;
    }

    IEnumerator montec()
    {
        canmonte = false;
        yield return new WaitForSeconds(1.5f);
        canmonte = true;
    }
}






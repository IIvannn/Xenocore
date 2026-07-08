
using BarthaSzabolcs.IsometricAiming;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerShoot : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint;
    public Transform aimer;
    public GameObject boomerang;
    public GameObject boomerangSpecial;
    public GameObject swarmSpell;
    public GameObject crystallizeSpell;
    public GameObject nullSpell;
    public GameObject starfallSpell;
    public GameObject radiationSpell;
    public GameObject tectonicSpellIndicator;
    public GameObject Holder;
    public GameObject crosshair;
    public GameObject Doordirection;

    float nextFireTime;
    [Header("Boomerang Attack")]
    public float firerate;
    public float boomerangSpeed;
    public float boomerangReturnSpeed;
    public float boomerangTimeBeforeReturn;
    public float boomerangDamage = 10;
    public float boomerangCritChance = 30f;
    public float boomerangCritDamage = 1.5f;
    public float energyPerHit;
    public static string attackType = "normal";

    [Header("Boomerang Special")]
    public float boomerangSpecialDamage = 5;
    public float boomerangSpecialDuration = 1;
    public float boomerangSpecialRotationSpeed = 3f;
    public float boomerangSpecialCooldown = 2f;
    public float boomerangSpecialCritChance = 10;
    public float boomerangSpecialCritDamage = 1.5f;
    public static string boomerangSpecialType = "normal";

    public bool canspecial = true;
    public bool specialing = false;

    private DialogueUI dialogueUI;

    [Header("Spell")]
    public static string spellType = "normal";
    public bool spelling = false;

    [Header("Other values")]
    public float ammo = 1;
    public float currentAmmo = 1;
    
    bool att = false;

    public AudioSource auso;
    public AudioClip attackSnd;
    public AudioClip specialSnd;
    public AudioClip spellSnd;


    public Image atta;
    public Image spec;
    public Image spell;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = ammo;
        dialogueUI = FindAnyObjectByType<DialogueUI>(FindObjectsInactive.Include);
    }

    // Update is called once per frame
    void Update()
    {
        switch (attackType)
        {
            case "normal":
                atta.color = new Color(1f, 1f, 1f);
                break;
            case "swarm":
                atta.color = new Color(0.7f, 1f, 0.2f);
                break;
            case "haunted":
                atta.color = new Color(0.2f, 0.8f, 0.7f);
                break;
            case "crystallize":
                atta.color = new Color(0.7f, 0.7f, 0.9f);
                break;
            case "null":
                atta.color = new Color(0.1f, 0.0f, 0.4f);
                break;
            case "starfall":
                atta.color = new Color(1f, 0.1f, 0.7f);
                break;
            case "rust":
                atta.color = new Color(0.6f, 0.1f, 0.2f);
                break;
            case "tectonic":
                atta.color = new Color(0.8f, 0.6f, 0.2f);
                break;
            case "radiation":
                atta.color = new Color(1f, 1f, 0.2f);
                break;
        }
        switch (boomerangSpecialType)
        {
            case "normal":
                spec.color = new Color(1f, 1f, 1f);
                break;
            case "swarm":
                spec.color = new Color(0.7f, 1f, 0.2f);
                break;
            case "haunted":
                spec.color = new Color(0.2f, 0.8f, 0.7f);
                break;
            case "crystallize":
                spec.color = new Color(0.7f, 0.7f, 0.9f);
                break;
            case "null":
                spec.color = new Color(0.1f, 0.0f, 0.4f);
                break;
            case "starfall":
                spec.color = new Color(1f, 0.1f, 0.7f);
                break;
            case "rust":
                spec.color = new Color(0.6f, 0.1f, 0.2f);
                break;
            case "tectonic":
                spec.color = new Color(0.8f, 0.6f, 0.2f);
                break;
            case "radiation":
                spec.color = new Color(1f, 1f, 0.2f);
                break;
        }
        switch (spellType)
        {
            case "normal":
                spell.color = new Color(1f, 1f, 1f);
                break;
            case "swarm":
                spell.color = new Color(0.7f, 1f, 0.2f);
                break;
            case "haunted":
                spell.color = new Color(0.2f, 0.8f, 0.7f);
                break;
            case "crystallize":
                spell.color = new Color(0.7f, 0.7f, 0.9f);
                break;
            case "null":
                spell.color = new Color(0.1f, 0.0f, 0.4f);
                break;
            case "starfall":
                spell.color = new Color(1f, 0.1f, 0.7f);
                break;
            case "rust":
                spell.color = new Color(0.6f, 0.1f, 0.2f);
                break;
            case "tectonic":
                spell.color = new Color(0.8f, 0.6f, 0.2f);
                break;
            case "radiation":
                spell.color = new Color(1f, 1f, 0.2f);
                break;
        }

        if (DoorScript.doorPos != null && BoonSTaticInfo.enemiesAlive.Count ==0)
        {
            Doordirection.transform.LookAt(DoorScript.doorPos);
        }
        
        crosshair.transform.position = IsometricAiming.mousePosition;

        if (dialogueUI != null && (dialogueUI.IsOpen || dialogueUI.OptionsActive))
            return;

        //Debug.Log(aimer.rotation.y);
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            FireNow();
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Special();
        }
        if (Keyboard.current.qKey.wasPressedThisFrame && !spelling)
        {
            //PlayerDamage body = gameObject.GetComponent<PlayerDamage>();
            if (PlayerDamage.currentEnergy >= BoonSTaticInfo.spellCost && spellType != "normal")
            {
                PlayerDamage.currentEnergy-= BoonSTaticInfo.spellCost;
                //Debug.Log("spell");

                //StartCoroutine()
                switch (spellType)
                {

                    case "swarm":
                        GameObject swspell = Instantiate(swarmSpell, IsometricAiming.mousePosition, transform.rotation);
                        break;
                    case "haunted":
                        StartCoroutine(HauntedSpell());
                        break;
                    case "crystallize":
                        StartCoroutine(CrystallizeSpell());
                        break;
                    case "null":
                        GameObject nuspell = Instantiate(nullSpell, firePoint.position, firePoint.rotation);
                        break;
                    case "starfall":
                        GameObject stspell = Instantiate(starfallSpell, firePoint.position, firePoint.rotation);
                        break;
                    case "rust":
                        StartCoroutine(RustSpell());
                        break;
                    case "tectonic":
                        StartCoroutine(TectonicSpell());
                        break;
                    case "radiation":
                        GameObject raspell = Instantiate(radiationSpell, firePoint.position, firePoint.rotation);
                        break;
                        
                }
                auso.PlayOneShot(spellSnd);

                if (BoonSTaticInfo.arcaneSwiftness)
                {
                    StartCoroutine(AS());

                }
            }
        }


        if (specialing)
        {
            Special();
        }

        if (Mouse.current.middleButton.wasPressedThisFrame)
        {
            if (!att)
            {
                att = true;
            }
            else
            {
                att = false;
            }
        }

        if (att)
        {
            FireNow();
        }
    }

    void Special()
    {
        if (canspecial && !spelling)
        {
            auso.PlayOneShot(specialSnd);
            PlayerMovement playerMovement = GetComponent<PlayerMovement>();
            playerMovement.animator.SetTrigger("special");

            
            
            canspecial = false;
            StartCoroutine(SpecialDuration());
            StartCoroutine(SpecialCooldown());
            GameObject ball = Instantiate(boomerangSpecial, transform.position, firePoint.rotation);
            ball.GetComponent<BoomerangSpecial>().source = gameObject;
            ball.GetComponent<BoomerangSpecial>().damage = boomerangSpecialDamage;
            ball.GetComponent<BoomerangSpecial>().rotationSpeed = boomerangSpecialRotationSpeed;
            ball.GetComponent<BoomerangSpecial>().duration = boomerangSpecialDuration;
            ball.GetComponent<BoomerangSpecial>().critChance = boomerangSpecialCritChance;
            ball.GetComponent<BoomerangSpecial>().critDamage = boomerangSpecialCritDamage;
            ball.GetComponent<BoomerangSpecial>().type = boomerangSpecialType;
            specialing = true;
        }
        
    }

    IEnumerator SpecialDuration()
    {
        yield return new WaitForSeconds(boomerangSpecialDuration);
        specialing = false;
    }
    IEnumerator SpecialCooldown()
    {
        yield return new WaitForSeconds(boomerangSpecialCooldown);
        canspecial = true;
    }

    void FireNow()
    {
        if (Time.time >= nextFireTime && currentAmmo >0 && !specialing && !spelling)
        {
            PlayerMovement playerMovement = GetComponent<PlayerMovement>();
            if (aimer.rotation.y >= 0.25f && aimer.rotation.y <= 0.75f)
            {
                if (playerMovement.right)
                {
                    playerMovement.animator.SetTrigger("RightAttack");
                }
                else
                {
                    playerMovement.animator.SetTrigger("LeftAttack");
                }
                //Debug.Log(aimer.localEulerAngles.y);
            }
            else if (aimer.rotation.y >= -0.25f && aimer.rotation.y <= 0.25f)
            {
                playerMovement.animator.SetTrigger("UpAttack");
                //Debug.Log(aimer.rotation.y);
            }
            else if (aimer.rotation.y <= -0.25f && aimer.rotation.y >= -0.75f)
            {
                if (playerMovement.right)
                {
                    playerMovement.animator.SetTrigger("LeftAttack");
                }
                else
                {
                    playerMovement.animator.SetTrigger("RightAttack");
                }
                //Debug.Log(aimer.rotation.y);
            }
            else if (aimer.rotation.y <= -0.75 || aimer.rotation.y >= 0.75)
            {
                playerMovement.animator.SetTrigger("DownAttack");
                //Debug.Log(aimer.rotation.y);
            }
            currentAmmo--;
            GameObject ball = Instantiate(boomerang, firePoint.position, firePoint.rotation);
            ball.GetComponent<boomerangScript>().source = gameObject;
            ball.GetComponent<boomerangScript>().moveSpeed = boomerangSpeed;
            ball.GetComponent<boomerangScript>().returnSpeed = boomerangReturnSpeed;
            ball.GetComponent<boomerangScript>().timeBeforeReturn = boomerangTimeBeforeReturn;
            ball.GetComponent<boomerangScript>().damage = boomerangDamage;
            ball.GetComponent<boomerangScript>().type = attackType;
            ball.GetComponent<boomerangScript>().critChance = boomerangCritChance;
            ball.GetComponent<boomerangScript>().critDamage = boomerangCritDamage;
            nextFireTime = Time.time + firerate;
            auso.PlayOneShot(attackSnd);

        }

    }


    
    public void onHit()
    {
        //Debug.Log("hit");
        //PlayerDamage body = gameObject.GetComponent<PlayerDamage>();
        PlayerDamage.currentEnergy += energyPerHit;
    }
    public void onCrit()
    {
        //Debug.Log("enemy hit");
    }
    public void onStatus(string type)
    {
        //Debug.Log("enemy hit");
    }


    IEnumerator AS()
    {
        float originalSpeed = boomerangSpeed;
        float attSpeed = firerate;

        firerate *= BoonSTaticInfo.arcaneSwiftnessBonus;
        boomerangSpeed *= BoonSTaticInfo.arcaneSwiftnessBonus;

        yield return new WaitForSeconds(BoonSTaticInfo.arcaneSwiftnessDuration);

        boomerangSpeed = originalSpeed;
        firerate = attSpeed;

    }


    IEnumerator Spell()
    {
        yield return new WaitForSeconds(1);
        
    }

    IEnumerator HauntedSpell()
    {
        PlayerMovement pm = GetComponent<PlayerMovement>();
        PlayerDamage pd = GetComponent<PlayerDamage>();
        float dc = pm.dashCooldwon;
        pm.dashCooldwon = 0.2f;
        pd.invincible = true;
        pm.hauntedSpell = true;
        yield return new WaitForSeconds(4);
        pm.hauntedSpell = false;
        pm.dashCooldwon = dc;
        pd.invincible = false;
    }

    IEnumerator CrystallizeSpell()
    {
        GameObject cspell = Instantiate(crystallizeSpell, transform.position, firePoint.rotation);
        BoonSTaticInfo.petrifyDuration *= 2;
        yield return new WaitForSeconds(5);
        BoonSTaticInfo.petrifyDuration /= 2;

    }

    IEnumerator RustSpell()
    {
        PlayerMovement pm = GetComponent<PlayerMovement>();
        pm.speed *= 1+(BoonSTaticInfo.rustSpellBonus/100);
        BoonSTaticInfo.globalCritChance += BoonSTaticInfo.rustSpellBonus;
        Debug.Log(pm.speed);

        yield return new WaitForSeconds(BoonSTaticInfo.rustSpellDuration);

        BoonSTaticInfo.globalCritChance -= BoonSTaticInfo.rustSpellBonus;
        pm.speed /= 1 + (BoonSTaticInfo.rustSpellBonus / 100);
        Debug.Log(pm.speed);
        
    }

    IEnumerator TectonicSpell()
    {
        tectonicSpellIndicator.SetActive(true);
        PlayerDamage pd = gameObject.GetComponent<PlayerDamage>();
        PlayerMovement pm = GetComponent<PlayerMovement>();
        Holder.GetComponent<Animator>().Play("jumpup");
        spelling = true;
        pd.invincible = true;
        yield return new WaitForSeconds(1.5f);
        spelling = false;
        Holder.GetComponent<Animator>().SetTrigger("jump");
        yield return new WaitForSeconds(0.3f);
        GameObject ball = Instantiate(pd.shockwave, firePoint.position, firePoint.rotation);
        ball.GetComponent<Shockwave>().range = 8;
        ball.GetComponent<Shockwave>().damage = 120;
        ball.GetComponent<Shockwave>().type = "tectonic";
        tectonicSpellIndicator.SetActive(false);
        yield return new WaitForSeconds(0.8f);
        pd.invincible = false;
    }

}

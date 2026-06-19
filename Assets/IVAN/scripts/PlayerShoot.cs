using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using BarthaSzabolcs.IsometricAiming;
public class PlayerShoot : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint;
    public Transform aimer;
    public GameObject boomerang;
    public GameObject boomerangSpecial;

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
    public string attackType = "normal";

    [Header("Boomerang Special")]
    public float boomerangSpecialDamage = 5;
    public float boomerangSpecialDuration = 1;
    public float boomerangSpecialRotationSpeed = 100f;
    public float boomerangSpecialCooldown = 1.7f;
    public float boomerangSpecialCritChance = 10;
    public float boomerangSpecialCritDamage = 1.5f;
    public string boomerangSpecialType = "normal";

    bool canspecial = true;
    bool specialing;
    


    [Header("Other values")]
    public float ammo = 1;
    public float currentAmmo = 1;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = ammo;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(aimer.rotation.y);
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            FireNow();
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Special();
        }

        if (specialing)
        {
            Special();
        }

    }

    void Special()
    {
        if (canspecial)
        {
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
        if (Time.time >= nextFireTime && currentAmmo >0 && !specialing)
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
        }

    }


    public void onHit()
    {
        PlayerDamage body = gameObject.GetComponent<PlayerDamage>();
        body.energy += energyPerHit;
    }
    public void onCrit()
    {
        //Debug.Log("enemy hit");
    }
    public void onStatus(string type)
    {
        //Debug.Log("enemy hit");
    }



}

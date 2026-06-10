using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class PlayerShoot : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint;
    public GameObject boomerang;

    float nextFireTime;
    [Header("Boomerang")]
    public float firerate;
    public float boomerangSpeed;
    public float boomerangReturnSpeed;
    public float boomerangTimeBeforeReturn;
    public float boomerangDamage = 10;
    public float boomerangCritChance = 30f;
    public float boomerangCritDamage = 1.5f;
    public float energyPerHit;


    [Header("Other values")]
    public float ammo = 1;
    public float currentAmmo = 1;
    public string attackType = "normal";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = ammo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            FireNow();
        }


    }

    void FireNow()
    {
        if (Time.time >= nextFireTime && currentAmmo >0)
        {
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

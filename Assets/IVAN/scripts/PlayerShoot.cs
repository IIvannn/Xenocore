using UnityEngine;
using UnityEngine.InputSystem;

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

            nextFireTime = Time.time + firerate;
        }

    }
}

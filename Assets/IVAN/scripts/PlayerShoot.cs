using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    float nextFireTime;
    public float firerate;
    public float boomerangSpeed;
    public float boomerangReturnSpeed;

    public float ammo = 1;
    public float currentAmmo = 1;

    public GameObject projectile;
    public Transform firePoint;


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
            GameObject ball = Instantiate(projectile, firePoint.position, firePoint.rotation);
            ball.GetComponent<boomerangScript>().source = gameObject;
            ball.GetComponent<boomerangScript>().moveSpeed = boomerangSpeed;
            ball.GetComponent<boomerangScript>().returnSpeed = boomerangReturnSpeed;

            nextFireTime = Time.time + firerate;
        }

    }
}

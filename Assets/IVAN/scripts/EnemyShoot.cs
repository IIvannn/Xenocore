using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject projectile;
    public Transform firePoint;
    public float projectileSpeed;
    public float damage;
    public float firerate;
    public float attackDistance;
    public float projectileLifetime;

    public bool homing = true;
    public float homingPrecision;
    float nextFireTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (!PlayerDamage.dead)
        {
            float distance = Vector3.Distance(PlayerMovement.playerPosition.position, gameObject.transform.position);
            if (distance <= attackDistance)
            {
                if (Time.time >= nextFireTime)
                {

                    GameObject ball = Instantiate(projectile, firePoint.position, firePoint.rotation);
                    ball.GetComponent<enemyProjectileScript>().source = gameObject;
                    ball.GetComponent<enemyProjectileScript>().moveSpeed = projectileSpeed;
                    ball.GetComponent<enemyProjectileScript>().damage = damage;
                    ball.GetComponent<enemyProjectileScript>().lifetime = projectileLifetime;
                    ball.GetComponent<enemyProjectileScript>().homing = homing;
                    ball.GetComponent<enemyProjectileScript>().homingPrecision = homingPrecision;

                    nextFireTime = Time.time + firerate;
                }
            }
        }
        
    }
}

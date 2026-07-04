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
    public GameObject hauntedProj;

    public bool rusted = false;
    public int rounds = 4;
    public int fired = 0;
    public bool firing;

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
        if (firing)
        {
            Shoot();
        }
        firePoint.transform.LookAt(PlayerMovement.playerPosition);
    }

    public void Fire()
    {
        if (fired <= rounds)
        {
            firing = true;
            fired++;
            
        }
        else
        {
            firing = false;
            fired = 0;
            //EnemyShootBrain esb = GetComponent<EnemyShootBrain>();
            
        }
    }


    public void Shoot()
    {
        
        EnemyDamage enemyDamage = GetComponent<EnemyDamage>();
        EnemyShootBrain esh = GetComponent<EnemyShootBrain>();
        EnemyBossBrain ebb = GetComponent<EnemyBossBrain>();

        if (!PlayerDamage.dead && !enemyDamage.petrified)
        {
            float distance = Vector3.Distance(PlayerMovement.playerPosition.position, gameObject.transform.position);
            if (Time.time >= nextFireTime)
            {
                if (esh != null)
                { esh.animator.SetTrigger("shoot"); }
                if (ebb !=null)
                {

                }
                Fire();
                if (BoonSTaticInfo.posession && enemyDamage.haunted)
                {
                    float rrchance = Random.Range(1, 100);
                    if (rrchance < BoonSTaticInfo.possessionChance)
                    {
                        GameObject hball = Instantiate(hauntedProj, firePoint.position, firePoint.rotation);
                        hball.GetComponent<DamageOnContact>().damage = damage;
                        nextFireTime = Time.time + firerate;

                    }
                }
                else
                {
                    EnemyDamage body = GetComponent<EnemyDamage>();


                    GameObject ball = Instantiate(projectile, new Vector3(firePoint.position.x - 0.3f, firePoint.position.y, firePoint.position.z), firePoint.rotation);
                    ball.GetComponent<enemyProjectileScript>().source = gameObject;
                    ball.GetComponent<enemyProjectileScript>().moveSpeed = projectileSpeed;
                    ball.GetComponent<enemyProjectileScript>().damage = damage;
                    ball.GetComponent<enemyProjectileScript>().lifetime = projectileLifetime;
                    ball.GetComponent<enemyProjectileScript>().homing = homing;
                    ball.GetComponent<enemyProjectileScript>().homingPrecision = homingPrecision;
                    EnemyShootBrain esb = GetComponent<EnemyShootBrain>();
                    if (esb !=null)
                    {
                        
                    }
                    GameObject ball2 = Instantiate(projectile, new Vector3(firePoint.position.x + 0.3f, firePoint.position.y, firePoint.position.z), firePoint.rotation);
                    ball2.GetComponent<enemyProjectileScript>().source = gameObject;
                    ball2.GetComponent<enemyProjectileScript>().moveSpeed = projectileSpeed;
                    ball2.GetComponent<enemyProjectileScript>().damage = damage;
                    ball2.GetComponent<enemyProjectileScript>().lifetime = projectileLifetime;
                    ball2.GetComponent<enemyProjectileScript>().homing = homing;
                    ball2.GetComponent<enemyProjectileScript>().homingPrecision = homingPrecision;

                    nextFireTime = Time.time + firerate;
                    float rchance = Random.Range(0, 100);
                    //Debug.Log("rust self damage chance = " + rchance);
                    if (rusted && BoonSTaticInfo.rustSelfDamageChance > rchance)
                    {


                        float embrittledBonus = 1;
                        if (BoonSTaticInfo.embrittled)
                        {
                            float embchance = Random.Range(1, 100);
                            if (embchance < BoonSTaticInfo.embrittledChance)
                            {
                                embrittledBonus = 3;
                            }
                        }

                        body.TakeDamage(BoonSTaticInfo.rustSelfDamage * embrittledBonus, "rust", 0, 0, null);
                        //Debug.Log(BoonSTaticInfo.rustSelfDamage * embrittledBonus);
                    }
                }

            }
        }
    }
}

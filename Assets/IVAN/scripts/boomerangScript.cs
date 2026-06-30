using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class boomerangScript : MonoBehaviour
{
    
    public float moveSpeed = 10f;
    public float returnSpeed = 1.0f;
    public float timeBeforeReturn = 3f;
    public float rotateSpeed = 10f;
    public float damage = 1f;
    public float critDamage;
    public float critChance;
    public string type;
    List <GameObject> enemieshitOnLaunch = new List<GameObject>();
    List<GameObject> enemieshitOnReturn = new List<GameObject>();

    bool returning = false;
    public GameObject source;
    public AudioSource shootsnd;
    public GameObject sprite;
    public LayerMask groundMask;
    public GameObject shockwave;
    float distance;
    float farb = 1;
    float bspeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        //Destroy(gameObject, 8);
        //shootsnd.Play();
        StartCoroutine(Return());
        if (BoonSTaticInfo.boomerangSpeed)
        {
            moveSpeed += BoonSTaticInfo.boomerangSpeedBonus;
            returnSpeed += BoonSTaticInfo.boomerangSpeedBonus;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerDamage.dead)
        {
            return;
        }

        if (BoonSTaticInfo.exponentiallity)
        {
            bspeed += BoonSTaticInfo.exponentiallityBonus * Time.deltaTime;
        }
        
        
        distance = Vector3.Distance(PlayerMovement.playerPosition.position, gameObject.transform.position);
        if (BoonSTaticInfo.farFar)
        {
            farb = ((distance * BoonSTaticInfo.farFarBonus) * 0.01f) + 1;
        }
        
        //Debug.Log(farb);

        sprite.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        if (Physics.CheckSphere(transform.position, 0.5f, groundMask))
        {
            ReturnTrigger();
        }


        if (Mouse.current.leftButton.wasPressedThisFrame && returning == false)
        {
            ReturnTrigger();
        }

        if (returning)
        {
            moveToPlayer();
        }
        else
        {

            transform.position += transform.forward * (moveSpeed+bspeed) * Time.deltaTime;
        }

        if (source == null)
        {
            destroyProj();
        }
    }

    void moveToPlayer()
    {
        Vector3 dir = (source.transform.position - transform.position).normalized;
        transform.position += dir * (returnSpeed+bspeed) * Time.deltaTime;
    }

    void RotateToPlayer()
    {
        Vector3 dir = source.transform.position - transform.position;
        dir.y = 0f;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
    }

    void destroyProj()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hit"+other.name);
        if (other.gameObject.CompareTag("Prism"))
        {
            
            other.GetComponent<Prism>().Hurt();
        }
    }

    private void OnTriggerStay(Collider other)
    {

        float bshen = 1;
        if (BoonSTaticInfo.shenanigans)
        {
            int shen = Random.Range(1, 4);
            
            //Debug.Log("shenanigans chance: " + shen);
            if (shen == 1)
            {
                bshen = 0.8f;
            }
            else if (shen == 2)
            {
                bshen = 1.05f;
            }
            else if (shen == 3)
            {
                bshen = 1.3f;
            }

        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerShoot>().currentAmmo = other.GetComponent<PlayerShoot>().ammo;
            //Debug.Log("Player has " + other.GetComponent<PlayerShoot>().currentAmmo + " ammo");
            destroyProj();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            
            if (!returning)
            {
                if (!enemieshitOnLaunch.Contains(other.gameObject))
                {
                    enemieshitOnLaunch.Add(other.gameObject);
                    //Debug.Log("EnemyDamage hit for:  " + damage);
                    

                    other.GetComponent<EnemyDamage>().TakeDamage(damage*farb* bshen, type, critChance, critDamage, source);
                    other.GetComponent<EnemyDamage>().ApplyStatus(type,source);

                    if (BoonSTaticInfo.doubleTrouble)
                    {
                        float dt = Random.Range(1, 100);
                        if (dt <= BoonSTaticInfo.doubleTroubleChance)
                        {
                            other.GetComponent<EnemyDamage>().TakeDamage(damage * farb * bshen, type, critChance, critDamage, source);
                        }

                    }


                    if (BoonSTaticInfo.boomerangGrow)
                    {
                        gameObject.transform.localScale += new Vector3(BoonSTaticInfo.boomerangGrowBonus, BoonSTaticInfo.boomerangGrowBonus, BoonSTaticInfo.boomerangGrowBonus);
                    }
                    source.GetComponent<PlayerShoot>().onHit();
                }
            }
            else
            {
                if (!enemieshitOnReturn.Contains(other.gameObject))
                {
                    enemieshitOnReturn.Add(other.gameObject);
                    //Debug.Log("EnemyDamage hit for:  " + damage);
                    other.GetComponent<EnemyDamage>().TakeDamage(damage * farb, type, critChance, critDamage, source);
                    other.GetComponent<EnemyDamage>().ApplyStatus(type,source);

                    if (BoonSTaticInfo.doubleTrouble)
                    {
                        float dt = Random.Range(1, 100);
                        if (dt <= BoonSTaticInfo.doubleTroubleChance)
                        {
                            other.GetComponent<EnemyDamage>().TakeDamage(damage * farb, type, critChance, critDamage, source);
                        }

                    }

                    if (BoonSTaticInfo.boomerangMark && enemieshitOnLaunch.Contains(other.gameObject))
                    {
                        Shockwave();
                    }
                    source.GetComponent<PlayerShoot>().onHit();
                } 
            }

            
        }
    }


    IEnumerator Return()
    {
        //Debug.Log("return");
        yield return new WaitForSeconds(timeBeforeReturn);
        returning = true;
    }

    void ReturnTrigger()
    {
        returning = true;
        if (BoonSTaticInfo.boomerangReturnShockwave)
        {
            Shockwave();
        }
    }

    void Shockwave()
    {
        Debug.Log("shockwave");
        GameObject ball = Instantiate(shockwave, transform.position, transform.rotation);
    }
}

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
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Destroy(gameObject, 8);
        //shootsnd.Play();
        StartCoroutine(Return());
    }

    // Update is called once per frame
    void Update()
    {
        sprite.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        if (Physics.CheckSphere(transform.position, 0.5f, groundMask))
        {
            returning = true;
        }


        if (Mouse.current.leftButton.wasPressedThisFrame && returning == false)
        {
            returning = true;
        }

        if (returning)
        {
            moveToPlayer();
        }
        else
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        if (source == null)
        {
            destroyProj();
        }
    }

    void moveToPlayer()
    {
        Vector3 dir = (source.transform.position - transform.position).normalized;
        transform.position += dir * returnSpeed * Time.deltaTime;
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
                    other.GetComponent<EnemyDamage>().TakeDamage(damage, type, critChance, critDamage, source);
                    other.GetComponent<EnemyDamage>().ApplyStatus(type,source);
                    
                }
            }
            else
            {
                if (!enemieshitOnReturn.Contains(other.gameObject))
                {
                    enemieshitOnReturn.Add(other.gameObject);
                    //Debug.Log("EnemyDamage hit for:  " + damage);
                    other.GetComponent<EnemyDamage>().TakeDamage(damage, type, critChance, critDamage, source);
                    other.GetComponent<EnemyDamage>().ApplyStatus(type,source);
                    
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
}

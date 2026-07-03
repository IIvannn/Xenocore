using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class EnemyDash : MonoBehaviour
{
    public float attackDistance = 7f;
    public float dashTime = 0.2f;
    public float dashSpeed = 30;
    public CharacterController controller;
    public bool dashing = false;
    public bool candash = true;
    public Transform firePoint;
    public LayerMask groundmask;
    bool hitwall = false;
    public float dashCooldown = 2;
    public GameObject chargeIndicator;
    public float chargeDuration;
    public GameObject dashhitbox;
    public float damage = 20;
    public float range = 2;
    public bool charging = false;
    public Vector3 move = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        firePoint.transform.LookAt(PlayerMovement.playerPosition);
        //Collider[] hitEnemies = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), 1.2f, groundmask);
        //foreach (Collider enemy in hitEnemies)
        //{
        //    Collider col = GetComponent<Collider>();
        //    dashhitbox.SetActive(false);
        //    dashhitbox.GetComponent<EnemyHitbox>().he.Clear();
        //    controller.center = Vector3.zero;
        //    hitwall = true;
        //    dashing = false;
        //    col.enabled = true;
        //    StartCoroutine(cooldown());
        //}
    }

    public void Dash()
    {
        StartCoroutine(charge());
    }

    IEnumerator EDash()
    {
        EnemeyDashBrain eda = GetComponent<EnemeyDashBrain>();
        EnemyDamage ed = GetComponent<EnemyDamage>();
        eda.animator.SetTrigger("dash");
        eda.animator.SetBool("dashing", true);
        dashhitbox.GetComponent<EnemyHitbox>().damage = damage;
        dashhitbox.GetComponent<EnemyHitbox>().range = range;
        dashhitbox.SetActive(true);
        EnemeyDashBrain edb = GetComponent<EnemeyDashBrain>();
        //controller.center = new Vector3(0f,5f,0f);
        Collider col = GetComponent<Collider>();
        col.enabled = false;
        dashing = true;
        
        float startTime = Time.time;
        Vector3 movev = move.normalized;

        

        while (Time.time < startTime + dashTime && !hitwall && !hitwall && !ed.petrified && !ed.fissured)
        {
            //Collider[] hitEnemies = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), 1.2f, groundmask);
            //foreach (Collider enemy in hitEnemies)
            //{
            //    hitwall = true;
            //}
            controller.Move(movev * dashSpeed * Time.deltaTime);
            yield return null;

        }
        eda.animator.SetBool("dashing", false);
        dashhitbox.SetActive(false);
        dashhitbox.GetComponent<EnemyHitbox>().he.Clear();
        //controller.center = Vector3.zero;
        hitwall = false;
        dashing = false;
        StartCoroutine(cooldown());
        col.enabled = true;
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        candash = true;
    }

    IEnumerator charge()
    {
        EnemeyDashBrain eda = GetComponent<EnemeyDashBrain>();
        eda.animator.SetTrigger("charge");
        charging = true;
        candash = false;
        //Debug.Log("charge");
        chargeIndicator.SetActive(true);
        yield return new WaitForSeconds(chargeDuration);
        charging = false;
        chargeIndicator.SetActive(false);
        StartCoroutine(EDash());
    }

}

using System;
using UnityEngine;
using System.Collections;

public class EnemyHeal : MonoBehaviour
{
    public float healDistance = 7f;
    public float dashTime = 0.1f;
    public float dashSpeed = 6;
    public CharacterController controller;
    public bool dashing = false;
    public bool candash = true;
    public LayerMask groundmask;
    bool hitwall = false;
    public float dashCooldown = 2;
    public float heal = 20;
    public float range = 15;
    public Vector3 move = Vector3.zero;
    public float attackDistance = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        Collider[] hitEnemies = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), 1.2f, groundmask);
        foreach (Collider enemy in hitEnemies)
        {
            hitwall = true;
            dashing = false;
        }
    }

    public void Dash()
    {
        StartCoroutine(EDash());
    }

    IEnumerator EDash()
    {
        
        
        EnemeyDashBrain edb = GetComponent<EnemeyDashBrain>();
        controller.center = new Vector3(0f, 5f, 0f);
        Collider col = GetComponent<Collider>();
        col.enabled = false;
        dashing = true;

        float startTime = Time.time;

        while (Time.time < startTime + dashTime && !hitwall)
        {
            controller.Move(move * dashSpeed * Time.deltaTime);
            yield return null;

        }

        
        controller.center = Vector3.zero;
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



}

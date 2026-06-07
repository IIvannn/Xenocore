using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public Slider healthbar;
    public Slider easeHealthbar;
    float lerpSpeed = 0.03f;
    public float hp = 100f;
    public float currentHp = 1f;
    public static bool dead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = (currentHp/hp);
        easeHealthbar.value = Mathf.Lerp(easeHealthbar.value, healthbar.value, lerpSpeed);

        if(currentHp > hp)
        {
            currentHp = hp;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        dead = true;
        Destroy(gameObject);
    }
}

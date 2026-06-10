using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    [Header("References")]
    public Slider healthbar;
    public Slider easeHealthbar;
    public Slider energybar;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI crystalText;
    float lerpSpeed = 0.03f;
    [Header("Health")]
    public float hp = 100f;
    public float currentHp = 1f;
    public static bool dead = false;
    [Header("Energy")]
    public float energy = 100;
    public float currentEnergy = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHp = hp;
        
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = (currentHp/hp);
        healthText.text = (currentHp + "/" + hp);
        easeHealthbar.value = Mathf.Lerp(easeHealthbar.value, healthbar.value, lerpSpeed);
        energybar.value = (currentEnergy / energy);
        energyText.text = (currentEnergy + "/" + energy);
        crystalText.text = (BoonSTaticInfo.crystals).ToString();
        if (currentHp > hp)
        {
            currentHp = hp;
        }
        if (currentEnergy > energy)
        {
            currentEnergy = energy;
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
